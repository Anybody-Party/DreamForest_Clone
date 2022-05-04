using _DreamForest.GameServices;
using RH.Utilities.ServiceLocator;
using RH.Utilities.UI;

namespace _DreamForest.Debug
{
    public class GoToNextLevelCheatButton : BaseActionButton
    {
        protected override void PerformOnClick() => 
            Services
                .Single<GlobalEventsService>()
                .GoToNewLevelIntent?
                .Invoke();
    }
}