using Assets._Project.Scripts.GameInput;
using Assets._Project.Scripts.Gameplay.BallLogic;
using Assets._Project.Scripts.Gameplay.Trajectory;
using Assets._Project.Scripts.ServiceLocatorSystem;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets._Project.Scripts.Gameplay.Player
{
    public class PlayerShooter : MonoBehaviour
    {
        [SerializeField] private Transform _shootPoint;
        [SerializeField] private TrajectoryStripDrawer _trajectory;

        [SerializeField] private float _shootForce = 15f;
        [SerializeField] private float _shootMaxAngle = 50f;
        [SerializeField] private float _newBallSpawnDelay = 1f;

        private BallFactory _ballFactory;
        private Ball _currentBall;

        private IShootInput _input;

        public void Init()
        {
            _ballFactory = ServiceLocator.Local.Get<BallFactory>();

#if UNITY_ANDROID && !UNITY_EDITOR
            _input = new MobileShootInput(_shootPoint, _shootMaxAngle);
#else
            _input = new EditorShootInput(_shootPoint, _shootMaxAngle);
#endif
        }

        private void Update()
        {
            if (_currentBall == null) return;

            Vector3 direction = _input.GetShootDirection();

            if (_input.IsAiming)
            {
                ShowTrajectory(direction);
            }

            if (_input.IsShooting)
            {
                ShootBall(direction);
                HideTrajectory();
            }
        }

        private void ShowTrajectory(Vector3 direction)
        {
            _trajectory.DrawTrajectory(_shootPoint.position, direction);
        }

        private void ShootBall(Vector3 direction)
        {
            _currentBall.SetVelocity(direction * _shootForce);
            _currentBall = null;

            StartCoroutine(SpawnNewBallWithDelay());
        }

        private void HideTrajectory()
        {
            _trajectory.Clear();
        }

        public void RespawnPlayerBall()
        {
            SpawnNewBall();
        }

        private void SpawnNewBall()
        {
            _currentBall = _ballFactory.SpawnBall(_shootPoint.position);
            _currentBall.Spawn();
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

        private IEnumerator SpawnNewBallWithDelay()
        {
            yield return new WaitForSeconds(_newBallSpawnDelay);
            SpawnNewBall();
        }
    }
}