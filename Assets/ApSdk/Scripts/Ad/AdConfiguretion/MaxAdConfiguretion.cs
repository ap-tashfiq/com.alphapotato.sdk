namespace APSdk
{
    using UnityEngine;

    [CreateAssetMenu(fileName = "MaxAdConfiguretion", menuName = "MaxAdConfiguretion")]
    public class MaxAdConfiguretion : BaseClassForAdConfiguretion
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

