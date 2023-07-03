using UnityEngine;

namespace SFXArtAssetManager
{
    static partial class CreateAsset
    {
        public static bool Mesh(AssetData assetData)
        {
            return TryCreatingAsset(Generate_Mesh, assetData);
        }
        //TODO
        private static Object Generate_Mesh(AssetData assetData)
        {
            return null;
        }
    }
}
