using Assets._Project.Scripts.Gameplay.BallLogic;
using UnityEngine;

namespace Assets._Project.Scripts.Gameplay.Player
{
    public class PlayerBallCollisionWatcher : MonoBehaviour
    {
        private bool _isHandled = false;

        private void OnCollisionEnter(Collision collision)
        {
            if (_isHandled)
                return;

            if (collision.collider.TryGetComponent(out Ball wallBall))
            {
                Ball ball = GetComponent<Ball>();
                BallCollisionHandler.Instance.HandleBallCollision(ball);
                _isHandled = true;
            }
        }
    }
}