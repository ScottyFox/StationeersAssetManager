using CharacterCustomisation;
using System.Collections.Generic;

namespace SFXArtAssetManager
{
    //KITSETS IS ALMOST ENTIRELY UTILITY, NO NEED TO UTILIZE THE HANDLERS UNLESS YOU PLAN TO IMPLIMENT NEW KITSETS...
    //Extension of KitManager, Primarily for general utility functions.
    static partial class KitManager
    {
        //Sets up the proper storage of kitsets.
        public static void Initialize_KitSets()
        {
            foreach (Species species in System.Enum.GetValues(typeof(Species)))
            {
                if (species == Species.None)
                    continue;
                var new_species = new List<List<CharacterKit>>();
                Current_KitSets.Add(species, new_species);
            }
        }
        //Read the ModData to properly distribute characterKits to the Kitsets.
        public static void Apply_CharacterKitData_To_KitSets()
        {
            foreach (CustomAssetMod customAssetMod in AssetManager.Loaded_CustomAssetMods)
            {
                foreach (CharacterKitData characterKitData in customAssetMod.CustomCharacterKits)
                {
                    if (characterKitData.Type == CharacterKitType.Create || characterKitData.Type == CharacterKitType.CreateUsingExisting)
                    {
                        var gender = Current_KitSets[characterKitData.Species][(int)characterKitData.Gender];
                        var characterKit = Get_CharacterKit(characterKitData.Name);
                        if (!gender.Contains(characterKit))
                            gender.Add(characterKit);
                    }
                }
            }
        }
        public static void Add_KitSet(KitSets kitSet)
        {
            int genderIndex = 0;
            foreach (KitGender kitGender in kitSet.Genders)
            {
                if (Current_KitSets[kitSet.species].Count < genderIndex + 1)
                    Current_KitSets[kitSet.species].Add(new List<CharacterKit>());
                foreach (CharacterKit characterKit in kitGender.kits)
                {
                    Current_KitSets[kitSet.species][genderIndex].Add(Get_CharacterKit(characterKit.name));
                }
                genderIndex++;
            }
        }
        public static void Add_KitSet_Array(KitSets[] kitSets, bool builtin = false)
        {
            if (builtin && Builtin_KitSets_Loaded)
                return;
            foreach (KitSets kitSet in kitSets)
            {
                Add_KitSet(kitSet);
            }
            if (builtin)
                Builtin_KitSets_Loaded = true;
        }
        //Create Game Ready KitSet.
        public static KitSets[] Generate_KitSet_Array()
        {
            KitSets[] newKitSets = new KitSets[Current_KitSets.Count];
            var i = 0;
            foreach (var species in Current_KitSets.Values)
            {
                newKitSets[i].species = (Species)System.Enum.Parse(typeof(Species), (i + 1).ToString());
                newKitSets[i].Genders = new KitGender[species.Count];
                var b = 0;
                foreach (var genders in species)
                {
                    newKitSets[i].Genders[b].kits = genders.ToArray();
                    b++;
                }
                i++;
            }
            return newKitSets;
        }
    }
}
