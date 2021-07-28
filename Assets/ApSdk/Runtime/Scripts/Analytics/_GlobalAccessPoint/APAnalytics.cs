﻿namespace APSdk
{
    using UnityEngine;
    using System.Collections.Generic;

#if APSdk_LionKit
    using LionStudios;
#endif

    public class APAnalytics
    {
        #region Custom Variables

        public static class Key
        {
            public static string level
            {
                get
                {
#if APSdk_LionKit
                    return Analytics.Key.Param.level;
#else
                    return "level";
#endif

                }
            }

            public static string score
            {
                get
                {
#if APSdk_LionKit
                    return Analytics.Key.Param.score;
#else
                    return "score";
#endif

                }
            }


            public static string rank
            {
                get
                {
#if APSdk_LionKit
                    return Analytics.Key.Param.rank;
#else
                    return "rank";
#endif

                }
            }

            public static string level_started
            {
                get
                {
#if APSdk_LionKit
                    return Analytics.Key.level_started;
#else
                    return "level_started";
#endif

                }
            }
            public static string level_complete
            {
                get
                {
#if APSdk_LionKit
                    return Analytics.Key.level_complete;
#else
                    return "level_complete";
#endif

                }
            }
            public static string level_failed
            {
                get
                {
#if APSdk_LionKit
                    return Analytics.Key.level_fail;
#else
                    return "level_failed";
#endif

                }
            }
        }

        #endregion

        //---------------
        #region Private Variables

        private bool _isLionKitIntegrated = false;
        private APSdkConfiguretionInfo _apSdkConfiguretionInfo;


#endregion

        //---------------
        #region Public Callback

        public APAnalytics (APSdkConfiguretionInfo apSdkConfiguretionInfo) {

            _apSdkConfiguretionInfo = apSdkConfiguretionInfo;


#if APSdk_LionKit
            _isLionKitIntegrated = true;
#endif

        }

    #endregion

    #region Event   :   Preset

    public void LevelStarted(object level, object score = null)
        {
            if (_apSdkConfiguretionInfo.IsAnalyticsEventEnabled)
            {
                Dictionary<string, object> eventParam = new Dictionary<string, object>();
                eventParam.Add(Key.level, level);
                if (score != null)
                    eventParam.Add(_isLionKitIntegrated ? Key.rank : Key.score, score);

#if APSdk_LionKit
                //if    :   LionKit Integrated
                Analytics.LogEvent(Key.level_started, eventParam);
#else
                //if    :   LionKit Not Integrated

#if APSdk_Facebook
            //if    :   Facebook Integrated

                
                APFacebookWrapper.Instance.LogEvent(Key.level_started, eventParam);
#endif

#if APSdk_Adjust
            //if    :   Adjust Integrated
            
                APAdjustWrapper.Instance.LogEvent(Key.level_started, eventParam);
#endif

#if APSdk_Firebase

                if (score == null)
                    APFirebaseWrapper.Instance.LogFirebaseEvent(Key.level_started);
                else
                {
                    APFirebaseWrapper.Instance.LogFirebaseEvent(
                            Key.level_started,
                            Key.score,
                        (string)score
                        );
                }

#endif

#endif

#if APSdk_GameAnalytics
                //if    :   GameAnalytics Integrated

                APGameAnalyticsWrapper.Instance.ProgressionEvents(
                        GameAnalyticsSDK.GAProgressionStatus.Start,
                        (int)level,
                        world: -1);
#endif
            }
        }

        public void LevelComplete(object level, object score = null)
        {
            

            if (_apSdkConfiguretionInfo.IsAnalyticsEventEnabled)
            {
                Dictionary<string, object> eventParam = new Dictionary<string, object>();
                eventParam.Add(Key.level, level);
                if (score != null)
                    eventParam.Add(_isLionKitIntegrated ? Key.rank : Key.score, score);

                

#if APSdk_LionKit
                //if    :   LionKit Integrated
                Analytics.LogEvent(Key.level_complete, eventParam);
#else
                //if    :   LionKit Not Integrated

#if APSdk_Facebook
            //if    :   Facebook Integrated
            
                
                APFacebookWrapper.Instance.LogEvent(Key.level_complete, eventParam);
#endif

#if APSdk_Adjust
            //if    :   Adjust Integrated
            
                APAdjustWrapper.Instance.LogEvent(Key.level_complete, eventParam);
#endif

#if APSdk_Firebase

                if (score == null)
                    APFirebaseWrapper.Instance.LogFirebaseEvent(Key.level_complete);
                else
                {
                    APFirebaseWrapper.Instance.LogFirebaseEvent(
                            Key.level_complete,
                            Key.score,
                        (string)score
                        );
                }

#endif

#endif




#if APSdk_GameAnalytics
                //if    :   GameAnalytics Integrated

                APGameAnalyticsWrapper.Instance.ProgressionEvents(
                        GameAnalyticsSDK.GAProgressionStatus.Complete,
                        (int)level,
                        world: -1);
#endif
            }
        }

        public void LevelFailed(object level, object score = null)
        {
            if (_apSdkConfiguretionInfo.IsAnalyticsEventEnabled)
            {
                Dictionary<string, object> eventParam = new Dictionary<string, object>();
                eventParam.Add(Key.level, level);
                if (score != null)
                    eventParam.Add(_isLionKitIntegrated ? Key.rank : Key.score, score);

#if APSdk_LionKit
                //if    :   LionKit Integrated
                Analytics.LogEvent(Key.level_failed, eventParam);
#else
                //if    :   LionKit Not Integrated

#if APSdk_Facebook
            //if    :   Facebook Integrated

                APFacebookWrapper.Instance.LogEvent(Key.level_failed, eventParam);
#endif

#if APSdk_Adjust
            //if    :   Adjust Integrated
            
                APAdjustWrapper.Instance.LogEvent(Key.level_failed, eventParam);
#endif

#if APSdk_Firebase

                if (score == null)
                    APFirebaseWrapper.Instance.LogFirebaseEvent(Key.level_failed);
                else
                {
                    APFirebaseWrapper.Instance.LogFirebaseEvent(
                            Key.level_failed,
                            Key.score,
                        (string)score
                        );
                }

#endif

#endif




#if APSdk_GameAnalytics
                //if    :   GameAnalytics Integrated

                APGameAnalyticsWrapper.Instance.ProgressionEvents(
                        GameAnalyticsSDK.GAProgressionStatus.Fail,
                        (int)level,
                        world: -1);
#endif
            }
        }

        #endregion

    }
}

