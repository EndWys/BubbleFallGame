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
        private Vector3 _lastTouchWorldPosition;

        public MobileShootInput(Transform shootPoint, float maxAngle)
        {
            _camera = Camera.main;
            _shootPoint = shootPoint;
            _maxAngle = maxAngle;
        }

        public bool IsAiming => _isAiming;
        public bool IsShooting
        {
            get
            {
                bool result = _isShooting;
                _isShooting = false;
                return result;
            }
        }

        public Vector3 GetShootDirection()
        {
            UpdateTouch();

            Vector3 direction = _lastTouchWorldPosition - _shootPoint.position;
            direction.y = 0f;

            float angle = Vector3.SignedAngle(Vector3.forward, direction, Vector3.up);
            angle = Mathf.Clamp(angle, -_maxAngle, _maxAngle);

            return Quaternion.Euler(0f, angle, 0f) * Vector3.forward;
        }

        private void UpdateTouch()
        {
            _isAiming = false;

            if (Input.touchCount == 0)
                return;

            Touch touch = Input.GetTouch(0);
            Vector3 screenPosition = touch.position;

            Ray ray = _camera.ScreenPointToRay(screenPosition);
            Plane plane = new Plane(Vector3.up, Vector3.zero);

            if (plane.Raycast(ray, out float enter))
            {
                _lastTouchWorldPosition = ray.GetPoint(enter);

                if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
                {
                    _isAiming = true;
                }
                else if (touch.phase == TouchPhase.Ended)
                {
                    _isShooting = true;
                }
            }
        }
    }
}