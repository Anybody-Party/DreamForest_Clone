using System;
using System.Linq;
using _DreamForest.LevelObjects;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace _DreamForest.Debug.Editor
{
    [CustomEditor(typeof(WoodResource))]
    public class WoodResourceEditor  : UnityEditor.Editor
    {
        private void OnEnable()
        {
            var uniqueId = (WoodResource) target;

            if (IsPrefab(uniqueId))
                return;
    
            if (string.IsNullOrEmpty(uniqueId.Id))
                Generate(uniqueId);
            else
            {
                WoodResource[] uniqueIds = FindObjectsOfType<WoodResource>();
    
                if (uniqueIds.Any(other => other != uniqueId && other.Id == uniqueId.Id))
                    Generate(uniqueId);
            }
        }
    
        private bool IsPrefab(WoodResource uniqueId) => 
            uniqueId.gameObject.scene.rootCount == 0;
    
        private void Generate(WoodResource uniqueId)
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