using Assets._Project.Scripts.Gameplay.BallLogic;
using Assets._Project.Scripts.ServiceLocatorSystem;
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
                ServiceLocator.Local.Get<BallCollisionHandler>().HandleBallCollision(ball);
                _isHandled = true;
            }
        }
    }
}