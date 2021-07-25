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

        [SerializeField] private bool _showGeneralSetting = false;
        [SerializeField] private bool _showAnalytics = false;
        [SerializeField] private bool _showAdNetworks = false;
        [SerializeField] private bool _showABTestSetting = false;
        [SerializeField] private bool _showDebuggingSetting = false;
#endif
        public APBaseClassForAdConfiguretion SelectedAdConfig
        {
            get {
                if (indexOfActiveAdConfiguretion >= 0 && indexOfActiveAdConfiguretion < listOfAdConfiguretion.Count)
                    return listOfAdConfiguretion[indexOfActiveAdConfiguretion];

                return null;
            }
        }

        [Header("Parameter  :   Analytics")]
        public bool logAnalyticsEvent = true;
        public List<APBaseClassForAnalyticsConfiguretion> listOfAnalyticsConfiguretion;

        [Header("Parameter  :   Ads")]
        public int indexOfActiveAdConfiguretion = -1;
        public List<APBaseClassForAdConfiguretion> listOfAdConfiguretion;

        [Header("Parameter  :   Debugger")]
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

