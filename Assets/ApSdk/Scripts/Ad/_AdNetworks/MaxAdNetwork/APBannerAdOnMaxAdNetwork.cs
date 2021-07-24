namespace APSdk
{
#if APSdk_MaxAdNetwork

    using LionStudios.Ads;

    public class APBannerAdOnMaxAdNetwork : BaseClassForBannerAdForAdNetwork
    {
        #region Private Variables

        private ShowAdRequest _ShowBannerAdRequest;

        #endregion

        #region Public Callback

        public APBannerAdOnMaxAdNetwork(BaseClassForAdConfiguretion adConfiguretion)
        {

            _adConfiguretion = adConfiguretion;

            _ShowBannerAdRequest = new ShowAdRequest();

            // Ad event callbacks
            _ShowBannerAdRequest.OnDisplayed += adUnitId =>
            {
                APSdkLogger.Log("Displayed BannerAd : Ad Unit ID = " + adUnitId);

                IsAdRunning = true;
            };
            _ShowBannerAdRequest.OnClicked += adUnitId =>
            {
                APSdkLogger.Log("Clicked BannerAd : Ad Unit ID = " + adUnitId);
            };
            _ShowBannerAdRequest.OnHidden += adUnitId =>
            {
                APSdkLogger.Log("Closed BannerAd : Ad Unit ID = " + adUnitId);

                IsAdRunning = false;
            };
            _ShowBannerAdRequest.OnFailedToDisplay += (adUnitId, error) =>
            {
                APSdkLogger.LogError("Failed To Display BannerAd :: Error = " + error + " :: Ad Unit ID = " + adUnitId);

                IsAdRunning = false;
            };

        }

        public override bool IsBannerAdReady()
        {
            return Banner.IsAdReady;
        }

        public override void HideBannerAd()
        {
            Banner.Hide();
        }

        public override void ShowBannerAd(string adPlacement = "banner", int playerLevel = 0)
        {
            if (_adConfiguretion.IsBannerAdEnabled)
            {

                _adPlacement = adPlacement;
                _ShowBannerAdRequest.SetLevel(playerLevel);
                Banner.Show(_ShowBannerAdRequest);
            }
            else {
                APSdkLogger.LogError(string.Format("BannerAd is set to disabled in APSDKIntegrationManager. Please set the flag to 'true' to see BannerAd"));
            }
        }

        #endregion

    }

#endif

}

