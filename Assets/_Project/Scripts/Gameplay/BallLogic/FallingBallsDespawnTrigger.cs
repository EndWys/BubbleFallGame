using Assets._Project.Scripts.ServiceLocatorSystem;
using UnityEngine;

namespace Assets._Project.Scripts.Gameplay.BallLogic
{
    public class FallingBallsDespawnTrigger : MonoBehaviour
    {
        private BallFactory _ballFactory;

        public void Init()
        {
            _ballFactory = ServiceLocator.Local.Get<BallFactory>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Ball ball))
                _ballFactory.DespawnBall(ball);
        }
    }
}