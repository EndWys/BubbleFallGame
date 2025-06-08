using Assets._Project.Scripts.Gameplay.GameManagment;
using UnityEngine;

namespace Assets._Project.Scripts.Gameplay.Wall
{
    public class WallMover : MonoBehaviour
    {
        [SerializeField] private WallGenerator _wallGenerator;
        [SerializeField] private float _moveSpeed = 1f;

        private Vector3 _starterPosition;

        private bool _canMoveWall = false;

        public void Init()
        {
            _starterPosition = transform.position;
        }

        public void ResetMover()
        {
            transform.position = _starterPosition;
            _canMoveWall = true;
        }

        public void Stop()
        {
            _canMoveWall = false;
        }

        private void Update()
        {
            if (!_canMoveWall)
                return;

            transform.position += Vector3.back * _moveSpeed * Time.deltaTime;

            _wallGenerator.GenerateRowsIfNeeded(transform.position.z);
        }
    }
}