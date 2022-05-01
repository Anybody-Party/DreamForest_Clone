using System;
using System.Linq;
using _DreamForest.Legacy;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace _DreamForest.Debug.Editor
{
    [CustomEditor(typeof(PermanentResourceDestroyer))]
    public class PermanentResourceDestroyerEditor  : UnityEditor.Editor
    {
        private void OnEnable()
        {
            var permanentResourceDestroyer = (PermanentResourceDestroyer) target;

            if (IsPrefab(permanentResourceDestroyer))
                return;

            if (string.IsNullOrEmpty(permanentResourceDestroyer.Id))
                Generate(permanentResourceDestroyer);
            else
            {
                PermanentResourceDestroyer[] uniqueIds = FindObjectsOfType<PermanentResourceDestroyer>();

                if (uniqueIds.Any(other => other != permanentResourceDestroyer && other.Id == permanentResourceDestroyer.Id))
                    Generate(permanentResourceDestroyer);
            }
        }

        private bool IsPrefab(PermanentResourceDestroyer uniqueId) => 
            uniqueId.gameObject.scene.rootCount == 0;

        private void Generate(PermanentResourceDestroyer uniqueId)
        {
            uniqueId.Id = $"{uniqueId.gameObject.scene.name}_{Guid.NewGuid().ToString()}";

            if (!Application.isPlaying)
            {
                EditorUtility.SetDirty(uniqueId);
                EditorSceneManager.MarkSceneDirty(uniqueId.gameObject.scene);
            }
        }
    }
}