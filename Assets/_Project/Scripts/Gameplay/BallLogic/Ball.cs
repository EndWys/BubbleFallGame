using UnityEngine;

namespace Assets._Project.Scripts.Gameplay.BallLogic
{
    [RequireComponent(typeof(Rigidbody))]
    public class Ball : MonoBehaviour
    {
        [SerializeField] private MeshRenderer _meshRenderer;
        [SerializeField] private Rigidbody _rigidbody;

        private BallColor _color;

        public BallColor Color => _color;

        public void Initialize(BallColor color, bool usePhysics = false)
        {
            _color = color;
            _meshRenderer.material = BallColorService.Instance.GetMaterialForColor(color);

            _rigidbody.isKinematic = !usePhysics;
            _rigidbody.useGravity = usePhysics;
        }

        public void EnablePhysics()
        {
            _rigidbody.isKinematic = false;
            _rigidbody.useGravity = true;
        }

        public void SetVelocity(Vector3 velocity)
        {
            _rigidbody.velocity = velocity;
        }

        public void Stop()
        {
            _rigidbody.velocity = Vector3.zero;
        }
    }
}