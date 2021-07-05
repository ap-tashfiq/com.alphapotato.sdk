
#if APSdk_Facebook

namespace APSdk
{
    using UnityEngine;

    //[CreateAssetMenu(fileName = "APFacebookInfo", menuName = "APSdk/APFacebookInfo")]
    public class APFacebookInfo : ScriptableObject
    {
        #region Public Variables

        public bool IsFacebookEventEnabled { get { return _enableFacebookEvent; } }

        public bool IsTrackingProgressionEvent { get { return _trackProgressionEvent; } }
        public bool IsTrackingAdEvent { get { return _trackAdEvent; } }

        public bool IsSubscribedToLionEvent { get { return _subscribeToLionEvent; } }
        public bool IsSubscribedToLionEventUA { get { return _subscribeToLionEventUA; } }

        #endregion

        #region Private Variables

        [SerializeField] private string _appName;
        [SerializeField] private string _appId;

        [SerializeField] private bool   _enableFacebookEvent = false;

        [SerializeField] private bool _trackProgressionEvent = false;
        [SerializeField] private bool _trackAdEvent = false;

        [SerializeField] private bool _subscribeToLionEvent = false;
        [SerializeField] private bool _subscribeToLionEventUA = false;

        #endregion
    }
}

#endif




