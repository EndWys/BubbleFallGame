using Assets._Project.Scripts.ServiceLocatorSystem;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets._Project.Scripts.Gameplay.BallLogic
{
    public class BallColorService : MonoBehaviour, IService
    {
        [SerializeField] private BallColorMaterial[] _colorMaterials;

        public BallColor GetRandomColor()
        {
            int length = Enum.GetValues(typeof(BallColor)).Length;
            return (BallColor)Random.Range(0, length);
        }

        public Material GetMaterialForColor(BallColor color)
        {
            foreach (var pair in _colorMaterials)
            {
                if (pair.Color == color)
                    return pair.Material;
            }

            Debug.LogWarning($"No material found for color: {color}");
            return null;
        }
    }
}