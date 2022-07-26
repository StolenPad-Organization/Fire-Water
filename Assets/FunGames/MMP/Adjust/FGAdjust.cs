using com.adjust.sdk;
using FunGamesSdk.FunGames.Analytics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FGAdjust : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(this);
        FGMmpMananger.Initialisation += Initialize;
        FGMmpMananger._Initialisation?.Invoke();
    }

    /// <summary>
    /// Private function that initializes all GA elements
    /// </summary>
    private static void Initialize()
    {
        AdjustConfig adjustConfig = new AdjustConfig(FGAdjustSettings.settings.AppToken, FGAdjustSettings.settings.environment);
        adjustConfig.setLogLevel(FGAdjustSettings.settings.logLevel);
        adjustConfig.setAttributionChangedDelegate(attributionChangedDelegate);

        Adjust.start(adjustConfig);
    }

    public static void attributionChangedDelegate(AdjustAttribution attribution)
    {
        Debug.Log("Attribution changed");
        FGAnalyticsManager.NewDesignEvent("NetworkAttribution:" + attribution.network);
    }
}
