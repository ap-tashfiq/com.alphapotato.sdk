namespace APSdk
{
    using UnityEngine;

    [CreateAssetMenu(fileName = "APMaxAdNetworkConfiguretion", menuName = "APMaxAdNetworkConfiguretion")]
    public class APMaxAdNetworkConfiguretion : BaseClassForAdConfiguretion
    {

#if UNITY_EDITOR
        public override void SetSDKNameAndIntegrationStatus()
        {
            SetSDKName(APSdkConstant.APSdk_MaxAdNetwork);
            _isAdSDKIntegrated = APSdkScriptDefiniedSymbol.CheckMaxAdNetworkIntegrated();
            
        }
#endif
    }
}

