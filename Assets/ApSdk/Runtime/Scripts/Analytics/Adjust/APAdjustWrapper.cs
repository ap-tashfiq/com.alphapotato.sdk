#if APSdk_Adjust

namespace APSdk
{
    using System.Collections.Generic;
    using UnityEngine;
    using com.adjust.sdk;

    public class APAdjustWrapper : MonoBehaviour
    {
        #region Public Variables

        public static APAdjustWrapper Instance;

        #endregion

        #region Private Variables

        private APSdkConfiguretionInfo _apSdkConfiguretionInfo;
        private APAdjustConfiguretion _adjustConfiguretion;


        #endregion

        #region Mono Behaviour

        private void OnApplicationPause(bool pause)
        {

#if UNITY_EDITOR
    return;
#elif UNITY_IOS
            // No action, iOS SDK is subscribed to iOS lifecycle notifications.
#elif UNITY_ANDROID
            if (pause)
                {
                    AdjustAndroid.OnPause();
                }
                else
                {
                    AdjustAndroid.OnResume();
                }
#endif
        }

        #endregion

        #region Public Callback

        public void Initialize(APSdkConfiguretionInfo apSdkConfiguretionInfo, APAdjustConfiguretion adjustConfiguretion) {

            _apSdkConfiguretionInfo = apSdkConfiguretionInfo;
            _adjustConfiguretion = adjustConfiguretion;

            AdjustConfig adjustConfig = new AdjustConfig(
                adjustConfiguretion.appToken,
                adjustConfiguretion.Environment,
                adjustConfiguretion.LogLevel == AdjustLogLevel.Suppress);

            adjustConfig.setLogLevel(adjustConfiguretion.LogLevel);
            adjustConfig.setSendInBackground(adjustConfiguretion.SendInBackground);
            adjustConfig.setEventBufferingEnabled(adjustConfiguretion.EventBuffering);
            adjustConfig.setLaunchDeferredDeeplink(adjustConfiguretion.LaunchDeferredDeeplink);

            adjustConfig.setDelayStart(adjustConfiguretion.StartDelay);

            Adjust.start(adjustConfig);

            APSdkLogger.Log("Adjust Initialized");
        }


        public void ProgressionEvent(string eventName, Dictionary<string, object> eventParams)
        {

            if (_adjustConfiguretion.IsTrackingProgressionEvent)
            {
                LogEvent(eventName, eventParams);
            }
            else
            {
                APSdkLogger.LogWarning("'ProgressionEvent' is disabled for 'AdjustSDK'");
            }
        }

        public void AdEvent(string eventName, Dictionary<string, object> eventParams)
        {

            if (_adjustConfiguretion.IsTrackingAdEvent)
            {
                LogEvent(eventName, eventParams);
            }
            else
            {
                APSdkLogger.LogWarning("'AdEvent' is disabled for 'AdjustSDK'");
            }
        }

        public void LogEvent(string eventName, Dictionary<string, object> eventParams)
        {
            if (_apSdkConfiguretionInfo.logAnalyticsEvent) {

                if (_adjustConfiguretion.IsAnalyticsEventEnabled)
                {
                    AdjustEvent newEvent = new AdjustEvent(eventName);
                    Adjust.trackEvent(newEvent);
                }
                else
                {

                    APSdkLogger.LogError("'logAdjustEvent' is currently turned off from APSDkIntegrationManager, please set it to 'true'");
                }
            }
        }

        #endregion
    }
}

#endif



