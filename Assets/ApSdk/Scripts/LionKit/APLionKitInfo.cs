#if APSdk_LionKit

namespace APSdk
{
    using UnityEngine;

    //[CreateAssetMenu(fileName = "APLionKitInfo", menuName = APSdkConstant.NameOfSDK + "/APLionKitInfo")]
    public class APLionKitInfo : ScriptableObject
    {
#if UNITY_EDITOR

        [SerializeField, HideInInspector] private bool _showRewardedAdSettings;
        [SerializeField, HideInInspector] private bool _showInterstitialAdSettings;
        [SerializeField, HideInInspector] private bool _showBannerAdSettings;
        [SerializeField, HideInInspector] private bool _showCrossPromoAdSettings;
#endif

#region Public Variables    :   RewardedAd

        public bool enableRewardedAd;
        
#endregion

#region Public Variables    :   InterstitialAd

        public bool enableInterstitialAd;

#endregion

#region Public Variables    :   BannerAd

        public bool enableBannerAd;
        public bool startBannerAdManually = false;

#endregion

#region Public Variables    :   CrossPromoAd

        public bool enableCrossPromoAd;

#endregion
    }
}


#endif


