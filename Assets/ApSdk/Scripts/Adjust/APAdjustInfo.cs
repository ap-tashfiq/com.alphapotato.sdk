#if APSdk_Adjust

namespace APSdk
{
    using UnityEngine;
    using com.adjust.sdk;

    //[CreateAssetMenu(fileName = "APAdjustInfo", menuName = "APSdk/APAdjustInfo")]
    public class APAdjustInfo : ScriptableObject
    {
        #region Public Variables

#if UNITY_EDITOR

        [SerializeField] private bool _showBasicInfo;
        [SerializeField] private bool _showAdvancedInfo;

#endif

        public string appToken
        {
            get
            {
#if UNITY_ANDROID
                return appTokenForAndroid;
#elif UNITY_IOS
                return appTokenForIOS;
#else
                return "invalid_platform";
#endif
            }
        }

        public bool logAdjustEvent = false;
        public bool logAdjustEventUA = false;

        public string appTokenForAndroid;
        public string appTokenForIOS;
        public AdjustEnvironment environment = AdjustEnvironment.Sandbox;

        public AdjustLogLevel logLevel = AdjustLogLevel.Suppress;
        public float startDelay = 0;
        public bool startManually = true;
        public bool eventBuffering;
        public bool sendInBackground;
        public bool launchDeferredDeeplink = true;


        #endregion


    }
}

#endif



