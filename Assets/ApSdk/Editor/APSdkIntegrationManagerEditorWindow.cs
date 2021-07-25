
namespace APSdk
{
#if UNITY_EDITOR
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEditor;
#if APSdk_Adjust
    using com.adjust.sdk;
#endif

    public class APSdkIntegrationManagerEditorWindow : EditorWindow
    {
        #region Public Variables

        public const float LabelWidth = 200;

        #endregion

        #region Private Variables   :   General

        private static EditorWindow _reference;
        

        private bool _IsInformationFetched = false;
        private Vector2 _scrollPosition;

        private GUIStyle _settingsTitleStyle;
        private GUIStyle _hyperlinkStyle;

        //private const string _linkForDownload       = "<a href=\"" + "https://portbucket2@bitbucket.org/portbucket2/apsdk.git" + "\"> Download </a>";
        //private const string _linkForDocumetation   = "<a href=\"" + "https://bitbucket.org/portbucket2/apsdk/src/master/" + "\"> Documentation </a>";

        private const string _linkForDownload       = "https://portbucket2@bitbucket.org/portbucket2/apsdk.git";
        private const string _linkForDocumetation   = "https://bitbucket.org/portbucket2/apsdk/src/master/";

        #endregion

        #region Private Variables   :   APSdkConfiguretionInfo

                private APSdkConfiguretionInfo  _apSDKConfiguretionInfo;
                private SerializedObject        _serializedSDKConfiguretionInfo;

                private GUIContent              _generalSettingContent;
                private GUIContent              _lionAdSettingContent;
                private GUIContent              _facebookSettingContent;
                private GUIContent              _adjustSettingContent;
                private GUIContent              _gameAnalyticsSettingContent;
                private GUIContent              _firebaseSettingContent;
                private GUIContent              _analyticsSettingContent;
                private GUIContent              _adNetworkSettingContent;
                private GUIContent              _abTestSettingContent;
                private GUIContent              _debuggingSettingContent;

        
                private SerializedProperty      _isLionKitSDKIntegrated;
                private SerializedProperty      _isFacebookSDKIntegrated;
                private SerializedProperty      _isAdjustSDKIntegrated;
                private SerializedProperty      _isGameAnalyticsSDKIntegrated;
                private SerializedProperty      _isFirebaseSDKIntegrated;

                private SerializedProperty      _showGeneralSettings;
                private SerializedProperty      _showLionAdSetting;
                private SerializedProperty      _showFacebookSetting;
                private SerializedProperty      _showAdjustSetting;
                private SerializedProperty      _showGameAnalyticsSetting;
                private SerializedProperty      _showFirebaseSetting;
                private SerializedProperty      _showAnalytics;
                private SerializedProperty      _showAdNetworks;
                private SerializedProperty      _showABTestSetting;
                private SerializedProperty      _showDebuggingSettings;

                private SerializedProperty      _logAnalyticsEvent;
                private SerializedProperty      _maxMediationDebugger;

                private SerializedProperty      _showAPSdkLogInConsole;

                private SerializedProperty      _infoLogColor;
                private SerializedProperty      _warningLogColor;
                private SerializedProperty      _errorLogColor;



                #endregion

        #region Private Variables   :   APLionKitInfo

#if APSdk_LionKit
        private APLionKitInfo        _apLionKitInfo;
        private SerializedObject    _serializedLionKitInfo;

        private SerializedProperty  _adShowRewardedAdSettings;
        private SerializedProperty  _adShowInterstitialAdSettings;
        private SerializedProperty  _adShowBannerAdSettings;
        private SerializedProperty  _adShowCrossPromoAdSettings;


        private SerializedProperty  _adEnableRewardedAd;
        private SerializedProperty  _adEnableInterstitialAd;
        private SerializedProperty  _adEnableBannerAd;
        private SerializedProperty  _adEnableCrossPromoAd;

        private SerializedProperty  _adStartBannerAdManually;
#endif

        #endregion

        

        #region Private Variables   :   GameAnalyticsInfo

#if APSdk_GameAnalytics

        private APGameAnalyticsInfo _apGameAnalyticsInfo;
        private SerializedObject    _serializedGameAnalyticsInfo;


        private SerializedProperty _enableGameAnalyticsEvent;

        private SerializedProperty _trackProgressionEventOnGA;
        private SerializedProperty _trackAdEventOnGA;

        private SerializedProperty _defaultWorldIndexOnGameAnalytics;


#endif

        #endregion

        #region Prvate Variables    :   Firebase

#if APSdk_Firebase
        private APFirebaseInfo _apFirebaseInfo;
        private SerializedObject _serializedFirebaseInfo;

        private SerializedProperty _enableFirebaseAnalyticsEvent;

        private SerializedProperty _trackProgressionEventOnFirebase;
        private SerializedProperty _trackAdEventOnFirebase;

        private SerializedProperty _subscribeToLionEventOnFirebase;
        private SerializedProperty _subscribeToLionEventUAOnFirebase;
#endif

        #endregion

        #region Editor

        [MenuItem("AP/APSdk Integration Manager")]
        public static void Create()
        {
            if (_reference == null)
                _reference = GetWindow<APSdkIntegrationManagerEditorWindow>("APSdk Integration Manager", typeof(APSdkIntegrationManagerEditorWindow));
            else
                _reference.Show();

            _reference.Focus();
        }

        private void OnEnable()
        {
            FetchAllTheReference();
            
        }

        private void OnDisable()
        {
            _IsInformationFetched = false;
        }

        private void OnFocus()
        {
            FetchAllTheReference();
        }

        private void OnLostFocus()
        {
            _IsInformationFetched = false;
        }

        private void OnGUI()
        {
            if (!_IsInformationFetched) {

                FetchAllTheReference();
                _IsInformationFetched = true;
            }

            _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition, false, false);
            {
                EditorGUILayout.Space();

                EditorGUI.indentLevel += 1;
                {
                    GeneralSettingGUI();

                    //EditorGUILayout.Space();
                    //LionAdSettingsGUI();

                    //EditorGUILayout.Space();
                    //FacebookSettingGUI();

                    //EditorGUILayout.Space();
                    //AdjustSettingsGUI();

                    //EditorGUILayout.Space();
                    //GameAnalyticsSettingsGUI();

                    //EditorGUILayout.Space();
                    //FirebaseSettingsGUI();

                    EditorGUILayout.Space();
                    AnalyticsSettingsGUI();

                    EditorGUILayout.Space();
                    AdNetworksSettingsGUI();

                    EditorGUILayout.Space();
                    ABTestSettingsGUI();

                    EditorGUILayout.Space();
                    DebuggingSettingsGUI();
                }
                EditorGUI.indentLevel -= 1;

                
            }
            EditorGUILayout.EndScrollView();

        }

#endregion

