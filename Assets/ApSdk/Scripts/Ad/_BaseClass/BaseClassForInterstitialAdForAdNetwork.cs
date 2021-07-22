namespace APSdk
{
    using UnityEngine.Events;

    public abstract class BaseClassForInterstitialAdForAdNetwork
    {
        #region Abstract Method

        public abstract void ShowInterstitialAd(
            string eventName,
            string paramName,
            object paramValue,
            string adPlacement = "interstitial",
            UnityAction OnAdFailed = null,
            UnityAction OnAdClosed = null);

        public abstract void ShowInterstitialAd(
            string adPlacement = "interstitial",
            UnityAction OnAdFailed = null,
            UnityAction OnAdClosed = null);

        #endregion
    }
}


