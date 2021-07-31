namespace APSdk
{

    using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

    public abstract class APBaseClassForAnalyticsConfiguretion : APBaseClassForConfiguretion
    {
#region Public Variables


        public bool IsAnalyticsEventEnabled { get { return _enableAnalyticsEvent; } }

        public bool IsTrackingProgressionEvent { get { return _trackProgressionEvent; } }
        public bool IsTrackingAdEvent { get { return _trackAdEvent; } }

        public bool IsSubscribedToLionEvent { get { return _subscribeToLionEvent; } }
        public bool IsSubscribedToLionEventUA { get { return _subscribeToLionEventUA; } }

        #endregion

        #region Protected Variables



        [HideInInspector, SerializeField] protected bool _enableAnalyticsEvent = false;

        [HideInInspector, SerializeField] protected bool _trackProgressionEvent = false;
        [HideInInspector, SerializeField] protected bool _trackAdEvent = false;

        [HideInInspector, SerializeField] protected bool _subscribeToLionEvent = false;
        [HideInInspector, SerializeField] protected bool _subscribeToLionEventUA = false;


#endregion

#region Abstract Method

        public abstract bool CanBeSubscribedToLionLogEvent();

        /// <summary>
        /// You can write your editor script for the variables on your derived class before the template editor script
        /// </summary>
        public abstract void PreCustomEditorGUI();

        /// <summary>
        /// You can write your editor script for the variables on your derived class after the template editor script
        /// </summary>
        public abstract void PostCustomEditorGUI();

        #endregion
    }
}


