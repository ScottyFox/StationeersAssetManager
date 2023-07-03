using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace SFXArtAssetManager
{
    [Serializable]
    public class AssetData
    {
        public string Name = string.Empty;
        public AssetType Type = AssetType.None;
        public string AssetBundle = string.Empty;
        public string Path = string.Empty;
        [XmlIgnore]
        public string RelativePath = string.Empty;
        [XmlIgnore]
        public UnityEngine.Object Asset;
        [XmlElement(IsNullable = true)]
        public CustomMaterial Material;
    }
    [Serializable]
    public enum AssetType : byte
    {
        [XmlEnum("None")]
        None,
        [XmlEnum("AssetBundle")]
        AssetBundle,
        [XmlEnum("Clone")]
        Clone,
        [XmlEnum("Modify")]
        Modify,
        [XmlEnum("CloneBuiltIn")]
        CloneBuiltIn,
        [XmlEnum("ModifyBuiltIn")]
        ModifyBuiltIn,
        [XmlEnum("GameObject")]
        GameObject,
        [XmlEnum("Image")]
        Image,
        [XmlEnum("Audio")]
        Audio,
        [XmlEnum("Mesh")]
        Mesh,
        [XmlEnum("Material")]
        Material,
        [XmlEnum("Shader")]
        Shader
    }
    //Quick class to verify Assets are valid.
    public static class AssetTypeCompatibles
    {
        public static bool IsImplimented(AssetType assetType)
        {
            return !_NotImplimented.Contains(assetType);
        }
        public static bool IsCompatible(AssetType assetType)
        {
            return _Compatible.Contains(assetType);
        }
        //Once something is Implimented, remove from this list.
        private static List<AssetType> _NotImplimented = new List<AssetType>()
        {
            AssetType.Clone,
            AssetType.Modify,
            AssetType.CloneBuiltIn,
            AssetType.ModifyBuiltIn,
            AssetType.Audio
        };
        //If Generator for a specific asset exists, add to this list.
        private static List<AssetType> _Compatible = new List<AssetType>()
        {
            AssetType.AssetBundle,
            AssetType.Image,
            AssetType.Material
        };
    }

    [Serializable]
    public class CustomMaterial
    {
        public string Name = string.Empty;
        public string Shader = "Standard";
        public string MainTexture = string.Empty;
        public List<MaterialModifier> Modifier = new List<MaterialModifier>();
    }
    [Serializable]
    public class MaterialModifier
    {
        public string Name = string.Empty;
        public ShaderModifierType Type = ShaderModifierType.None;
        public string Value = string.Empty;
    }
    public enum ShaderModifierType : byte
    {
        [XmlEnum("None")]
        None,
        [XmlEnum("Texture")]
        Texture,
        [XmlEnum("TextureScale")]
        TextureScale,
        [XmlEnum("TextureOffset")]
        TextureOffset,
        [XmlEnum("Color")]
        Color,
        [XmlEnum("Integar")]
        Integar,
        [XmlEnum("Float")]
        Float
    }
}
