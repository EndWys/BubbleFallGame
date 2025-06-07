using Assets._Project.Scripts.Gameplay.BallLogic;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets._Project.Scripts.Gameplay.Player
{
    public class PlayerShooter : MonoBehaviour
    {
        private const string PLAYER_BALL_LAYERNAME = "PlayerBall";

        [SerializeField] private Transform _shootPoint;
        [SerializeField] private BallFactory _ballFactory;
        [SerializeField] private LineRenderer _trajectoryLine;

        [SerializeField] private float _shootForce = 15f;
        [SerializeField] private float _newBallSpawnDelay = 1f;

        private Camera _mainCamera;
        private Ball _currentBall;

        public void Init()
        {
            _mainCamera = Camera.main;
        }

        public void RespawnPlayerBall()
        {
            SpawnNewBall();
        }

        public void DespawnCurrentPlayerBall()
        {
            StopAllCoroutines();

            if (_currentBall != null)
            {
                _ballFactory.DespawnBall(_currentBall);
            }
        }

        private void Update()
        {
            //TODO: Meka a separated class for input
            if (_currentBall == null) return;

            if (Input.GetMouseButton(0))
            {
                ShowTrajectory();
            }

            if (Input.GetMouseButtonUp(0))
            {
                ShootBall();
            }
        }

        private void SpawnNewBall()
        {
            BallColor color = BallColorService.Instance.GetRandomColor();
            _currentBall = _ballFactory.SpawnBall(_shootPoint.position, color);
            _currentBall.AddComponent<PlayerBallCollisionWatcher>();

            _currentBall.gameObject.layer = LayerMask.NameToLayer(PLAYER_BALL_LAYERNAME);
        }

        //TODO: Make a separated entetiy for trajectory drawing
        private void ShowTrajectory()
        {
            Vector3 mouseWorld = GetMouseWorldPositionOnPlane();
            Vector3 direction = (mouseWorld - _shootPoint.position).normalized;

            _trajectoryLine.positionCount = 2;
            _trajectoryLine.SetPosition(0, _shootPoint.position);
            _trajectoryLine.SetPosition(1, _shootPoint.position + direction * 10f);
        }

        private void ShootBall()
        {
            Vector3 mouseWorld = GetMouseWorldPositionOnPlane();
            Vector3 direction = (mouseWorld - _shootPoint.position).normalized;

            _currentBall.SetVelocity(direction * _shootForce);
            _currentBall = null;

            StartCoroutine(SpawnNewBallWithDelay());
        }

        private IEnumerator SpawnNewBallWithDelay()
        {
            yield return new WaitForSeconds(_newBallSpawnDelay);
            SpawnNewBall();
        }


        private Vector3 GetMouseWorldPositionOnPlane()
        {
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            Plane plane = new Plane(Vector3.up, Vector3.zero);
            if (plane.Raycast(ray, out float enter))
            {
                return ray.GetPoint(enter);
            }

            return Vector3.zero;
        }
    }
}