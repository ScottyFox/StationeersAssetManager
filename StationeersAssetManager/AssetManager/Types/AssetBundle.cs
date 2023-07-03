using System.IO;
using UnityEngine;

namespace SFXArtAssetManager
{
    static partial class CreateAsset
    {
        public static bool AssetBundle(AssetData assetData)
        {
            return TryCreatingAsset(Generate_AssetBundle, assetData);
        }
        private static AssetBundle Generate_AssetBundle(AssetData assetData)
        {
            return UnityEngine.AssetBundle.LoadFromFile(Path.Combine(assetData.RelativePath, assetData.Path));
        }
    }
}
