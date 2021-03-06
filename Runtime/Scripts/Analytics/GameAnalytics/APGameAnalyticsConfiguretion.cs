namespace com.alphapotato.sdk
{

    using UnityEngine;
    

#if UNITY_EDITOR
    using UnityEditor;
#endif

    //[CreateAssetMenu(fileName = "APGameAnalyticsConfiguretion", menuName = "APGameAnalyticsConfiguretion")]
    public class APGameAnalyticsConfiguretion : APBaseClassForAnalyticsConfiguretion
    {
        

        #region Public Variables

        public int DefaultWorldIndex { get { return _defaultWorldIndex; } }

        #endregion

        #region Private Variables

        [HideInInspector, SerializeField] private int _defaultWorldIndex = 1;

#if UNITY_EDITOR && APSdk_GameAnalytics
        private GameAnalyticsSDK.Setup.Settings _gaSettings;
        private Editor _gaSettingsEditor;
        private bool _isShowingGASettings;
        
#endif

        #endregion

        #region Override Method

        public override void SetNameAndIntegrationStatus()
        {
            string sdkName = APSdkConstant.NameOfSDK + "_GameAnalytics";
            SetNameOfConfiguretion(sdkName);
#if UNITY_EDITOR
            _isSDKIntegrated = APSdkScriptDefiniedSymbol.CheckGameAnalyticsIntegration(sdkName);
#endif
        }

        public override bool CanBeSubscribedToLionLogEvent()
        {
            return false;
        }

        public override void PreCustomEditorGUI()
        {
#if UNITY_EDITOR && APSdk_GameAnalytics

            if (IsAnalyticsEventEnabled) {

                if (_gaSettings == null)
                    _gaSettings = Resources.Load<GameAnalyticsSDK.Setup.Settings>("GameAnalytics/Settings");

                if (_gaSettings == null)
                    EditorGUILayout.HelpBox("You need to create GA_'Settings' by going to 'Window/Game Analytics/Select Settings' from menu in order to ga_sdk for working properly", MessageType.Error);
                else
                {
                    EditorGUILayout.HelpBox("If you haven't setup your game on GA, please do by loging, adding platform and selecting your games from down below. Make sure to put the right 'sdk key' and 'secret key' for your specefic platform", MessageType.Warning);
                    APSdkEditorModule.DrawSettingsEditor(_gaSettings, null, ref _isShowingGASettings, ref _gaSettingsEditor);
                }

                
                APSdkEditorModule.DrawHorizontalLine();
            }
            

#endif
        }

        public override void PostCustomEditorGUI()
        {
#if UNITY_EDITOR && APSdk_GameAnalytics
                APSdkEditorModule.DrawHorizontalLine();

                EditorGUILayout.BeginVertical();
                {
                    EditorGUILayout.BeginHorizontal();
                    {
                        EditorGUILayout.LabelField("DefaultWorldIndexOnGameAnalytics", GUILayout.Width(APSdkConstant.EDITOR_LABEL_WIDTH));
                        _defaultWorldIndex = EditorGUILayout.IntField(_defaultWorldIndex);
                    }
                    EditorGUILayout.EndHorizontal();


                }
                EditorGUILayout.EndVertical();

#endif
        }

        public override void Initialize(APSdkConfiguretionInfo apSdkConfiguretionInfo, bool isATTEnable = false)
        {
#if APSdk_GameAnalytics
            if (APGameAnalyticsWrapper.Instance == null && IsAnalyticsEventEnabled)
            {
                Instantiate(Resources.Load("GameAnalytics/AP_GameAnalytics"));

                GameObject newAPGameAnalyticsWrapper = new GameObject("APGameAnalyticsWrapper");
                APGameAnalyticsWrapper.Instance = newAPGameAnalyticsWrapper.AddComponent<APGameAnalyticsWrapper>();

                DontDestroyOnLoad(newAPGameAnalyticsWrapper);

                APGameAnalyticsWrapper.Instance.Initialize(apSdkConfiguretionInfo, this);
            }
#endif
        }

#endregion



    }
}

