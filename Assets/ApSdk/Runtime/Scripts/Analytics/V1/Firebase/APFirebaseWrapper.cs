﻿#if APSdk_Firebase

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using APSdk;
using Firebase;
using Firebase.Analytics;

[DefaultExecutionOrder(APSdkConstant.EXECUTION_ORDER_Firebase)]
public class APFirebaseWrapper : MonoBehaviour
{
    #region Public Variables

    public static   APFirebaseWrapper Instance;

    #endregion

    #region Private Variables

    private APSdkConfiguretionInfo _apSdkConfiguretionInfo;
    private APFirebaseInfo _apFirebaseInfo;

    #endregion

    #region Configuretion

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void OnGameStart()
    {
        if (Instance == null)
        {

            GameObject newAPFirebaseWrapper = new GameObject("APFirebaseWrapper");
            Instance = newAPFirebaseWrapper.AddComponent<APFirebaseWrapper>();

            DontDestroyOnLoad(newAPFirebaseWrapper);
        }
    }

    #endregion

    #region Public Callback

    public void Initialize(APSdkConfiguretionInfo apSdkConfiguretionInfo, UnityAction OnInitialized = null)
    {
        _apSdkConfiguretionInfo = apSdkConfiguretionInfo;
        _apFirebaseInfo = Resources.Load<APFirebaseInfo>("Firebase/APFirebaseInfo");

        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            var dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                // subscribe to firebase events
                // subscribe here so avoid error if dependency check fails
                
                Debug.Log("Firebase Initialized");
                OnInitialized?.Invoke();
            }
            else
            {
                Debug.LogError($"Firebase: Could not resolve all Firebase dependencies: {dependencyStatus}");
            }
        });
    }


    public void LogFirebaseEvent(string eventName) {

        if (_apFirebaseInfo.IsFirebaseAnalyticsEventEnabled) {

            FirebaseAnalytics.LogEvent(
               eventName);
        }
        
    }

    public void LogFirebaseEvent(string eventName, string parameName, string paramValue) {

        if (_apSdkConfiguretionInfo.logAnalyticsEvent && _apFirebaseInfo.IsFirebaseAnalyticsEventEnabled)
        {
            FirebaseAnalytics.LogEvent(
                    eventName,
                    parameName,
                    paramValue
                );
        }
    }

    public void LogFirebaseEvent(string eventName, List<Parameter> parameter) {

        if (_apSdkConfiguretionInfo.logAnalyticsEvent && _apFirebaseInfo.IsFirebaseAnalyticsEventEnabled) {

            FirebaseAnalytics.LogEvent(
                eventName,
                parameter.ToArray()
            );
        } 
    }

    public void ProgressionEvent(string eventName, string parameName, string paramValue)
    {

        if (_apFirebaseInfo.IsTrackingProgressionEvent)
        {
            LogFirebaseEvent(eventName, parameName, paramValue);
        }
        else
        {
            APSdkLogger.LogWarning("'ProgressionEvent' is disabled for 'FirebaseSDK'");
        }
    }

    public void AdEvent(string eventName, string parameName, string paramValue)
    {

        if (_apFirebaseInfo.IsTrackingAdEvent)
        {
            LogFirebaseEvent(eventName, parameName, paramValue);
        }
        else
        {
            APSdkLogger.LogWarning("'AdEvent' is disabled for 'FirebaseSDK'");
        }
    }

    #endregion
}

#endif