        #region CustomGUI

        private void DrawHeaderGUI(string title, ref GUIContent gUIContent, ref GUIStyle gUIStyle, ref SerializedProperty serializedProperty) {

            EditorGUILayout.BeginVertical(GUI.skin.box);
            {
                if (GUILayout.Button(gUIContent, gUIStyle, GUILayout.Width(EditorGUIUtility.currentViewWidth)))
                {
                    serializedProperty.boolValue = !serializedProperty.boolValue;
                    serializedProperty.serializedObject.ApplyModifiedProperties();

                    gUIContent = new GUIContent(
                        "[" + (!serializedProperty.boolValue ? "+" : "-") + "] " + title
                    );
                }
            }
            EditorGUILayout.EndVertical();
        }

        private void DrawAnalyticsGUI(APBaseClassForAnalyticsConfiguretion analyticsConfiguretion) {

            //Referencing Variables
            SerializedObject serailizedAnalyticsConfiguretion = new SerializedObject(analyticsConfiguretion);

            SerializedProperty _nameOfConfiguretion = serailizedAnalyticsConfiguretion.FindProperty("_nameOfConfiguretion");
            SerializedProperty _isSDKIntegrated     = serailizedAnalyticsConfiguretion.FindProperty("_isSDKIntegrated");

            SerializedProperty _showSettings = serailizedAnalyticsConfiguretion.FindProperty("_showSettings");

            SerializedProperty _enableAnalyticsEvent = serailizedAnalyticsConfiguretion.FindProperty("_enableAnalyticsEvent");

            SerializedProperty _trackProgressionEvent = serailizedAnalyticsConfiguretion.FindProperty("_trackProgressionEvent");
            SerializedProperty _trackAdEvent = serailizedAnalyticsConfiguretion.FindProperty("_trackAdEvent");

            SerializedProperty _subscribeToLionEvent = serailizedAnalyticsConfiguretion.FindProperty("_subscribeToLionEvent");
            SerializedProperty _subscribeToLionEventUA = serailizedAnalyticsConfiguretion.FindProperty("_subscribeToLionEventUA");

            //Setting Titles
            GUIContent titleContent = new GUIContent(
                    "[" + (!_showSettings.boolValue ? "+" : "-") + "] " + (_nameOfConfiguretion.stringValue + (_isSDKIntegrated.boolValue ? "" : "SDK Not Found"))
                );
            GUIStyle titleStyle = new GUIStyle(EditorStyles.boldLabel);
            titleStyle.alignment = TextAnchor.MiddleLeft;
            titleStyle.padding.left = 18;

            EditorGUILayout.BeginHorizontal(GUI.skin.box);
            {
                if (GUILayout.Button(titleContent, titleStyle, GUILayout.Width(EditorGUIUtility.currentViewWidth - 100f)))
                {
                    _showSettings.boolValue = !_showSettings.boolValue;
                    _showSettings.serializedObject.ApplyModifiedProperties();

                    titleContent = new GUIContent(
                        "[" + (!_showSettings.boolValue ? "+" : "-") + "] " + (_nameOfConfiguretion.stringValue + (_isSDKIntegrated.boolValue ? "" : "SDK Not Found"))
                    );
                }

                if (GUILayout.Button(_enableAnalyticsEvent.boolValue ? "Disable" : "Enable", GUILayout.Width(80)))
                {
                    _enableAnalyticsEvent.boolValue = !_enableAnalyticsEvent.boolValue;
                    _enableAnalyticsEvent.serializedObject.ApplyModifiedProperties();
                }

                GUILayout.FlexibleSpace();
            }
            EditorGUILayout.EndHorizontal();


            //Showing Settings
            if (_showSettings.boolValue) {

                EditorGUI.BeginDisabledGroup(!_enableAnalyticsEvent.boolValue);
                {
                    analyticsConfiguretion.PreCustomEditorGUI();

                    if (analyticsConfiguretion.CanBeSubscribedToLionLogEvent())
                    {

                        EditorGUI.indentLevel += 1;
                        {
#if APSdk_LionKit
                            EditorGUILayout.BeginHorizontal();
                            {
                                EditorGUILayout.LabelField(_subscribeToLionEvent.displayName, GUILayout.Width(LabelWidth));
                                EditorGUI.BeginChangeCheck();
                                _subscribeToLionEvent.boolValue = EditorGUILayout.Toggle(_subscribeToLionEvent.boolValue);
                                if (EditorGUI.EndChangeCheck())
                                    _subscribeToLionEvent.serializedObject.ApplyModifiedProperties();
                            }
                            EditorGUILayout.EndHorizontal();

                            EditorGUILayout.BeginHorizontal();
                            {
                                EditorGUILayout.LabelField(_subscribeToLionEventUA.displayName, GUILayout.Width(LabelWidth));
                                EditorGUI.BeginChangeCheck();
                                _subscribeToLionEventUA.boolValue = EditorGUILayout.Toggle(_subscribeToLionEventUA.boolValue);
                                if (EditorGUI.EndChangeCheck())
                                    _subscribeToLionEventUA.serializedObject.ApplyModifiedProperties();
                            }
                            EditorGUILayout.EndHorizontal();

#else
                        EditorGUILayout.BeginHorizontal();
                        {
                            EditorGUILayout.LabelField(_trackProgressionEvent.displayName, GUILayout.Width(_labelWidth));
                            EditorGUI.BeginChangeCheck();
                            _trackProgressionEvent.boolValue = EditorGUILayout.Toggle(_trackProgressionEvent.boolValue);
                            if (EditorGUI.EndChangeCheck())
                                _trackProgressionEvent.serializedObject.ApplyModifiedProperties();
                        }
                        EditorGUILayout.EndHorizontal();

                        EditorGUILayout.BeginHorizontal();
                        {
                            EditorGUILayout.LabelField(_trackAdEvent.displayName, GUILayout.Width(_labelWidth));
                            EditorGUI.BeginChangeCheck();
                            _trackAdEvent.boolValue = EditorGUILayout.Toggle(_trackAdEvent.boolValue);
                            if (EditorGUI.EndChangeCheck())
                                _trackAdEvent.serializedObject.ApplyModifiedProperties();
                        }
                        EditorGUILayout.EndHorizontal();
#endif
                        }
                        EditorGUI.indentLevel -= 1;


                    }
                    else
                    {

                        EditorGUILayout.BeginHorizontal();
                        {
                            EditorGUILayout.LabelField(_trackProgressionEvent.displayName, GUILayout.Width(LabelWidth));
                            EditorGUI.BeginChangeCheck();
                            _trackProgressionEvent.boolValue = EditorGUILayout.Toggle(_trackProgressionEvent.boolValue);
                            if (EditorGUI.EndChangeCheck())
                                _trackProgressionEvent.serializedObject.ApplyModifiedProperties();
                        }
                        EditorGUILayout.EndHorizontal();

                        EditorGUILayout.BeginHorizontal();
                        {
                            EditorGUILayout.LabelField(_trackAdEvent.displayName, GUILayout.Width(LabelWidth));
                            EditorGUI.BeginChangeCheck();
                            _trackAdEvent.boolValue = EditorGUILayout.Toggle(_trackAdEvent.boolValue);
                            if (EditorGUI.EndChangeCheck())
                                _trackAdEvent.serializedObject.ApplyModifiedProperties();
                        }
                        EditorGUILayout.EndHorizontal();
                    }

                    analyticsConfiguretion.PostCustomEditorGUI();

                }
                EditorGUI.EndDisabledGroup();

            }
        }

