using UnityEngine;

namespace SFXArtAssetManager
{
    static partial class CreateAsset
    {
        public static bool Shader(AssetData assetData)
        {
            return TryCreatingAsset(null, assetData);
        }
        //TODO
        private static Object Generate_Shader(AssetData assetData)
        {
            return null;
        }
    }
}
