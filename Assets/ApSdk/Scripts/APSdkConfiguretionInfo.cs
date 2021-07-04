namespace APSdk
{
    using UnityEngine;

    //[CreateAssetMenu(fileName = "APSdkConfiguretionInfo", menuName = APSdkConstant.NameOfSDK + "/APSdkConfiguretionInfo")]
    public class APSdkConfiguretionInfo : ScriptableObject
    {
        #region Public Variables

#if UNITY_EDITOR

        [SerializeField] private bool _isLionKitSDKIntegrated = false;
        [SerializeField] private bool _isFacebookSDKIntegrated = false;
        [SerializeField] private bool _isAdjustSDKIntegrated = false;
        [SerializeField] private bool _isGameAnalyticsSDKIntegrated = false;

        [SerializeField] private bool _showGeneralSetting = false;
        [SerializeField] private bool _showLionAdSetting = false;
        [SerializeField] private bool _showFacebookSetting = false;
        [SerializeField] private bool _showAdjustSetting = false;
        [SerializeField] private bool _showGameAnalyticsSetting = false;
        [SerializeField] private bool _showABTestSetting = false;
        [SerializeField] private bool _showDebuggingSetting = false;
#endif

        public bool logAnalyticsEvent = true;
        public bool maxMediationDebugger = false;

        [Space(5.0f)]
        public bool showAPSdkLogInConsole = true;

        [Space(5.0f)]
        public Color infoLogColor = Color.cyan;
        public Color warningLogColor = Color.yellow;
        public Color errorLogColor = Color.red;

        #endregion

    }
}

