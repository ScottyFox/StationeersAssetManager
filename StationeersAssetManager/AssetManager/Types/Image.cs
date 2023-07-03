using System.IO;
using UnityEngine;

namespace SFXArtAssetManager
{
    static partial class CreateAsset
    {
        public static bool Image(AssetData assetData)
        {
            return TryCreatingAsset(Generate_Image, assetData);
        }
        private static Object Generate_Image(AssetData assetData)
        {
            Texture2D texture2D = new Texture2D(1, 1);
            texture2D.LoadImage(File.ReadAllBytes(Path.Combine(assetData.RelativePath, assetData.Path)));
            return texture2D;
        }
    }
}
