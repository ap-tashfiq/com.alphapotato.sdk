namespace APSdk
{
#if APSdk_MaxAdNetwork

    using UnityEngine.Events;
    using LionStudios.Ads;

    public class APRewardedAdOnMaxAdNetwork : BaseClassForRewardedAdForAdNetwork
    {
        #region Public Variables

        public bool IsRewardedAdReady { get { return RewardedAd.IsAdReady; } }
        public bool IsAdRunning { get; private set; }

        #endregion

        #region Private Variables

        private BaseClassForAdConfiguretion _adConfiguretion;

        private bool                        _isEligibleForReward = false;

        private string                      _adPlacement;

        private ShowAdRequest               _ShowRewardedAdRequest;

        private UnityAction                 _OnAdFailed;
        private UnityAction<bool>           _OnAdClosed;

        #endregion

        #region Configuretion

        #endregion

        #region Public Callback

        public APRewardedAdOnMaxAdNetwork(APSdkConfiguretionInfo apSdkConfiguretionInfo, BaseClassForAdConfiguretion adConfiguretion) {

            _ShowRewardedAdRequest = new ShowAdRequest();

            // Ad event callbacks
            _ShowRewardedAdRequest.OnDisplayed += adUnitId =>
            {
                APSdkLogger.Log("Displayed Rewarded Ad :: Ad Unit ID = " + adUnitId);

                _isEligibleForReward = false;
                IsAdRunning = true;


            };
            _ShowRewardedAdRequest.OnClicked += adUnitId =>
            {
                APSdkLogger.Log("Clicked Rewarded Ad :: Ad Unit ID = " + adUnitId);
            };
            _ShowRewardedAdRequest.OnHidden += adUnitId =>
            {
                APSdkLogger.Log("Closed Rewarded Ad :: Ad Unit ID = " + adUnitId);

                IsAdRunning = false;
                _OnAdClosed?.Invoke(_isEligibleForReward);
            };
            _ShowRewardedAdRequest.OnFailedToDisplay += (adUnitId, error) =>
            {
                APSdkLogger.LogError("Failed To Display Rewarded Ad :: Error = " + error + " :: Ad Unit ID = " + adUnitId);

                IsAdRunning = false;
                _OnAdFailed?.Invoke();
            };
            _ShowRewardedAdRequest.OnReceivedReward += (adUnitId, reward) =>
            {
                APSdkLogger.Log("Received Reward :: Reward = " + reward + " :: Ad Unit ID = " + adUnitId);
                _isEligibleForReward = true;


            };
        }

        public override void ShowRewardedAd(string adPlacement, UnityAction<bool> OnAdClosed, UnityAction OnAdFailed = null)
        {
            if (_adConfiguretion.IsRewardedAdEnabled)
            {
                _adPlacement = string.IsNullOrEmpty(adPlacement) ? "rewarded_video" : adPlacement;
                _OnAdClosed = OnAdClosed;
                _OnAdFailed = OnAdFailed;

                RewardedAd.Show(_ShowRewardedAdRequest);
            }
            else
            {
                APSdkLogger.LogError(string.Format("RewardedAd is set to disabled in APSDKIntegrationManager. Please set the flag to 'true' to see RewardedAD"));
            }
        }


        #endregion


    }

#endif


}


