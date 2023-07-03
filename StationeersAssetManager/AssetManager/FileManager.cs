using Assets.Scripts.Serialization;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

namespace SFXArtAssetManager
{
    class FileManager
    {
        public const string CUSTOMASSETMOD_DIRECTORY = "CustomAssets\\";
        public static void CheckBepInExPluginsForCustomAssetMods()
        {
            var plugin_path = BepInEx.Paths.PluginPath;
            Debug.Log("Scanning BeInEx Plugins for Custom Assets...");
            foreach (string plugin in Directory.GetDirectories(plugin_path))
            {
                if (Directory.Exists(Path.Combine(plugin, CUSTOMASSETMOD_DIRECTORY)))
                {
                    LoadCustomAssetModsAtPath(plugin);
                }
            }
        }
        public static void CheckEnabledModsForCustomAssetMods()
        {
            WorkshopMenu.LoadModConfig();
            Debug.Log("Scanning Stationeer Mods for Custom Assets...");
            foreach (ModData modData in WorkshopMenu.ModsConfig.GetEnabledMods())
            {
                if (Directory.Exists(Path.Combine(modData.LocalPath, CUSTOMASSETMOD_DIRECTORY)))
                {
                    LoadCustomAssetModsAtPath(modData.LocalPath);
                }
            }
        }
        public static CustomAssetMod DeserializeXML_CustomAssetMod(string path)
        {
            var CustomModDataSerializer = new XmlSerializer(typeof(CustomAssetMod), XmlSaveLoad.ExtraTypes);
            return XmlSerialization.Deserialize(CustomModDataSerializer, path) as CustomAssetMod;
        }
        public static void LoadCustomAssetModsAtPath(string path)
        {
            if (!Directory.Exists(path))
                return;
            List<CustomAssetMod> customAssetMods = new List<CustomAssetMod>();
            foreach (string moddata_path in Directory.GetFiles(path, "*.xml", SearchOption.AllDirectories))
            {
                if (moddata_path.Contains(CUSTOMASSETMOD_DIRECTORY))
                {
                    var modData = DeserializeXML_CustomAssetMod(moddata_path);
                    if (modData != null)
                    {
                        modData.Path = moddata_path;
                        modData.ParentModName = Path.GetFileName(path);
                        customAssetMods.Add(modData);
                    }
                }
            }
            foreach (CustomAssetMod assetMod in customAssetMods)
            {
                AssetManager.Add_CustomAssetMod(assetMod);
            }
        }
        public static void InitializeCustomAssetMods()
        {
            AssetManager.Sort_CustomAssetMods();
            foreach (CustomAssetMod customAssetMod in AssetManager.Loaded_CustomAssetMods)
            {
                ParseCustomAssetMods(customAssetMod);
            }
        }
        public static void ParseCustomAssetMods(CustomAssetMod customAssetMod)
        {
            foreach (AssetData assetData in customAssetMod.CustomAssets)
            {
                assetData.RelativePath = Path.GetDirectoryName(customAssetMod.Path);
                AssetManager.Read_AssetData(assetData);
            }
            foreach (CharacterKitData characterKitData in customAssetMod.CustomCharacterKits)
            {
                KitManager.Read_CharacterKitData(characterKitData);
            }
        }
    }
}