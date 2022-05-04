using _DreamForest.GameServices;
using Legacy;
using RH.Utilities.UI;
using UnityEngine;

namespace _DreamForest.Debug
{
    public class IncreaseSpeedCheatButton : BaseActionButton
    {
        protected override void PerformOnClick() => 
            Object.FindObjectOfType<PlayerController>()
                .ChangeSpeed(Resources.Load<ConfigsService>("Main configs").Speed * 3f);
    }
}