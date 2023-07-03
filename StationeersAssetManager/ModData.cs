using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace SFXArtAssetManager
{
    [Serializable]
    [XmlRoot]
    public class CustomAssetMod
    {
        [XmlIgnore]
        public string Path = string.Empty;
        [XmlIgnore]
        public string ParentModName = string.Empty;
        public int Priority = -1;
        public List<AssetData> CustomAssets = new List<AssetData>();
        public List<CharacterKitData> CustomCharacterKits = new List<CharacterKitData>();
        //TODO? CustomClothing
        //TODO? CustomDevices
        //TODO? CustomCartridges
        //TODO? CustomItems 
        //TODO? CustomMinables 
    }
}
