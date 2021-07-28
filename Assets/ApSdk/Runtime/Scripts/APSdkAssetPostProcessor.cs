namespace APSdk
{

#if UNITY_EDITOR

    using UnityEngine;
    using UnityEditor;
    public class APSdkAssetPostProcessor : AssetPostprocessor
    {
        public static void LookForSDK() {

            APSdkConfiguretionInfo _apSDKConfiguretionInfo = Resources.Load<APSdkConfiguretionInfo>("APSdkConfiguretionInfo");
            SerializedObject _serializedSDKConfiguretionInfo = new SerializedObject(_apSDKConfiguretionInfo);

            foreach (APBaseClassForAnalyticsConfiguretion adConfig in _apSDKConfiguretionInfo.listOfAnalyticsConfiguretion)
                adConfig.SetNameAndIntegrationStatus();

            foreach (APBaseClassForAdConfiguretion adConfig in _apSDKConfiguretionInfo.listOfAdConfiguretion)
                adConfig.SetNameAndIntegrationStatus();

            _serializedSDKConfiguretionInfo.ApplyModifiedProperties();
        }

        static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
        {
            LookForSDK();
        }
    }
#endif


}
