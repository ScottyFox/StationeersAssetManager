using System.Collections.Generic;
using UnityEngine;

namespace SFXArtAssetManager
{
    //I've Considered Splitting off into Having the CustomAssetMods contain their own data indivually
    //However, Throwing them all into a sort of melting pot and publically available allows for modification
    //by other mods... So this is fine... for now.
    partial class AssetManager
    {
        public static List<CustomAssetMod> Loaded_CustomAssetMods = new List<CustomAssetMod>();
        public static Dictionary<string, AssetBundle> Loaded_AssetBundles = new Dictionary<string, AssetBundle>();
        public static Dictionary<string, AssetData> Loaded_AssetData = new Dictionary<string, AssetData>();
        public static void Add_CustomAssetMod(CustomAssetMod assetMod)
        {
            Loaded_CustomAssetMods.Add(assetMod);
        }
        public static void Sort_CustomAssetMods()
        {
            Loaded_CustomAssetMods.Sort((x, y) => y.Priority.CompareTo(x.Priority));
        }
        public static void Add_AssetBundle(string path, AssetBundle assetBundle)
        {
            Loaded_AssetBundles.Add(path, assetBundle);
        }
        public static void Add_AssetData(AssetData assetData)
        {
            Loaded_AssetData.Add(assetData.Name, assetData);
        }
        public static AssetData Get_AssetData(string name)
        {
            AssetData asset = null;
            if (Has_AssetData(name))
                asset = Loaded_AssetData[name];
            else
                Debug.LogError("=!=Asset \"" + name + "\" Doesn't Exist=!=");
            return asset;
        }
        public static Object Get_Asset(string name)
        {
            AssetData assetData = Get_AssetData(name);
            Object asset = null;
            if (assetData != null)
                asset = assetData.Asset;
            else
                Debug.LogError("=!=AssetData \"" + name + "\" Doesn't Contain A Asset=!=");
            return asset;
        }
        public static bool Has_AssetData(string name)
        {
            return Loaded_AssetData.ContainsKey(name);
        }
        public static Object Load_Asset_From_AssetBundle(AssetData assetData)
        {
            Object found_object = null;
            if (Loaded_AssetBundles.ContainsKey(assetData.AssetBundle) && Loaded_AssetBundles[assetData.AssetBundle].Contains(assetData.Path))
                found_object = Loaded_AssetBundles[assetData.AssetBundle].LoadAsset(assetData.Path);
            return found_object;
        }
        private static bool Validate_AssetData(AssetData assetData)
        {
            if (!string.IsNullOrEmpty(assetData.Name) && Has_AssetData(assetData.Name))
            {
                Debug.LogError("=!=Asset with the name \"" + assetData.Name + "\" already exists=!=");
                return false;
            }
            return true;
        }
        public static void Read_AssetData(AssetData assetData)
        {
            //Modifiers
            switch (assetData.Type)
            {
                case AssetType.Clone:
                    Debug.LogError("=!=Clone Is Not Implimented Yet=!=");
                    return;
                case AssetType.CloneBuiltIn:
                    Debug.LogError("=!=CloneBuiltIn Is Not Implimented Yet=!=");
                    return;
                case AssetType.Modify:
                    Debug.LogError("=!=Modify Is Not Implimented Yet=!=");
                    return;
                case AssetType.ModifyBuiltIn:
                    Debug.LogError("=!=ModifyBuiltIn Is Not Implimented Yet=!=");
                    return;
                default:
                    //...Not A Modifier...//
                    break;
            }
            //Validate Data.
            if (!Validate_AssetData(assetData))
                return;

            switch (assetData.Type)
            {
                case AssetType.AssetBundle:
                    CreateAsset.AssetBundle(assetData);
                    break;
                case AssetType.GameObject:
                    CreateAsset.GameObject(assetData);
                    break;
                case AssetType.Image:
                    CreateAsset.Image(assetData);
                    break;
                case AssetType.Audio:
                    CreateAsset.Audio(assetData);
                    break;
                case AssetType.Mesh:
                    CreateAsset.Mesh(assetData);
                    break;
                case AssetType.Material:
                    CreateAsset.Material(assetData);
                    break;
                case AssetType.Shader:
                    CreateAsset.Shader(assetData);
                    break;
                default:
                    Debug.LogError("=!=Invalid Asset Type=!=");
                    return;
            }
        }
    }
}