        private void DrawAdNetworkGUI(int adConfiguretionIndex, APBaseClassForAdConfiguretion adConfiguretion) {

            //Referencing Variables
            SerializedObject serailizedAdConfiguretion  = new SerializedObject(adConfiguretion);

            SerializedProperty _nameOfConfiguretion = serailizedAdConfiguretion.FindProperty("_nameOfConfiguretion");
            SerializedProperty _isSDKIntegrated = serailizedAdConfiguretion.FindProperty("_isSDKIntegrated");

            SerializedProperty _showSettings            = serailizedAdConfiguretion.FindProperty("_showSettings");
            SerializedProperty _showRewardedAdSettings  = serailizedAdConfiguretion.FindProperty("_showRewardedAdSettings");
            SerializedProperty _showInterstitialAdSettings  = serailizedAdConfiguretion.FindProperty("_showInterstitialAdSettings");
            SerializedProperty _showBannerAdSettings    = serailizedAdConfiguretion.FindProperty("_showBannerAdSettings");
            SerializedProperty _showCrossPromoAdSettings = serailizedAdConfiguretion.FindProperty("_showCrossPromoAdSettings");

            SerializedProperty _enableRewardedAd   = serailizedAdConfiguretion.FindProperty("_enableRewardedAd");

            SerializedProperty _enableInterstitialAd    = serailizedAdConfiguretion.FindProperty("_enableInterstitialAd");

            SerializedProperty _enableBannerAd          = serailizedAdConfiguretion.FindProperty("_enableBannerAd");
            SerializedProperty _showBannerAdManually    = serailizedAdConfiguretion.FindProperty("_showBannerAdManually");

            SerializedProperty _enableCrossPromoAd      = serailizedAdConfiguretion.FindProperty("_enableCrossPromoAd");


            //Setting Titles
            GUIContent titleContent = new GUIContent(
                    "[" + (!_showSettings.boolValue ? "+" : "-") + "] " + (_nameOfConfiguretion.stringValue + (_isSDKIntegrated.boolValue ? "" : "SDK Not Found"))
                );
            GUIStyle titleStyle = new GUIStyle(EditorStyles.boldLabel);
            titleStyle.alignment = TextAnchor.MiddleLeft;
            titleStyle.padding.left = 18;

            EditorGUILayout.BeginHorizontal(GUI.skin.box);
            {
                if (GUILayout.Button(titleContent, titleStyle, GUILayout.Width(EditorGUIUtility.currentViewWidth - 100f)))
                {
                    _showSettings.boolValue = !_showSettings.boolValue;
                    _showSettings.serializedObject.ApplyModifiedProperties();

                    titleContent = new GUIContent(
                        "[" + (!_showSettings.boolValue ? "+" : "-") + "] " + (_nameOfConfiguretion.stringValue + (_isSDKIntegrated.boolValue ? "" : "SDK Not Found"))
                    );
                }

                if (_apSDKConfiguretionInfo.indexOfActiveAdConfiguretion == adConfiguretionIndex)
                {
                    if (GUILayout.Button("Disable", GUILayout.Width(80)))
                    {
                        _apSDKConfiguretionInfo.indexOfActiveAdConfiguretion = -1;
                    }
                }
                else {
                    if (GUILayout.Button("Enable", GUILayout.Width(80)))
                    {
                        _apSDKConfiguretionInfo.indexOfActiveAdConfiguretion = adConfiguretionIndex;
                    }
                }

                GUILayout.FlexibleSpace();
            }
            EditorGUILayout.EndHorizontal();

            if (_showSettings.boolValue) {

                EditorGUI.BeginDisabledGroup((_apSDKConfiguretionInfo.indexOfActiveAdConfiguretion == adConfiguretionIndex) ? false : true);
                {
                    //AdType Configuretion
                    GUIStyle adTypeStyle = new GUIStyle(EditorStyles.boldLabel);
                    adTypeStyle.alignment = TextAnchor.MiddleLeft;
                    adTypeStyle.padding.left = 36;

                    //------------------------------
                    #region RewardedAd

                    EditorGUI.indentLevel += 1;
                    {
                        EditorGUILayout.BeginHorizontal(GUI.skin.box);
                        {
                            string rewardedAdLabel = "[" + (!_showRewardedAdSettings.boolValue ? "+" : "-") + "] [RewardedAd]";
                            GUIContent rewardedAdLabelContent = new GUIContent(
                                    rewardedAdLabel

                                );

                            if (GUILayout.Button(rewardedAdLabelContent, adTypeStyle, GUILayout.Width(EditorGUIUtility.currentViewWidth)))
                            {
                                _showRewardedAdSettings.boolValue = !_showRewardedAdSettings.boolValue;
                                _showRewardedAdSettings.serializedObject.ApplyModifiedProperties();
                            }
                        }
                        EditorGUILayout.EndHorizontal();

                        if (_showRewardedAdSettings.boolValue)
                        {

                            EditorGUI.indentLevel += 2;
                            {
                                EditorGUILayout.BeginHorizontal();
                                {
                                    EditorGUILayout.LabelField(_enableRewardedAd.displayName, GUILayout.Width(LabelWidth));
                                    EditorGUI.BeginChangeCheck();
                                    _enableRewardedAd.boolValue = EditorGUILayout.Toggle(_enableRewardedAd.boolValue);
                                    if (EditorGUI.EndChangeCheck())
                                        _enableRewardedAd.serializedObject.ApplyModifiedProperties();
                                }
                                EditorGUILayout.EndHorizontal();
                            }
                            EditorGUI.indentLevel -= 2;

                        }
                    }
                    EditorGUI.indentLevel -= 1;


                    #endregion

                    //------------------------------
                    #region InterstitialAd

                    EditorGUI.indentLevel += 1;
                    {
                        EditorGUILayout.BeginHorizontal(GUI.skin.box);
                        {
                            string interstitialAdLabel = "[" + (!_showInterstitialAdSettings.boolValue ? "+" : "-") + "] [InterstitialAd]";
                            GUIContent interstialAdLabelContent = new GUIContent(
                                    interstitialAdLabel

                                );

                            if (GUILayout.Button(interstialAdLabelContent, adTypeStyle, GUILayout.Width(EditorGUIUtility.currentViewWidth)))
                            {
                                _showInterstitialAdSettings.boolValue = !_showInterstitialAdSettings.boolValue;
                                _showInterstitialAdSettings.serializedObject.ApplyModifiedProperties();
                            }
                        }
                        EditorGUILayout.EndHorizontal();

                        if (_showInterstitialAdSettings.boolValue)
                        {
                            EditorGUI.indentLevel += 2;
                            {
                                EditorGUILayout.BeginHorizontal();
                                {
                                    EditorGUILayout.LabelField(_enableInterstitialAd.displayName, GUILayout.Width(LabelWidth));
                                    EditorGUI.BeginChangeCheck();
                                    _enableInterstitialAd.boolValue = EditorGUILayout.Toggle(_enableInterstitialAd.boolValue);
                                    if (EditorGUI.EndChangeCheck())
                                        _enableInterstitialAd.serializedObject.ApplyModifiedProperties();
                                }
                                EditorGUILayout.EndHorizontal();
                            }
                            EditorGUI.indentLevel -= 2;

                        }
                    }
                    EditorGUI.indentLevel -= 1;


                    #endregion

                    //------------------------------
                    #region BannerAd

                    EditorGUI.indentLevel += 1;
                    {
                        EditorGUILayout.BeginHorizontal(GUI.skin.box);
                        {
                            string bannerAdLabel = "[" + (!_showBannerAdSettings.boolValue ? "+" : "-") + "] [BannerAd]";
                            GUIContent bannerAdLabelContent = new GUIContent(
                                    bannerAdLabel

                                );

                            if (GUILayout.Button(bannerAdLabelContent, adTypeStyle, GUILayout.Width(EditorGUIUtility.currentViewWidth)))
                            {
                                _showBannerAdSettings.boolValue = !_showBannerAdSettings.boolValue;
                                _showBannerAdSettings.serializedObject.ApplyModifiedProperties();
                            }
                        }
                        EditorGUILayout.EndHorizontal();

                        if (_showBannerAdSettings.boolValue)
                        {
                            EditorGUI.indentLevel += 2;
                            {
                                EditorGUILayout.BeginHorizontal();
                                {
                                    EditorGUILayout.LabelField(_enableBannerAd.displayName, GUILayout.Width(LabelWidth));
                                    EditorGUI.BeginChangeCheck();
                                    _enableBannerAd.boolValue = EditorGUILayout.Toggle(_enableBannerAd.boolValue);
                                    if (EditorGUI.EndChangeCheck())
                                        _enableBannerAd.serializedObject.ApplyModifiedProperties();
                                }
                                EditorGUILayout.EndHorizontal();

                                EditorGUILayout.BeginHorizontal();
                                {
                                    EditorGUILayout.LabelField(_showBannerAdManually.displayName, GUILayout.Width(LabelWidth));
                                    EditorGUI.BeginChangeCheck();
                                    _showBannerAdManually.boolValue = EditorGUILayout.Toggle(_showBannerAdManually.boolValue);
                                    if (EditorGUI.EndChangeCheck())
                                        _showBannerAdManually.serializedObject.ApplyModifiedProperties();
                                }
                                EditorGUILayout.EndHorizontal();
                            }
                            EditorGUI.indentLevel -= 2;

                        }
                    }
                    EditorGUI.indentLevel -= 1;


                    #endregion


                }
                EditorGUI.EndDisabledGroup();

            }

        }

