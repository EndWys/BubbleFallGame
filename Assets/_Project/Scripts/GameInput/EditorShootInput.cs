using UnityEngine;

namespace Assets._Project.Scripts.GameInput
{
    public class EditorShootInput : IShootInput
    {
        private readonly Camera _camera;
        private readonly Transform _shootPoint;
        private readonly float _maxAngle;

        public EditorShootInput(Transform shootPoint, float maxAngle)
        {
            _camera = Camera.main;
            _shootPoint = shootPoint;
            _maxAngle = maxAngle;
        }

        public bool IsAiming => Input.GetMouseButton(0);
        public bool IsShooting => Input.GetMouseButtonUp(0);

        public Vector3 GetShootDirection()
        {
            Vector3 mouseWorld = GetMouseWorldPositionOnPlane();
            Vector3 direction = mouseWorld - _shootPoint.position;
            direction.y = 0f;

            float angle = Vector3.SignedAngle(Vector3.forward, direction, Vector3.up);
            angle = Mathf.Clamp(angle, -_maxAngle, _maxAngle);

            return Quaternion.Euler(0f, angle, 0f) * Vector3.forward;
        }

        private Vector3 GetMouseWorldPositionOnPlane()
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            Plane plane = new Plane(Vector3.up, Vector3.zero);

            if (plane.Raycast(ray, out float enter))
            {
                return ray.GetPoint(enter);
            }

            return Vector3.zero;
        }
    }
}