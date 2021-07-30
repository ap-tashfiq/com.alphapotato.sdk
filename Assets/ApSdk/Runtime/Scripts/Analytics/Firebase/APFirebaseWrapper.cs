namespace APSdk
{
#if APSdk_Firebase

    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Events;
    using Firebase;
    using Firebase.Analytics;

    public class APFirebaseWrapper : MonoBehaviour
    {
        #region Public Variables

        public static APFirebaseWrapper Instance;

        #endregion

        #region Private Variables

        private APSdkConfiguretionInfo _apSdkConfiguretionInfo;
        private APFirebaseConfiguretion _apFirebaseConfiguretion;

        #endregion



        #region Public Callback

        public void Initialize(APSdkConfiguretionInfo apSdkConfiguretionInfo, APFirebaseConfiguretion apFirebaseConfiguretion, UnityAction OnInitialized = null)
        {
            _apSdkConfiguretionInfo = apSdkConfiguretionInfo;
            _apFirebaseConfiguretion = apFirebaseConfiguretion;

            FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
            {
                var dependencyStatus = task.Result;
                if (dependencyStatus == DependencyStatus.Available)
                {
                    // subscribe to firebase events
                    // subscribe here so avoid error if dependency check fails

                    APSdkLogger.Log("Firebase Initialized");
                    OnInitialized?.Invoke();
                }
                else
                {
                    APSdkLogger.LogError($"Firebase: Could not resolve all Firebase dependencies: {dependencyStatus}");
                }
            });
        }


        public void LogFirebaseEvent(string eventName)
        {
            if (_apFirebaseConfiguretion.IsAnalyticsEventEnabled)
            {

                FirebaseAnalytics.LogEvent(
                   eventName);
            }

        }

        public void LogFirebaseEvent(string eventName, string parameName, string paramValue)
        {

            if (_apSdkConfiguretionInfo.IsAnalyticsEventEnabled && _apFirebaseConfiguretion.IsAnalyticsEventEnabled)
            {
                FirebaseAnalytics.LogEvent(
                        eventName,
                        parameName,
                        paramValue
                    );
            }
        }

        public void LogFirebaseEvent(string eventName, List<Parameter> parameter)
        {

            if (_apSdkConfiguretionInfo.IsAnalyticsEventEnabled && _apFirebaseConfiguretion.IsAnalyticsEventEnabled)
            {

                FirebaseAnalytics.LogEvent(
                    eventName,
                    parameter.ToArray()
                );
            }
        }

        public void ProgressionEvent(string eventName, string parameName, string paramValue)
        {

            if (_apFirebaseConfiguretion.IsTrackingProgressionEvent)
            {
                LogFirebaseEvent(eventName, parameName, paramValue);
            }
            else
            {
                APSdkLogger.LogWarning("'ProgressionEvent' is disabled for 'FirebaseSDK'");
            }
        }

        public void AdEvent(string eventName, string parameName, string paramValue)
        {

            if (_apFirebaseConfiguretion.IsTrackingAdEvent)
            {
                LogFirebaseEvent(eventName, parameName, paramValue);
            }
            else
            {
                APSdkLogger.LogWarning("'AdEvent' is disabled for 'FirebaseSDK'");
            }
        }

        #endregion
    }

#endif
}




