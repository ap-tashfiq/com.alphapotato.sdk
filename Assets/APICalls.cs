//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using APSdk;

//public class APICalls : MonoBehaviour
//{
//    // Start is called before the first frame update
//    void Start()
//    {
//        int levelIndex = 0;

//        APAnalytics.LevelStarted(levelIndex);

//        APAnalytics.LevelComplete(levelIndex);

//        APAnalytics.LevelFailed(levelIndex);
//    }

//    // Update is called once per frame
//    void Update()
//    {
        
//    }
//}

//using APSdk;
//public static class AnalyticsCall
//{
//    public static void LogLevelStarted(int levelIndex = 0) {

//        APAnalytics.LevelStarted(levelIndex);
//    }

//    public static void LogLevelComplete(int levelIndex = 0)
//    {
//        APAnalytics.LevelComplete(levelIndex);
//    }

//    public static void LogLevelFailed(int levelIndex = 0)
//    {
//        APAnalytics.LevelFailed(levelIndex);
//    }
//}

using APSdk;
using UnityEngine.Events;
public static class AdNetworkCall
{
    //Ad    :   RewardedAd
    public static bool IsRewardedAdReady()
    {
        return APRewardedAd.IsAdReady();
    }

    /// <param name="adPlacement">In 'adPlacement', you need to pass the info, for which the Ad has been shown. If it was for revive, then maybe you can pass "rewadedAd_revive"</param>
    /// <param name="OnAdClosed">It will pass "true" if the user is eligible for reward, else it will pass "false" </param>
    /// <param name="OnAdFailed">[Optional] if somehow, it shown was failed (Network error, ads not ready etc....)</param>
    public static void ShowRewardedAd(string adPlacement, UnityAction<bool> OnAdClosed, UnityAction OnAdFailed = null)
    {
        APRewardedAd.Show(adPlacement, OnAdClosed, OnAdFailed);
    }


    //----------
    //Ad    :   InterstitialAd
    public static bool IsInterstitialAdReady()
    {
        return APInterstitialAd.IsAdReady();
    }

    /// <param name="adPlacement">[Optional] For 'adPlacement', you can pass the information where the Ad's been shown. If it was shown after the level failed, you can pass 'interstitialAd_levelFailed'</param>
    /// <param name="OnAdClosed">[Optional] When user press the close button</param>
    /// <param name="OnAdFailed">[Optional] if somehow, it shown was failed (Network error, ads not ready etc....)</param>
    public static void ShowInterstitialAd(string adPlacement = "InterstitialAd", UnityAction OnAdClosed = null, UnityAction OnAdFailed = null)
    {
        APInterstitialAd.Show(adPlacement, OnAdClosed, OnAdFailed);
    }


    //----------
    //Ad    :   BannerAd
    public static bool IsBannerAdReady()
    {
        return APBannerAd.IsAdReady();
    }

    
    /// <param name="adPlacement"></param>
    /// <param name="playerLevel"></param>
    public static void ShowBannerAd(string adPlacement = "bannerAd", int playerLevel = 0)
    {
        APBannerAd.Show();
    }

    public static void HideBannerAd()
    {
        APBannerAd.Hide();
    }
}


