using com.adjust.sdk;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_IOS
using UnityEngine.iOS;
#endif
namespace FunGamesSdk.FunGames.Analytics
{
    #region Enum
    public enum LevelStatus
    {
        Start = 1,
        Complete = 2,
        Fail = 3
    };

    public enum AdAction
    {
        Clicked = 1,
        FailedShow = 2,
        Loaded = 3,
        Request = 4,
        RewardReceived = 5,
        Show = 6,
        Undefined = 7
    };

    public enum AdType
    {
        Banner = 1,
        Interstitial = 2,
        OfferWall = 3,
        Playable = 4,
        RewardedVideo = 5,
        Video = 6,
        Undefined = 7
    };
    #endregion
    public class FGAnalyticsManager : MonoBehaviour
    {
        #region Event
        private static Action _Initialisation;
        public static event Action Initialisation
        {
            add
            {
                _Initialisation += value;
            }
            remove
            {
                _Initialisation -= value;
            }
        }

        private static Action<LevelStatus, string, string, int> _ProgressionEvent;
        public static event Action<LevelStatus, string, string, int> ProgressionEvent
        {
            add
            {
                _ProgressionEvent += value;
            }
            remove
            {
                _ProgressionEvent -= value;
            }
        }

        private static Action<string, float> _DesignEventSimple;
        public static event Action<string, float> DesignEventSimple
        {
            add
            {
                _DesignEventSimple += value;
            }
            remove
            {
                _DesignEventSimple -= value;
            }
        }

        private static Action<string, Dictionary<string, object>, float> _DesignEventDictio;
        public static event Action<string, Dictionary<string, object>, float> DesignEventDictio
        {
            add
            {
                _DesignEventDictio += value;
            }
            remove
            {
                _DesignEventDictio -= value;
            }
        }

        private static Action<AdAction, AdType, string, string> _AdEvent;
        public static event Action<AdAction, AdType, string, string> AdEvent
        {
            add
            {
                _AdEvent += value;
            }
            remove
            {
                _AdEvent -= value;
            }
        }
        #endregion

        #region Init
        private static void Init()
        {
            _Initialisation.Invoke();
        }
        #endregion

        #region Mono Function
        // Start is called before the first frame update
        void Start()
        {
            DontDestroyOnLoad(this);
            Init();
        }
        #endregion

        #region event call
        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        /// <param name="level"></param>
        /// <param name="subLevel"></param>
        /// <param name="score"></param>
        public static void NewProgressionEvent(LevelStatus status, string level, string subLevel = "", int score = -1)
        {
            _ProgressionEvent.Invoke(status, level, subLevel, score);
        }

        public static void NewDesignEvent(string eventId, float eventValue = 0)
        {
            _DesignEventSimple.Invoke(eventId, eventValue);
        }
        
        public static void NewDesignEvent(string eventId, Dictionary<string, object> customFields, float eventValue = 0)
        {
            _DesignEventDictio.Invoke(eventId, customFields, eventValue);
        }

        public static void NewAdEvent(AdAction adAction, AdType adType, string adSdkName, string adPlacement)
        {
            _AdEvent.Invoke(adAction, adType, adSdkName, adPlacement);
        }
        #endregion

    }
}
