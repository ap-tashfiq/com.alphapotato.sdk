namespace APSdk
{

    using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

    public abstract class BaseClassForAnalyticsInfo : ScriptableObject
    {
#region Public Variables

        public bool IsAbleToSubscribeToLionLogEvent { get { return CanBeSubscribedToLionLogEvent(); } }

        public bool IsAnalyticsEventEnabled { get { return _enableAnalyticsEvent; } }

        public bool IsTrackingProgressionEvent { get { return _trackProgressionEvent; } }
        public bool IsTrackingAdEvent { get { return _trackAdEvent; } }

        public bool IsSubscribedToLionEvent { get { return _subscribeToLionEvent; } }
        public bool IsSubscribedToLionEventUA { get { return _subscribeToLionEventUA; } }

#endregion

#region Protected Variables

        [SerializeField] protected bool _enableAnalyticsEvent = false;

        [SerializeField] protected bool _trackProgressionEvent = false;
        [SerializeField] protected bool _trackAdEvent = false;

        [SerializeField] private bool _subscribeToLionEvent = false;
        [SerializeField] private bool _subscribeToLionEventUA = false;


#endregion

#region Abstract Method

        protected abstract bool CanBeSubscribedToLionLogEvent();

        /// <summary>
        /// You can write your editor script for the variables on your derived class
        /// </summary>
        protected abstract void CustomEditorGUI();

#endregion
    }
}


