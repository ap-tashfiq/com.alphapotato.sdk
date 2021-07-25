
#if APSdk_Firebase
using UnityEngine;

//[CreateAssetMenu(fileName = "APFirebaseInfo", menuName = "Firebase/APFirebaseInfo")]
public class APFirebaseInfo : ScriptableObject
{
    #region Public Variables

    public bool IsFirebaseAnalyticsEventEnabled { get { return _enableFirebaseAnalyticsEvent; } }

    public bool IsTrackingProgressionEvent { get { return _trackProgressionEvent; } }
    public bool IsTrackingAdEvent { get { return _trackAdEvent; } }

    public bool IsSubscribedToLionEvent { get { return _subscribeToLionEvent; } }
    public bool IsSubscribedToLionEventUA { get { return _subscribeToLionEventUA; } }



    #endregion

    #region Private Variables

    [SerializeField] private bool _enableFirebaseAnalyticsEvent;

    [SerializeField] private bool _trackProgressionEvent = false;
    [SerializeField] private bool _trackAdEvent = false;

    [SerializeField] private bool _subscribeToLionEvent = false;
    [SerializeField] private bool _subscribeToLionEventUA = false;

    

#endregion
}

#endif

