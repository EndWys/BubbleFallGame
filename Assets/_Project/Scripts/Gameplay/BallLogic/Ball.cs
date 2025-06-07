using Assets._Project.Scripts.ObjectPoolSytem;
using UnityEngine;

namespace Assets._Project.Scripts.Gameplay.BallLogic
{
    [RequireComponent(typeof(Rigidbody))]
    public class Ball : PoolObject
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
            _rigidbody.isKinematic = false;
            _rigidbody.velocity = velocity;
        }

        public void DisablePhysics()
        {
            _rigidbody.isKinematic = true;
            _rigidbody.useGravity = false;
        }

        public override void OnGetFromPool()
        {
            gameObject.SetActive(true);
        }

        public override void OnReleaseToPool()
        {
            DisablePhysics();
            gameObject.SetActive(false);
        }
    }
}