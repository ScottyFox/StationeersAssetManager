using UnityEngine;

namespace SFXArtAssetManager
{
    static partial class CreateAsset
    {
        public static bool Material(AssetData assetData)
        {
            return TryCreatingAsset(Generate_Material, assetData);
        }
        private static Material Generate_Material(AssetData assetData)
        {
            Material newMaterial = null;
            if (assetData.Material == null)
            {
                Debug.LogError("=!=Custom Material Data Does Not Exist In \"" + assetData.Name + "\"=!=");
            }
            else try
                {
                    newMaterial = new Material(UnityEngine.Shader.Find(assetData.Material.Shader));
                    newMaterial.name = assetData.Material.Name;
                    if (!string.IsNullOrEmpty(assetData.Material.MainTexture))
                    {
                        newMaterial.mainTexture = AssetManager.Get_Asset(assetData.Material.MainTexture) as Texture2D;
                    }
                    foreach (MaterialModifier materialModifier in assetData.Material.Modifier)
                    {
                        switch (materialModifier.Type)
                        {
                            case ShaderModifierType.Texture:
                                newMaterial.SetTexture(materialModifier.Name, AssetManager.Get_Asset(materialModifier.Value) as Texture2D);
                                break;
                            case ShaderModifierType.TextureScale:
                                Vector2 textureScale = Vector2.zero;
                                string[] texScale_splitInput = materialModifier.Value.Split(':');
                                if (texScale_splitInput.Length != 2 || !(float.TryParse(texScale_splitInput[0], out textureScale.x) && float.TryParse(texScale_splitInput[1], out textureScale.y)))
                                {
                                    Debug.LogError("=!=Material Modifier \"" + materialModifier.Name + "\" in \"" + assetData.Name + "\" has invalid Vector2 values, example 10:10 =!=");
                                    break;
                                }
                                newMaterial.SetTextureScale(materialModifier.Name, textureScale);
                                break;
                            case ShaderModifierType.TextureOffset:
                                Vector2 textureOffset = Vector2.zero;
                                string[] texOffset_splitInput = materialModifier.Value.Split(':');
                                if (texOffset_splitInput.Length != 2 || !(float.TryParse(texOffset_splitInput[0], out textureScale.x) && float.TryParse(texOffset_splitInput[1], out textureScale.y)))
                                {
                                    Debug.LogError("=!=Material Modifier \"" + materialModifier.Name + "\" in \"" + assetData.Name + "\" has invalid Vector2 values, example 10:10 =!=");
                                    break;
                                }
                                newMaterial.SetTextureOffset(materialModifier.Name, textureOffset);
                                break;
                            case ShaderModifierType.Color:
                                Color newcolor;
                                if (!ColorUtility.TryParseHtmlString(materialModifier.Value, out newcolor))
                                {
                                    Debug.LogError("=!=Material Modifier \"" + materialModifier.Name + "\" in \"" + assetData.Name + "\" has invalid Color value, example #FFFFFF =!=");
                                    break;
                                }
                                newMaterial.SetColor(materialModifier.Name, newcolor);
                                break;
                            case ShaderModifierType.Float:
                                float newfloat;
                                if (!float.TryParse(materialModifier.Value, out newfloat))
                                {
                                    Debug.LogError("=!=Material Modifier \"" + materialModifier.Name + "\" in \"" + assetData.Name + "\" has invalid Float value, example 123.123 =!=");
                                    break;
                                }
                                newMaterial.SetFloat(materialModifier.Name, newfloat);
                                break;
                            case ShaderModifierType.Integar:
                                int newint;
                                if (!int.TryParse(materialModifier.Value, out newint))
                                {
                                    Debug.LogError("=!=Material Modifier \"" + materialModifier.Name + "\" in \"" + assetData.Name + "\" has invalid Float value, example 123 =!=");
                                    break;
                                }
                                newMaterial.SetFloat(materialModifier.Name, newint);
                                break;
                            default:
                                Debug.LogError("=!=Material Modifier \"" + materialModifier.Name + "\" in \"" + assetData.Name + "\" is not valid=!=");
                                break;
                        }
                    }
                }
                catch (System.Exception e)
                {
                    Debug.LogError("=!=Custom Material Data in \"" + assetData.Name + "\" has caused a Error=!=");
                    Debug.LogError(e.Message);
                }
            return newMaterial;
        }
    }
}
