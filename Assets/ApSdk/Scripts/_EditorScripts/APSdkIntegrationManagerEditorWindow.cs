
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
        #region Private Variables   :   General

        private static EditorWindow _reference;
        private const float _labelWidth = 200;

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

        #region Private Variables   :   FacebookInfo

#if APSdk_Facebook


        private APFacebookInfo _apFacebookInfo;
        private SerializedObject _serializedFacebookInfo;

        private SerializedProperty _facebookAppName;
        private SerializedProperty _facebookAppId;
        private SerializedProperty _logFacebookEvent;

#endif

        #endregion

        #region Private Variables   :   APAdjustInfo

#if APSdk_Adjust

        private APAdjustInfo _apAdjustInfo;
        private SerializedObject _serializedAdjustInfo;

        private SerializedProperty _showAdjustBasicInfo;
        private SerializedProperty _showAdjustAdvancedInfo;

        private SerializedProperty _logAdjustEvent;
        private SerializedProperty _logAdjustEventUA;

        private SerializedProperty _adjustAppTokenForAndroid;
        private SerializedProperty _adjustAppTokenForIOS;

        private SerializedProperty _adjustEnvironment;

        private SerializedProperty _adjustLogLevel;

        private SerializedProperty _adjustStartDelay;
        private SerializedProperty _adjustStartManually;

        private SerializedProperty _adjustEventBuffering;
        private SerializedProperty _adjustSendInBackground;
        private SerializedProperty _adjustLaunchDeferredDeeplink;

#endif

        #endregion

        #region Private Variables   :   GameAnalyticsInfo

#if APSdk_GameAnalytics

        private APGameAnalyticsInfo _apGameAnalyticsInfo;
        private SerializedObject    _serializedGameAnalyticsInfo;

        private SerializedProperty _defaultWorldIndexOnGameAnalytics;
        private SerializedProperty _trackProgressionEventOnGA;
#if APSdk_LionKit
        private SerializedProperty _trackAdEventOnGA;
