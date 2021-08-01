namespace APSdk
{
#if APSdk_LionKit

    using UnityEngine.Events;
    using LionStudios.Ads;

    public class APInterstitialAdOnLionKitAdNetwork : APBaseClassForInterstitialAdForAdNetwork
    {
        #region Private Variables

        private ShowAdRequest _showInterstitialAdRequest;

        #endregion

        #region Public Callback

        public APInterstitialAdOnLionKitAdNetwork(APBaseClassForAdConfiguretion adConfiguretion) {

            _adConfiguretion = adConfiguretion;

            _showInterstitialAdRequest = new ShowAdRequest();

            // Ad event callbacks
            _showInterstitialAdRequest.OnDisplayed += adUnitId =>
            {
                APSdkLogger.Log("Displayed InterstitialAd :: Ad Unit ID = " + adUnitId);

                IsAdRunning = true;


            };
            _showInterstitialAdRequest.OnClicked += adUnitId =>
            {
                APSdkLogger.Log("Clicked InterstitialAd :: Ad Unit ID = " + adUnitId);
            };
            _showInterstitialAdRequest.OnHidden += adUnitId =>
            {
                APSdkLogger.Log("Closed InterstitialAd :: Ad Unit ID = " + adUnitId);

                IsAdRunning = false;
                _OnAdClosed?.Invoke();

            };
            _showInterstitialAdRequest.OnFailedToDisplay += (adUnitId, error) =>
            {
                APSdkLogger.LogError("Failed To Display InterstitialAd :: Error = " + error + " :: Ad Unit ID = " + adUnitId);

                IsAdRunning = false;
                _OnAdFailed?.Invoke();

            };
        }

        public override bool IsInterstitialAdReady()
        {
            return Interstitial.IsAdReady;
        }

        public override void ShowInterstitialAd(string adPlacement = "interstitial", UnityAction OnAdFailed = null, UnityAction OnAdClosed = null)
        {
            if (_adConfiguretion.IsInterstitialAdEnabled)
            {

                _adPlacement = string.IsNullOrEmpty(adPlacement) ? "interstitial" : adPlacement;
                _OnAdClosed = OnAdClosed;
                _OnAdFailed = OnAdFailed;

                Interstitial.Show(_showInterstitialAdRequest);
            }
            else {

                APSdkLogger.LogError(string.Format("InterstitialAd is set to disabled in APSDKIntegrationManager. Please set the flag to 'true' to see InterstitialAd"));
            }
        }

        #endregion


    }

#endif
}

