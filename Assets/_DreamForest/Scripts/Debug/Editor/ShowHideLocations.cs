using System.Linq;
using _DreamForest.LevelObjects;
using UnityEditor;
using UnityEngine;

namespace _DreamForest.Scripts.Editor
{
    public static class ShowHideLocations
    {
        [MenuItem("🎮 Game/Show all locations")]
        public static void ShowAll() =>
            Object.FindObjectsOfType<Location>(true)
                .ToList()
                .ForEach(x => x.gameObject.SetActive(true));

        [MenuItem("🎮 Game/Hide all locations except first")]
        public static void HideAllExceptFirst() =>
            Object.FindObjectsOfType<Location>(true)
                .Where(x => x.name != "Location_0")
                .ToList()
                .ForEach(x => x.gameObject.SetActive(false));
    }
}