using System;
using UnityEditor;
using UnityEngine;

#if AIRSHIP_EXT_VERSION_LOCAL || AIRSHIP_EXT_VERSION_COMPAT
namespace Editor
{
    public class VorliasEditorSettingsWindow : UnityEditor.EditorWindow {
        private static VorliasEditorSettingsWindow _instance;
        internal static VorliasEditorSettingsWindow Instance
        {
            get
            {
                if (!_instance)
                {
                    _instance = CreateWindow<VorliasEditorSettingsWindow>();
                }

                return _instance;
            }
        }
        
        [MenuItem("Airship/Extension Settings...", priority = 500)]
        public static void ShowWindow()
        {
            Instance.titleContent = new GUIContent("Editor Extension Settings");
            Instance.Show();
        }

        private void OnGUI()
        {
            AirshipEditorGUI.BeginGroup(new GUIContent("Generate Config Enums"));
            {
                EditorGUILayout.LabelField("Enable the generation of enums for game constants such as layers, scenes and tags.");
                
                VorliasEditorProjectSettings.instance.generateSceneEnum = EditorGUILayout.ToggleLeft("Generate Scene Enums", VorliasEditorProjectSettings.instance.generateSceneEnum);
                VorliasEditorProjectSettings.instance.generateLayerEnum = EditorGUILayout.ToggleLeft("Generate Layer Enums", VorliasEditorProjectSettings.instance.generateLayerEnum);
                VorliasEditorProjectSettings.instance.generateTagEnum = EditorGUILayout.ToggleLeft("Generate Tag Enums", VorliasEditorProjectSettings.instance.generateTagEnum);
            }
            AirshipEditorGUI.EndGroup();

            if (GUI.changed)
            {
                VorliasEditorProjectSettings.instance.Modify();
                VorliasEditorLocalSettings.instance.Modify();
                VorliasEditorService.GenerateEnums();
            }
        }
    }
}
#endif