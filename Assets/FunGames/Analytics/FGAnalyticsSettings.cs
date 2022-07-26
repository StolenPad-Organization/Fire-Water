using Unity.Collections;
using UnityEngine;

namespace FunGamesSdk.FunGames.Analytics
{
    [CreateAssetMenu(fileName = "Assets/Resources/FGAnalyticsSettings", menuName = "FunGamesSdk/Settings/Analytics/Analytics Settings", order = 1000)]
    public class FGAnalyticsSettings : ScriptableObject
    {
        private static FGAnalyticsSettings _settings;
        public static FGAnalyticsSettings settings
        {
            get
            {
                if (_settings == null)
                    _settings = Resources.Load<FGAnalyticsSettings>("FGAnalyticsSettings");
                return _settings;
            }
        }
        [Header("Sdk Version")]

        [Tooltip("FunGames Game Analytics Sdk Version")]
        [ReadOnly] public string version = "1.0.1";
    }
}
