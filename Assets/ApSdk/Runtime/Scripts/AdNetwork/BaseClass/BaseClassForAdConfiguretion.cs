namespace APSdk
{
    using UnityEngine;
    using UnityEngine.Events;

    public abstract class BaseClassForAdConfiguretion : APBaseClassForConfiguretion
    {
        #region Public Variables

        public bool IsRewardedAdEnabled { get { return _enableRewardedAd; } }
        public bool IsInterstitialAdEnabled { get { return _enableInterstitialAd; } }
        public bool IsBannerAdEnabled { get { return _enableBannerAd; } }

        #endregion

        #region Private Variables

#if UNITY_EDITOR
        [SerializeField] private bool _showSettings;

        [SerializeField] private bool _showRewardedAdSettings;
        [SerializeField] private bool _showInterstitialAdSettings;
        [SerializeField] private bool _showBannerAdSettings;

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

        public abstract void Initialize();

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


        #endregion

    
    }


}


