namespace APSdk
{
#if APSdk_MaxAdNetwork

    using UnityEngine;

    public class APBannerAdOnMaxAdNetwork : APBaseClassForBannerAdForAdNetwork
    {
        #region Public Callback

        public APBannerAdOnMaxAdNetwork(APBaseClassForAdConfiguretion adConfiguretion)
        {

        }

        #endregion

        #region Override Method

        public override void HideBannerAd()
        {
            throw new System.NotImplementedException();
        }

        public override bool IsBannerAdReady()
        {
            throw new System.NotImplementedException();
        }

        public override void ShowBannerAd(string adPlacement = "banner", int playerLevel = 0)
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
#endif
}

