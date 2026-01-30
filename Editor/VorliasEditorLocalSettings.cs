#if AIRSHIP_EXT_VERSION_LOCAL || AIRSHIP_EXT_VERSION_COMPAT
using UnityEditor;

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
}
#endif