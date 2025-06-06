using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets._Project.Scripts.Gameplay.BallLogic
{
    public class BallColorService : MonoBehaviour
    {
        public static BallColorService Instance { get; private set; }

        [SerializeField] private BallColorMaterial[] _colorMaterials;

        private void Awake()
        {
            if (Instance != null) Destroy(gameObject);
            Instance = this;
        }

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