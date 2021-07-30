namespace APSdk
{
#if APSdk_MaxAdNetwork

    using UnityEngine;
    using UnityEngine.Events;

    public class APRewardedAdOnMaxAdNetwork : APBaseClassForRewardedAdForAdNetwork
    {
        #region Public Callback

        public APRewardedAdOnMaxAdNetwork(APBaseClassForAdConfiguretion adConfiguretion) {

        }

        #endregion

        #region Override Method

        public override bool IsRewardedAdReady()
        {
            throw new System.NotImplementedException();
        }

        public override void ShowRewardedAd(string adPlacement, UnityAction<bool> OnAdClosed, UnityAction OnAdFailed = null)
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
#endif
}




