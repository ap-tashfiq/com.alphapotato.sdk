namespace APSdk
{
    using UnityEngine;

    public abstract class BaseClassForAdConfiguretion : ScriptableObject
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

        [SerializeField] protected string _nameOfAdNetwork;
        
#endif

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
        

#endregion

#region Protected Method
#if UNITY_EDITOR
        protected void SetSDKName(string scriptDefineSymbol) {

            string[] splited = scriptDefineSymbol.Split('_');
            _nameOfAdNetwork = splited[1];
        }
#endif
#endregion
    }


}


