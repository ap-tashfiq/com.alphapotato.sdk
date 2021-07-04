
#if APSdk_Firebase
using UnityEngine;

//[CreateAssetMenu(fileName = "APFirebaseInfo", menuName = "Firebase/APFirebaseInfo")]
public class APFirebaseInfo : ScriptableObject
{
    #region Public Variables

    public bool TrackEvent {
        get {

#if APSdk_LionKit
        return _subscribeToLionEvent;
#else
        return _trackProgressionEvent;
#endif

        }
    }

    public bool TrackAdEvent
    {
        get
        {
            return _trackAdEvent;
        }
    }

#endregion

#region Private Variables

    [SerializeField] private bool _subscribeToLionEvent = false;
    [SerializeField] private bool _trackEvent = false;
    [SerializeField] private bool _trackAdEvent = false;

#endregion
}

#endif

