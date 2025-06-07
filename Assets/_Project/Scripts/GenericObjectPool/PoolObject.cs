using UnityEngine;

namespace Assets._Project.Scripts.ObjectPoolSytem
{
    public abstract class PoolObject : MonoBehaviour, IPoolObject
    {
        public abstract void OnGetFromPool();

        public abstract void OnReleaseToPool();
    }
}