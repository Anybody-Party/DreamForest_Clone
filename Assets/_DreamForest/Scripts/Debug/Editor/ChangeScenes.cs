using UnityEditor;
using UnityEditor.SceneManagement;

namespace _DreamForest.Debug.Editor
{
    public static class ChangeScenes
    {
        [MenuItem("🎮 Game/Scenes/🏁 To start")]
        public static void ToStartScene() => LoadScene("Assets/_DreamForest/Scenes/Bootstrapper.unity");

        [MenuItem("🎮 Game/Scenes/1️⃣ To level 0")]
        public static void ToLevel0() => LoadScene("Assets/_DreamForest/Scenes/Level0.unity");

        [MenuItem("🎮 Game/Scenes/2️⃣ To level 1")]
        public static void ToLevel1() => LoadScene("Assets/_DreamForest/Scenes/Level1.unity");

        private static void LoadScene(string name)
        {
            EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
            EditorSceneManager.OpenScene(name);
        }
    }
}