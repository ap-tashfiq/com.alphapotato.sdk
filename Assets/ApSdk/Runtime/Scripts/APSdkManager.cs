namespace APSdk
{
    using UnityEngine;
#if APSdk_LionKit
    using LionStudios;
#endif

#if UNITY_IOS
    using UnityEngine.iOS;
#endif

    public static class APSdkManager
    {
        public static bool IsATTEnabled
        {
            get;
            private set;
        } = false;

        private static void InitializeAnalytics(APSdkConfiguretionInfo _apSdkConfiguretionInfo, bool IsATTEnabled = false) {

            Object[] analyticsConfiguretionObjects = Resources.LoadAll("", typeof(APBaseClassForAnalyticsConfiguretion));
            foreach (Object analyticsConfiguretionObject in analyticsConfiguretionObjects)
            {

                APBaseClassForAnalyticsConfiguretion analyticsConfiguretion = (APBaseClassForAnalyticsConfiguretion)analyticsConfiguretionObject;
                if (analyticsConfiguretion != null)
                    analyticsConfiguretion.Initialize(_apSdkConfiguretionInfo, IsATTEnabled);
            }
        }

        private static void InitializeAdNetworks(APSdkConfiguretionInfo _apSdkConfiguretionInfo, bool IsATTEnabled = false) {

            Object[] adNetworkConfiguretionObjects = Resources.LoadAll("", typeof(APBaseClassForAdConfiguretion));
            foreach (Object adNetoworkConfiguretionObject in adNetworkConfiguretionObjects)
            {

                APBaseClassForAdConfiguretion adNetworkConfiguretion = (APBaseClassForAdConfiguretion)adNetoworkConfiguretionObject;
                if (adNetworkConfiguretion != null && _apSdkConfiguretionInfo.SelectedAdConfig == adNetworkConfiguretion)
                    adNetworkConfiguretion.Initialize(_apSdkConfiguretionInfo, IsATTEnabled);
            }
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void OnGameStart()
        {

            APSdkConfiguretionInfo _apSdkConfiguretionInfo = Resources.Load<APSdkConfiguretionInfo>("APSdkConfiguretionInfo");

            APAnalytics.Initialize(_apSdkConfiguretionInfo);

#if APSdk_LionKit

            MaxSdkCallbacks.OnSdkInitializedEvent += (MaxSdkBase.SdkConfiguration sdkConfiguration) =>
            {

                APSdkLogger.Log("MaxSDK Initialized");

#if UNITY_IOS

            if (MaxSdkUtils.CompareVersions(Device.systemVersion, "14.5") != MaxSdkUtils.VersionComparisonResult.Lesser)
            {
                APSdkLogger.Log("iOS 14.5+ detected!! SetAdvertiserTrackingEnabled = true");
                IsATTEnabled = sdkConfiguration.AppTrackingStatus == MaxSdkBase.AppTrackingStatus.Authorized;

            }
            else
            {
                APSdkLogger.Log("iOS <14.5 detected!! Normal Mode");
            }
#endif

                
            };

            LionKit.OnInitialized += () =>
            {
                APSdkLogger.Log("LionKit Initialized");

                InitializeAnalytics(_apSdkConfiguretionInfo, IsATTEnabled);
                InitializeAdNetworks(_apSdkConfiguretionInfo, IsATTEnabled);
            };
#else
            InitializeAnalytics(_apSdkConfiguretionInfo, IsATTEnabled);
            InitializeAdNetworks(_apSdkConfiguretionInfo, IsATTEnabled);
#endif


        }
    }
}

