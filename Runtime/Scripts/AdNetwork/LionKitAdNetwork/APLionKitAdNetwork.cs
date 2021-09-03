namespace com.alphapotato.sdk
{
#if APSdk_LionKit

    public static class APLionKitAdNetwork
    {
    #region Public Variables

        public static APRewardedAdOnLionKitAdNetwork RewardedAd { get; private set; }
        public static APInterstitialAdOnLionKitAdNetwork InterstitialAd { get; private set; }
        public static APBannerAdOnLionKitAdNetwork BannerAd { get; private set; }

    #endregion

    #region Public Variables

        public static void Initialize(APBaseClassForAdConfiguretion adConfiguretion) {

            RewardedAd = new APRewardedAdOnLionKitAdNetwork(adConfiguretion);
            InterstitialAd = new APInterstitialAdOnLionKitAdNetwork(adConfiguretion);
            BannerAd = new APBannerAdOnLionKitAdNetwork(adConfiguretion);
        }

    #endregion


    }

#endif
}

