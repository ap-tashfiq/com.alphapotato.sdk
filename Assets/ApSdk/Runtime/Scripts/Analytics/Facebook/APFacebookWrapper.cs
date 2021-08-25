
#if APSdk_Facebook

namespace APSdk
{
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Events;
    using Facebook.Unity;

    public class APFacebookWrapper : MonoBehaviour
    {
    #region Public Variables

        public static APFacebookWrapper Instance;

        public static bool IsFacebookInitialized { get { return FB.IsInitialized; } }

        #endregion

        #region private Variables

        
        private APSdkConfiguretionInfo _apSdkConfiguretionInfo;
        private APFacebookConfiguretion _facebookConfiguretion;
        private bool _isATTEnabled = false;
        private UnityAction _OnInitialized;
        

        #endregion

        #region Configuretion

        

        private void OnInitializeCallback() {

            if (FB.IsInitialized)
            {
                FB.ActivateApp();
                APSdkLogger.Log("FacebookSDK initialized");


#if UNITY_IOS

                APSdkLogger.Log(string.Format("Facebook ATT Status (iOS) = {0}", _isATTEnabled));
                FB.Mobile.SetAdvertiserTrackingEnabled(_isATTEnabled);
                AudienceNetwork.AdSettings.SetAdvertiserTrackingEnabled(_isATTEnabled);

#endif
                _OnInitialized?.Invoke();
            }
            else
                APSdkLogger.LogError("Failed to Initialize the Facebook SDK");
        }

        private void OnHideUnityCallback(bool isGameShown) {


        }

#endregion

#region Public Callback

        public void Initialize(APSdkConfiguretionInfo apSdkConfiguretionInfo, APFacebookConfiguretion facebookConfiguretion, bool isATTEnabled, UnityAction OnInitialized = null) {

            _apSdkConfiguretionInfo = apSdkConfiguretionInfo;
            _facebookConfiguretion = facebookConfiguretion;
            _isATTEnabled = isATTEnabled;
            _OnInitialized = OnInitialized;

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

            if (_facebookConfiguretion.IsTrackingProgressionEvent)
            {
                LogEvent(eventName, eventParams);
            }
            else
            {
                APSdkLogger.LogWarning("'ProgressionEvent' is disabled for 'FacebookSDK'");
            }
        }

        public void AdEvent(string eventName, Dictionary<string, object> eventParams) {

            if (_facebookConfiguretion.IsTrackingAdEvent)
            {
                LogEvent(eventName, eventParams);
            }
            else {
                APSdkLogger.LogWarning("'AdEvent' is disabled for 'FacebookSDK'");
            }
        }

        public void LogEvent(string eventName, Dictionary<string, object> eventParams) {

            if (_apSdkConfiguretionInfo.IsAnalyticsEventEnabled)
            {

                if (_facebookConfiguretion.IsAnalyticsEventEnabled)
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
                        APSdkLogger.LogError(string.Format("{0}\n{1}", "Failed to log event for facebook analytics! as it's not initialized", eventName, eventParams));
                    }
                }
                else
                {
                    APSdkLogger.LogWarning("'logFacebookEvent' is currently turned off from APSDkIntegrationManager, please set it to 'true'");
                }
            }
            else {

                APSdkLogger.LogWarning("Analytics events are currently disabled under the 'Analytics'->'EnableAnalyticsEvents' on 'APSdk IntegrationManager'");
            }
        }

#endregion
    }
}



#endif

