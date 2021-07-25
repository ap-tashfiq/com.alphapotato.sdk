namespace APSdk
{
    using System.Collections.Generic;
    using System.Collections;
    using UnityEngine;

#if APSdk_LionKit
    using LionStudios;
#endif

    [DefaultExecutionOrder(APSdkConstant.EXECUTION_ORDER_LionKitWrapper)]
    public class APLionKitWrapper : MonoBehaviour
    {
#region Public Variables

        public static APLionKitWrapper Instance { get; private set; }
        public static APSdkAnalytics Analytics { get; private set; }


#if APSdk_LionKit


        public static APLionKitRewardedAdController RewardedAd { get; private set; }
        public static APLionKitInterstitialAdController InterstitialAd { get; private set; } 
        public static APLionKitBannerAdController BannerAd { get; private set; }

#endif



#endregion

#region Private Variables

        private static bool _IsMaxMediationDebuggerRequested = false;

#endregion

#region Mono Behaviour

        private void Awake()
        {
            StartCoroutine(ShowMaxMediationDebugger());   
        }

#endregion

#region Configuretion

        private IEnumerator ShowMaxMediationDebugger() {

            yield return new WaitForSeconds(1f);

            if (Instance != this)
            {
                Destroy(gameObject);
            }
            else {

                if (!_IsMaxMediationDebuggerRequested)
                {
                    APSdkConfiguretionInfo apSdkConfiguretionInfo = Resources.Load<APSdkConfiguretionInfo>("APSdkConfiguretionInfo");

                    if (apSdkConfiguretionInfo.maxMediationDebugger)
                    {
#if APSdk_LionKit
                        MaxSdk.ShowMediationDebugger();
#endif
                        APSdkLogger.Log("Showing Mediation Debugger");
                    }

                    _IsMaxMediationDebuggerRequested = true;
                }
            }
        }

#if APSdk_LionKit

public static void LogLionGameEvent(string prefix, LionGameEvent gameEvent) {

            string logEvent = " [" +  prefix + "]";
            logEvent += " [EventName] : " + gameEvent.eventName + "";
            logEvent += " [EventParams]\n";

            List<string> keyList = new List<string>();
            List<string> valueList = new List<string>();

            Dictionary<string, object>.KeyCollection keys       = gameEvent.eventParams.Keys;
            Dictionary<string, object>.ValueCollection values   = gameEvent.eventParams.Values;

            foreach (object key in keys) {
                keyList.Add(key.ToString());
            }

            foreach (object value in values) {
                valueList.Add(value.ToString());
            }


            int numberOfEventParams = keyList.Count;
            
            for (int i = 0; i < numberOfEventParams; i++)
            {
                logEvent += keyList[i] + " = " + valueList[i] + ((i != (numberOfEventParams - 1)) ? " __ " : "") ;
            }

            APSdkLogger.Log(logEvent);
        }

#endif



        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void OnGameStart()
        {
            if (Instance == null)
            {
                GameObject newAPLionKitWrapper = new GameObject("APLionKitWrapper");
                Instance = newAPLionKitWrapper.AddComponent<APLionKitWrapper>();

                DontDestroyOnLoad(newAPLionKitWrapper);

                APSdkConfiguretionInfo apSdkConfiguretionInfo = Resources.Load<APSdkConfiguretionInfo>("APSdkConfiguretionInfo");



#if APSdk_LionKit

LionKit.OnInitialized += () => {

#if UNITY_IOS || UNITY_IPHONE
                    if (MaxSdkUtils.CompareVersions(UnityEngine.iOS.Device.systemVersion, "14.5") != MaxSdkUtils.VersionComparisonResult.Lesser)
                    {
                        APSdkLogger.Log("iOS 14.5+ detected!! SetAdvertiserTrackingEnabled = true");
#if APSdk_Facebook
        Facebook.Unity.FB.Mobile.SetAdvertiserTrackingEnabled(true);
#endif
                    }
                    else
                    {
                        APSdkLogger.Log("iOS <14.5 detected!! Normal Mode");
                    }
#endif

                    
                    APLionKitInfo apLionKitInfo = Resources.Load<APLionKitInfo>("LionKit/APLionKitInfo");

                    if (RewardedAd == null) {
                        RewardedAd = new GameObject("APLionKitRewardedAdController").AddComponent<APLionKitRewardedAdController>();
                        RewardedAd.Initialization(apSdkConfiguretionInfo, apLionKitInfo);
                    }

                    if (InterstitialAd == null) {
                        InterstitialAd = new GameObject("APLionKitInterstitialAdController").AddComponent<APLionKitInterstitialAdController>();
                        InterstitialAd.Initialization(apSdkConfiguretionInfo, apLionKitInfo);
                    }

                    if (BannerAd == null) {
                        BannerAd = new GameObject("APLionKitBannerAdController").AddComponent<APLionKitBannerAdController>();
                        BannerAd.Initialization(apSdkConfiguretionInfo, apLionKitInfo);
                    }
                    

                    APSdkLogger.Log("LionKit Initialized");

#if APSdk_Facebook

                APFacebookWrapper.Instance.Initialize(apSdkConfiguretionInfo, ()=> {

                    APFacebookInfo apFacebookInfo = Resources.Load<APFacebookInfo>("Facebook/APFacebookInfo");
                    if (apFacebookInfo.IsSubscribedToLionEvent)
                    {

                        LionStudios.Analytics.OnLogEvent += (gameEvent) =>
                        {
                            LogLionGameEvent("Facebook", gameEvent);
                            APFacebookWrapper.Instance.LogEvent(
                                    gameEvent.eventName,
                                    gameEvent.eventParams
                                );
                        };
                    }

                    if (apFacebookInfo.IsSubscribedToLionEventUA)
                    {

                        LionStudios.Analytics.OnLogEventUA += (gameEvent) =>
                        {
                            LogLionGameEvent("FacebookUA", gameEvent);
                            APFacebookWrapper.Instance.LogEvent(
                                    gameEvent.eventName,
                                    gameEvent.eventParams
                                );
                        };
                    }
                });
                

#endif

#if APSdk_Adjust

                    // do adjust init
                    APAdjustWrapper.Instance.Initialize(apSdkConfiguretionInfo);
                    APAdjustInfo aPAdjustInfo = Resources.Load<APAdjustInfo>("Adjust/APAdjustInfo");
                    if (aPAdjustInfo.IsSubscribedToLionEvent) {

                        LionStudios.Analytics.OnLogEvent += (gameEvent) =>
                        {
                            LogLionGameEvent("Adjust", gameEvent);
                            APAdjustWrapper.Instance.LogEvent(
                                    gameEvent.eventName,
                                    gameEvent.eventParams
                                );
                        };
                    }

                    if (aPAdjustInfo.IsSubscribedToLionEventUA) {

                        LionStudios.Analytics.OnLogEventUA += (gameEvent) =>
                        {
                            LogLionGameEvent("Adjust|UA", gameEvent);
                            APAdjustWrapper.Instance.LogEvent(
                                    gameEvent.eventName,
                                    gameEvent.eventParams
                                );
                        };
                    }

#endif

#if APSdk_Firebase
                
                APFirebaseInfo apFirebaseInfo = Resources.Load<APFirebaseInfo>("Firebase/APFirebaseInfo");
                APFirebaseWrapper.Instance.Initialize(apSdkConfiguretionInfo, ()=> {

                    if (apFirebaseInfo.IsFirebaseAnalyticsEventEnabled && apFirebaseInfo.IsSubscribedToLionEvent) {

                        LionStudios.Analytics.OnLogEvent += (gameEvent) =>
                        {
                            List<Firebase.Analytics.Parameter> parameters = new List<Firebase.Analytics.Parameter>();

                            List<string> keyList = new List<string>();
                            List<string> valueList = new List<string>();

                            Dictionary<string, object>.KeyCollection keys = gameEvent.eventParams.Keys;
                            Dictionary<string, object>.ValueCollection values = gameEvent.eventParams.Values;

                            foreach (object key in keys)
                            {
                                keyList.Add(key.ToString());
                            }

                            foreach (object value in values)
                            {
                                valueList.Add(value.ToString());
                            }

                            int numberOfEventParams = keyList.Count;

                            for (int i = 0; i < numberOfEventParams; i++) {
                                parameters.Add(new Firebase.Analytics.Parameter(keyList[i], valueList[i]));
                            }

                            APFirebaseWrapper.Instance.LogFirebaseEvent(gameEvent.eventName, parameters);
                        };
                    }

                    if (apFirebaseInfo.IsFirebaseAnalyticsEventEnabled && apFirebaseInfo.IsSubscribedToLionEventUA)
                    {

                        LionStudios.Analytics.OnLogEventUA += (gameEvent) =>
                        {
                            List<Firebase.Analytics.Parameter> parameters = new List<Firebase.Analytics.Parameter>();

                            List<string> keyList = new List<string>();
                            List<string> valueList = new List<string>();

                            Dictionary<string, object>.KeyCollection keys = gameEvent.eventParams.Keys;
                            Dictionary<string, object>.ValueCollection values = gameEvent.eventParams.Values;

                            foreach (object key in keys)
                            {
                                keyList.Add(key.ToString());
                            }

                            foreach (object value in values)
                            {
                                valueList.Add(value.ToString());
                            }

                            int numberOfEventParams = keyList.Count;

                            for (int i = 0; i < numberOfEventParams; i++)
                            {
                                parameters.Add(new Firebase.Analytics.Parameter(keyList[i], valueList[i]));
                            }

                            APFirebaseWrapper.Instance.LogFirebaseEvent(gameEvent.eventName, parameters);
                        };
                    }
                });

#endif

#if APSdk_GameAnalytics

                    APGameAnalyticsWrapper.Instance.Initialize(apSdkConfiguretionInfo);

#endif


    if (!apLionKitInfo.startBannerAdManually && apLionKitInfo.enableBannerAd) {
                        BannerAd.ShowBannerAd();
                    }
                };


#else
                // if LionKit not integrated

#if APSdk_Facebook
                APFacebookWrapper.Instance.Initialize(apSdkConfiguretionInfo);
#endif

#if APSdk_Adjust
                APAdjustWrapper.Instance.Initialize(apSdkConfiguretionInfo);
#endif

#if APSdk_GameAnalytics
                APGameAnalyticsWrapper.Instance.Initialize(apSdkConfiguretionInfo);
#endif

#if APSdk_Firebase
                APFirebaseWrapper.Instance.Initialize(apSdkConfiguretionInfo);
#endif

#endif

                Analytics = new APSdkAnalytics(apSdkConfiguretionInfo);

            }
        }


#endregion

    }
}