        private void GeneralSettingGUI()
        {
            DrawHeaderGUI("General", ref _generalSettingContent, ref _settingsTitleStyle, ref _showGeneralSettings);

            if (_showGeneralSettings.boolValue) {

                EditorGUI.indentLevel += 1;
                {
                    EditorGUILayout.BeginHorizontal();
                    {
                        EditorGUILayout.LabelField("Reference/Link", GUILayout.Width(LabelWidth + 30));
                        if (GUILayout.Button("Download", _hyperlinkStyle, GUILayout.Width(100))) {
                            Application.OpenURL(_linkForDownload);
                        }
                        if (GUILayout.Button("Documentation", _hyperlinkStyle, GUILayout.Width(100)))
                        {
                            Application.OpenURL(_linkForDocumetation);
                        }
                    }
                    EditorGUILayout.EndHorizontal();


                    EditorGUILayout.BeginHorizontal();
                    {
                        EditorGUILayout.LabelField(_logAnalyticsEvent.displayName, GUILayout.Width(LabelWidth));
                        EditorGUI.BeginChangeCheck();
                        _logAnalyticsEvent.boolValue = EditorGUILayout.Toggle(_logAnalyticsEvent.boolValue);
                        if (EditorGUI.EndChangeCheck())
                            _logAnalyticsEvent.serializedObject.ApplyModifiedProperties();

                    }
                    EditorGUILayout.EndHorizontal();
                }
                EditorGUI.indentLevel -= 1;
            }
        }