#endif

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

                    EditorGUILayout.Space();
                    LionAdSettingsGUI();

                    EditorGUILayout.Space();
                    FacebookSettingGUI();

                    EditorGUILayout.Space();
                    AdjustSettingsGUI();

                    EditorGUILayout.Space();
                    GameAnalyticsSettingsGUI();

                    EditorGUILayout.Space();
                    FirebaseSettingsGUI();

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

        private void GeneralSettingGUI()
        {
            DrawHeaderGUI("General", ref _generalSettingContent, ref _settingsTitleStyle, ref _showGeneralSettings);

            if (_showGeneralSettings.boolValue) {

                EditorGUI.indentLevel += 1;
                {
                    EditorGUILayout.BeginHorizontal();
                    {
                        EditorGUILayout.LabelField("Reference/Link", GUILayout.Width(_labelWidth + 30));
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
                        EditorGUILayout.LabelField(_logAnalyticsEvent.displayName, GUILayout.Width(_labelWidth));
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
                                EditorGUILayout.LabelField(_adEnableRewardedAd.displayName, GUILayout.Width(_labelWidth));
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
                                EditorGUILayout.LabelField(_adEnableInterstitialAd.displayName, GUILayout.Width(_labelWidth));
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
                                EditorGUILayout.LabelField(_adEnableBannerAd.displayName, GUILayout.Width(_labelWidth));
                                EditorGUI.BeginChangeCheck();
                                _adEnableBannerAd.boolValue = EditorGUILayout.Toggle(_adEnableBannerAd.boolValue);
                                if (EditorGUI.EndChangeCheck())
                                    _adEnableBannerAd.serializedObject.ApplyModifiedProperties();
                            }
                            EditorGUILayout.EndHorizontal();

                            EditorGUILayout.BeginHorizontal();
                            {
                                EditorGUILayout.LabelField(_adStartBannerAdManually.displayName, GUILayout.Width(_labelWidth));
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
                                EditorGUILayout.LabelField(_adEnableCrossPromoAd.displayName, GUILayout.Width(_labelWidth));
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

        private void FacebookSettingGUI() {

            string title = string.Format("{0}{1}", "Facebook", _isFacebookSDKIntegrated.boolValue ? "" : "- SDK Not Found");
            DrawHeaderGUI(title, ref _facebookSettingContent, ref _settingsTitleStyle, ref _showFacebookSetting);

#if APSdk_Facebook

            if (_showFacebookSetting.boolValue)
            {

                EditorGUI.indentLevel += 1;
                {
                    EditorGUILayout.BeginHorizontal();
                    {
                        EditorGUILayout.LabelField(_facebookAppName.displayName, GUILayout.Width(_labelWidth));
                        EditorGUI.BeginChangeCheck();
                        _facebookAppName.stringValue = EditorGUILayout.TextField(_facebookAppName.stringValue);
                        if (EditorGUI.EndChangeCheck())
                        {
                            _facebookAppName.serializedObject.ApplyModifiedProperties();
                            Facebook.Unity.Settings.FacebookSettings.AppLabels = new List<string>() { _facebookAppName.stringValue };
                        }

                    }
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    {
                        EditorGUILayout.LabelField(_facebookAppId.displayName, GUILayout.Width(_labelWidth));
                        EditorGUI.BeginChangeCheck();
                        _facebookAppId.stringValue = EditorGUILayout.TextField(_facebookAppId.stringValue);
                        if (EditorGUI.EndChangeCheck())
                        {

                            _facebookAppId.serializedObject.ApplyModifiedProperties();
                            Facebook.Unity.Settings.FacebookSettings.AppIds = new List<string>() { _facebookAppId.stringValue };
                        }


                    }
                    EditorGUILayout.EndHorizontal();


                    EditorGUILayout.BeginHorizontal();
                    {
                        string logFacebookEventTitle = _logFacebookEvent.displayName;
#if APSdk_LionKit
                        logFacebookEventTitle = "SubscribeToLionEvent";
#endif
                        EditorGUILayout.LabelField(logFacebookEventTitle, GUILayout.Width(_labelWidth));
                        EditorGUI.BeginChangeCheck();
                        _logFacebookEvent.boolValue = EditorGUILayout.Toggle(_logFacebookEvent.boolValue);
                        if (EditorGUI.EndChangeCheck())
                            _logFacebookEvent.serializedObject.ApplyModifiedProperties();

                    }
                    EditorGUILayout.EndHorizontal();

                }
                EditorGUI.indentLevel -= 1;
            }

#endif

        }

        private void AdjustSettingsGUI()
        {
            string title = string.Format("{0}{1}", "Adjust", _isAdjustSDKIntegrated.boolValue ? "" : "- SDK Not Found");
            DrawHeaderGUI(title, ref _adjustSettingContent, ref _settingsTitleStyle, ref _showAdjustSetting);

#if APSdk_Adjust

    if (_showAdjustSetting.boolValue)
            {
                EditorGUI.indentLevel += 1;
                {
                    EditorGUILayout.BeginHorizontal();
                    {
                        EditorGUILayout.LabelField(_logAdjustEvent.displayName, GUILayout.Width(_labelWidth));
                        EditorGUI.BeginChangeCheck();
                        _logAdjustEvent.boolValue = EditorGUILayout.Toggle(_logAdjustEvent.boolValue);
                        if (EditorGUI.EndChangeCheck())
                            _logAdjustEvent.serializedObject.ApplyModifiedProperties();
                    }
                    EditorGUILayout.EndHorizontal();
#if APSdk_LionKit
                    EditorGUILayout.BeginHorizontal();
                    {
                        string logAdjustEventTitle = _logAdjustEvent.displayName;
#if APSdk_LionKit
                        logAdjustEventTitle = "SubscribeToLionEvent";
#endif

                        EditorGUILayout.LabelField(logAdjustEventTitle, GUILayout.Width(_labelWidth));
                        EditorGUI.BeginChangeCheck();
                        _logAdjustEventUA.boolValue = EditorGUILayout.Toggle(_logAdjustEventUA.boolValue);
                        if (EditorGUI.EndChangeCheck())
                            _logAdjustEventUA.serializedObject.ApplyModifiedProperties();
                    }
                    EditorGUILayout.EndHorizontal();


#else
                    EditorGUILayout.HelpBox("To track UA event, you need to integrated 'LionKit' to your project", MessageType.Info);
#endif

                    #region Settings    :   Basic

                    EditorGUI.indentLevel += 1;
                    {
                        EditorGUILayout.BeginHorizontal(GUI.skin.box);
                        {
                            string basicLabel = "[" + (!_showAdjustBasicInfo.boolValue ? "+" : "-") + "] [Settings : Basic]";
                            GUIContent basicLabelContent = new GUIContent(
                                    basicLabel

                                );
                            GUIStyle basicLabelStyle = new GUIStyle(EditorStyles.boldLabel);
                            basicLabelStyle.alignment = TextAnchor.MiddleLeft;
                            basicLabelStyle.padding.left = 28;

                            if (GUILayout.Button(basicLabelContent, basicLabelStyle, GUILayout.Width(EditorGUIUtility.currentViewWidth)))
                            {
                                _showAdjustBasicInfo.boolValue = !_showAdjustBasicInfo.boolValue;
                                _showAdjustBasicInfo.serializedObject.ApplyModifiedProperties();
                            }
                        }
                        EditorGUILayout.EndHorizontal();

                        if (_showAdjustBasicInfo.boolValue)
                        {

                            EditorGUILayout.BeginVertical(GUI.skin.box);
                            {
                                EditorGUILayout.BeginHorizontal();
                                {
                                    EditorGUILayout.LabelField(_adjustAppTokenForAndroid.displayName, GUILayout.Width(_labelWidth));
                                    EditorGUI.BeginChangeCheck();
                                    _adjustAppTokenForAndroid.stringValue = EditorGUILayout.TextField(_adjustAppTokenForAndroid.stringValue);
                                    if (EditorGUI.EndChangeCheck())
                                        _adjustAppTokenForAndroid.serializedObject.ApplyModifiedProperties();
                                }
                                EditorGUILayout.EndHorizontal();

                                EditorGUILayout.BeginHorizontal();
                                {
                                    EditorGUILayout.LabelField(_adjustAppTokenForIOS.displayName, GUILayout.Width(_labelWidth));
                                    EditorGUI.BeginChangeCheck();
                                    _adjustAppTokenForIOS.stringValue = EditorGUILayout.TextField(_adjustAppTokenForIOS.stringValue);
                                    if (EditorGUI.EndChangeCheck())
                                        _adjustAppTokenForIOS.serializedObject.ApplyModifiedProperties();
                                }
                                EditorGUILayout.EndHorizontal();

                                EditorGUILayout.BeginHorizontal();
                                {
                                    EditorGUILayout.LabelField(_adjustEnvironment.displayName, GUILayout.Width(_labelWidth));
                                    EditorGUI.BeginChangeCheck();
                                    _adjustEnvironment.enumValueIndex = (int)((AdjustEnvironment)EditorGUILayout.EnumPopup(_apAdjustInfo.environment));
                                    if (EditorGUI.EndChangeCheck())
                                    {
                                        _adjustEnvironment.serializedObject.ApplyModifiedProperties();
                                    }
                                }
                                EditorGUILayout.EndHorizontal();
                            }
                            EditorGUILayout.EndVertical();

                        }
                    }
                    EditorGUI.indentLevel -= 1;

                    #endregion

                    //-------------
                    #region Settings    :   Advance

                    EditorGUI.indentLevel += 1;
                    {
                        EditorGUILayout.BeginHorizontal(GUI.skin.box);
                        {
                            string advanceLabel = "[" + (!_showAdjustAdvancedInfo.boolValue ? "+" : "-") + "] [Settings : Advance]";
                            GUIContent advanceLabelContent = new GUIContent(
                                    advanceLabel

                                );
                            GUIStyle advanceLabelStyle = new GUIStyle(EditorStyles.boldLabel);
                            advanceLabelStyle.alignment = TextAnchor.MiddleLeft;
                            advanceLabelStyle.padding.left = 28;

                            if (GUILayout.Button(advanceLabelContent, advanceLabelStyle, GUILayout.Width(EditorGUIUtility.currentViewWidth)))
                            {
                                _showAdjustAdvancedInfo.boolValue = !_showAdjustAdvancedInfo.boolValue;
                                _showAdjustAdvancedInfo.serializedObject.ApplyModifiedProperties();
                            }
                        }
                        EditorGUILayout.EndHorizontal();

                        EditorGUILayout.BeginVertical(GUI.skin.box);
                        {
                            if (_showAdjustAdvancedInfo.boolValue) {

                                EditorGUILayout.BeginHorizontal();
                                {
                                    EditorGUILayout.LabelField(_adjustLogLevel.displayName, GUILayout.Width(_labelWidth));
                                    EditorGUI.BeginChangeCheck();
                                    _adjustLogLevel.enumValueIndex = ((int)((AdjustEnvironment)EditorGUILayout.EnumPopup(_apAdjustInfo.logLevel))) - 1;
                                    if (EditorGUI.EndChangeCheck())
                                    {
                                        _adjustLogLevel.serializedObject.ApplyModifiedProperties();
                                    }
                                }
                                EditorGUILayout.EndHorizontal();



                                EditorGUILayout.BeginHorizontal();
                                {
                                    EditorGUILayout.LabelField(_adjustStartDelay.displayName, GUILayout.Width(_labelWidth));
                                    EditorGUI.BeginChangeCheck();
                                    _adjustStartDelay.floatValue = EditorGUILayout.FloatField(_adjustStartDelay.floatValue);
                                    if (EditorGUI.EndChangeCheck())
                                        _adjustStartDelay.serializedObject.ApplyModifiedProperties();
                                }
                                EditorGUILayout.EndHorizontal();



                                EditorGUILayout.BeginHorizontal();
                                {
                                    EditorGUILayout.LabelField(_adjustStartManually.displayName, GUILayout.Width(_labelWidth));
                                    EditorGUI.BeginChangeCheck();
                                    _adjustStartManually.boolValue = EditorGUILayout.Toggle(_adjustStartManually.boolValue);
                                    if (EditorGUI.EndChangeCheck())
                                        _adjustStartManually.serializedObject.ApplyModifiedProperties();
                                }
                                EditorGUILayout.EndHorizontal();


                                EditorGUILayout.Space();
                                EditorGUILayout.BeginHorizontal();
                                {
                                    EditorGUILayout.LabelField(_adjustEventBuffering.displayName, GUILayout.Width(_labelWidth));
                                    EditorGUI.BeginChangeCheck();
                                    _adjustEventBuffering.boolValue = EditorGUILayout.Toggle(_adjustEventBuffering.boolValue);
                                    if (EditorGUI.EndChangeCheck())
                                        _adjustEventBuffering.serializedObject.ApplyModifiedProperties();
                                }
                                EditorGUILayout.EndHorizontal();



                                EditorGUILayout.BeginHorizontal();
                                {
                                    EditorGUILayout.LabelField(_adjustSendInBackground.displayName, GUILayout.Width(_labelWidth));
                                    EditorGUI.BeginChangeCheck();
                                    _adjustSendInBackground.boolValue = EditorGUILayout.Toggle(_adjustSendInBackground.boolValue);
                                    if (EditorGUI.EndChangeCheck())
                                        _adjustSendInBackground.serializedObject.ApplyModifiedProperties();
                                }
                                EditorGUILayout.EndHorizontal();



                                EditorGUILayout.BeginHorizontal();
                                {
                                    EditorGUILayout.LabelField(_adjustLaunchDeferredDeeplink.displayName, GUILayout.Width(_labelWidth));
                                    EditorGUI.BeginChangeCheck();
                                    _adjustLaunchDeferredDeeplink.boolValue = EditorGUILayout.Toggle(_adjustLaunchDeferredDeeplink.boolValue);
                                    if (EditorGUI.EndChangeCheck())
                                        _adjustLaunchDeferredDeeplink.serializedObject.ApplyModifiedProperties();
                                }
                                EditorGUILayout.EndHorizontal();
                            }
                        }
                        EditorGUILayout.EndVertical();
                    }
                    EditorGUI.indentLevel -= 1;


                    #endregion
                }
                EditorGUI.indentLevel -= 1;
            }


#endif


                }

        private void GameAnalyticsSettingsGUI() {

            string title = string.Format("{0}{1}", "GameAnalytics", _isGameAnalyticsSDKIntegrated.boolValue ? "" : "- SDK Not Found");
            DrawHeaderGUI(title, ref _gameAnalyticsSettingContent, ref _settingsTitleStyle, ref _showGameAnalyticsSetting);

#if APSdk_GameAnalytics

            if (_showGameAnalyticsSetting.boolValue) {

                EditorGUI.indentLevel += 1;

                EditorGUILayout.BeginVertical();
                {
                    EditorGUILayout.BeginHorizontal();
                    {
                        EditorGUILayout.LabelField(_defaultWorldIndexOnGameAnalytics.displayName, GUILayout.Width(_labelWidth));
                        EditorGUI.BeginChangeCheck();
                        _defaultWorldIndexOnGameAnalytics.intValue = EditorGUILayout.IntField(_defaultWorldIndexOnGameAnalytics.intValue);
                        if (EditorGUI.EndChangeCheck())
                        {
                            _defaultWorldIndexOnGameAnalytics.serializedObject.ApplyModifiedProperties();
                        }
                    }
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    {
                        EditorGUILayout.LabelField(_trackProgressionEventOnGA.displayName, GUILayout.Width(_labelWidth));
                        EditorGUI.BeginChangeCheck();
                        _trackProgressionEventOnGA.boolValue = EditorGUILayout.Toggle(_trackProgressionEventOnGA.boolValue);
                        if (EditorGUI.EndChangeCheck())
                        {
                            _trackProgressionEventOnGA.serializedObject.ApplyModifiedProperties();
                        }
                    }
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    {
                        EditorGUILayout.LabelField(_trackAdEventOnGA.displayName, GUILayout.Width(_labelWidth));
                        EditorGUI.BeginChangeCheck();
                        _trackAdEventOnGA.boolValue = EditorGUILayout.Toggle(_trackAdEventOnGA.boolValue);
                        if (EditorGUI.EndChangeCheck())
                        {
                            _trackAdEventOnGA.serializedObject.ApplyModifiedProperties();
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

            string title = string.Format("{0}{1}", "Firebase", _isGameAnalyticsSDKIntegrated.boolValue ? "" : "- SDK Not Found");
            DrawHeaderGUI(title, ref _firebaseSettingContent, ref _settingsTitleStyle, ref _showFirebaseSetting);

#if APSdk_Firebase
            if (_showFirebaseSetting.boolValue) {

                EditorGUI.indentLevel += 1;
                {
                    EditorGUILayout.BeginVertical();
                    {
                        EditorGUILayout.BeginHorizontal();
                        {
                            EditorGUILayout.LabelField("EnableFirebaseEvent", GUILayout.Width(_labelWidth));
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
                            EditorGUILayout.LabelField("SubscribeToLionEvent", GUILayout.Width(_labelWidth));
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
                            EditorGUILayout.LabelField("SubscribeToLionEventUA", GUILayout.Width(_labelWidth));
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
                        EditorGUILayout.LabelField(_maxMediationDebugger.displayName, GUILayout.Width(_labelWidth));
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
                        EditorGUILayout.LabelField(_showAPSdkLogInConsole.displayName, GUILayout.Width(_labelWidth));
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
            _showFirebaseSetting = _serializedSDKConfiguretionInfo.FindProperty("_showGameAnalyticsSetting");
            _showABTestSetting = _serializedSDKConfiguretionInfo.FindProperty("_showFirebaseSetting");
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
            #region FacebookInfo

#if APSdk_Facebook
            _apFacebookInfo = Resources.Load<APFacebookInfo>("Facebook/APFacebookInfo");

            _serializedFacebookInfo = new SerializedObject(_apFacebookInfo);

            _facebookAppName = _serializedFacebookInfo.FindProperty("appName");
            _facebookAppId = _serializedFacebookInfo.FindProperty("appId");
            _logFacebookEvent = _serializedFacebookInfo.FindProperty("logFacebookEvent");
#endif

            #endregion

            //-------------
            #region AdjustInfo

#if APSdk_Adjust

            _apAdjustInfo = Resources.Load<APAdjustInfo>("Adjust/APAdjustInfo");

            _serializedAdjustInfo = new SerializedObject(_apAdjustInfo);


            _showAdjustBasicInfo = _serializedAdjustInfo.FindProperty("_showBasicInfo");
            _showAdjustAdvancedInfo = _serializedAdjustInfo.FindProperty("_showAdvancedInfo");

            _logAdjustEvent = _serializedAdjustInfo.FindProperty("logAdjustEvent");
            _logAdjustEventUA = _serializedAdjustInfo.FindProperty("logAdjustEventUA");

            _adjustAppTokenForAndroid = _serializedAdjustInfo.FindProperty("appTokenForAndroid");
            _adjustAppTokenForIOS = _serializedAdjustInfo.FindProperty("appTokenForIOS");

            _adjustEnvironment = _serializedAdjustInfo.FindProperty("environment");

            _adjustLogLevel = _serializedAdjustInfo.FindProperty("logLevel");
            _adjustStartManually = _serializedAdjustInfo.FindProperty("startManually");
            _adjustStartDelay = _serializedAdjustInfo.FindProperty("startDelay");
            _adjustEventBuffering = _serializedAdjustInfo.FindProperty("eventBuffering");
            _adjustSendInBackground = _serializedAdjustInfo.FindProperty("sendInBackground");
            _adjustLaunchDeferredDeeplink = _serializedAdjustInfo.FindProperty("launchDeferredDeeplink");

#endif



            #endregion

            #region GameAnalyticsInfo

#if APSdk_GameAnalytics

            _apGameAnalyticsInfo = Resources.Load<APGameAnalyticsInfo>("GameAnalytics/APGameAnalyticsInfo");

            _serializedGameAnalyticsInfo = new SerializedObject(_apGameAnalyticsInfo);

            _defaultWorldIndexOnGameAnalytics = _serializedGameAnalyticsInfo.FindProperty("_defaultWorldIndex");
            _trackProgressionEventOnGA              = _serializedGameAnalyticsInfo.FindProperty("_trackProgressionEvent");
            _trackAdEventOnGA = _serializedGameAnalyticsInfo.FindProperty("_trackAdEvent");

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

