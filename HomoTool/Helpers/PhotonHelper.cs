using ExitGames.Client.Photon;

namespace HomoTool.Helpers
{
    public static class PhotonHelper
    {
        public static bool SendChat(string message)
        {
            RaiseEventOptions_Internal raiseEventOptions = new RaiseEventOptions_Internal 
            { 
                field_Public_EnumPublicSealedvaDoMeReAdReSlAd13SlUnique_0 = EnumPublicSealedvaDoMeReAdReSlAd13SlUnique.DoNotCache, 
                field_Public_EnumPublicSealedvaOtAlMa4vUnique_0 = EnumPublicSealedvaOtAlMa4vUnique.Others 
            };

            return RaiseEvent(43, message, raiseEventOptions, default);
        }

        public static bool RaiseEvent(byte eventCode, System.Object eventContent, RaiseEventOptions_Internal raiseEventOptions, SendOptions sendOptions)
        {
            Il2CppSystem.Object content = SerializationHelper.FromManagedToIL2CPP<Il2CppSystem.Object>(eventContent);
            return PhotonNetwork_Internal.Method_Public_Static_Boolean_Byte_Object_ObjectPublicObByObInByObObUnique_SendOptions_0(eventCode, content, raiseEventOptions, sendOptions);
        }

        public static bool RaiseEvent(byte eventCode, Il2CppSystem.Object eventContent, RaiseEventOptions_Internal raiseEventOptions, SendOptions sendOptions)
        {
            return PhotonNetwork_Internal.Method_Public_Static_Boolean_Byte_Object_ObjectPublicObByObInByObObUnique_SendOptions_0(eventCode, eventContent, raiseEventOptions, sendOptions);
        }
    }
}