        private void LionAdSettingsGUI()
        {

            DrawHeaderGUI((_isLionKitSDKIntegrated.boolValue ? "LionKitAd" : "LionKitAd = LionKit Not Found"), ref _lionAdSettingContent, ref _settingsTitleStyle, ref _showLionAdSetting);

#if APSdk_LionKit

            if (_showLionAdSetting.boolValue) {

                    #region RewardedAd

                EditorGUI.indentLevel += 1;
                {
                    EditorGUILayout.BeginHorizontal(GUI.skin.box);
                    {
                        string rewardedAdLabel = "[" + (!_adShowRewardedAdSettings.boolValue ? "+" : "-") + "] [RewardedAd]";
                        GUIContent rewardedAdLabelContent = new GUIContent(
                                rewardedAdLabel

                            );
                        GUIStyle rewardedAdLabelStyle = new GUIStyle(EditorStyles.boldLabel);
                        rewardedAdLabelStyle.alignment = TextAnchor.MiddleLeft;
                        rewardedAdLabelStyle.padding.left = 18;

                        if (GUILayout.Button(rewardedAdLabelContent, rewardedAdLabelStyle, GUILayout.Width(EditorGUIUtility.currentViewWidth)))
                        {
                            _adShowRewardedAdSettings.boolValue = !_adShowRewardedAdSettings.boolValue;
                            _adShowRewardedAdSettings.serializedObject.ApplyModifiedProperties();
                        }
                    }
                    EditorGUILayout.EndHorizontal();

                    if (_adShowRewardedAdSettings.boolValue)
                    {

                        EditorGUI.indentLevel += 1;
                        {
                            EditorGUILayout.BeginHorizontal();
                            {
                                EditorGUILayout.LabelField(_adEnableRewardedAd.displayName, GUILayout.Width(LabelWidth));
                                EditorGUI.BeginChangeCheck();
                                _adEnableRewardedAd.boolValue = EditorGUILayout.Toggle(_adEnableRewardedAd.boolValue);
                                if (EditorGUI.EndChangeCheck())
                                    _adEnableRewardedAd.serializedObject.ApplyModifiedProperties();
                            }
                            EditorGUILayout.EndHorizontal();
                        }
                        EditorGUI.indentLevel -= 1;

                    }
                }
                EditorGUI.indentLevel -= 1;


                    #endregion

            //------------------------------
                    #region InterstitialAd

                EditorGUI.indentLevel += 1;
                {
                    EditorGUILayout.BeginHorizontal(GUI.skin.box);
                    {
                        string interstitialAdLabel = "[" + (!_adShowInterstitialAdSettings.boolValue ? "+" : "-") + "] [InterstitialAd]";
                        GUIContent interstialAdLabelContent = new GUIContent(
                                interstitialAdLabel

                            );
                        GUIStyle interstiaialAdLabelStyle = new GUIStyle(EditorStyles.boldLabel);
                        interstiaialAdLabelStyle.alignment = TextAnchor.MiddleLeft;
                        interstiaialAdLabelStyle.padding.left = 18;

                        if (GUILayout.Button(interstialAdLabelContent, interstiaialAdLabelStyle, GUILayout.Width(EditorGUIUtility.currentViewWidth)))
                        {
                            _adShowInterstitialAdSettings.boolValue = !_adShowInterstitialAdSettings.boolValue;
                            _adShowInterstitialAdSettings.serializedObject.ApplyModifiedProperties();
                        }
                    }
                    EditorGUILayout.EndHorizontal();

                    if (_adShowInterstitialAdSettings.boolValue)
                    {
                        EditorGUI.indentLevel += 1;
                        {
                            EditorGUILayout.BeginHorizontal();
                            {
                                EditorGUILayout.LabelField(_adEnableInterstitialAd.displayName, GUILayout.Width(LabelWidth));
                                EditorGUI.BeginChangeCheck();
                                _adEnableInterstitialAd.boolValue = EditorGUILayout.Toggle(_adEnableInterstitialAd.boolValue);
                                if (EditorGUI.EndChangeCheck())
                                    _adEnableInterstitialAd.serializedObject.ApplyModifiedProperties();
                            }
                            EditorGUILayout.EndHorizontal();
                        }
                        EditorGUI.indentLevel -= 1;

                    }
                }
                EditorGUI.indentLevel -= 1;


                    #endregion

            //------------------------------
                    #region BannerAd

                EditorGUI.indentLevel += 1;
                {
                    EditorGUILayout.BeginHorizontal(GUI.skin.box);
                    {
                        string bannerAdLabel = "[" + (!_adShowBannerAdSettings.boolValue ? "+" : "-") + "] [BannerAd]";
                        GUIContent bannerAdLabelContent = new GUIContent(
                                bannerAdLabel

                            );
                        GUIStyle bannerAdLabelStyle = new GUIStyle(EditorStyles.boldLabel);
                        bannerAdLabelStyle.alignment = TextAnchor.MiddleLeft;
                        bannerAdLabelStyle.padding.left = 18;

                        if (GUILayout.Button(bannerAdLabelContent, bannerAdLabelStyle, GUILayout.Width(EditorGUIUtility.currentViewWidth)))
                        {
                            _adShowBannerAdSettings.boolValue = !_adShowBannerAdSettings.boolValue;
                            _adShowBannerAdSettings.serializedObject.ApplyModifiedProperties();
                        }
                    }
                    EditorGUILayout.EndHorizontal();

                    if (_adShowBannerAdSettings.boolValue)
                    {
                        EditorGUI.indentLevel += 1;
                        {
                            EditorGUILayout.BeginHorizontal();
                            {
                                EditorGUILayout.LabelField(_adEnableBannerAd.displayName, GUILayout.Width(LabelWidth));
                                EditorGUI.BeginChangeCheck();
                                _adEnableBannerAd.boolValue = EditorGUILayout.Toggle(_adEnableBannerAd.boolValue);
                                if (EditorGUI.EndChangeCheck())
                                    _adEnableBannerAd.serializedObject.ApplyModifiedProperties();
                            }
                            EditorGUILayout.EndHorizontal();

                            EditorGUILayout.BeginHorizontal();
                            {
                                EditorGUILayout.LabelField(_adStartBannerAdManually.displayName, GUILayout.Width(LabelWidth));
                                EditorGUI.BeginChangeCheck();
                                _adStartBannerAdManually.boolValue = EditorGUILayout.Toggle(_adStartBannerAdManually.boolValue);
                                if (EditorGUI.EndChangeCheck())
                                    _adStartBannerAdManually.serializedObject.ApplyModifiedProperties();
                            }
                            EditorGUILayout.EndHorizontal();
                        }
                        EditorGUI.indentLevel -= 1;

                    }
                }
                EditorGUI.indentLevel -= 1;


                    #endregion


            //------------------------------
                    #region CrossPromoAd

                EditorGUI.indentLevel += 1;
                {
                    EditorGUILayout.BeginHorizontal(GUI.skin.box);
                    {
                        string crossPromoAdLabel = "[" + (!_adShowCrossPromoAdSettings.boolValue ? "+" : "-") + "] [CrossPromoAd]--[InDevelopment]";
                        GUIContent crossPromoAdLabelContent = new GUIContent(
                                crossPromoAdLabel

                            );
                        GUIStyle crossPromoAdLabelStyle = new GUIStyle(EditorStyles.boldLabel);
                        crossPromoAdLabelStyle.alignment = TextAnchor.MiddleLeft;
                        crossPromoAdLabelStyle.padding.left = 18;

                        if (GUILayout.Button(crossPromoAdLabelContent, crossPromoAdLabelStyle, GUILayout.Width(EditorGUIUtility.currentViewWidth)))
                        {
                            _adShowCrossPromoAdSettings.boolValue = !_adShowCrossPromoAdSettings.boolValue;
                            _adShowCrossPromoAdSettings.serializedObject.ApplyModifiedProperties();
                        }
                    }
                    EditorGUILayout.EndHorizontal();

                    if (_adShowCrossPromoAdSettings.boolValue)
                    {
                        EditorGUI.indentLevel += 1;
                        {
                            EditorGUILayout.BeginHorizontal();
                            {
                                EditorGUILayout.LabelField(_adEnableCrossPromoAd.displayName, GUILayout.Width(LabelWidth));
                                EditorGUI.BeginChangeCheck();
                                _adEnableCrossPromoAd.boolValue = EditorGUILayout.Toggle(_adEnableCrossPromoAd.boolValue);
                                if (EditorGUI.EndChangeCheck())
                                    _adEnableCrossPromoAd.serializedObject.ApplyModifiedProperties();
                            }
                            EditorGUILayout.EndHorizontal();
                        }
                        EditorGUI.indentLevel -= 1;

                    }
                }
                EditorGUI.indentLevel -= 1;


                    #endregion

            }


#endif


        }

      
        
