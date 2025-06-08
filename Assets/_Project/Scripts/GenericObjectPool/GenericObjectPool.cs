using UnityEngine;
using UnityEngine.Pool;

namespace Assets._Project.Scripts.ObjectPoolSytem
{
    public abstract class GenericObjectPool<TObject> : MonoBehaviour where TObject : PoolObject
    {
        protected ObjectPool<TObject> _pool;

        protected abstract bool _collectionCheck { get; }
        protected abstract int _defaultCapacity { get; }

        public void CreatePool()
        {
            _pool = new ObjectPool<TObject>(
                createFunc: CratePoolObject,
                actionOnGet: OnGetObjectFromPool,
                actionOnRelease: OnReleaseObjectToPool,
                actionOnDestroy: Destroy,
                collectionCheck: _collectionCheck,
                defaultCapacity: _defaultCapacity
            );
        }

        protected abstract TObject CratePoolObject();
        protected virtual void OnGetObjectFromPool(TObject poolObject)
        {
            poolObject.OnGetFromPool();
        }
        protected virtual void OnReleaseObjectToPool(TObject poolObject)
        {
            poolObject.OnReleaseToPool();
        }

        public virtual TObject GetObject()
        {
            return _pool.Get();
        }

        public virtual void ReleaseObject(TObject poolObject)
        {
            _pool.Release(poolObject);
        }
    }
}