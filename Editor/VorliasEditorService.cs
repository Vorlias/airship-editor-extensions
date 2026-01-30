#if AIRSHIP_EXT_VERSION_LOCAL || AIRSHIP_EXT_VERSION_COMPAT
using System.IO;
using System.Linq;
using Editor.TypescriptAst;
using UnityEditor;

namespace Editor
{
    public class VorliasEditorService : AssetPostprocessor
    {
        private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets,
            string[] movedFromAssetPaths)
        {
            if (importedAssets.Contains("Assets/GameConfig.asset")) GenerateEnums();
        }

        [MenuItem("Airship/Generate Config Enums...", priority = 500)]
        public static void GenerateEnums()
        {
            var gameConfig = GameConfig.Load();
            var sourceFile = new SourceFile();
            
            if (VorliasEditorProjectSettings.instance.generateSceneEnum)
            {
                var sceneEnum = new EnumDeclaration("SceneName")
                {
                    Declare = true,
                    Const = true,
                };
                
                foreach (var scene in gameConfig.gameScenes)
                {
                    sceneEnum.Members.Add(new EnumMember(new StringLiteral(scene.name), new StringLiteral(scene.name)));
                }
                
                if (gameConfig.startingScene != null)
                {
                    if (gameConfig.gameScenes.Contains(gameConfig.startingScene))
                    {
                        sceneEnum.Members.Add(new EnumMember(new StringLiteral("Default"), new Identifier(gameConfig.startingScene.name)));
                    }
                    else
                    {
                        sceneEnum.Members.Add(new EnumMember(new StringLiteral("Default"), new StringLiteral(gameConfig.startingScene.name)));
                    }
                }
                
                sourceFile.Statements.Add(sceneEnum);
            }

            if (VorliasEditorProjectSettings.instance.generateLayerEnum)
            {
                var layerIdEnum = new EnumDeclaration("GameLayerId")
                {
                    Declare = true,
                    Const = true,
                };

                var layers = GameConfig.Load().gameLayers;
                for (var i = 0; i < layers.Length; i++)
                {
                    var layerName = layers[i];
                    if (string.IsNullOrEmpty(layerName)) continue;
                    
                    layerIdEnum.Members.Add(new EnumMember(new StringLiteral(layerName), new NumberLiteral(i + 1)));
                }
                
                sourceFile.Statements.Add(layerIdEnum);
                
                var layerNameEnum = new EnumDeclaration("GameLayerName")
                {
                    Declare = true,
                    Const = true,
                };
                
                for (var i = 0; i < layers.Length; i++)
                {
                    var layerName = layers[i];
                    if (string.IsNullOrEmpty(layerName)) continue;
                    
                    layerNameEnum.Members.Add(new EnumMember(new StringLiteral(layerName), new StringLiteral(layerName)));
                }
                
                sourceFile.Statements.Add(layerNameEnum);
            }
            
            if (VorliasEditorProjectSettings.instance.generateTagEnum)
            {
                var tagEnum = new EnumDeclaration("GameTag")
                {
                    Declare = true,
                    Const = true,
                };

                foreach (var tag in gameConfig.gameTags)
                {
                    tagEnum.Members.Add(new EnumMember(new StringLiteral(tag), new StringLiteral(tag)));
                }
                
                sourceFile.Statements.Add(tagEnum);
            }

            const string outPath = "Assets/_generated.d.ts";
            if (sourceFile.Statements.Count > 0)
            {
                File.WriteAllText(outPath, sourceFile.ToString());
                AssetDatabase.ImportAsset(outPath);
            } else if (File.Exists(outPath))
            {
                File.Delete(outPath);
            }
        }
    }
}
#endif