        private void GameAnalyticsSettingsGUI() {

            string title = string.Format("{0}{1}", "GameAnalytics", _isGameAnalyticsSDKIntegrated.boolValue ? "" : "- SDK Not Found");
            DrawHeaderGUI(title, ref _gameAnalyticsSettingContent, ref _settingsTitleStyle, ref _showGameAnalyticsSetting);

#if APSdk_GameAnalytics

            if (_showGameAnalyticsSetting.boolValue) {

                EditorGUI.indentLevel += 1;

                EditorGUILayout.BeginHorizontal();
                {
                    EditorGUILayout.LabelField(
                        new GUIContent(
                            "EnableGameAnalyticsEvent",
                            "Enable GameAnalytics event"
                            ),
                        GUILayout.Width(LabelWidth));
                    EditorGUI.BeginChangeCheck();
                    _enableGameAnalyticsEvent.boolValue = EditorGUILayout.Toggle(_enableGameAnalyticsEvent.boolValue);
                    if (EditorGUI.EndChangeCheck())
                    {
                        _enableGameAnalyticsEvent.serializedObject.ApplyModifiedProperties();
                    }
                }
                EditorGUILayout.EndHorizontal();

                APSdkEditorModule.DrawHorizontalLine();

                EditorGUILayout.BeginHorizontal();
                {
                    EditorGUILayout.LabelField(_trackProgressionEventOnGA.displayName, GUILayout.Width(LabelWidth));
                    EditorGUI.BeginChangeCheck();
                    _trackProgressionEventOnGA.boolValue = EditorGUILayout.Toggle(_trackProgressionEventOnGA.boolValue);
                    if (EditorGUI.EndChangeCheck())
                    {
                        _trackProgressionEventOnGA.serializedObject.ApplyModifiedProperties();
                    }
                }
                EditorGUILayout.EndHorizontal();

#if APSdk_LionKit
                EditorGUILayout.BeginHorizontal();
                {
                    EditorGUILayout.LabelField(_trackAdEventOnGA.displayName, GUILayout.Width(LabelWidth));
                    EditorGUI.BeginChangeCheck();
                    _trackAdEventOnGA.boolValue = EditorGUILayout.Toggle(_trackAdEventOnGA.boolValue);
                    if (EditorGUI.EndChangeCheck())
                    {
                        _trackAdEventOnGA.serializedObject.ApplyModifiedProperties();
                    }
                }
                EditorGUILayout.EndHorizontal();
#endif



                APSdkEditorModule.DrawHorizontalLine();

                EditorGUILayout.BeginVertical();
                {
                    EditorGUILayout.BeginHorizontal();
                    {
                        EditorGUILayout.LabelField(_defaultWorldIndexOnGameAnalytics.displayName, GUILayout.Width(LabelWidth));
                        EditorGUI.BeginChangeCheck();
                        _defaultWorldIndexOnGameAnalytics.intValue = EditorGUILayout.IntField(_defaultWorldIndexOnGameAnalytics.intValue);
                        if (EditorGUI.EndChangeCheck())
                        {
                            _defaultWorldIndexOnGameAnalytics.serializedObject.ApplyModifiedProperties();
                        }
                    }
                    EditorGUILayout.EndHorizontal();


                }
                EditorGUILayout.EndVertical();

                EditorGUI.indentLevel -= 1;
            }

#endif
        }

        private void FirebaseSettingsGUI() {

            string title = string.Format("{0}{1}", "Firebase", _isFirebaseSDKIntegrated.boolValue ? "" : "- SDK Not Found");
            DrawHeaderGUI(title, ref _firebaseSettingContent, ref _settingsTitleStyle, ref _showFirebaseSetting);

#if APSdk_Firebase
            if (_showFirebaseSetting.boolValue) {

                EditorGUI.indentLevel += 1;
                {
                    EditorGUILayout.BeginVertical();
                    {
                        EditorGUILayout.BeginHorizontal();
                        {
                            EditorGUILayout.LabelField(
                                new GUIContent(
                                "EnableFirebaseEvent",
                                "Enable firebase event"
                                ),
                                GUILayout.Width(LabelWidth));
                            EditorGUI.BeginChangeCheck();
                            _enableFirebaseAnalyticsEvent.boolValue = EditorGUILayout.Toggle(_enableFirebaseAnalyticsEvent.boolValue);
                            if (EditorGUI.EndChangeCheck())
                            {
                                _enableFirebaseAnalyticsEvent.serializedObject.ApplyModifiedProperties();
                            }
                        }
                        EditorGUILayout.EndHorizontal();

                        APSdkEditorModule.DrawHorizontalLine();

#if APSdk_LionKit
                        EditorGUILayout.BeginHorizontal();
                        {
                            EditorGUILayout.LabelField("SubscribeToLionEvent", GUILayout.Width(LabelWidth));
                            EditorGUI.BeginChangeCheck();
                            _subscribeToLionEventOnFirebase.boolValue = EditorGUILayout.Toggle(_subscribeToLionEventOnFirebase.boolValue);
                            if (EditorGUI.EndChangeCheck())
                            {
                                _subscribeToLionEventOnFirebase.serializedObject.ApplyModifiedProperties();
                            }
                        }
                        EditorGUILayout.EndHorizontal();

                        EditorGUILayout.BeginHorizontal();
                        {
                            EditorGUILayout.LabelField("SubscribeToLionEventUA", GUILayout.Width(LabelWidth));
                            EditorGUI.BeginChangeCheck();
                            _subscribeToLionEventUAOnFirebase.boolValue = EditorGUILayout.Toggle(_subscribeToLionEventUAOnFirebase.boolValue);
                            if (EditorGUI.EndChangeCheck())
                            {
                                _subscribeToLionEventUAOnFirebase.serializedObject.ApplyModifiedProperties();
                            }
                        }
                        EditorGUILayout.EndHorizontal();
#else

                        EditorGUILayout.BeginHorizontal();
                        {
                            EditorGUILayout.LabelField(_trackProgressionEventOnFirebase.displayName, GUILayout.Width(_labelWidth));
                            EditorGUI.BeginChangeCheck();
                            _trackProgressionEventOnFirebase.boolValue = EditorGUILayout.Toggle(_trackProgressionEventOnFirebase.boolValue);
                            if (EditorGUI.EndChangeCheck())
                            {
                                _trackProgressionEventOnFirebase.serializedObject.ApplyModifiedProperties();
                            }
                        }
                        EditorGUILayout.EndHorizontal();

                        EditorGUILayout.BeginHorizontal();
                        {
                            EditorGUILayout.LabelField(_trackAdEventOnFirebase.displayName, GUILayout.Width(_labelWidth));
                            EditorGUI.BeginChangeCheck();
                            _trackAdEventOnFirebase.boolValue = EditorGUILayout.Toggle(_trackAdEventOnFirebase.boolValue);
                            if (EditorGUI.EndChangeCheck())
                            {
                                _trackAdEventOnFirebase.serializedObject.ApplyModifiedProperties();
                            }
                        }
                        EditorGUILayout.EndHorizontal();

#endif

                    }
                    EditorGUILayout.EndVertical();
                }
                EditorGUI.indentLevel -= 1;
            }
#endif
                    }

