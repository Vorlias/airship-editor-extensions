using System;
using UnityEditor;
using UnityEngine;

#if AIRSHIP_EXT_VERSION_LOCAL || AIRSHIP_EXT_VERSION_COMPAT
namespace Editor
{
    [FilePath("Library/VorliasEditorLocalSettings", FilePathAttribute.Location.ProjectFolder)]
    public class VorliasEditorLocalSettings : ScriptableSingleton<VorliasEditorLocalSettings>
    {
        public bool firstTimeSetup;
        internal void Modify()
        {
            Save(true);
        }
    }
    
    [FilePath("Assets/VorliasEditorExtensions", FilePathAttribute.Location.ProjectFolder)]
    public class VorliasEditorProjectSettings : ScriptableSingleton<VorliasEditorProjectSettings>
    {
        [SerializeField] internal bool generateSceneEnum;
        [SerializeField] internal bool generateLayerEnum;
        
        internal void Modify()
        {
            Save(true);
        }
    }
    
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

        private int _selectedTabIndex = 0;
        private readonly GUIContent[] _tabs = new[] {
            new GUIContent("Typescript"),
            new GUIContent("Hierarchy")
        };

        private void OnGUI()
        {
            _selectedTabIndex = AirshipEditorGUI.BeginTabs(_selectedTabIndex, _tabs);
            {
                if (_selectedTabIndex == 0)
                {
                    AirshipEditorGUI.BeginGroup(new GUIContent("Typescript File Generation"));
                    {
                        EditorGUILayout.LabelField("Enable the generation of enums for game constants such as layers, scenes and tags.");
                        
                        VorliasEditorProjectSettings.instance.generateSceneEnum = EditorGUILayout.ToggleLeft("Auto-generate Scene Enums", VorliasEditorProjectSettings.instance.generateSceneEnum);
                        VorliasEditorProjectSettings.instance.generateLayerEnum = EditorGUILayout.ToggleLeft("Auto-generate Layer Enums", VorliasEditorProjectSettings.instance.generateLayerEnum);
                    }
                    AirshipEditorGUI.EndGroup();
                    
                    
                }
            }
            AirshipEditorGUI.EndTabs();
            


            if (GUI.changed)
            {
                VorliasEditorProjectSettings.instance.Modify();
                VorliasEditorLocalSettings.instance.Modify();
            }
        }
    }
}
#endif