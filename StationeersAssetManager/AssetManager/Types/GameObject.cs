using UnityEngine;

namespace SFXArtAssetManager
{
    static partial class CreateAsset
    {
        public static bool GameObject(AssetData assetData)
        {
            return TryCreatingAsset(Generate_GameObject, assetData);
        }

        //TODO
        private static Object Generate_GameObject(AssetData assetData)
        {
            return null;
        }
    }
}
