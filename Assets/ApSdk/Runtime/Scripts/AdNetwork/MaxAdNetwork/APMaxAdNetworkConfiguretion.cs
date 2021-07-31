namespace APSdk
{
    using UnityEngine;
    using UnityEngine.Events;

    //[CreateAssetMenu(fileName = "APMaxAdNetworkConfiguretion", menuName = "APMaxAdNetworkConfiguretion")]
    public class APMaxAdNetworkConfiguretion : APBaseClassForAdConfiguretion
    {
        public override void HideBannerAd()
        {
            throw new System.NotImplementedException();
        }

        public override void HideCrossPromoAd()
        {
            throw new System.NotImplementedException();
        }

        public override void Initialize(APSdkConfiguretionInfo apSdkConfiguretionInfo)
        {
            throw new System.NotImplementedException();
        }

        public override bool IsBannerAdReady()
        {
            throw new System.NotImplementedException();
        }

        public override bool IsCrossPromoAdReady()
        {
            throw new System.NotImplementedException();
        }

        public override bool IsInterstitialAdReady()
        {
            throw new System.NotImplementedException();
        }

        public override bool IsRewardedAdReady()
        {
            throw new System.NotImplementedException();
        }

        public override void SetNameAndIntegrationStatus()
        {
            string sdkName = APSdkConstant.NameOfSDK + "_MaxAdNetwork";
            SetNameOfConfiguretion(sdkName);
#if UNITY_EDITOR
            _isSDKIntegrated = APSdkScriptDefiniedSymbol.CheckMaxAdNetworkIntegrated(sdkName);
#endif
        }

        public override void ShowBannerAd(string adPlacement = "banner", int playerLevel = 0)
        {
            throw new System.NotImplementedException();
        }

        public override void ShowCrossPromoAd(string adPlacement = "crossPromo")
        {
            throw new System.NotImplementedException();
        }

        public override void ShowInterstitialAd(string adPlacement = "interstitial", UnityAction OnAdFailed = null, UnityAction OnAdClosed = null)
        {
            throw new System.NotImplementedException();
        }

        public override void ShowRewardedAd(string adPlacement, UnityAction<bool> OnAdClosed, UnityAction OnAdFailed = null)
        {
            throw new System.NotImplementedException();
        }
    }
}

