using GameAnalyticsSDK;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FunGamesSdk.FunGames.Analytics.GA
{
    public class FGGameAnalyticsManager : MonoBehaviour
    {
        static bool isInit = false;
        void Awake()
        {
            DontDestroyOnLoad(this);
            FGAnalyticsManager.Initialisation += Initialize;
            FGAnalyticsManager.ProgressionEvent += ProgressionEvent;
            FGAnalyticsManager.DesignEventSimple += DesignEventSimple;
            FGAnalyticsManager.DesignEventDictio += DesignEventDictio;
            FGAnalyticsManager.AdEvent += AdEvent;
        }

        /// <summary>
        /// Private function that initializes all GA elements
        /// </summary>
        private static void Initialize()
        {
            if (isInit)
            {
                Debug.LogWarning("FunGamesSDK : GameAnalytics is already initialize");
                return;
            }
            isInit = true;
            var flag = !FGGameAnalyticsSettings.settings.gameAnalyticsAndroidGameKey.Equals(string.Empty) && !FGGameAnalyticsSettings.settings.gameAnalyticsAndroidSecretKey.Equals(string.Empty);

            if (flag == false)
            {
                flag = !FGGameAnalyticsSettings.settings.gameAnalyticsIosGameKey.Equals(string.Empty) && !FGGameAnalyticsSettings.settings.gameAnalyticsIosSecretKey.Equals(string.Empty);
            }

            var gameAnalytics = UnityEngine.Object.FindObjectOfType<GameAnalytics>();

            if (gameAnalytics == null)
            {
                throw new Exception("It seems like you haven't instantiated GameAnalytics GameObject");
            }

            AddOrUpdatePlatform(RuntimePlatform.IPhonePlayer, FGGameAnalyticsSettings.settings.gameAnalyticsIosGameKey, FGGameAnalyticsSettings.settings.gameAnalyticsIosSecretKey);

            if (flag)
            {
                AddOrUpdatePlatform(RuntimePlatform.Android, FGGameAnalyticsSettings.settings.gameAnalyticsAndroidGameKey, FGGameAnalyticsSettings.settings.gameAnalyticsAndroidSecretKey);
            }
            else
            {
                RemovePlatform(RuntimePlatform.Android);
            }

            GameAnalytics.SettingsGA.InfoLogBuild = false;
            GameAnalytics.SettingsGA.InfoLogEditor = false;
            GameAnalyticsILRD.SubscribeMaxImpressions();
            GameAnalytics.Initialize();
        }

        /// <summary>
        /// Init the Game Analytic settings for the game on each plateform is on
        /// </summary>
        /// <param name="platform">Android or iOS</param>
        /// <param name="gameKey">GA Gamekey (public key)</param>
        /// <param name="secretKey">GA Secret Key</param>
        private static void AddOrUpdatePlatform(RuntimePlatform platform, string gameKey, string secretKey)
        {
            if (!GameAnalytics.SettingsGA.Platforms.Contains(platform))
            {
                GameAnalytics.SettingsGA.AddPlatform(platform);
            }

            var index = GameAnalytics.SettingsGA.Platforms.IndexOf(platform);

            GameAnalytics.SettingsGA.UpdateGameKey(index, gameKey);
            GameAnalytics.SettingsGA.UpdateSecretKey(index, secretKey);
            GameAnalytics.SettingsGA.Build[index] = Application.version;
        }

        /// <summary>
        /// Remove the settings of GA for a plateform
        /// </summary>
        /// <param name="platform">Android or iOS</param>
        private static void RemovePlatform(RuntimePlatform platform)
        {
            if (GameAnalytics.SettingsGA.Platforms.Contains(platform) == false)
            {
                return;
            }

            var index = GameAnalytics.SettingsGA.Platforms.IndexOf(platform);

            GameAnalytics.SettingsGA.RemovePlatformAtIndex(index);
        }

        /// <summary>
        /// Private function that sends a Progression Event from GA with FGA data
        /// </summary>
        /// <param name="statusFG"></param>
        /// <param name="level"></param>
        /// <param name="subLevel"></param>
        /// <param name="score"></param>
        private static void ProgressionEvent(LevelStatus statusFG, string level, string subLevel = "", int score = -1)
        {
            if (!isInit)
            {
                Debug.LogWarning("FunGamesSDK : GameAnalytics is not initialize");
                return;
            }
            GAProgressionStatus status;

            switch (statusFG)
            {

                case LevelStatus.Complete:
                    status = GAProgressionStatus.Complete;
                    break;
                case LevelStatus.Fail:
                    status = GAProgressionStatus.Fail;
                    break;
                default:
                    status = GAProgressionStatus.Start;
                    break;
            }

            if (score == -1)
            {
                GameAnalytics.NewProgressionEvent(status, level, subLevel);
            }
            else
            {
                GameAnalytics.NewProgressionEvent(status, level, subLevel, score);
            }
        }

        /// <summary>
        /// Private function that sends a simple Design Event from GA with FGA data
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="eventValue"></param>
        private static void DesignEventSimple(string eventId, float eventValue)
        {
            if (!isInit)
            {
                Debug.LogWarning("FunGamesSDK : GameAnalytics is not initialize");
                return;
            }
            GameAnalytics.NewDesignEvent(eventId, eventValue);
        }

        /// <summary>
        /// Private function that sends a Desisgn Event with dictionnary data store in it from GA with FGA data
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="customFields"></param>
        /// <param name="eventValue"></param>
        private static void DesignEventDictio(string eventId, Dictionary<string, object> customFields, float eventValue)
        {
            if (!isInit)
            {
                Debug.LogWarning("FunGamesSDK : GameAnalytics is not initialize");
                return;
            }
            GameAnalytics.NewDesignEvent(eventId, eventValue, customFields);
        }

        /// <summary>
        /// Private function that sends a Ad Event from GA with FGA data
        /// </summary>
        /// <param name="adAction"></param>
        /// <param name="adType"></param>
        /// <param name="adSdkName"></param>
        /// <param name="adPlacement"></param>
        private static void AdEvent(AdAction adAction, AdType adType, string adSdkName, string adPlacement)
        {
            if (!isInit)
            {
                Debug.LogWarning("FunGamesSDK : GameAnalytics is not initialize");
                return;
            }
            GAAdAction action;
            switch (adAction)
            {
                case AdAction.Clicked:
                    action = GAAdAction.Clicked;
                    break;
                case AdAction.FailedShow:
                    action = GAAdAction.FailedShow;
                    break;
                case AdAction.Loaded:
                    action = GAAdAction.Loaded;
                    break;
                case AdAction.Request:
                    action = GAAdAction.Request;
                    break;
                case AdAction.RewardReceived:
                    action = GAAdAction.RewardReceived;
                    break;
                case AdAction.Show:
                    action = GAAdAction.Show;
                    break;
                default:
                    action = GAAdAction.Undefined;
                    break;
            }

            GAAdType type;
            switch (adType)
            {
                case AdType.Banner:
                    type = GAAdType.Banner;
                    break;
                case AdType.Interstitial:
                    type = GAAdType.Interstitial;
                    break;
                case AdType.OfferWall:
                    type = GAAdType.OfferWall;
                    break;
                case AdType.Playable:
                    type = GAAdType.Playable;
                    break;
                case AdType.RewardedVideo:
                    type = GAAdType.RewardedVideo;
                    break;
                case AdType.Video:
                    type = GAAdType.Video;
                    break;
                default:
                    type = GAAdType.Undefined;
                    break;
            }
            GameAnalytics.NewAdEvent(action, type, adSdkName, adPlacement);
        }
    }
}