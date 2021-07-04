#if APSdk_GameAnalytics

namespace APSdk
{
    using System.Collections;
    using UnityEngine;
    using GameAnalyticsSDK;

    [DefaultExecutionOrder(APSdkConstant.EXECUTION_ORDER_GameAnalytics)]
    public class APGameAnalyticsWrapper : MonoBehaviour, IGameAnalyticsATTListener
    {
        #region Public Variables

        public static APGameAnalyticsWrapper Instance;

        #endregion

        #region Private Variables

        private APGameAnalyticsInfo _apGameAnalyticsInfo;

        #endregion

        #region Configuretion

        private IEnumerator InitializationWithDelay() {

            yield return new WaitForSeconds(0.1f);

#if UNITY_IOS

            GameAnalytics.RequestTrackingAuthorization(this);

#else
            GameAnalytics.Initialize();
#endif
            _apGameAnalyticsInfo = Resources.Load<APGameAnalyticsInfo>("GameAnalytics/APGameAnalyticsInfo");

        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void OnGameStart()
        {
            if (Instance == null)
            {

                Instantiate(Resources.Load("GameAnalytics/AP_GameAnalytics"));

                GameObject newAPGameAnalyticsWrapper = new GameObject("APGameAnalyticsWrapper");
                Instance = newAPGameAnalyticsWrapper.AddComponent<APGameAnalyticsWrapper>();

                DontDestroyOnLoad(newAPGameAnalyticsWrapper);
            }
        }

        #endregion

        #region GameAnalytics Callback

        public void GameAnalyticsATTListenerAuthorized()
        {
            GameAnalytics.Initialize();
        }

        public void GameAnalyticsATTListenerDenied()
        {
            GameAnalytics.Initialize();
        }

        public void GameAnalyticsATTListenerNotDetermined()
        {
            GameAnalytics.Initialize();
        }

        public void GameAnalyticsATTListenerRestricted()
        {
            GameAnalytics.Initialize();
        }

        #endregion

        #region Public Callback

        public void Initialize() {

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
                ProgressionEvent(progressionStatus, string.Format("world{0}", world < 0 ? 1 : world), string.Format("level{0}", level));
            else
                ProgressionEvent(progressionStatus, string.Format("world{0}", world < 0 ? 1 : world), string.Format("level{0}", level), score);
        }

        public void ProgressionEvent(GAProgressionStatus progressionStatus, string progression01)
        {
            if(_apGameAnalyticsInfo.TrackProgressionEvent)
                GameAnalytics.NewProgressionEvent(progressionStatus, progression01);
        }

        public void ProgressionEvent(GAProgressionStatus progressionStatus, string progression01, int score)
        {
            if (_apGameAnalyticsInfo.TrackProgressionEvent)
                GameAnalytics.NewProgressionEvent(progressionStatus, progression01, score);
        }

        public void ProgressionEvent(GAProgressionStatus progressionStatus, string progression01, string progression02)
        {
            if (_apGameAnalyticsInfo.TrackProgressionEvent)
                GameAnalytics.NewProgressionEvent(progressionStatus, progression01, progression02);
        }

        public void ProgressionEvent(GAProgressionStatus progressionStatus, string progression01, string progression02, int score)
        {
            if (_apGameAnalyticsInfo.TrackProgressionEvent)
                GameAnalytics.NewProgressionEvent(progressionStatus, progression01, progression02, score);
        }

        public void ProgressionEvent(GAProgressionStatus progressionStatus, string progression01, string progression02, string progression03)
        {
            if (_apGameAnalyticsInfo.TrackProgressionEvent)
                GameAnalytics.NewProgressionEvent(progressionStatus, progression01, progression02, progression03);
        }

        public void ProgressionEvent(GAProgressionStatus progressionStatus, string progression01, string progression02, string progression03, int score)
        {
            if (_apGameAnalyticsInfo.TrackProgressionEvent)
                GameAnalytics.NewProgressionEvent(progressionStatus, progression01, progression02, progression03, score);
        }

        #endregion

        #region Ad Event

        public void AdEvent(GAAdAction adAction, GAAdType adType, string sdkName, string adPlacement)
        {
            if(_apGameAnalyticsInfo.TrackAdEvent)
                GameAnalytics.NewAdEvent(adAction, adType, sdkName, adPlacement);
        }

        public void AdEvent(GAAdAction adAction, GAAdType adType, string sdkName, string adPlacement, long duration) {

            if (_apGameAnalyticsInfo.TrackAdEvent)
                GameAnalytics.NewAdEvent(adAction, adType, sdkName, adPlacement, duration);
        }

        public void AdEvent(GAAdAction adAction, GAAdType adType, string sdkName, string adPlacement, GAAdError noAdError)
        {
            if (_apGameAnalyticsInfo.TrackAdEvent)
                GameAnalytics.NewAdEvent(adAction, adType, sdkName, adPlacement, noAdError);
        }

        #endregion
    }
}

#endif

