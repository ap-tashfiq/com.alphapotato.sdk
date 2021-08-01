namespace APSdk
{
    using UnityEngine;

    public abstract class APBaseClassForConfiguretion : ScriptableObject
    {
        #region Public Variables

        public string NameOfConfiguretion { get { return _nameOfConfiguretion; } }

        #endregion

        #region Protected Variables

#if UNITY_EDITOR
        [ SerializeField] protected bool _showSettings;
#endif

        [HideInInspector, SerializeField] protected string _nameOfConfiguretion;
        [HideInInspector, SerializeField] protected bool _isSDKIntegrated;

        #endregion

        #region Protected Method

        /// <summary>
        /// Editor Only
        /// </summary>
        /// <param name="scriptDefineSymbol"></param>
        protected void SetNameOfConfiguretion(string scriptDefineSymbol,string concatinate = "")
        {

            string[] splited = scriptDefineSymbol.Split('_');
            _nameOfConfiguretion = splited[1] + concatinate;
        }

        #endregion

        #region Abstract Method

        public abstract void SetNameAndIntegrationStatus();

        public abstract void Initialize(APSdkConfiguretionInfo apSdkConfiguretionInfo);

        #endregion
    }
}

