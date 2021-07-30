namespace APSdk
{
#if APSdk_MaxAdNetwork

    using UnityEngine;
    using UnityEngine.Events;

    public class APInterstitialAdOnMaxAdNetwork : APBaseClassForInterstitialAdForAdNetwork
    {

        #region Public Callback

        public APInterstitialAdOnMaxAdNetwork(APBaseClassForAdConfiguretion adConfiguretion)
        {

        }

        #endregion

        #region Override Method

        public override bool IsInterstitialAdReady()
        {
            throw new System.NotImplementedException();
        }

        public override void ShowInterstitialAd(string adPlacement = "interstitial", UnityAction OnAdFailed = null, UnityAction OnAdClosed = null)
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
#endif
}

