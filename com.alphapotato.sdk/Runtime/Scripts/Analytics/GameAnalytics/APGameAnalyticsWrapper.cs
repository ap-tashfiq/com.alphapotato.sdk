﻿#if APSdk_GameAnalytics

namespace com.alphapotato.sdk
{
    using System.Collections;
    using UnityEngine;
    using GameAnalyticsSDK;

    public class APGameAnalyticsWrapper : MonoBehaviour, IGameAnalyticsATTListener
    {
        #region Public Variables

        public static APGameAnalyticsWrapper Instance;

        #endregion

        #region Private Variables

        private APSdkConfiguretionInfo _apSdkConfiguretionInfo;
        private APGameAnalyticsConfiguretion _apGameAnalyticsConfiguretion;

        #endregion

        #region Configuretion

        private bool CanLogEvent() {

            if (_apSdkConfiguretionInfo.IsAnalyticsEventEnabled)
            {

                if (_apGameAnalyticsConfiguretion.IsAnalyticsEventEnabled)
                {
                    return true;
                }
                else
                {
                    APSdkLogger.LogWarning("'logGAEvent' is currently turned off from APSDkIntegrationManager, please set it to 'true'");
                }
            }
            else {
                APSdkLogger.LogWarning("Analytics events are currently disabled under the 'Analytics'->'EnableAnalyticsEvents' on 'APSdk IntegrationManager'");
            }

            return false;
        }

        private IEnumerator InitializationWithDelay() {

            yield return new WaitForSeconds(1f);

            if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
                GameAnalytics.RequestTrackingAuthorization(this);
            }
            else
            {
                APSdkLogger.Log("GA Initialized");
                GameAnalytics.Initialize();
            }
            
        }

        #endregion

        #region GameAnalytics Callback

        public void GameAnalyticsATTListenerAuthorized()
        {
            GameAnalytics.Initialize();
            APSdkLogger.Log("GA : ATTListenerAuthorized");
        }

        public void GameAnalyticsATTListenerDenied()
        {
            GameAnalytics.Initialize();
            APSdkLogger.LogError("GA : ATTListenerDenied");
        }

        public void GameAnalyticsATTListenerNotDetermined()
        {
            GameAnalytics.Initialize();
            APSdkLogger.LogError("GA : ATTListenerNotDetermined");
        }

        public void GameAnalyticsATTListenerRestricted()
        {
            GameAnalytics.Initialize();
            APSdkLogger.LogWarning("GA : ATTListenerRestricted");
        }

        #endregion

        #region Public Callback

        public void Initialize(APSdkConfiguretionInfo apSdkConfiguretionInfo, APGameAnalyticsConfiguretion apGameAnalyticsConfiguretion) {

            _apSdkConfiguretionInfo = apSdkConfiguretionInfo;
            _apGameAnalyticsConfiguretion = apGameAnalyticsConfiguretion;

            StartCoroutine(InitializationWithDelay());
        }

        #endregion

        #region Progression Event

        public void ProgressionEvents(GAProgressionStatus progressionStatus, int level, int score = -1)
        {
            ProgressionEvents(progressionStatus, level, -1, score);
        }

        public void ProgressionEvents(GAProgressionStatus progressionStatus, int level, int world = -1, int score = -1)
        {
            if (score < 0)
                ProgressionEvent(progressionStatus, string.Format("world{0}", world == -1 ? _apGameAnalyticsConfiguretion.DefaultWorldIndex : world), string.Format("level{0}", level));
            else
                ProgressionEvent(progressionStatus, string.Format("world{0}", world == -1 ? _apGameAnalyticsConfiguretion.DefaultWorldIndex : world), string.Format("level{0}", level), score);
        }

        public void ProgressionEvent(GAProgressionStatus progressionStatus, string progression01)
        {
            if(CanLogEvent() && _apGameAnalyticsConfiguretion.IsTrackingProgressionEvent)
                GameAnalytics.NewProgressionEvent(progressionStatus, progression01);
        }

        public void ProgressionEvent(GAProgressionStatus progressionStatus, string progression01, int score)
        {
            if (CanLogEvent() && _apGameAnalyticsConfiguretion.IsTrackingProgressionEvent)
                GameAnalytics.NewProgressionEvent(progressionStatus, progression01, score);
        }

        public void ProgressionEvent(GAProgressionStatus progressionStatus, string progression01, string progression02)
        {
            if (CanLogEvent() && _apGameAnalyticsConfiguretion.IsTrackingProgressionEvent)
                GameAnalytics.NewProgressionEvent(progressionStatus, progression01, progression02);
        }

        public void ProgressionEvent(GAProgressionStatus progressionStatus, string progression01, string progression02, int score)
        {
            if (CanLogEvent() && _apGameAnalyticsConfiguretion.IsTrackingProgressionEvent)
                GameAnalytics.NewProgressionEvent(progressionStatus, progression01, progression02, score);
        }

        public void ProgressionEvent(GAProgressionStatus progressionStatus, string progression01, string progression02, string progression03)
        {
            if (CanLogEvent() && _apGameAnalyticsConfiguretion.IsTrackingProgressionEvent)
                GameAnalytics.NewProgressionEvent(progressionStatus, progression01, progression02, progression03);
        }

        public void ProgressionEvent(GAProgressionStatus progressionStatus, string progression01, string progression02, string progression03, int score)
        {
            if (CanLogEvent() && _apGameAnalyticsConfiguretion.IsTrackingProgressionEvent)
                GameAnalytics.NewProgressionEvent(progressionStatus, progression01, progression02, progression03, score);
        }

        #endregion

        #region Ad Event

        public void AdEvent(GAAdAction adAction, GAAdType adType, string sdkName, string adPlacement)
        {
            if(_apSdkConfiguretionInfo.IsAnalyticsEventEnabled && _apGameAnalyticsConfiguretion.IsAnalyticsEventEnabled && _apGameAnalyticsConfiguretion.IsTrackingAdEvent)
                GameAnalytics.NewAdEvent(adAction, adType, sdkName, adPlacement);
        }

        public void AdEvent(GAAdAction adAction, GAAdType adType, string sdkName, string adPlacement, long duration) {

            if (_apSdkConfiguretionInfo.IsAnalyticsEventEnabled && _apGameAnalyticsConfiguretion.IsAnalyticsEventEnabled && _apGameAnalyticsConfiguretion.IsTrackingAdEvent)
                GameAnalytics.NewAdEvent(adAction, adType, sdkName, adPlacement, duration);
        }

        public void AdEvent(GAAdAction adAction, GAAdType adType, string sdkName, string adPlacement, GAAdError noAdError)
        {
            if (_apSdkConfiguretionInfo.IsAnalyticsEventEnabled && _apGameAnalyticsConfiguretion.IsAnalyticsEventEnabled && _apGameAnalyticsConfiguretion.IsTrackingAdEvent)
                GameAnalytics.NewAdEvent(adAction, adType, sdkName, adPlacement, noAdError);
        }

        #endregion
    }
}

#endif

