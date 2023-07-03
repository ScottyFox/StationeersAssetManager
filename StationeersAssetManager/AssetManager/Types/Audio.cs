using UnityEngine;

namespace SFXArtAssetManager
{
    static partial class CreateAsset
    {
        public static bool Audio(AssetData assetData)
        {
            return TryCreatingAsset(Generate_Audio, assetData);
        }

        //TODO
        private static Object Generate_Audio(AssetData assetData)
        {
            return null;
        }
    }
}
