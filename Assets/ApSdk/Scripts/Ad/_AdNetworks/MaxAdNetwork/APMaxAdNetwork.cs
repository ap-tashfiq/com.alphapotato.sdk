namespace APSdk
{
#if APSdk_MaxAdNetwork

    public static class APMaxAdNetwork
    {
        #region Public Variables

        public static APRewardedAdOnMaxAdNetwork RewardedAd { get; private set; }
        public static APInterstitialAdOnMaxAdNetwork InterstitialAd { get; private set; }
        public static APBannerAdOnMaxAdNetwork BannerAd { get; private set; }

        #endregion

        #region Public Variables

        public static void Initialize(BaseClassForAdConfiguretion adConfiguretion) {

            RewardedAd = new APRewardedAdOnMaxAdNetwork(adConfiguretion);
            InterstitialAd = new APInterstitialAdOnMaxAdNetwork(adConfiguretion);
            BannerAd = new APBannerAdOnMaxAdNetwork(adConfiguretion);
        }

        #endregion


    }

#endif
}

