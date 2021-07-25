namespace APSdk
{
#if APSdk_Adjust

    using UnityEngine;

#if UNITY_EDITOR
    using UnityEditor;
#endif

    [CreateAssetMenu(fileName = "APAdjustConfiguretion",menuName = "APAdjustConfiguretion")]
    public class APAdjustConfiguretion : APBaseClassForAnalyticsConfiguretion
    {
        public override void SetNameAndIntegrationStatus()
        {
            SetNameOfConfiguretion(APSdkConstant.APSdk_Adjust);
#if UNITY_EDITOR
            _isSDKIntegrated = APSdkScriptDefiniedSymbol.CheckAdjustIntegration();
#endif
        }

        protected override bool CanBeSubscribedToLionLogEvent()
        {
            return true;
        }

        protected override void CustomEditorGUI()
        {
#if UNITY_EDITOR

#endif
        }
    }
#endif
}

