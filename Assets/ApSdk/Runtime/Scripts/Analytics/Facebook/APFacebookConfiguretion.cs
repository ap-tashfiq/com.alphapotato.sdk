namespace APSdk
{

#if APSdk_Facebook

    using System.Collections.Generic;
    using UnityEngine;
#if UNITY_EDITOR
    using UnityEditor;
#endif

    [CreateAssetMenu(fileName = "APFacebookConfiguretion", menuName = "APFacebookConfiguretion")]
    public class APFacebookConfiguretion : APBaseClassForAnalyticsConfiguretion
    {
        #region Private Variables

        [SerializeField] private string _facebookAppName;
        [SerializeField] private string _facebookAppId;

        #endregion

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
#if UNITY_EDITOR
            EditorGUILayout.BeginHorizontal();
            {
                EditorGUILayout.LabelField("appName", GUILayout.Width(APSdkConstant.EDITOR_LABEL_WIDTH));
                EditorGUI.BeginChangeCheck();
                _facebookAppName = EditorGUILayout.TextField(_facebookAppName);
                if (EditorGUI.EndChangeCheck())
                {
                    Facebook.Unity.Settings.FacebookSettings.AppLabels = new List<string>() { _facebookAppName };
                }

            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            {
                EditorGUILayout.LabelField("appId", GUILayout.Width(APSdkConstant.EDITOR_LABEL_WIDTH));
                EditorGUI.BeginChangeCheck();
                _facebookAppId = EditorGUILayout.TextField(_facebookAppId);
                if (EditorGUI.EndChangeCheck())
                {

                    Facebook.Unity.Settings.FacebookSettings.AppIds = new List<string>() { _facebookAppId };
                }
            }
            EditorGUILayout.EndHorizontal();

            APSdkEditorModule.DrawHorizontalLine();
#endif
        }

        public override void Initialize(APSdkConfiguretionInfo apSdkConfiguretionInfo)
        {
            if (APFacebookWrapper.Instance == null && IsAnalyticsEventEnabled)
            {

                GameObject newAPFacebookWrapper = new GameObject("APFacebookWrapper");
                APFacebookWrapper.Instance = newAPFacebookWrapper.AddComponent<APFacebookWrapper>();

                DontDestroyOnLoad(newAPFacebookWrapper);

#if APSdk_LionKit
                LionStudios.LionKit.OnInitialized += () =>
                {

                    APFacebookWrapper.Instance.Initialize(
                        apSdkConfiguretionInfo,
                        this,
                        () => {
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
        APFacebookWrapper.Instance.Initialize(apSdkConfiguretionInfo, this);
#endif

            }


        }

#endregion


    }

#endif

        }

