namespace APSdk
{
#if APSdk_MaxAdNetwork

    using UnityEngine.Events;
    using LionStudios.Ads;

    public class APRewardedAdOnMaxAdNetwork : BaseClassForRewardedAdForAdNetwork
    {

        #region Private Variables

        private ShowAdRequest               _showRewardedAdRequest;

        #endregion

        #region Configuretion

        #endregion

        #region Public Callback

        public APRewardedAdOnMaxAdNetwork(BaseClassForAdConfiguretion adConfiguretion) {

            _adConfiguretion = adConfiguretion;

            _showRewardedAdRequest = new ShowAdRequest();

            // Ad event callbacks
            _showRewardedAdRequest.OnDisplayed += adUnitId =>
            {
                APSdkLogger.Log("Displayed Rewarded Ad :: Ad Unit ID = " + adUnitId);

                _isEligibleForReward = false;
                IsAdRunning = true;


            };
            _showRewardedAdRequest.OnClicked += adUnitId =>
            {
                APSdkLogger.Log("Clicked Rewarded Ad :: Ad Unit ID = " + adUnitId);
            };
            _showRewardedAdRequest.OnHidden += adUnitId =>
            {
                APSdkLogger.Log("Closed Rewarded Ad :: Ad Unit ID = " + adUnitId);

                IsAdRunning = false;
                _OnAdClosed?.Invoke(_isEligibleForReward);
            };
            _showRewardedAdRequest.OnFailedToDisplay += (adUnitId, error) =>
            {
                APSdkLogger.LogError("Failed To Display Rewarded Ad :: Error = " + error + " :: Ad Unit ID = " + adUnitId);

                IsAdRunning = false;
                _OnAdFailed?.Invoke();
            };
            _showRewardedAdRequest.OnReceivedReward += (adUnitId, reward) =>
            {
                APSdkLogger.Log("Received Reward :: Reward = " + reward + " :: Ad Unit ID = " + adUnitId);
                _isEligibleForReward = true;


            };
        }

        public override bool IsRewardedAdReady()
        {
            return LionStudios.Ads.RewardedAd.IsAdReady;
        }

        public override void ShowRewardedAd(string adPlacement, UnityAction<bool> OnAdClosed, UnityAction OnAdFailed = null)
        {
            if (_adConfiguretion.IsRewardedAdEnabled)
            {
                _adPlacement = string.IsNullOrEmpty(adPlacement) ? "rewarded_video" : adPlacement;
                _OnAdClosed = OnAdClosed;
                _OnAdFailed = OnAdFailed;

                LionStudios.Ads.RewardedAd.Show(_showRewardedAdRequest);
            }
            else
            {
                APSdkLogger.LogError(string.Format("RewardedAd is set to disabled in APSDKIntegrationManager. Please set the flag to 'true' to see RewardedAd"));
            }
        }

        


        #endregion


    }

#endif


}


