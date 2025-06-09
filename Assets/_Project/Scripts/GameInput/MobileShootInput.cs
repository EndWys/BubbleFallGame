using UnityEngine;

namespace Assets._Project.Scripts.GameInput
{
    public class MobileShootInput : IShootInput
    {
        private readonly Camera _camera;
        private readonly Transform _shootPoint;
        private readonly float _maxAngle;

        private bool _isAiming;
        private bool _isShooting;

        public MobileShootInput(Transform shootPoint, float maxAngle)
        {
            _camera = Camera.main;
            _shootPoint = shootPoint;
            _maxAngle = maxAngle;
        }

        public bool IsAiming => _isAiming;
        public bool IsShooting => _isShooting;

        public Vector3 GetShootDirection()
        {
            Vector3 touchWorld = GetTouchWorldPositionOnPlane();
            Vector3 direction = touchWorld - _shootPoint.position;
            direction.y = 0f;

            float angle = Vector3.SignedAngle(Vector3.forward, direction, Vector3.up);
            angle = Mathf.Clamp(angle, -_maxAngle, _maxAngle);

            return Quaternion.Euler(0f, angle, 0f) * Vector3.forward;
        }

        private Vector3 GetTouchWorldPositionOnPlane()
        {
            if (Input.touchCount == 0)
                return _shootPoint.position + Vector3.forward;

            Touch touch = Input.GetTouch(0);
            Vector3 screenPosition = touch.position;

            Ray ray = _camera.ScreenPointToRay(screenPosition);
            Plane plane = new Plane(Vector3.up, Vector3.zero);

            if (plane.Raycast(ray, out float enter))
            {
                _isAiming = touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary;
                _isShooting = touch.phase == TouchPhase.Ended;
                return ray.GetPoint(enter);
            }

            _isAiming = false;
            _isShooting = false;
            return _shootPoint.position + Vector3.forward;
        }
    }
}