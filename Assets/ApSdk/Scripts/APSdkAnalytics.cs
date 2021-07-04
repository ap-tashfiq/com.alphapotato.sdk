﻿namespace APSdk
{
    using UnityEngine;
    using System.Collections.Generic;

#if APSdk_LionKit
    using LionStudios;
#endif

    public class APSdkAnalytics
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

#if APSdk_Facebook
        private APFacebookInfo _apFacebookInfo;
#endif

#if APSdk_Adjust
        private APAdjustInfo _apAdjustInfo;
#endif

#if APSdk_GameAnalytics
        private APGameAnalyticsInfo _apGameAnalyticsInfo;
#endif

#endregion

        //---------------
#region Public Callback

        public APSdkAnalytics (APSdkConfiguretionInfo apSdkConfiguretionInfo) {

            _apSdkConfiguretionInfo = apSdkConfiguretionInfo;

#if APSdk_Facebook
            _apFacebookInfo = Resources.Load<APFacebookInfo>("Facebook/APFacebookInfo");
#endif

#if APSdk_Adjust
            _apAdjustInfo = Resources.Load<APAdjustInfo>("Adjust/APAdjustInfo");
#endif

#if APSdk_GameAnalytics
            _apGameAnalyticsInfo = Resources.Load<APGameAnalyticsInfo>("GameAnalytics/APGameAnalyticsInfo");
#endif

#if APSdk_LionKit
            _isLionKitIntegrated = true;
#endif

        }

        #endregion

        #region Event   :   Preset

        public void LevelStarted(object level, object score = null)
        {
            if (_apSdkConfiguretionInfo.logAnalyticsEvent)
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

#endif




#if APSdk_GameAnalytics
                //if    :   GameAnalytics Integrated
                
                    APGameAnalyticsWrapper.Instance.ProgressionEvents(
                        GameAnalyticsSDK.GAProgressionStatus.Start,
                        (int)level,
                        world: _apGameAnalyticsInfo.DefaultWorldIndex);
#endif
            }
        }

        public void LevelComplete(object level, object score = null)
        {
            if (_apSdkConfiguretionInfo.logAnalyticsEvent)
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

#endif




#if APSdk_GameAnalytics
                //if    :   GameAnalytics Integrated
                
                    APGameAnalyticsWrapper.Instance.ProgressionEvents(
                        GameAnalyticsSDK.GAProgressionStatus.Complete,
                        (int)level,
                        world: _apGameAnalyticsInfo.DefaultWorldIndex);
#endif
            }
        }

        public void LevelFailed(object level, object score = null)
        {
            if (_apSdkConfiguretionInfo.logAnalyticsEvent)
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

#endif




#if APSdk_GameAnalytics
                //if    :   GameAnalytics Integrated
                
                    APGameAnalyticsWrapper.Instance.ProgressionEvents(
                        GameAnalyticsSDK.GAProgressionStatus.Fail,
                        (int)level,
                        world: _apGameAnalyticsInfo.DefaultWorldIndex);
#endif
            }
        }

        #endregion

    }
}

