using UnityEngine;

namespace SFXArtAssetManager
{
    static partial class CreateCosmetic
    {
        private static bool TryCreatingCosmetic(object original, System.Func<object, bool> validator, System.Func<object, Object> generator, System.Action<object> collector)
        {
            try
            {
                object thing = null;
                if (validator(original))
                {
                    thing = generator(original);
                }
                if (thing != null)
                {
                    collector(thing);
                }
            }
            catch (System.Exception e)
            {
                Debug.LogError("=!=Error Loading Cosmetic of type... \"" + typeof(object).Name + "\"=!=");
                Debug.LogError(e.Message);
                return false;
            }
            return true;
        }
    }
}
