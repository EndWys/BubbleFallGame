using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Project.Scripts.Gameplay.Trajectory
{
    [RequireComponent(typeof(LineRenderer))]
    public class TrajectoryStripDrawer : MonoBehaviour
    {
        [SerializeField] private int _maxBounces = 5;
        [SerializeField] private float _maxDistance = 50f;
        [SerializeField] private LayerMask _reflectionMask;

        [SerializeField] private float _groundY = 0.01f;

        private LineRenderer _lineRenderer;

        private void Awake()
        {
            _lineRenderer = GetComponent<LineRenderer>();
            _lineRenderer.positionCount = 0;
            _lineRenderer.startWidth = 0.3f;
            _lineRenderer.endWidth = 0.3f;
            _lineRenderer.useWorldSpace = true;
        }

        public void DrawTrajectory(Vector3 startPosition, Vector3 direction)
        {
            List<Vector3> points = new() { new Vector3(startPosition.x, _groundY, startPosition.z) };

            Vector3 currentPosition = startPosition;
            Vector3 currentDirection = direction.normalized;

            for (int i = 0; i < _maxBounces; i++)
            {
                if (!Physics.Raycast(currentPosition, currentDirection, out RaycastHit hit, _maxDistance, _reflectionMask))
                {
                    // if no hit, go forward max distance
                    Vector3 end = currentPosition + currentDirection * _maxDistance;
                    points.Add(new Vector3(end.x, _groundY, end.z));
                    break;
                }

                Vector3 hitPoint = hit.point;
                hitPoint.y = _groundY; // flatten to ground
                points.Add(hitPoint);

                currentPosition = hit.point;
                currentDirection = Vector3.Reflect(currentDirection, hit.normal);
            }

            _lineRenderer.positionCount = points.Count;
            _lineRenderer.SetPositions(points.ToArray());
        }

        public void Clear()
        {
            _lineRenderer.positionCount = 0;
        }
    }
}