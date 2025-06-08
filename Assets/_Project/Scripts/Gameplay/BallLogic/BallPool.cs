using Assets._Project.Scripts.ObjectPoolSytem;
using UnityEngine;

namespace Assets._Project.Scripts.Gameplay.BallLogic
{
    public class BallPool : GenericObjectPool<Ball>
    {
        [SerializeField] private Ball _ballPrefab;

        protected override bool _collectionCheck => false;

        protected override int _defaultCapacity => 100;

        protected override Ball CratePoolObject()
        {
            Ball ball = Instantiate(_ballPrefab);
            return ball;
        }
    }
}