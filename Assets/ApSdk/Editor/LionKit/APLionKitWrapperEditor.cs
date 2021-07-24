#if APSdk_LionKit

namespace APSdk
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

            EditorGUILayout.BeginHorizontal(GUI.skin.box);
            {
                if (GUILayout.Button("RewardedAd")) {
                    APLionKitWrapper.RewardedAd.ShowRewardedAd("test_rewarded_video",(isRecieveAward)=> { });
                }

                if (GUILayout.Button("InterstitialAd"))
                {
                    APLionKitWrapper.InterstitialAd.ShowInterstitialAd();
                }
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal(GUI.skin.box);
            {
                if (GUILayout.Button("Show Banner")) {

                    APLionKitWrapper.BannerAd.ShowBannerAd();
                }

                if (GUILayout.Button("HideBanner Banner"))
                {
                    APLionKitWrapper.BannerAd.HideBannerAd();
                }
            }
            EditorGUILayout.EndHorizontal();

            serializedObject.ApplyModifiedProperties();
        }

#endregion
    }
#endif
}

#endif


