﻿namespace APSdk
{
    using UnityEngine.Events;

    public abstract class BaseClassForInterstitialAdForAdNetwork
    {
        #region Public Variables

        public bool IsAdRunning { get; protected set; }

        #endregion

        #region Protected Variables

        protected BaseClassForAdConfiguretion _adConfiguretion;

        protected string _adPlacement;

        protected UnityAction _OnAdFailed;
        protected UnityAction _OnAdClosed;

        #endregion

        #region Abstract Method

        public abstract bool IsInterstitialAdReady();

        public abstract void ShowInterstitialAd(
            string adPlacement = "interstitial",
            UnityAction OnAdFailed = null,
            UnityAction OnAdClosed = null);

        #endregion
    }
}


