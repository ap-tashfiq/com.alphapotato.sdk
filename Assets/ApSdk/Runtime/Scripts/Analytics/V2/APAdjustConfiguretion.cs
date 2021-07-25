namespace APSdk
{
#if APSdk_Adjust

    using UnityEngine;
    using com.adjust.sdk;

#if UNITY_EDITOR
    using UnityEditor;
#endif

    [CreateAssetMenu(fileName = "APAdjustConfiguretion",menuName = "APAdjustConfiguretion")]
    public class APAdjustConfiguretion : APBaseClassForAnalyticsConfiguretion
    {
        #region Public Variables

        public string appToken
        {
            get
            {
#if UNITY_ANDROID
                return _appTokenForAndroid;
#elif UNITY_IOS
                return _appTokenForIOS;
#else
                return "invalid_platform";
#endif
            }
        }

        public AdjustEnvironment Environment { get { return _environment; } }

        public AdjustLogLevel LogLevel { get { return _logLevel; } }
        public float StartDelay { get { return _startDelay; } }
        public bool StartManually { get { return _startManually; } }
        public bool EventBuffering { get { return _eventBuffering; } }
        public bool SendInBackground { get { return _sendInBackground; } }
        public bool LaunchDeferredDeeplink { get { return _launchDeferredDeeplink; } }

        #endregion


        #region Private Variables

#if UNITY_EDITOR

        [SerializeField] private bool _showBasicInfo;
        [SerializeField] private bool _showAdvancedInfo;

#endif

        [SerializeField] private string _appTokenForAndroid;
        [SerializeField] private string _appTokenForIOS;
        [SerializeField] private AdjustEnvironment _environment = AdjustEnvironment.Sandbox;

        [SerializeField] private AdjustLogLevel _logLevel = AdjustLogLevel.Suppress;
        [SerializeField] private float _startDelay = 0;
        [SerializeField] private bool _startManually = true;
        [SerializeField] private bool _eventBuffering;
        [SerializeField] private bool _sendInBackground;
        [SerializeField] private bool _launchDeferredDeeplink = true;

        #endregion

        #region Override Methods

        public override void SetNameAndIntegrationStatus()
        {
            SetNameOfConfiguretion(APSdkConstant.APSdk_Adjust);
#if UNITY_EDITOR
            _isSDKIntegrated = APSdkScriptDefiniedSymbol.CheckAdjustIntegration();
#endif
        }

        public override void Initialize()
        {

            APSdkConfiguretionInfo _apSdkConfiguretionInfo = Resources.Load<APSdkConfiguretionInfo>("APSdkConfiguretionInfo");

#if APSdk_LionKit

            LionStudios.LionKit.OnInitialized += () =>
            {
                APAdjustWrapper.Instance.Initialize(_apSdkConfiguretionInfo, this);

                if (_subscribeToLionEvent) {

                    LionStudios.Analytics.OnLogEvent += (gameEvent) =>
                    {
                        APLionKitWrapper.LogLionGameEvent("Adjust", gameEvent);
                        APAdjustWrapper.Instance.LogEvent(
                                    gameEvent.eventName,
                                    gameEvent.eventParams
                                );
                    };
                }

                if (_subscribeToLionEventUA)
                {
                    LionStudios.Analytics.OnLogEventUA += (gameEvent) =>
                    {
                        APLionKitWrapper.LogLionGameEvent("AdjustUA", gameEvent);
                        APAdjustWrapper.Instance.LogEvent(
                                    gameEvent.eventName,
                                    gameEvent.eventParams
                                );
                    };
                }
            };
#else

            APAdjustWrapper.Instance.Initialize(_apSdkConfiguretionInfo, this);

#endif
        }

        public override bool CanBeSubscribedToLionLogEvent()
        {
            return true;
        }

        public override void PreCustomEditorGUI()
        {
#if UNITY_EDITOR

#endif
        }

        public override void PostCustomEditorGUI()
        {
#if UNITY_EDITOR

            #region Settings    :   Basic

            EditorGUI.indentLevel += 2;
            {
                EditorGUILayout.BeginHorizontal(GUI.skin.box);
                {
                    string basicLabel = "[" + (!_showBasicInfo ? "+" : "-") + "] [Settings : Basic]";
                    GUIContent basicLabelContent = new GUIContent(
                            basicLabel

                        );
                    GUIStyle basicLabelStyle = new GUIStyle(EditorStyles.boldLabel);
                    basicLabelStyle.alignment = TextAnchor.MiddleLeft;
                    basicLabelStyle.padding.left = 28;

                    if (GUILayout.Button(basicLabelContent, basicLabelStyle, GUILayout.Width(EditorGUIUtility.currentViewWidth)))
                    {
                        _showBasicInfo = !_showBasicInfo;
                    }
                }
                EditorGUILayout.EndHorizontal();

                if (_showBasicInfo)
                {

                    EditorGUILayout.BeginVertical(GUI.skin.box);
                    {
                        EditorGUILayout.BeginHorizontal();
                        {
                            EditorGUILayout.LabelField("AppToken : Android", GUILayout.Width(APSdkConstant.EDITOR_LABEL_WIDTH));
                            _appTokenForAndroid = EditorGUILayout.TextField(_appTokenForAndroid);
                        }
                        EditorGUILayout.EndHorizontal();

                        EditorGUILayout.BeginHorizontal();
                        {
                            EditorGUILayout.LabelField("AppToken : iOS", GUILayout.Width(APSdkConstant.EDITOR_LABEL_WIDTH));
                            _appTokenForIOS = EditorGUILayout.TextField(_appTokenForIOS);
                        }
                        EditorGUILayout.EndHorizontal();

                        EditorGUILayout.BeginHorizontal();
                        {
                            EditorGUILayout.LabelField("Environment", GUILayout.Width(APSdkConstant.EDITOR_LABEL_WIDTH));
                            _environment = (AdjustEnvironment) EditorGUILayout.EnumPopup(_environment);
                        }
                        EditorGUILayout.EndHorizontal();
                    }
                    EditorGUILayout.EndVertical();

                }


            }
            EditorGUI.indentLevel -= 2;

            #endregion

            //-------------
            #region Settings    :   Advance

            EditorGUI.indentLevel += 2;
            {
                EditorGUILayout.BeginHorizontal(GUI.skin.box);
                {
                    string advanceLabel = "[" + (!_showAdvancedInfo ? "+" : "-") + "] [Settings : Advance]";
                    GUIContent advanceLabelContent = new GUIContent(
                            advanceLabel

                        );
                    GUIStyle advanceLabelStyle = new GUIStyle(EditorStyles.boldLabel);
                    advanceLabelStyle.alignment = TextAnchor.MiddleLeft;
                    advanceLabelStyle.padding.left = 28;

                    if (GUILayout.Button(advanceLabelContent, advanceLabelStyle, GUILayout.Width(EditorGUIUtility.currentViewWidth)))
                    {
                        _showAdvancedInfo = !_showAdvancedInfo;
                    }
                }
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginVertical(GUI.skin.box);
                {
                    if (_showAdvancedInfo)
                    {

                        EditorGUILayout.BeginHorizontal();
                        {
                            EditorGUILayout.LabelField("LogLevel", GUILayout.Width(APSdkConstant.EDITOR_LABEL_WIDTH));
                            _logLevel = (AdjustLogLevel)EditorGUILayout.EnumPopup(_logLevel);
                        }
                        EditorGUILayout.EndHorizontal();



                        EditorGUILayout.BeginHorizontal();
                        {
                            EditorGUILayout.LabelField("StartDelay", GUILayout.Width(APSdkConstant.EDITOR_LABEL_WIDTH));
                            _startDelay = EditorGUILayout.FloatField(_startDelay);
                        }
                        EditorGUILayout.EndHorizontal();



                        EditorGUILayout.BeginHorizontal();
                        {
                            EditorGUILayout.LabelField("StartManually", GUILayout.Width(APSdkConstant.EDITOR_LABEL_WIDTH));
                            _startManually = EditorGUILayout.Toggle(_startManually);
                        }
                        EditorGUILayout.EndHorizontal();


                        EditorGUILayout.Space();
                        EditorGUILayout.BeginHorizontal();
                        {
                            EditorGUILayout.LabelField("EventBuffering", GUILayout.Width(APSdkConstant.EDITOR_LABEL_WIDTH));
                            _eventBuffering = EditorGUILayout.Toggle(_eventBuffering);
                        }
                        EditorGUILayout.EndHorizontal();



                        EditorGUILayout.BeginHorizontal();
                        {
                            EditorGUILayout.LabelField("SendInBackground", GUILayout.Width(APSdkConstant.EDITOR_LABEL_WIDTH));
                            _sendInBackground = EditorGUILayout.Toggle(_sendInBackground);
                        }
                        EditorGUILayout.EndHorizontal();



                        EditorGUILayout.BeginHorizontal();
                        {
                            EditorGUILayout.LabelField("LaunchDeferredDeeplink", GUILayout.Width(APSdkConstant.EDITOR_LABEL_WIDTH));
                            _launchDeferredDeeplink = EditorGUILayout.Toggle(_launchDeferredDeeplink);
                        }
                        EditorGUILayout.EndHorizontal();
                    }
                }
                EditorGUILayout.EndVertical();
            }
            EditorGUI.indentLevel -= 2;


            #endregion

#endif
        }

        

#endregion
    }


#endif
        }

