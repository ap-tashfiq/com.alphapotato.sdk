#if APSdk_GameAnalytics

namespace APSdk
{
    using UnityEngine;

    //[CreateAssetMenu(fileName = "APGameAnalyticsInfo", menuName = "GameAnalytics/APGameAnalyticsInfo")]
    public class APGameAnalyticsInfo : ScriptableObject
    {
        #region Public Variables

        public bool IsGameAnalyticsEventEnabled { get { return _enableGameAnalyticsEvent; } }

        public bool TrackProgressionEvent { get { return _trackProgressionEvent; } }
        public bool TrackAdEvent { get { return _trackAdEvent; } }

        public int DefaultWorldIndex { get { return _defaultWorldIndex; } }



        #endregion

        #region Private Variables

        [SerializeField] private bool _enableGameAnalyticsEvent = false;

        [SerializeField] private bool _trackProgressionEvent = false;
        [SerializeField] private bool _trackAdEvent = false;

        [SerializeField] private int _defaultWorldIndex = 1;
        

    #endregion
    }
}

#endif


