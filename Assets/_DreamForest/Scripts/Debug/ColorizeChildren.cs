using System;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _DreamForest.Debug
{
    public class ColorizeChildren : MonoBehaviour
    {
        [SerializeField] private ColorGroup[] _colorGroups;
        [SerializeField] private EColor _targetColor;

        private void Awake()
        {
            if (!Application.isEditor)
                Destroy(this);
        }

        [ContextMenu(nameof(Colorize))]
        private void Colorize()
        {
            GetComponentsInChildren<MeshRenderer>()
                .ToList()
                .ForEach(x => x.material = _colorGroups.First(y => y.EColor == _targetColor).Material);
        }
        
        [ContextMenu(nameof(ColorizeRandom))]
        private void ColorizeRandom()
        {
            Material material = _colorGroups[Random.Range(0, _colorGroups.Length)].Material;

            GetComponentsInChildren<MeshRenderer>()
                .ToList()
                .ForEach(x => x.material = material);
        }

        private enum EColor
        {
            Blue,
            BrightBlue,
            BrightGreen,
            BrightPurple,
            Green,
            Purple,
            Red,
            Yellow,
        }

        [Serializable]
        private class ColorGroup
        {
            public EColor EColor;
            public Material Material;
        }
    }
}