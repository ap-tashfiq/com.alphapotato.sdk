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

            SerializedProperty _isLionKitSDKIntegrated = _serializedSDKConfiguretionInfo.FindProperty("_isLionKitSDKIntegrated");
            SerializedProperty _isFacebookSDKIntegrated = _serializedSDKConfiguretionInfo.FindProperty("_isFacebookSDKIntegrated");
            SerializedProperty _isAdjustSDKIntegrated = _serializedSDKConfiguretionInfo.FindProperty("_isAdjustSDKIntegrated");
            SerializedProperty _isGameAnalyticsSDKIntegrated = _serializedSDKConfiguretionInfo.FindProperty("_isGameAnalyticsSDKIntegrated");
            SerializedProperty _isFirebaseSDKIntegrated = _serializedSDKConfiguretionInfo.FindProperty("_isFirebaseSDKIntegrated");

            _isLionKitSDKIntegrated.boolValue = APSdkScriptDefiniedSymbol.CheckLionKitIntegration();
            _isLionKitSDKIntegrated.serializedObject.ApplyModifiedProperties();

            _isFacebookSDKIntegrated.boolValue = APSdkScriptDefiniedSymbol.CheckFacebookIntegration();
            _isFacebookSDKIntegrated.serializedObject.ApplyModifiedProperties();

            _isAdjustSDKIntegrated.boolValue = APSdkScriptDefiniedSymbol.CheckAdjustIntegration();
            _isAdjustSDKIntegrated.serializedObject.ApplyModifiedProperties();

            _isGameAnalyticsSDKIntegrated.boolValue = APSdkScriptDefiniedSymbol.CheckGameAnalyticsIntegration();
            _isGameAnalyticsSDKIntegrated.serializedObject.ApplyModifiedProperties();

            _isFirebaseSDKIntegrated.boolValue = APSdkScriptDefiniedSymbol.CheckFirebaseIntegration();
            _isFirebaseSDKIntegrated.serializedObject.ApplyModifiedProperties();

            foreach (BaseClassForAdConfiguretion adConfig in _apSDKConfiguretionInfo.listOfAdConfiguretion)
                adConfig.SetSDKNameAndIntegrationStatus();

            _serializedSDKConfiguretionInfo.ApplyModifiedProperties();
        }

        static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
        {
            LookForSDK();
        }
    }
#endif


}
