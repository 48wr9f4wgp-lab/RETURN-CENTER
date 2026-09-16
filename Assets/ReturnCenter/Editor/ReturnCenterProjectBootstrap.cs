#if UNITY_EDITOR
using System.Linq;
using ReturnCenter.Presentation;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ReturnCenter.Editor
{
    [InitializeOnLoad]
    public static class ReturnCenterProjectBootstrap
    {
        private const string ScenePath = "Assets/ReturnCenter/Scenes/VS_Warehouse.unity";

        static ReturnCenterProjectBootstrap()
        {
            EditorApplication.delayCall += EnsureBootstrapScene;
        }

        [MenuItem("RETURN CENTER/Bootstrap Vertical Slice Scene")]
        public static void EnsureBootstrapScene()
        {
            if (AssetDatabase.LoadAssetAtPath<SceneAsset>(ScenePath) != null)
            {
                EnsureBuildSettings();
                return;
            }

            var scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
            var bootstrap = new GameObject("RETURN_CENTER_BOOTSTRAP");
            bootstrap.AddComponent<ReturnCenterBootstrap>();
            EditorSceneManager.SaveScene(scene, ScenePath);
            EnsureBuildSettings();
            Debug.Log("RETURN CENTER: created VS_Warehouse bootstrap scene.");
        }

        [MenuItem("RETURN CENTER/Open Vertical Slice Scene")]
        public static void OpenScene()
        {
            EnsureBootstrapScene();
            EditorSceneManager.OpenScene(ScenePath, OpenSceneMode.Single);
        }

        private static void EnsureBuildSettings()
        {
            var scenes = EditorBuildSettings.scenes.ToList();
            if (scenes.Any(s => s.path == ScenePath)) return;
            scenes.Insert(0, new EditorBuildSettingsScene(ScenePath, true));
            EditorBuildSettings.scenes = scenes.ToArray();
        }
    }
}
#endif
