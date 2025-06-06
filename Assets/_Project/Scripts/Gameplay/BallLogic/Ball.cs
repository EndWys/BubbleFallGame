using UnityEngine;

namespace Assets._Project.Scripts.Gameplay.BallLogic
{
    [RequireComponent(typeof(Rigidbody))]
    public class Ball : MonoBehaviour
    {
        [SerializeField] private BallColor _ballColor;
        private Rigidbody _ballBody;

        public BallColor Color => _ballColor;

        public void Initialize(BallColor color, bool usePhysics = false)
        {
            _ballColor = color;

            if (TryGetComponent(out Rigidbody rb))
            {
                _ballBody = rb;
                if (!usePhysics)
                {
                    _ballBody.isKinematic = true;
                    _ballBody.useGravity = false;
                }
            }
        }

        public void EnablePhysics()
        {
            if (_ballBody == null) return;

            _ballBody.isKinematic = false;
            _ballBody.useGravity = true;
        }

        public void SetVelocity(Vector3 velocity)
        {
            if (_ballBody == null) return;

            _ballBody.velocity = velocity;
        }

        public void Stop()
        {
            if (_ballBody == null) return;

            _ballBody.velocity = Vector3.zero;
        }
    }
}