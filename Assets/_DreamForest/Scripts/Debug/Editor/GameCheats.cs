using System.Linq;
using _DreamForest.GameServices;
using _DreamForest.LevelObjects;
using Legacy;
using UnityEditor;
using UnityEngine;

namespace _DreamForest.Debug.EditorЫ
{
    public static class GameCheats
    {
        [MenuItem("🎮 Game/🐱‍💻 Cheats/Show all locations(without entrance)")]
        public static void ShowAllLocations() =>
            Object.FindObjectsOfType<Location>(true)
                .ToList()
                .ForEach(x => x.gameObject.SetActive(true));

        [MenuItem("🎮 Game/🐱‍💻 Cheats/Hide all locations except first")]
        public static void HideAllLocationsExceptFirst() =>
            Object.FindObjectsOfType<Location>(true)
                .Where(x => x.name != "Location_0")
                .ToList()
                .ForEach(x => x.gameObject.SetActive(false));

        [MenuItem("🎮 Game/🐱‍💻 Cheats/Increase player speed %#q")]
        public static void IncreasePlayerSpeed() => 
            Object.FindObjectOfType<PlayerController>()
                .ChangeSpeed(Resources.Load<ConfigsService>("Main configs").Speed * 3f);

        [MenuItem("🎮 Game/🐱‍💻 Cheats/Set normal player speed %#w")]
        public static void SetNormalPlayerSpeed() =>
            Object.FindObjectOfType<PlayerController>()
                .ChangeSpeed(Resources.Load<ConfigsService>("Main configs").Speed);

        [MenuItem("🎮 Game/🐱‍💻 Cheats/⚠ Tear down before build")]
        public static void TearDown()
        {
            SetNormalPlayerSpeed();
            HideAllLocationsExceptFirst();
        }
    }
}