namespace com.alphapotato.sdk
{
#if APSdk_MaxAdNetwork

    using UnityEngine;

    public class APBannerAdOnMaxAdNetwork : APBaseClassForBannerAdForAdNetwork
    {
        #region Public Callback

        public APBannerAdOnMaxAdNetwork(APBaseClassForAdConfiguretion adConfiguretion)
        {
            _adConfiguretion = adConfiguretion;

            MaxSdk.CreateBanner(_adConfiguretion.AdUnitId_BannerAd, MaxSdkBase.BannerPosition.BottomCenter);
            MaxSdk.SetBannerBackgroundColor(_adConfiguretion.AdUnitId_BannerAd, Color.white);
        }

        #endregion

        #region Override Method

        public override void HideBannerAd()
        {
            MaxSdk.HideBanner(_adConfiguretion.AdUnitId_BannerAd);
        }

        public override bool IsBannerAdReady()
        {
            return true;
        }

        public override void ShowBannerAd(string adPlacement = "banner", int playerLevel = 0)
        {
            if (_adConfiguretion.IsBannerAdEnabled)
            {

                _adPlacement = adPlacement;
                MaxSdk.ShowBanner(_adConfiguretion.AdUnitId_BannerAd);
            }
            else
            {
                APSdkLogger.LogError(string.Format("BannerAd is set to disabled in APSDKIntegrationManager. Please set the flag to 'true' to see BannerAd"));
            }
        }

        #endregion
    }
#endif
}

