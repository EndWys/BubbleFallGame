using Assets._Project.Scripts.Gameplay.BallLogic;
using Assets._Project.Scripts.Gameplay.Trajectory;
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
        [SerializeField] private TrajectoryStripDrawer _trajectory;

        [SerializeField] private float _shootForce = 15f;
        [SerializeField] private float _shootMaxAngle = 50f;
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

        private void SpawnNewBall()
        {
            BallColor color = BallColorService.Instance.GetRandomColor();
            _currentBall = _ballFactory.SpawnBall(_shootPoint.position, color);
            _currentBall.AddComponent<PlayerBallCollisionWatcher>();

            _currentBall.gameObject.layer = LayerMask.NameToLayer(PLAYER_BALL_LAYERNAME);
        }

        public void DespawnCurrentPlayerBall()
        {
            StopAllCoroutines();

            if (_currentBall != null)
            {
                _ballFactory.DespawnBall(_currentBall);
                _currentBall = null;
            }
        }

        private void Update()
        {
            if (_currentBall == null) return;

            Vector3 direction = GetBallShootDirection();

            //TODO: Meka a separated class for input
            if (Input.GetMouseButton(0))
            {
                ShowTrajectory(direction);
            }

            if (Input.GetMouseButtonUp(0))
            {
                ShootBall(direction);
                HideTrajectory();
            }
        }

        private void ShowTrajectory(Vector3 direction)
        {
            _trajectory.DrawTrajectory(_shootPoint.position, direction);
        }

        private void HideTrajectory()
        {
            _trajectory.Clear();
        }

        private void ShootBall(Vector3 direction)
        {
            _currentBall.SetVelocity(direction * _shootForce);
            _currentBall = null;

            StartCoroutine(SpawnNewBallWithDelay());
        }

        private IEnumerator SpawnNewBallWithDelay()
        {
            yield return new WaitForSeconds(_newBallSpawnDelay);
            SpawnNewBall();
        }

        private Vector3 GetBallShootDirection()
        {
            Vector3 mouseWorld = GetMouseWorldPositionOnPlane();
            Vector3 direction = mouseWorld - _shootPoint.position;
            direction.y = 0f;

            float angle = Vector3.SignedAngle(Vector3.forward, direction, Vector3.up);
            angle = Mathf.Clamp(angle, -_shootMaxAngle, _shootMaxAngle);

            Vector3 limitedDirection = Quaternion.Euler(0f, angle, 0f) * Vector3.forward;

            return limitedDirection;
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