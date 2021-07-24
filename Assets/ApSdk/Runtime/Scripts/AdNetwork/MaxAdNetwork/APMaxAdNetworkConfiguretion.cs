namespace APSdk
{
    using UnityEngine;
    using UnityEngine.Events;

    [CreateAssetMenu(fileName = "APMaxAdNetworkConfiguretion", menuName = "APMaxAdNetworkConfiguretion")]
    public class APMaxAdNetworkConfiguretion : BaseClassForAdConfiguretion
    {
        
        public override void Initialize()
        {
#if APSdk_MaxAdNetwork
            APMaxAdNetwork.Initialize(this);
#endif
        }

        public override bool IsBannerAdReady()
        {
#if APSdk_MaxAdNetwork
            return APMaxAdNetwork.BannerAd.IsBannerAdReady();
#endif
        }

        public override void ShowBannerAd(string adPlacement = "banner", int playerLevel = 0)
        {
#if APSdk_MaxAdNetwork
            APMaxAdNetwork.BannerAd.ShowBannerAd(adPlacement, playerLevel);
#endif
        }

        public override void HideBannerAd()
        {
#if APSdk_MaxAdNetwork
            APMaxAdNetwork.BannerAd.HideBannerAd();
#endif
        }

        public override bool IsInterstitialAdReady()
        {
#if APSdk_MaxAdNetwork
            return APMaxAdNetwork.InterstitialAd.IsInterstitialAdReady();
#endif
        }

        public override void ShowInterstitialAd(string adPlacement = "interstitial", UnityAction OnAdFailed = null, UnityAction OnAdClosed = null)
        {
#if APSdk_MaxAdNetwork
            APMaxAdNetwork.InterstitialAd.ShowInterstitialAd(adPlacement, OnAdFailed, OnAdClosed);
#endif
        }

        public override bool IsRewardedAdReady()
        {
#if APSdk_MaxAdNetwork
            return APMaxAdNetwork.RewardedAd.IsRewardedAdReady();
#endif
        }

        public override void ShowRewardedAd(string adPlacement, UnityAction<bool> OnAdClosed, UnityAction OnAdFailed = null)
        {
#if APSdk_MaxAdNetwork
            APMaxAdNetwork.RewardedAd.ShowRewardedAd(adPlacement, OnAdClosed, OnAdFailed);
#endif
        }

        public override void SetNameAndIntegrationStatus()
        {
            SetNameOfConfiguretion(APSdkConstant.APSdk_MaxAdNetwork);
            _isSDKIntegrated = APSdkScriptDefiniedSymbol.CheckMaxAdNetworkIntegrated();
        }
    }
}

