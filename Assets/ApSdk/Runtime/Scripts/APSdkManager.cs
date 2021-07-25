namespace APSdk
{
    using UnityEngine;


    public static class APSdkManager
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void OnGameStart()
        {
           APSdkConfiguretionInfo _apSdkConfiguretionInfo = Resources.Load<APSdkConfiguretionInfo>("APSdkConfiguretionInfo");

            foreach (APBaseClassForAnalyticsConfiguretion analyticsConfiguretion in _apSdkConfiguretionInfo.listOfAnalyticsConfiguretion)
                analyticsConfiguretion.Initialize();

            foreach (APBaseClassForAdConfiguretion adConfiguretion in _apSdkConfiguretionInfo.listOfAdConfiguretion)
                adConfiguretion.Initialize();
        }
    }
}

