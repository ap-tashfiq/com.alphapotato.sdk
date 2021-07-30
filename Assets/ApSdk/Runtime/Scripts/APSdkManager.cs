﻿namespace APSdk
{
    using UnityEngine;


    public static class APSdkManager
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void OnGameStart()
        {
            APSdkConfiguretionInfo _apSdkConfiguretionInfo = Resources.Load<APSdkConfiguretionInfo>("APSdkConfiguretionInfo");

            APAnalytics.Initialize(_apSdkConfiguretionInfo);

            Object[] analyticsConfiguretionObjects = Resources.LoadAll("", typeof(APBaseClassForAnalyticsConfiguretion));
            foreach (Object analyticsConfiguretionObject in analyticsConfiguretionObjects)
            {

                APBaseClassForAnalyticsConfiguretion analyticsConfiguretion = (APBaseClassForAnalyticsConfiguretion)analyticsConfiguretionObject;
                if (analyticsConfiguretion != null)
                    analyticsConfiguretion.Initialize(_apSdkConfiguretionInfo);
            }

            Object[] adNetworkConfiguretionObjects = Resources.LoadAll("", typeof(APBaseClassForAdConfiguretion));
            foreach (Object adNetoworkConfiguretionObject in adNetworkConfiguretionObjects)
            {

                APBaseClassForAdConfiguretion adNetworkConfiguretion = (APBaseClassForAdConfiguretion)adNetoworkConfiguretionObject;
                if (adNetworkConfiguretion != null && _apSdkConfiguretionInfo.SelectedAdConfig == adNetworkConfiguretion)
                    adNetworkConfiguretion.Initialize(_apSdkConfiguretionInfo);
            }

        }
    }
}

