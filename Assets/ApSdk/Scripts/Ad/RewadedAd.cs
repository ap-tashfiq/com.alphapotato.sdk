namespace APSdk
{
    using UnityEngine;
    using UnityEngine.Events;

    public static class RewardedAd
    {
        #region Private Variables

        private static APSdkConfiguretionInfo _apSdkConfiguretionInfo;

        private static UnityAction _OnAdFailed;
        private static UnityAction<bool> _OnAdClosed;

        #endregion

        #region Configuretion

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void OnGameStart()
        {

            _apSdkConfiguretionInfo = Resources.Load<APSdkConfiguretionInfo>("APSdkConfiguretionInfo");
        }

        #endregion

        #region Public Callback


        public static void ShowRewardedAd(
                string eventName,
                string paramName,
                object paramValue,
                string adPlacement,
                UnityAction<bool> OnAdClosed,
                UnityAction OnAdFailed = null)
        {

            if (_apSdkConfiguretionInfo.indexOfActiveAdConfiguretion == -1) {
                APSdkLogger.LogError("No AdNetwork Selected");
                return;
            }

            _OnAdClosed = OnAdClosed;
            _OnAdFailed = OnAdFailed;
        }

        public static void ShowRewardedAd(
            string adPlacement,
            UnityAction<bool> OnAdClosed,
            UnityAction OnAdFailed = null)
        {
            if (_apSdkConfiguretionInfo.indexOfActiveAdConfiguretion == -1)
            {
                APSdkLogger.LogError("No AdNetwork Selected");
                return;
            }

            _OnAdClosed = OnAdClosed;
            _OnAdFailed = OnAdFailed;
        }

        #endregion
    }

}