        private void AnalyticsSettingsGUI() {

            DrawHeaderGUI("Analytics", ref _analyticsSettingContent, ref _settingsTitleStyle, ref _showAnalytics);

            if (_showAnalytics.boolValue)
            {
                int numberOfAnalytics = _apSDKConfiguretionInfo.listOfAnalyticsConfiguretion.Count;

                {
                    for (int i = 0; i < numberOfAnalytics; i++)
                    {
                        DrawAnalyticsGUI(_apSDKConfiguretionInfo.listOfAnalyticsConfiguretion[i]);
                    }
                }
            }
        }

        private void AdNetworksSettingsGUI() {

            DrawHeaderGUI("AdNetworks", ref _adNetworkSettingContent, ref _settingsTitleStyle, ref _showAdNetworks);

            if (_showAdNetworks.boolValue) {
                int numberOfAdNetwork = _apSDKConfiguretionInfo.listOfAdConfiguretion.Count;
                
                {
                    for (int i = 0; i < numberOfAdNetwork; i++)
                    {
                        DrawAdNetworkGUI(i, _apSDKConfiguretionInfo.listOfAdConfiguretion[i]);
                    }
                }
                
            }
        }

        private void ABTestSettingsGUI() {

            DrawHeaderGUI("A/B Test", ref _abTestSettingContent, ref _settingsTitleStyle, ref _showABTestSetting);

            if (_showABTestSetting.boolValue) {

                EditorGUI.indentLevel += 1;
                {
                    EditorGUILayout.HelpBox("The following section is under development!", MessageType.Info);
                }
                EditorGUI.indentLevel -= 1;
            }

            
        }

        private void DebuggingSettingsGUI() {

            DrawHeaderGUI("Debugging", ref _debuggingSettingContent, ref _settingsTitleStyle, ref _showDebuggingSettings);

            if (_showDebuggingSettings.boolValue)
            {
                EditorGUI.indentLevel += 1;

                EditorGUILayout.BeginVertical();
                {
                    EditorGUILayout.BeginHorizontal();
                    {
                        EditorGUILayout.LabelField(_maxMediationDebugger.displayName, GUILayout.Width(LabelWidth));
                        EditorGUI.BeginChangeCheck();
                        _maxMediationDebugger.boolValue = EditorGUILayout.Toggle(_maxMediationDebugger.boolValue);
                        if (EditorGUI.EndChangeCheck())
                        {
                            _maxMediationDebugger.serializedObject.ApplyModifiedProperties();
                        }
                    }
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    {
                        EditorGUILayout.LabelField(_showAPSdkLogInConsole.displayName, GUILayout.Width(LabelWidth));
                        EditorGUI.BeginChangeCheck();
                        _showAPSdkLogInConsole.boolValue = EditorGUILayout.Toggle(_showAPSdkLogInConsole.boolValue);
                        if (EditorGUI.EndChangeCheck())
                        {
                            _showAPSdkLogInConsole.serializedObject.ApplyModifiedProperties();
                        }
                    }
                    EditorGUILayout.EndHorizontal();


                    EditorGUILayout.BeginHorizontal(GUI.skin.box);
                    {
                        EditorGUI.BeginChangeCheck();
                        EditorGUILayout.PropertyField(_infoLogColor);
                        if (EditorGUI.EndChangeCheck())
                        {

                            _infoLogColor.serializedObject.ApplyModifiedProperties();
                        }

                        EditorGUI.BeginChangeCheck();
                        EditorGUILayout.PropertyField(_warningLogColor);
                        if (EditorGUI.EndChangeCheck())
                        {
                            _warningLogColor.serializedObject.ApplyModifiedProperties();
                        }

                        EditorGUI.BeginChangeCheck();
                        EditorGUILayout.PropertyField(_errorLogColor);
                        if (EditorGUI.EndChangeCheck())
                        {
                            _errorLogColor.serializedObject.ApplyModifiedProperties();
                        }
                    }
                    EditorGUILayout.EndHorizontal();
                }
                EditorGUILayout.EndVertical();

                EditorGUI.indentLevel -= 1;
            }
        }

#endregion

                    #region Configuretion


