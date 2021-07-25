namespace APSdk
{
#if APSdk_GameAnalytics
    using UnityEngine;

#if UNITY_EDITOR
    using UnityEditor;
#endif

    [CreateAssetMenu(fileName = "APGameAnalyticsConfiguretion", menuName = "APGameAnalyticsConfiguretion")]
    public class APGameAnalyticsConfiguretion : APBaseClassForAnalyticsConfiguretion
    {
        #region Public Variables

        public int DefaultWorldIndex { get { return _defaultWorldIndex; } }

        #endregion

        #region Private Variables

        [SerializeField] private int _defaultWorldIndex = 1;

        #endregion

        #region Override Method

        public override void SetNameAndIntegrationStatus()
        {
            SetNameOfConfiguretion(APSdkConstant.APSdk_GameAnalytics);
#if UNITY_EDITOR
            _isSDKIntegrated = APSdkScriptDefiniedSymbol.CheckGameAnalyticsIntegration();
#endif
        }

        public override bool CanBeSubscribedToLionLogEvent()
        {
            return false;
        }

        public override void PreCustomEditorGUI()
        {
            
        }

        public override void PostCustomEditorGUI()
        {
#if UNITY_EDITOR
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

        public override void Initialize(APSdkConfiguretionInfo apSdkConfiguretionInfo)
        {
            if (APGameAnalyticsWrapper.Instance == null)
            {
                Instantiate(Resources.Load("GameAnalytics/AP_GameAnalytics"));

                GameObject newAPGameAnalyticsWrapper = new GameObject("APGameAnalyticsWrapper");
                APGameAnalyticsWrapper.Instance = newAPGameAnalyticsWrapper.AddComponent<APGameAnalyticsWrapper>();

                DontDestroyOnLoad(newAPGameAnalyticsWrapper);

                APGameAnalyticsWrapper.Instance.Initialize(apSdkConfiguretionInfo, this);
            }
        }

#endregion



    }
#endif
        }

