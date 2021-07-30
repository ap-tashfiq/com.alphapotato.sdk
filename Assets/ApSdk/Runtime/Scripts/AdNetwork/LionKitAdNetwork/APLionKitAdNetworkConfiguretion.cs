namespace APSdk
{
    using UnityEngine;
    using UnityEngine.Events;

    [CreateAssetMenu(fileName = "APLionKitAdNetworkConfiguretion", menuName = "APLionKitAdNetworkConfiguretion")]
    public class APLionKitAdNetworkConfiguretion : APBaseClassForAdConfiguretion
    {
        
        public override void Initialize(APSdkConfiguretionInfo apSdkConfiguretionInfo)
        {
#if APSdk_LionKit
            APLionKitAdNetwork.Initialize(this);
#endif
        }

        public override bool IsBannerAdReady()
        {
#if APSdk_LionKit
            return APLionKitAdNetwork.BannerAd.IsBannerAdReady();
#else
            return false;
#endif
        }

        public override void ShowBannerAd(string adPlacement = "banner", int playerLevel = 0)
        {
#if APSdk_LionKit
            APLionKitAdNetwork.BannerAd.ShowBannerAd(adPlacement, playerLevel);
#endif
        }

        public override void HideBannerAd()
        {
#if APSdk_LionKit
            APLionKitAdNetwork.BannerAd.HideBannerAd();
#endif
        }

        public override bool IsInterstitialAdReady()
        {
#if APSdk_LionKit
            return APLionKitAdNetwork.InterstitialAd.IsInterstitialAdReady();
#else
            return false;
#endif
        }

        public override void ShowInterstitialAd(string adPlacement = "interstitial", UnityAction OnAdFailed = null, UnityAction OnAdClosed = null)
        {
#if APSdk_LionKit
            APLionKitAdNetwork.InterstitialAd.ShowInterstitialAd(adPlacement, OnAdFailed, OnAdClosed);
#endif
        }

        public override bool IsRewardedAdReady()
        {
#if APSdk_LionKit
            return APLionKitAdNetwork.RewardedAd.IsRewardedAdReady();
#else
            return false;
#endif
        }

        public override void ShowRewardedAd(string adPlacement, UnityAction<bool> OnAdClosed, UnityAction OnAdFailed = null)
        {
#if APSdk_LionKit
            APLionKitAdNetwork.RewardedAd.ShowRewardedAd(adPlacement, OnAdClosed, OnAdFailed);
#endif
        }

        public override void SetNameAndIntegrationStatus()
        {
            string sdkName = APSdkConstant.APSdk_LionKit;
            SetNameOfConfiguretion(sdkName, "AdNetwork");
#if UNITY_EDITOR
            _isSDKIntegrated = APSdkScriptDefiniedSymbol.CheckLionKitIntegration(sdkName);
#endif
        }

        public override bool IsCrossPromoAdReady()
        {
            throw new System.NotImplementedException();
        }

        public override void ShowCrossPromoAd(string adPlacement = "crossPromo")
        {
            throw new System.NotImplementedException();
        }

        public override void HideCrossPromoAd()
        {
            throw new System.NotImplementedException();
        }
    }
}

