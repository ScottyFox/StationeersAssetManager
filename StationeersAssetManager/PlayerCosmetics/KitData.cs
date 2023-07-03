using CharacterCustomisation;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace SFXArtAssetManager
{
    [Serializable]
    public class CharacterKitData
    {
        public string Name = string.Empty;
        public string ID = string.Empty;
        public Species Species = Species.None;
        public Gender Gender = Gender.Other;
        public CharacterKitType Type = CharacterKitType.None;
        public List<CharacterKitReference> References = new List<CharacterKitReference>();
        public List<KitItemData> KitItems = new List<KitItemData>();
    }
    [Serializable]
    public class CharacterKitReference
    {
        [XmlAttribute]
        public string Name = string.Empty;
        public List<string> KitItem = new List<string>();
    }

    [Serializable]
    public enum CharacterKitType : byte
    {
        [XmlEnum("None")]
        None,
        [XmlEnum("Create")]
        Create,
        [XmlEnum("CreateUsingExisting")]
        CreateUsingExisting,
        [XmlEnum("AddToExisting")]
        AddToExisting,
        [XmlEnum("ModifyExisting")]
        ModifyExisting
    }
    [Serializable]
    public class KitItemData
    {
        public string Name = string.Empty;
        public string ID = string.Empty;
        public KitItemSlot Slot = KitItemSlot.None;
        public string Asset = string.Empty;
    }
    [Serializable]
    public enum KitItemSlot : byte
    {
        [XmlEnum("None")]
        None,
        [XmlEnum("Body")]
        Body,
        [XmlEnum("Head")]
        Head,
        [XmlEnum("Eyes")]
        Eyes,
        [XmlEnum("EyeColour")]
        EyeColour,
        [XmlEnum("SkinColour")]
        SkinColour,
        [XmlEnum("Hair")]
        Hair,
        [XmlEnum("HairColour")]
        HairColour,
        [XmlEnum("FacialHair")]
        FacialHair
    }
}
