﻿namespace APSdk
{
    using UnityEngine;
    using System.Collections.Generic;

    //[CreateAssetMenu(fileName = "APSdkConfiguretionInfo", menuName = APSdkConstant.NameOfSDK + "/APSdkConfiguretionInfo")]
    public class APSdkConfiguretionInfo : ScriptableObject
    {
        #region Private Variables

#if UNITY_EDITOR

        [HideInInspector, SerializeField] private bool _showGeneralSetting = false;
        [HideInInspector, SerializeField] private bool _showLionKitSettings = false;
        [HideInInspector, SerializeField] private bool _showAnalytics = false;
        [HideInInspector, SerializeField] private bool _showAdNetworks = false;
        [HideInInspector, SerializeField] private bool _showABTestSetting = false;
        [HideInInspector, SerializeField] private bool _showDebuggingSetting = false;
#endif

        [HideInInspector, SerializeField] private bool _isLionKitIntegrated;

        [HideInInspector, SerializeField] private bool _enableAnalyticsEvents = true;
        [HideInInspector, SerializeField] private int _indexOfActiveAdConfiguretion = -1;

        [HideInInspector, SerializeField] private bool _showMaxMediationDebugger = false;

        [HideInInspector, SerializeField] private bool _showAPSdkLogInConsole = true;

        [Space(5.0f)]
        [HideInInspector, SerializeField] private Color _infoLogColor = Color.cyan;
        [HideInInspector, SerializeField] private Color _warningLogColor = Color.yellow;
        [HideInInspector, SerializeField] private Color _errorLogColor = Color.red;

        #endregion

        #region Public Variables

        //Analytics
        public bool IsAnalyticsEventEnabled { get { return _enableAnalyticsEvents; } }

        //AdNework
        public int  IndexOfActiveAdConfiguretion { get { return _indexOfActiveAdConfiguretion; } }
        public APBaseClassForAdConfiguretion SelectedAdConfig
        {
            get {
                if (IndexOfActiveAdConfiguretion >= 0 && IndexOfActiveAdConfiguretion < listOfAdConfiguretion.Count)
                    return listOfAdConfiguretion[IndexOfActiveAdConfiguretion];

                return null;
            }
        }

        //LionKit
        public bool ShowMaxMediationDebugger { get { return _showMaxMediationDebugger; } }

        //Debugging
        public bool ShowAPSdkLogInConsole { get { return _showAPSdkLogInConsole; } }

        public Color InfoLogColor { get { return _infoLogColor; } }
        public Color WarningLogColor { get { return _warningLogColor; } }
        public Color ErrorLogColor { get { return _errorLogColor; } }        

        #endregion

        #region Public Callback

#if UNITY_EDITOR
        public void SetIndexForActiveAdConfiguretion(int index) {
            _indexOfActiveAdConfiguretion = index;
        }
#endif

#endregion

    }
}

