
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

    public class APLionKitRewardedAdController  :   MonoBehaviour
    {
        //Approch2  :   LionKit

        #region Public Variables

        public static APLionKitRewardedAdController Instance;

        public bool IsRewardedAdReady { get { return RewardedAd.IsAdReady; } }
        public bool IsAdRunning { get; private set; }

        #endregion

        #region Private Variables


        private APSdkConfiguretionInfo _apSdkConfiguretionInfo;
        private APLionKitInfo           _apLionKitInfo;
#if APSdk_GameAnalytics
        private APGameAnalyticsInfo _apGameAnalyticsInfo;
#endif

        private ShowAdRequest _ShowRewardedAdRequest;
        
        
        private bool _isEligibleForReward = false;

        private string _adPlacement;

        private UnityAction _OnCallingLionKitAnalytics;
        private UnityAction _OnAdFailed;
        private UnityAction<bool> _OnAdClosed;

        #endregion

        #region Mono Behaviour

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this);
            }
            else {

                Destroy(gameObject);
            }
        }

#if APSdk_GameAnalytics

        private void OnApplicationPause(bool pause)
        {
            if (_apSdkConfiguretionInfo.logAnalyticsEvent
            && _apGameAnalyticsInfo.TrackAdEvent) {

                if (IsAdRunning) {

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

        private void ShowAd() {

           
            RewardedAd.Show(_ShowRewardedAdRequest);
        }

#endregion

#region Public Callback

        public void Initialization(APSdkConfiguretionInfo apSdkConfiguretionInfo, APLionKitInfo apLionAdInfo) {

            _apSdkConfiguretionInfo = apSdkConfiguretionInfo;
            _apLionKitInfo          = apLionAdInfo;
            

#if APSdk_GameAnalytics
            _apGameAnalyticsInfo = Resources.Load<APGameAnalyticsInfo>("GameAnalytics/APGameAnalyticsInfo");
#endif

            _ShowRewardedAdRequest = new ShowAdRequest();

            // Ad event callbacks
            _ShowRewardedAdRequest.OnDisplayed += adUnitId =>
            {
                APSdkLogger.Log("Displayed Rewarded Ad :: Ad Unit ID = " + adUnitId);

                _isEligibleForReward = false;
                

#if APSdk_GameAnalytics
                GameAnalytics.StartTimer(_adPlacement);
#endif

                IsAdRunning = true;

                
            };
            _ShowRewardedAdRequest.OnClicked += adUnitId =>
            {
                APSdkLogger.Log("Clicked Rewarded Ad :: Ad Unit ID = " + adUnitId);
            };
            _ShowRewardedAdRequest.OnHidden += adUnitId =>
            {
                APSdkLogger.Log("Closed Rewarded Ad :: Ad Unit ID = " + adUnitId);
                if (_isEligibleForReward) {

                    _OnCallingLionKitAnalytics?.Invoke();

                }

#if APSdk_GameAnalytics
                if (_apSdkConfiguretionInfo.logAnalyticsEvent && _apGameAnalyticsInfo.TrackAdEvent) {

                    APGameAnalyticsWrapper.Instance.AdEvent(
                        _isEligibleForReward ? GAAdAction.RewardReceived : GAAdAction.Show,
                        GAAdType.RewardedVideo,
                        "max_sdk",
                        _adPlacement,
                        GameAnalytics.StopTimer(_adPlacement)
                    );
                }
#endif
                IsAdRunning = false;
                _OnAdClosed?.Invoke(_isEligibleForReward);
            };
            _ShowRewardedAdRequest.OnFailedToDisplay += (adUnitId, error) =>
            {
                APSdkLogger.LogError("Failed To Display Rewarded Ad :: Error = " + error + " :: Ad Unit ID = " + adUnitId);

#if APSdk_GameAnalytics
                if (_apSdkConfiguretionInfo.logAnalyticsEvent && _apGameAnalyticsInfo.TrackAdEvent)
                {
                    APGameAnalyticsWrapper.Instance.AdEvent(
                        GAAdAction.FailedShow,
                        GAAdType.RewardedVideo,
                        "max_sdk",
                        _adPlacement,
                        (GAAdError) error
                    );
                }
#endif

                IsAdRunning = false;
                _OnAdFailed?.Invoke();
            };
            _ShowRewardedAdRequest.OnReceivedReward += (adUnitId, reward) =>
            {
                APSdkLogger.Log("Received Reward :: Reward = " + reward + " :: Ad Unit ID = " + adUnitId);
                _isEligibleForReward = true;

                
            };
        }

        public void ShowRewardedAd(
            string eventName,
            string paramName,
            object paramValue,
            string adPlacement,
            UnityAction<bool> OnAdClosed,
            UnityAction OnAdFailed = null)
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

                _adPlacement = string.IsNullOrEmpty(adPlacement) ? "rewarded_video" : adPlacement; ;
                _OnAdClosed = OnAdClosed;
                _OnAdFailed = OnAdFailed;

                ShowAd();
            }
            else {
                APSdkLogger.LogError(string.Format("RewardedAd is set to disabled in APSDKIntegrationManager. Please set the flag to 'true' to see RewardedAD"));
            }
        }

        public void ShowRewardedAd(
            string adPlacement,
            UnityAction<bool> OnAdClosed,
            UnityAction OnAdFailed = null) {


            if (_apLionKitInfo.enableRewardedAd) {

                _OnCallingLionKitAnalytics = null;

                _adPlacement = string.IsNullOrEmpty(adPlacement) ? "rewarded_video" : adPlacement;
                _OnAdClosed = OnAdClosed;
                _OnAdFailed = OnAdFailed;

                ShowAd();
            }
            else
            {
                APSdkLogger.LogError(string.Format("RewardedAd is set to disabled in APSDKIntegrationManager. Please set the flag to 'true' to see RewardedAD"));
            }
        }

#endregion

    }
}
#endif





