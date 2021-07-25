﻿
#if APSdk_Facebook

namespace APSdk
{
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Events;
    using Facebook.Unity;

    [DefaultExecutionOrder(APSdkConstant.EXECUTION_ORDER_FacebookWrapper)]
    public class APFacebookWrapper : MonoBehaviour
    {
    #region Public Variables

        public static APFacebookWrapper Instance;

        public static bool IsFacebookInitialized { get { return FB.IsInitialized; } }

        #endregion

        #region private Variables

        private APSdkConfiguretionInfo _apSdkConfiguretionInfo;
        private UnityAction _OnInitialized;
        private APFacebookInfo _apFacebookInfo;

        #endregion

        #region Configuretion

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void OnGameStart() {
            if (Instance == null)
            {

                GameObject newAPFacebookWrapper = new GameObject("APFacebookWrapper");
                Instance = newAPFacebookWrapper.AddComponent<APFacebookWrapper>();

                DontDestroyOnLoad(newAPFacebookWrapper);
            }
        }

        private void OnInitializeCallback() {

            if (FB.IsInitialized)
            {
                FB.ActivateApp();
                APSdkLogger.Log("FacebookSDK initialized");
                _OnInitialized?.Invoke();
            }
            else
                APSdkLogger.LogError("Failed to Initialize the Facebook SDK");
        }

        private void OnHideUnityCallback(bool isGameShown) {


        }

#endregion

#region Public Callback

        public void Initialize(APSdkConfiguretionInfo apSdkConfiguretionInfo, UnityAction OnInitialized = null) {

            _apSdkConfiguretionInfo = apSdkConfiguretionInfo;
            _OnInitialized = OnInitialized;

            _apFacebookInfo = Resources.Load<APFacebookInfo>("Facebook/APFacebookInfo");

            if (!FB.IsInitialized)
            {
                FB.Init(OnInitializeCallback, OnHideUnityCallback);
            }
            else {

                APSdkLogger.Log("FacebookSDK already initialized");
            }
        }

        public void ProgressionEvent(string eventName, Dictionary<string, object> eventParams)
        {

            if (_apFacebookInfo.IsTrackingProgressionEvent)
            {
                LogEvent(eventName, eventParams);
            }
            else
            {
                APSdkLogger.LogWarning("'ProgressionEvent' is disabled for 'FacebookSDK'");
            }
        }

        public void AdEvent(string eventName, Dictionary<string, object> eventParams) {

            if (_apFacebookInfo.IsTrackingAdEvent)
            {
                LogEvent(eventName, eventParams);
            }
            else {
                APSdkLogger.LogWarning("'AdEvent' is disabled for 'FacebookSDK'");
            }
        }

        public void LogEvent(string eventName, Dictionary<string, object> eventParams) {

            if (_apSdkConfiguretionInfo.logAnalyticsEvent) {

                if (_apFacebookInfo.IsFacebookEventEnabled)
                {

                    if (FB.IsInitialized)
                    {
                        FB.LogAppEvent(
                                eventName,
                                parameters: eventParams
                            );
                    }
                    else
                    {
                        APSdkLogger.LogError(string.Format("{0}\n{1}", "Failed to log event for facebook analytics!", eventName));
                    }
                }
                else
                {

                    APSdkLogger.LogError("'logEventOnFacebook' is currently turned off from APSDkIntegrationManager, please set it to 'true'");
                }
            }
        }

#endregion
    }
}



#endif
