#if APSdk_LionKit

namespace com.alphapotato.sdk
{
#if UNITY_EDITOR

    using UnityEngine;
    using UnityEditor;

    [CustomEditor(typeof(APLionKitWrapper))]
    public class APLionKitWrapperEditor : Editor
    {
#region Private Variables

        private APLionKitWrapper _reference;

#endregion

#region Editor

        private void OnEnable()
        {
            _reference = (APLionKitWrapper)target;

            if (_reference == null)
                return;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            serializedObject.Update();

            

            serializedObject.ApplyModifiedProperties();
        }

#endregion
    }
#endif
}

#endif


