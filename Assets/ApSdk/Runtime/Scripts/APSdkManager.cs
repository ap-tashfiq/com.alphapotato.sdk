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
                analyticsConfiguretion.Initialize(_apSdkConfiguretionInfo);

            int numberOfAdConfiguretion = _apSdkConfiguretionInfo.listOfAdConfiguretion.Count;
            for (int i = 0; i < numberOfAdConfiguretion; i++) {

                if (i == _apSdkConfiguretionInfo.IndexOfActiveAdConfiguretion)
                    _apSdkConfiguretionInfo.listOfAdConfiguretion[i].Initialize(_apSdkConfiguretionInfo);
            }
        }
    }
}

