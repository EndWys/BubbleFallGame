using UnityEngine;

namespace Assets._Project.Scripts.GameInput
{
    public interface IShootInput
    {
        bool IsAiming { get; }
        bool IsShooting { get; }

        Vector3 GetShootDirection();
    }
}