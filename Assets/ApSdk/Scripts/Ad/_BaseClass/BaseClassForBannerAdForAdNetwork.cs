namespace APSdk
{
    public abstract class BaseClassForBannerAdForAdNetwork
    {
        #region Abstract Method

        public abstract void ShowBannerAd(string adPlacement = "banner", int playerLevel = 0);
        public abstract void HideBannerAd();

        #endregion
    }
}

