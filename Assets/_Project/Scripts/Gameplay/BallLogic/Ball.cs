using Assets._Project.Scripts.Gameplay.Player;
using Assets._Project.Scripts.ObjectPoolSytem;
using Assets._Project.Scripts.ServiceLocatorSystem;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Assets._Project.Scripts.Gameplay.BallLogic
{
    [RequireComponent(typeof(Rigidbody))]
    public class Ball : PoolObject
    {
        private const string GRID_BALL_LAYERNAME = "GridBall";
        private const string FALLING_BALL_LAYERNAME = "FallingBall";
        private const string PLAYER_BALL_LAYERNAME = "PlayerBall";

        [SerializeField] private MeshRenderer _meshRenderer;
        [SerializeField] private Rigidbody _rigidbody;

        [SerializeField] private BallTweenAnimator _tweenAnimator;

        [SerializeField] private float _fallingJumpPower = 3f;

        public BallColor Color { get; private set; }

        public void Init(BallColor color, Material material)
        {
            Color = color;
            _meshRenderer.material = material;

            _tweenAnimator.ResetState();
        }

        public void Spawn()
        {
            gameObject.AddComponent<PlayerBallCollisionWatcher>();
            gameObject.layer = LayerMask.NameToLayer(PLAYER_BALL_LAYERNAME);

            _tweenAnimator.PlaySpawnAnimation();
        }

        public void SetVelocity(Vector3 velocity)
        {
            _rigidbody.isKinematic = false;
            _rigidbody.velocity = velocity;
        }

        public void StartFalling()
        {
            EnablePhysics();
            transform.SetParent(null);
            _rigidbody.AddForce(Vector3.up * _fallingJumpPower, ForceMode.Impulse);
            gameObject.layer = LayerMask.NameToLayer(FALLING_BALL_LAYERNAME);

            _tweenAnimator.PlayPreFallAnimation();
        }

        private void EnablePhysics()
        {
            _rigidbody.isKinematic = false;
            _rigidbody.useGravity = true;
        }

        public void Attach(Transform parent, Vector3 position)
        {
            DisablePhysics();
            transform.parent = parent;
            transform.position = position;
            gameObject.layer = LayerMask.NameToLayer(GRID_BALL_LAYERNAME);

            _tweenAnimator.PlayAttachAnimation();
        }

        private void DisablePhysics()
        {
            _rigidbody.isKinematic = true;
            _rigidbody.useGravity = false;
        }

        public async UniTask Pop()
        {
            await _tweenAnimator.PlayPopAnimation();
        }

        public override void OnGetFromPool()
        {
            gameObject.SetActive(true);
        }

        public override void OnReleaseToPool()
        {
            DisablePhysics();
            transform.SetParent(null);
            gameObject.SetActive(false);
        }
    }
}