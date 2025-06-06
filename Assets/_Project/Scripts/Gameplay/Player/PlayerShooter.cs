using Assets._Project.Scripts.Gameplay.BallLogic;
using System;
using UnityEngine;

namespace Assets._Project.Scripts.Gameplay.Player
{
    public class PlayerShooter : MonoBehaviour
    {
        [SerializeField] private Transform _shootPoint;
        [SerializeField] private BallFactory _ballFactory;
        [SerializeField] private float _shootForce = 15f;
        [SerializeField] private LineRenderer _trajectoryLine;

        private Camera _mainCamera;
        private Ball _currentBall;

        private void Start()
        {
            _mainCamera = Camera.main;
            SpawnNewBall();
        }

        private void Update()
        {
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
            _currentBall = _ballFactory.SpawnBall(_shootPoint.position, usePhysics: true);
        }

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

            Invoke(nameof(SpawnNewBall), 0.5f);
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