using System;
using System.Linq;
using _DreamForest.Legacy;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace _DreamForest.Debug.Editor
{
    [CustomEditor(typeof(ResourceConsumer)), CanEditMultipleObjects]
    public class ResourceConsumerEditor : UnityEditor.Editor
    {
        private void OnEnable()
        {
            var resourceConsumer = (ResourceConsumer) target;

            if (IsPrefab(resourceConsumer))
                return;

            if (string.IsNullOrEmpty(resourceConsumer.Id))
                Generate(resourceConsumer);
            else
            {
                ResourceConsumer[] uniqueIds = FindObjectsOfType<ResourceConsumer>();

                if (uniqueIds.Any(other => other != resourceConsumer && other.Id == resourceConsumer.Id))
                    Generate(resourceConsumer);
            }
        }

        private bool IsPrefab(ResourceConsumer uniqueId) => 
            uniqueId.gameObject.scene.rootCount == 0;

        private void Generate(ResourceConsumer uniqueId)
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