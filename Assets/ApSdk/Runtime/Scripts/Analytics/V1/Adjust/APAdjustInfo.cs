#if APSdk_Adjust

namespace APSdk
{
    using com.adjust.sdk;
    using UnityEngine;

    //[CreateAssetMenu(fileName = "APAdjustInfo", menuName = "APSdk/APAdjustInfo")]
    public class APAdjustInfo : ScriptableObject
    {
        #region Public Variables

#if UNITY_EDITOR

        [SerializeField] private bool _showBasicInfo;
        [SerializeField] private bool _showAdvancedInfo;

#endif

        public bool IsAdjustEventEnabled { get { return _enableAdjustEvent; } }

        public bool IsTrackingProgressionEvent { get { return _trackProgressionEvent; } }
        public bool IsTrackingAdEvent { get { return _trackAdEvent; } }

        public bool IsSubscribedToLionEvent { get { return _subscribeToLionEvent; } }
        public bool IsSubscribedToLionEventUA { get { return _subscribeToLionEventUA; } }

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

        [SerializeField] private bool _enableAdjustEvent = false;

        [SerializeField] private bool _trackProgressionEvent = false;
        [SerializeField] private bool _trackAdEvent = false;

        [SerializeField] private bool _subscribeToLionEvent = false;
        [SerializeField] private bool _subscribeToLionEventUA = false;

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


    }
}

#endif



