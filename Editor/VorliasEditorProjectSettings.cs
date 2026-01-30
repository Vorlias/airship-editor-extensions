#if AIRSHIP_EXT_VERSION_LOCAL || AIRSHIP_EXT_VERSION_COMPAT
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [FilePath("Assets/VorliasEditorExtensions", FilePathAttribute.Location.ProjectFolder)]
    public class VorliasEditorProjectSettings : ScriptableSingleton<VorliasEditorProjectSettings>
    {
        [SerializeField] internal bool generateSceneEnum;
        [SerializeField] internal bool generateLayerEnum;
        [SerializeField] internal bool generateTagEnum;
        
        internal void Modify()
        {
            Save(true);
        }
    }
}
#endif