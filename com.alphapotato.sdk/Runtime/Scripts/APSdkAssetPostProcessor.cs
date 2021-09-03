namespace com.alphapotato.sdk
{

#if UNITY_EDITOR

    using UnityEngine;
    using UnityEditor;
    public class APSdkAssetPostProcessor : AssetPostprocessor
    {
        public static void LookForSDK() {

            APSdkConfiguretionInfo _apSDKConfiguretionInfo = Resources.Load<APSdkConfiguretionInfo>("APSdkConfiguretionInfo");
            SerializedObject _serializedSDKConfiguretionInfo = new SerializedObject(_apSDKConfiguretionInfo);

            SerializedProperty _isLionKitIntegrated = _serializedSDKConfiguretionInfo.FindProperty("_isLionKitIntegrated");

            _isLionKitIntegrated.boolValue = APSdkScriptDefiniedSymbol.CheckLionKitIntegration(APSdkConstant.APSdk_LionKit);
            _isLionKitIntegrated.serializedObject.ApplyModifiedProperties();

            Object[] analyticsConfiguretionObjects = Resources.LoadAll("", typeof(APBaseClassForAnalyticsConfiguretion));
            foreach (Object analyticsConfiguretionObject in analyticsConfiguretionObjects) {

                APBaseClassForAnalyticsConfiguretion analyticsConfiguretion = (APBaseClassForAnalyticsConfiguretion)analyticsConfiguretionObject;
                if (analyticsConfiguretion != null)
                    analyticsConfiguretion.SetNameAndIntegrationStatus();
            }

            Object[] adNetworkConfiguretionObjects = Resources.LoadAll("", typeof(APBaseClassForAdConfiguretion));
            foreach (Object adNetoworkConfiguretionObject in adNetworkConfiguretionObjects)
            {

                APBaseClassForAdConfiguretion adNetworkConfiguretion = (APBaseClassForAdConfiguretion)adNetoworkConfiguretionObject;
                if (adNetworkConfiguretion != null)
                    adNetworkConfiguretion.SetNameAndIntegrationStatus();
            }

            _serializedSDKConfiguretionInfo.ApplyModifiedProperties();
        }

        static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
        {
            LookForSDK();
        }
    }
#endif


}
