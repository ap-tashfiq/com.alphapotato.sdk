namespace APSdk
{
    using UnityEngine.Events;

    public abstract class BaseClassForRewardedAdForAdNetwork
    {
        #region Abstract Method


        public abstract void ShowRewardedAd(
            string adPlacement,
            UnityAction<bool> OnAdClosed,
            UnityAction OnAdFailed = null);

        #endregion

    }
}


