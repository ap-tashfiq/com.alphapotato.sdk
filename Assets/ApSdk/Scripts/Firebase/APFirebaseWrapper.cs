#if APSdk_Firebase

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

    public void Initialize(UnityAction OnInitialized = null)
    {
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

        if (_apFirebaseInfo.TrackEvent) {

            FirebaseAnalytics.LogEvent(
               eventName);
        }
        
    }

    public void LogFirebaseEvent(string eventName, List<Parameter> parameter) {

        if (_apFirebaseInfo.TrackEvent) {

            FirebaseAnalytics.LogEvent(
                eventName,
                parameter.ToArray()
            );
        } 
    }

    #endregion
}

#endif


