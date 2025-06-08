using UnityEngine;

namespace Assets._Project.Scripts.Gameplay.BallLogic
{
    public class FallingBallsDespawnTrigger : MonoBehaviour
    {
        [SerializeField] private BallFactory _ballFactory;
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Ball ball))
                _ballFactory.DespawnBall(ball);
        }
    }
}