        private void FetchAllTheReference() {

                    #region APSdkConfiguretionInfo

            _apSDKConfiguretionInfo = Resources.Load<APSdkConfiguretionInfo>("APSdkConfiguretionInfo");
            _serializedSDKConfiguretionInfo = new SerializedObject(_apSDKConfiguretionInfo);

            _isLionKitSDKIntegrated = _serializedSDKConfiguretionInfo.FindProperty("_isLionKitSDKIntegrated");
            _isFacebookSDKIntegrated = _serializedSDKConfiguretionInfo.FindProperty("_isFacebookSDKIntegrated");
            _isAdjustSDKIntegrated = _serializedSDKConfiguretionInfo.FindProperty("_isAdjustSDKIntegrated");
            _isGameAnalyticsSDKIntegrated = _serializedSDKConfiguretionInfo.FindProperty("_isGameAnalyticsSDKIntegrated");
            _isFirebaseSDKIntegrated = _serializedSDKConfiguretionInfo.FindProperty("_isFirebaseSDKIntegrated");

            _showGeneralSettings = _serializedSDKConfiguretionInfo.FindProperty("_showGeneralSetting");
            _showLionAdSetting = _serializedSDKConfiguretionInfo.FindProperty("_showLionAdSetting");
            _showFacebookSetting = _serializedSDKConfiguretionInfo.FindProperty("_showFacebookSetting");
            _showAdjustSetting = _serializedSDKConfiguretionInfo.FindProperty("_showAdjustSetting");
            _showGameAnalyticsSetting = _serializedSDKConfiguretionInfo.FindProperty("_showGameAnalyticsSetting");
            _showFirebaseSetting = _serializedSDKConfiguretionInfo.FindProperty("_showFirebaseSetting");
            _showAnalytics = _serializedSDKConfiguretionInfo.FindProperty("_showAnalytics");
            _showAdNetworks = _serializedSDKConfiguretionInfo.FindProperty("_showAdNetworks");
            _showABTestSetting = _serializedSDKConfiguretionInfo.FindProperty("_showABTestSetting");
            _showDebuggingSettings = _serializedSDKConfiguretionInfo.FindProperty("_showDebuggingSetting");

            _logAnalyticsEvent = _serializedSDKConfiguretionInfo.FindProperty("logAnalyticsEvent");
            _maxMediationDebugger = _serializedSDKConfiguretionInfo.FindProperty("maxMediationDebugger");

            _showAPSdkLogInConsole = _serializedSDKConfiguretionInfo.FindProperty("showAPSdkLogInConsole");

            _infoLogColor = _serializedSDKConfiguretionInfo.FindProperty("infoLogColor");
            _warningLogColor = _serializedSDKConfiguretionInfo.FindProperty("warningLogColor");
            _errorLogColor = _serializedSDKConfiguretionInfo.FindProperty("errorLogColor");

            _generalSettingContent = new GUIContent(
                        "[" + (!_showGeneralSettings.boolValue ? "+" : "-") + "] General"
                    );
            _lionAdSettingContent = new GUIContent(
                        "[" + (!_showLionAdSetting.boolValue ? "+" : "-") + "] " + (_isLionKitSDKIntegrated.boolValue ? "LionKitAd" : "LionKitAd - LionKit Not Found")
                    );

            _facebookSettingContent = new GUIContent(
                        "[" + (!_showFacebookSetting.boolValue ? "+" : "-") + "] " + (_isFacebookSDKIntegrated.boolValue ? "Facebook" : "Facebook - SDK Not Found")
                    );

            _adjustSettingContent = new GUIContent(
                        "[" + (!_showAdjustSetting.boolValue ? "+" : "-") + "] " + (_isAdjustSDKIntegrated.boolValue? "Adjust" : "Adjust - SDK Not Found")
                    );

            _gameAnalyticsSettingContent = new GUIContent(
                        "[" + (!_showGameAnalyticsSetting.boolValue ? "+" : "-") + "] " + (_isGameAnalyticsSDKIntegrated.boolValue ? "GameAnalytics" : "GameAnalytics - SDK Not Found")
                    );

            _firebaseSettingContent = new GUIContent(
                        "[" + (!_showFirebaseSetting.boolValue ? "+" : "-") + "] " + (_isFirebaseSDKIntegrated.boolValue ? "Firebase" : "Firebase - SDK Not Found")
                    );

            _analyticsSettingContent = new GUIContent(
                        "[" + (!_showAnalytics.boolValue ? "+" : "-") + "] " + "Analytics"
                    );

            _adNetworkSettingContent = new GUIContent(
                        "[" + (!_showAdNetworks.boolValue ? "+" : "-") + "] " + "AdNetwork"
                    );

            _abTestSettingContent = new GUIContent(
                        "[" + (!_showABTestSetting.boolValue ? "+" : "-") + "] A/B Test"
                    );

            _debuggingSettingContent = new GUIContent(
                        "[" + (!_showDebuggingSettings.boolValue ? "+" : "-") + "] Debugging"
                    );

            _settingsTitleStyle = new GUIStyle(EditorStyles.boldLabel);
            _settingsTitleStyle.alignment = TextAnchor.MiddleLeft;

            _hyperlinkStyle = new GUIStyle(EditorStyles.boldLabel);
            _hyperlinkStyle.normal.textColor = new Color(50 / 255.0f, 139 / 255.0f, 217 / 255.0f);
            _hyperlinkStyle.wordWrap = true;
            _hyperlinkStyle.richText = true;

                    #endregion

            //-------------
                    #region LionKitInfo

#if APSdk_LionKit

            _apLionKitInfo = Resources.Load<APLionKitInfo>("LionKit/APLionKitInfo");

            _serializedLionKitInfo = new SerializedObject(_apLionKitInfo);

            _adShowRewardedAdSettings = _serializedLionKitInfo.FindProperty("_showRewardedAdSettings");
            _adShowInterstitialAdSettings = _serializedLionKitInfo.FindProperty("_showInterstitialAdSettings");
            _adShowBannerAdSettings = _serializedLionKitInfo.FindProperty("_showBannerAdSettings");
            _adShowCrossPromoAdSettings = _serializedLionKitInfo.FindProperty("_showCrossPromoAdSettings");

            

            _adEnableRewardedAd = _serializedLionKitInfo.FindProperty("enableRewardedAd");
            _adEnableInterstitialAd = _serializedLionKitInfo.FindProperty("enableInterstitialAd");
            _adEnableBannerAd = _serializedLionKitInfo.FindProperty("enableBannerAd");
            _adEnableCrossPromoAd = _serializedLionKitInfo.FindProperty("enableCrossPromoAd");
            _adStartBannerAdManually = _serializedLionKitInfo.FindProperty("startBannerAdManually");

#endif



                    #endregion

            

            //-------------
                    

                    #region GameAnalyticsInfo

#if APSdk_GameAnalytics

            _apGameAnalyticsInfo = Resources.Load<APGameAnalyticsInfo>("GameAnalytics/APGameAnalyticsInfo");

            _serializedGameAnalyticsInfo = new SerializedObject(_apGameAnalyticsInfo);

            _enableGameAnalyticsEvent   = _serializedGameAnalyticsInfo.FindProperty("_enableGameAnalyticsEvent");

            _trackProgressionEventOnGA  = _serializedGameAnalyticsInfo.FindProperty("_trackProgressionEvent");
            _trackAdEventOnGA           = _serializedGameAnalyticsInfo.FindProperty("_trackAdEvent");

            _defaultWorldIndexOnGameAnalytics = _serializedGameAnalyticsInfo.FindProperty("_defaultWorldIndex");

#endif

                    #endregion

#if APSdk_Firebase

            _apFirebaseInfo = Resources.Load<APFirebaseInfo>("Firebase/APFirebaseInfo");
            _serializedFirebaseInfo = new SerializedObject(_apFirebaseInfo);

            _enableFirebaseAnalyticsEvent = _serializedFirebaseInfo.FindProperty("_enableFirebaseAnalyticsEvent");

            _trackProgressionEventOnFirebase = _serializedFirebaseInfo.FindProperty("_trackProgressionEvent");
            _trackAdEventOnFirebase = _serializedFirebaseInfo.FindProperty("_trackAdEvent");

            _subscribeToLionEventOnFirebase = _serializedFirebaseInfo.FindProperty("_subscribeToLionEvent");
            _subscribeToLionEventUAOnFirebase = _serializedFirebaseInfo.FindProperty("_subscribeToLionEventUA");
#endif

            APSdkAssetPostProcessor.LookForSDK();

        }

                    #endregion

    }


#endif


                }

