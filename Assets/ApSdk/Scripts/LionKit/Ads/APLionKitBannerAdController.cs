#if APSdk_LionKit

namespace APSdk
{
    using UnityEngine;
    using LionStudios.Ads;

#if APSdk_GameAnalytics
    using GameAnalyticsSDK;
#endif

    public class APLionKitBannerAdController    :   MonoBehaviour
    {
        //Approch2  :   LionKit

        #region Public Variables

        public static APLionKitBannerAdController Instance;

        public bool IsBannerAdReady { get { return Banner.IsAdReady; } }
        public bool IsAdRunning { get; private set; }
        #endregion

        #region Private Variables

        private APSdkConfiguretionInfo _apSdkConfiguretionInfo;
        private APLionKitInfo _apLionKitInfo;

#if APSdk_GameAnalytics
        private APGameAnalyticsInfo _apGameAnalyticsInfo;
#endif

        private ShowAdRequest _ShowBannerAdRequest;

        private string _adPlacement;

        #endregion

        #region Mono Behaviour

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

        #region Public Callback

        public void Initialization(APSdkConfiguretionInfo apSdkConfiguretionInfo, APLionKitInfo apLionAdInfo) {

            _apSdkConfiguretionInfo = apSdkConfiguretionInfo;
            _apLionKitInfo = apLionAdInfo;

#if APSdk_GameAnalytics
            _apGameAnalyticsInfo = Resources.Load<APGameAnalyticsInfo>("GameAnalytics/APGameAnalyticsInfo");
#endif

            _ShowBannerAdRequest = new ShowAdRequest();

            // Ad event callbacks
            _ShowBannerAdRequest.OnDisplayed += adUnitId =>
            {
                APSdkLogger.Log("Displayed BannerAd :: Ad Unit ID = " + adUnitId);


#if APSdk_GameAnalytics
                GameAnalytics.StartTimer(_adPlacement);
#endif
                IsAdRunning = true;
            };
            _ShowBannerAdRequest.OnClicked += adUnitId =>
            {
                APSdkLogger.Log("Clicked BannerAd :: Ad Unit ID = " + adUnitId);
            };
            _ShowBannerAdRequest.OnHidden += adUnitId =>
            {
                APSdkLogger.Log("Closed BannerAd :: Ad Unit ID = " + adUnitId);

#if APSdk_GameAnalytics
                if (_apSdkConfiguretionInfo.logAnalyticsEvent && _apGameAnalyticsInfo.TrackAdEvent)
                {

                    APGameAnalyticsWrapper.Instance.AdEvent(
                        GAAdAction.Show,
                        GAAdType.Banner,
                        "max_sdk",
                        adUnitId,
                        GameAnalytics.StopTimer(_adPlacement)
                    );
                }
#endif
                IsAdRunning = false;
            };
            _ShowBannerAdRequest.OnFailedToDisplay += (adUnitId, error) =>
            {
                APSdkLogger.LogError("Failed To Display BannerAd :: Error = " + error + " :: Ad Unit ID = " + adUnitId);

#if APSdk_GameAnalytics
                if (_apSdkConfiguretionInfo.logAnalyticsEvent && _apGameAnalyticsInfo.TrackAdEvent)
                {
                    APGameAnalyticsWrapper.Instance.AdEvent(
                        GAAdAction.FailedShow,
                        GAAdType.Banner,
                        "max_sdk",
                        adUnitId,
                        (GAAdError)error
                    );
                }
#endif

                IsAdRunning = false;
            };
        }

        public void ShowBannerAd(string adPlacement = "banner",int playerLevel = 0)
        {
            if (_apLionKitInfo.enableBannerAd)
            {
                _adPlacement = adPlacement;
                _ShowBannerAdRequest.SetLevel(playerLevel);
                Banner.Show(_ShowBannerAdRequest);
            }
            else {
                APSdkLogger.LogError(string.Format("BannerAd is set to disabled in APSDKIntegrationManager. Please set the flag to 'true' to see BannerAd"));
            }
        }

        public void HideBannerAd() {

            Banner.Hide();
        }

#endregion
    }
}

#endif



