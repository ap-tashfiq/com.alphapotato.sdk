namespace APSdk
{
    using UnityEngine;
    using System.Collections.Generic;

    //[CreateAssetMenu(fileName = "APSdkConfiguretionInfo", menuName = APSdkConstant.NameOfSDK + "/APSdkConfiguretionInfo")]
    public class APSdkConfiguretionInfo : ScriptableObject
    {
        #region Public Variables

#if UNITY_EDITOR

        [Header("Parameter  :   Editor")]
        [SerializeField] private bool _isLionKitSDKIntegrated = false;
        [SerializeField] private bool _isFacebookSDKIntegrated = false;
        [SerializeField] private bool _isAdjustSDKIntegrated = false;
        [SerializeField] private bool _isGameAnalyticsSDKIntegrated = false;
        [SerializeField] private bool _isFirebaseSDKIntegrated = false;

        [SerializeField] private bool _showGeneralSetting = false;
        [SerializeField] private bool _showLionAdSetting = false;
        [SerializeField] private bool _showFacebookSetting = false;
        [SerializeField] private bool _showAdjustSetting = false;
        [SerializeField] private bool _showGameAnalyticsSetting = false;
        [SerializeField] private bool _showFirebaseSetting = false;
        [SerializeField] private bool _showAdNetworks = false;
        [SerializeField] private bool _showABTestSetting = false;
        [SerializeField] private bool _showDebuggingSetting = false;
#endif
        public BaseClassForAdConfiguretion SelectedAdConfig
        {
            get {
                if (indexOfActiveAdConfiguretion >= 0 && indexOfActiveAdConfiguretion < listOfAdConfiguretion.Count)
                    return listOfAdConfiguretion[indexOfActiveAdConfiguretion];

                return null;
            }
        }

        [Header("Parameter  :   Analytics")]
        public bool logAnalyticsEvent = true;

        [Header("Parameter  :   Ads")]
        public int indexOfActiveAdConfiguretion = -1;
        public List<BaseClassForAdConfiguretion> listOfAdConfiguretion;

        [Header("Parameter  :   Debugger")]
        public bool maxMediationDebugger = false;

        [Space(5.0f)]
        public bool showAPSdkLogInConsole = true;

        [Space(5.0f)]
        public Color infoLogColor = Color.cyan;
        public Color warningLogColor = Color.yellow;
        public Color errorLogColor = Color.red;

        #endregion

        #region Configuretion

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void OnGameStart() {

            APSdkConfiguretionInfo apSdkConfiguretionInfo = Resources.Load<APSdkConfiguretionInfo>("APSdkConfiguretionInfo");
            if (apSdkConfiguretionInfo.SelectedAdConfig != null)
            {
                apSdkConfiguretionInfo.SelectedAdConfig.Initialize();
            }
            else {

                APSdkLogger.LogError("No Ad Network Selected");
            }
        }

        #endregion

    }
}

