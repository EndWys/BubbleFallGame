using Assets._Project.Scripts.Gameplay.GameManagment;
using UnityEngine;

namespace Assets._Project.Scripts.Gameplay.Wall
{
    public class WallMover : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed = 1f;
        [SerializeField] private float _stopZ = 0f;

        private void Update()
        {
            transform.position += Vector3.back * _moveSpeed * Time.deltaTime;

            if (transform.position.z <= _stopZ)
            {
                GameOverHandler.Instance.TriggerGameOver();
            }
        }
    }
}