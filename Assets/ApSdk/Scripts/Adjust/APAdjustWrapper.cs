#if APSdk_Adjust

namespace APSdk
{


    using System.Collections.Generic;
    using UnityEngine;
    using com.adjust.sdk;

    [DefaultExecutionOrder(APSdkConstant.EXECUTION_ORDER_AdjustWrapper)]
    public class APAdjustWrapper : MonoBehaviour
    {
        #region Public Variables

        public static APAdjustWrapper Instance;

        #endregion

        #region Private Variables

        private APAdjustInfo _aPAdjustInfo;

        #endregion

        #region Configuretion

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void OnGameStart()
        {
            if (Instance == null)
            {

                GameObject newAPAdjustWrapper = new GameObject("APAdjustWrapper");
                Instance = newAPAdjustWrapper.AddComponent<APAdjustWrapper>();

                DontDestroyOnLoad(newAPAdjustWrapper);
            }
        }

        #endregion

        #region Mono Behaviour

        private void Awake()
        {
            _aPAdjustInfo = Resources.Load<APAdjustInfo>("Adjust/APAdjustInfo");
        }

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

        public void Initialize()
        {

            APAdjustInfo adjustInfo = Resources.Load<APAdjustInfo>("Adjust/APAdjustInfo");

            AdjustConfig adjustConfig = new AdjustConfig(
                adjustInfo.appToken,
                adjustInfo.environment,
                adjustInfo.logLevel == AdjustLogLevel.Suppress);

            adjustConfig.setLogLevel(adjustInfo.logLevel);
            adjustConfig.setSendInBackground(adjustInfo.sendInBackground);
            adjustConfig.setEventBufferingEnabled(adjustInfo.eventBuffering);
            adjustConfig.setLaunchDeferredDeeplink(adjustInfo.launchDeferredDeeplink);

            adjustConfig.setDelayStart(adjustInfo.startDelay);

            Adjust.start(adjustConfig);

            APSdkLogger.Log("Adjust Initialized");
        }

        public void LogEvent(string eventName, Dictionary<string, object> eventParams)
        {
            if (_aPAdjustInfo.logAdjustEvent) {
                AdjustEvent newEvent = new AdjustEvent(eventName);
                Adjust.trackEvent(newEvent);
            }
            else
            {

                APSdkLogger.LogError("'logAdjustEvent' is currently turned off from APSDkIntegrationManager, please set it to 'true'");
            }

        }

        #endregion
    }
}

#endif



