#if APSdk_LionKit

namespace APSdk
{
    using UnityEngine;
    using UnityEngine.Events;
    using LionStudios;
    using LionStudios.Ads;

#if APSdk_GameAnalytics
    using GameAnalyticsSDK;
#endif

    public class APLionKitInterstitialAdController  :   MonoBehaviour
    {
        //Approch2  :   LionKit

        #region Public Variables

        public static APLionKitInterstitialAdController Instance;

        public bool IsInterstitialAdReady { get { return Interstitial.IsAdReady; } }
        public bool IsAdRunning { get; private set; }

        #endregion

        #region Private Variables

        private APSdkConfiguretionInfo _apSdkConfiguretionInfo;
        private APLionKitInfo _apLionKitInfo;
#if APSdk_GameAnalytics
        private APGameAnalyticsInfo _apGameAnalyticsInfo;
#endif

        private ShowAdRequest _showInterstitialAdRequest;

        private string _adPlacement;

        private UnityAction _OnCallingLionKitAnalytics;
        private UnityAction _OnAdClosed;
        private UnityAction _OnAdFailed;

        #endregion

        #region MonoBehaviour

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this);
            }
            else
            {

                Destroy(gameObject);
            }
        }

#if APSdk_GameAnalytics

        private void OnApplicationPause(bool pause)
        {
            if (_apSdkConfiguretionInfo.logAnalyticsEvent
            && _apGameAnalyticsInfo.TrackAdEvent)
            {

                if (IsAdRunning)
                {

                    if (pause)
                    {
                        GameAnalytics.PauseTimer(_adPlacement);
                    }
                    else
                    {
                        GameAnalytics.ResumeTimer(_adPlacement);
                    }
                }
            }
        }

#endif

        #endregion

        #region Configuretion

        private void ShowAd()
        {
            Interstitial.Show(_showInterstitialAdRequest);
        }

        #endregion

        #region Public Callback

        public void Initialization(APSdkConfiguretionInfo apSdkConfiguretionInfo, APLionKitInfo apLionAdInfo) {

            _apSdkConfiguretionInfo = apSdkConfiguretionInfo;
            _apLionKitInfo = apLionAdInfo;

#if APSdk_GameAnalytics
            _apGameAnalyticsInfo = Resources.Load<APGameAnalyticsInfo>("GameAnalytics/APGameAnalyticsInfo");
#endif

            _showInterstitialAdRequest = new ShowAdRequest();

            // Ad event callbacks
            _showInterstitialAdRequest.OnDisplayed += adUnitId =>
            {
                APSdkLogger.Log("Displayed InterstitialAd :: Ad Unit ID = " + adUnitId);


#if APSdk_GameAnalytics
                GameAnalytics.StartTimer(_adPlacement);
#endif
                IsAdRunning = true;

                
            };
            _showInterstitialAdRequest.OnClicked += adUnitId =>
            {
                APSdkLogger.Log("Clicked InterstitialAd :: Ad Unit ID = " + adUnitId);
            };
            _showInterstitialAdRequest.OnHidden += adUnitId =>
            {
                APSdkLogger.Log("Closed InterstitialAd :: Ad Unit ID = " + adUnitId);

#if APSdk_GameAnalytics
                if (_apSdkConfiguretionInfo.logAnalyticsEvent && _apGameAnalyticsInfo.TrackAdEvent)
                {

                    APGameAnalyticsWrapper.Instance.AdEvent(
                        GAAdAction.Show,
                        GAAdType.Interstitial,
                        "max_sdk",
                        _adPlacement,
                        GameAnalytics.StopTimer(_adPlacement)
                    );
                }
#endif

                _OnCallingLionKitAnalytics?.Invoke();

                IsAdRunning = false;
                _OnAdClosed?.Invoke();

            };
            _showInterstitialAdRequest.OnFailedToDisplay += (adUnitId, error) =>
            {
                APSdkLogger.LogError("Failed To Display InterstitialAd :: Error = " + error + " :: Ad Unit ID = " + adUnitId);

#if APSdk_GameAnalytics
                if (_apSdkConfiguretionInfo.logAnalyticsEvent && _apGameAnalyticsInfo.TrackAdEvent)
                {
                    APGameAnalyticsWrapper.Instance.AdEvent(
                        GAAdAction.FailedShow,
                        GAAdType.Interstitial,
                        "max_sdk",
                        _adPlacement,
                        (GAAdError)error
                    );
                }
#endif

                IsAdRunning = false;
                _OnAdFailed?.Invoke();

            };
        }

        public void ShowInterstitialAd(
            string eventName,
            string paramName,
            object paramValue,
            string adPlacement = "interstitial",
            UnityAction OnAdFailed = null,
            UnityAction OnAdClosed = null)
        {
            if (_apLionKitInfo.enableRewardedAd)
            {

                _OnCallingLionKitAnalytics = () =>
                {
                    Analytics.LogEvent(
                        eventName,
                        paramName,
                        paramValue);
                };

                _adPlacement = adPlacement;
                _OnAdFailed = OnAdFailed;
                _OnAdClosed = OnAdClosed;

                ShowAd();
            }
            else
            {
                APSdkLogger.LogError(string.Format("InterstitialAd is set to disabled in APSDKIntegrationManager. Please set the flag to 'true' to see InterstitialAd"));
            }
        }

        public void ShowInterstitialAd(
            string adPlacement = "interstitial",
            UnityAction OnAdFailed = null,
            UnityAction OnAdClosed = null)
        {
            if (_apLionKitInfo.enableInterstitialAd)
            {

                _OnCallingLionKitAnalytics = null;

                _adPlacement = adPlacement;
                _OnAdFailed = OnAdFailed;
                _OnAdClosed = OnAdClosed;

                ShowAd();
            }
            else {
                APSdkLogger.LogError(string.Format("InterstitialAd is set to disabled in APSDKIntegrationManager. Please set the flag to 'true' to see InterstitialAd"));
            }
        }

#endregion

    }
}


#endif

