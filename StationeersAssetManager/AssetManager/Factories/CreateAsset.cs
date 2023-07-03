using UnityEngine;

namespace SFXArtAssetManager
{
    static partial class CreateAsset
    {
        //General Function to Handle Creating Assets Safely.
        private static bool TryCreatingAsset(System.Func<AssetData, Object> assetGenerator, AssetData assetData)
        {
            try
            {
                //Check if Asset is Implimented.
                if (!AssetTypeCompatibles.IsImplimented(assetData.Type))
                {
                    Debug.LogError("=!=" + assetData.Type.ToString() + " Is Not Implimented Yet=!=");
                    return false;
                }
                Object newGameObjectAsset = null;
                //Check if Asset wants AssetBundle
                if (string.IsNullOrEmpty(assetData.AssetBundle) || assetData.Type == AssetType.AssetBundle)
                {
                    //Check if Asset can generate itself.
                    if (!AssetTypeCompatibles.IsCompatible(assetData.Type))
                    {
                        Debug.LogError("=!=" + assetData.Type.ToString() + " are currently only supported from AssetBundles=!=");
                        return false;
                    }
                    else
                    {
                        newGameObjectAsset = assetGenerator(assetData);
                    }
                }
                else
                {
                    newGameObjectAsset = AssetManager.Load_Asset_From_AssetBundle(assetData);
                }
                //Check if Asset Successfully Loaded.
                if (newGameObjectAsset == null)
                {
                    Debug.LogError("=!=Could Not Load " + assetData.Type.ToString() + " \"" + assetData.Name + "\"=!=");
                    return false;
                }
                //Check if it's a assetbundle, and give it a prefix.
                if (assetData.Type == AssetType.AssetBundle)
                {
                    AssetManager.Add_AssetBundle(assetData.Name, newGameObjectAsset as AssetBundle);
                    assetData.Name = "ASSETBUNDLE~$~" + assetData;
                }
                assetData.Asset = newGameObjectAsset;
                AssetManager.Add_AssetData(assetData);
            }
            catch (System.Exception e)
            {
                Debug.LogError("=!=Error Loading AssetType." + assetData.Type.ToString() + " : \"" + assetData.Name + "\"=!=");
                Debug.LogError(e.Message);
                return false;
            }
            return true;
        }
    }
}
