namespace APSdk
{
    using UnityEngine;
    using UnityEngine.Events;

    public abstract class APBaseClassForAdConfiguretion : APBaseClassForConfiguretion
    {
        #region Public Variables

        public bool IsRewardedAdEnabled { get { return _enableRewardedAd; } }
        public bool IsInterstitialAdEnabled { get { return _enableInterstitialAd; } }
        public bool IsBannerAdEnabled { get { return _enableBannerAd; } }
        public bool IsCrossPromoAdEnabled { get { return _enableCrossPromoAd; } }

        #endregion

        #region Private Variables

#if UNITY_EDITOR
        
        [SerializeField] private bool _showRewardedAdSettings;
        [SerializeField] private bool _showInterstitialAdSettings;
        [SerializeField] private bool _showBannerAdSettings;
        [SerializeField] private bool _showCrossPromoAdSettings;
#endif



        [Space(5.0f)]
        [SerializeField] private bool _enableRewardedAd;

        [Space(5.0f)]
        [SerializeField] private bool _enableInterstitialAd;

        [Space(5.0f)]
        [SerializeField] private bool _enableBannerAd;
        [SerializeField] private bool _showBannerAdManually;

        [Space(5.0f)]
        [SerializeField] private bool _enableCrossPromoAd;

        #endregion

        #region Abstract Method

        public abstract bool IsRewardedAdReady();
        public abstract void ShowRewardedAd(
            string adPlacement,
            UnityAction<bool> OnAdClosed,
            UnityAction OnAdFailed = null);

        public abstract bool IsInterstitialAdReady();
        public abstract void ShowInterstitialAd(
            string adPlacement = "interstitial",
            UnityAction OnAdFailed = null,
            UnityAction OnAdClosed = null);

        public abstract bool IsBannerAdReady();
        public abstract void ShowBannerAd(string adPlacement = "banner", int playerLevel = 0);
        public abstract void HideBannerAd();

        public abstract bool IsCrossPromoAdReady();
        public abstract void ShowCrossPromoAd(string adPlacement = "crossPromo");
        public abstract void HideCrossPromoAd();

        #endregion

    
    }


}


