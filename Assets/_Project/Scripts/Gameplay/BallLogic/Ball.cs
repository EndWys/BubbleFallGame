using UnityEngine;

namespace Assets._Project.Scripts.Gameplay.BallLogic
{
    [RequireComponent(typeof(Rigidbody))]
    public class Ball : MonoBehaviour
    {
        [SerializeField] private MeshRenderer _meshRenderer;
        [SerializeField] private Rigidbody _rigidbody;

        public BallColor Color { get; private set; }

        public void Init(BallColor color)
        {
            Color = color;
            _meshRenderer.material = BallColorService.Instance.GetMaterialForColor(color);
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

        public void DisablePhysics()
        {
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.isKinematic = true;
            _rigidbody.useGravity = false;
        }
    }
}