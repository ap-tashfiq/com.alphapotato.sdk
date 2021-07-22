namespace APSdk
{
    using UnityEngine;

    public abstract class BaseClassForAdConfiguretion : ScriptableObject
    {
        #region Private Variables

#if UNITY_EDITOR
        [SerializeField] protected string _nameOfAdNetwork;
#endif

        [Space(5.0f)]
        [SerializeField] protected bool _isAdSDKIntegrated;

        [Space(5.0f)]
        [SerializeField] private bool _enableRewardedVideoAd;

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


