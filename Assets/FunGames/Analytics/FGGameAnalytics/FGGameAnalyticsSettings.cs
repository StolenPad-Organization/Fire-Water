using Unity.Collections;
using UnityEngine;

namespace FunGamesSdk.FunGames.Analytics.GA
{
    [CreateAssetMenu(fileName = "Assets/Resources/FGGameAnalyticsSettings", menuName = "FunGamesSdk/Settings/Analytics/GameAnalytics Settings", order = 1000)]
    public class FGGameAnalyticsSettings : ScriptableObject
    {
        private static FGGameAnalyticsSettings _settings;
        public static FGGameAnalyticsSettings settings
        {
            get
            {
                if (_settings == null)
                    _settings = Resources.Load<FGGameAnalyticsSettings>("FGGameAnalyticsSettings");
                return _settings;
            }
        }
        [Header("Sdk Version")]

        [Tooltip("FunGames Game Analytics Sdk Version")]
        [ReadOnly] public string version = "1.0";

        [Header("GameAnalytics")]

        [Tooltip("Use GameAnalytics")]
        public bool useGameAnalytics = true;

        [Tooltip("GameAnalytics Ios Game Key")]
        public string gameAnalyticsIosGameKey;

        [Tooltip("GameAnalytics Ios Secret Key")]
        public string gameAnalyticsIosSecretKey;

        [Tooltip("GameAnalytics Android Game Key")]
        public string gameAnalyticsAndroidGameKey;

        [Tooltip("GameAnalytics Android Secret Key")]
        public string gameAnalyticsAndroidSecretKey;
    }
}
