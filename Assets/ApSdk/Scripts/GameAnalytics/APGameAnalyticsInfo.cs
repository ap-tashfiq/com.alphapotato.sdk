#if APSdk_GameAnalytics

namespace APSdk
{
    using UnityEngine;

    //[CreateAssetMenu(fileName = "APGameAnalyticsInfo", menuName = "GameAnalytics/APGameAnalyticsInfo")]
    public class APGameAnalyticsInfo : ScriptableObject
    {
        #region Public Variables

        public int DefaultWorldIndex { get { return _defaultWorldIndex; } }
        public bool TrackProgressionEvent { get { return _trackProgressionEvent; } }
        public bool TrackAdEvent { get { return _trackAdEvent; } }


        #endregion

        #region Private Variables
        [SerializeField] private int _defaultWorldIndex = 1;
        [SerializeField] private bool _trackProgressionEvent = false;
        [SerializeField] private bool _trackAdEvent = false;

    #endregion
    }
}

#endif


