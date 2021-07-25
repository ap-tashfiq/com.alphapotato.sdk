namespace APSdk
{
#if APSdk_Facebook

    using UnityEngine;

    [CreateAssetMenu(fileName = "APFacebookConfiguretion", menuName = "APFacebookConfiguretion")]
    public class APFacebookConfiguretion : APBaseClassForAnalyticsConfiguretion
    {
        #region Override Method

        public override void SetNameAndIntegrationStatus()
        {
            SetNameOfConfiguretion(APSdkConstant.APSdk_Facebook);
#if UNITY_EDITOR
            _isSDKIntegrated = APSdkScriptDefiniedSymbol.CheckFacebookIntegration();
#endif
        }

        public override bool CanBeSubscribedToLionLogEvent()
        {
            return true;
        }

        public override void PostCustomEditorGUI()
        {
            
        }

        public override void PreCustomEditorGUI()
        {
            
        }

        public override void Initialize(APSdkConfiguretionInfo apSdkConfiguretionInfo)
        {
            if (APFacebookWrapper.Instance == null)
            {

                GameObject newAPFacebookWrapper = new GameObject("APFacebookWrapper");
                APFacebookWrapper.Instance = newAPFacebookWrapper.AddComponent<APFacebookWrapper>();

                DontDestroyOnLoad(newAPFacebookWrapper);
            }

#if APSdk_LionKit
            LionStudios.LionKit.OnInitialized += () =>
            {

                APFacebookWrapper.Instance.Initialize(
                    apSdkConfiguretionInfo,
                    this,
                    ()=> {
                        if (_subscribeToLionEvent)
                        {

                            LionStudios.Analytics.OnLogEvent += (gameEvent) =>
                            {
                                APLionKitWrapper.LogLionGameEvent("Facebook", gameEvent);
                                APFacebookWrapper.Instance.LogEvent(
                                        gameEvent.eventName,
                                        gameEvent.eventParams
                                    );
                            };
                        }

                        if (_subscribeToLionEventUA)
                        {

                            LionStudios.Analytics.OnLogEventUA += (gameEvent) =>
                            {
                                APLionKitWrapper.LogLionGameEvent("FacebookUA", gameEvent);
                                APFacebookWrapper.Instance.LogEvent(
                                        gameEvent.eventName,
                                        gameEvent.eventParams
                                    );
                            };
                        }
                    });

            };
#else
        APFacebookWrapper.Instance.Initialize(apSdkConfiguretionInfo);
#endif
        }

        #endregion


    }

#endif

        }

