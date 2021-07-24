namespace APSdk
{
    using UnityEngine;
    using UnityEngine.Events;

    public abstract class BaseClassForAdConfiguretion : ScriptableObject
    {
        #region Public Variables

        public string NameOfAdNetwork { get { return _nameOfAdNetwork; } }

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

        [SerializeField] protected string _nameOfAdNetwork;

        [Space(5.0f)]
        [SerializeField] protected bool _isAdSDKIntegrated;

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
#if UNITY_EDITOR
        public abstract void SetSDKNameAndIntegrationStatus();
#endif
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

        #region Protected Method
#if UNITY_EDITOR

        /// <summary>
        /// Editor Only
        /// </summary>
        /// <param name="scriptDefineSymbol"></param>
        protected void SetSDKName(string scriptDefineSymbol) {

            string[] splited = scriptDefineSymbol.Split('_');
            _nameOfAdNetwork = splited[1];
        }
#endif
        #endregion
    }


}


