
#if APSdk_Facebook

namespace APSdk
{
    using UnityEngine;

    //[CreateAssetMenu(fileName = "APFacebookInfo", menuName = "APSdk/APFacebookInfo")]
    public class APFacebookInfo : ScriptableObject
    {
#region Public Variables

        public string appName;
        public string appId;
        public bool logFacebookEvent = false;

#endregion
    }
}

#endif




