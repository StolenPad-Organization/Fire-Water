using com.adjust.sdk;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;


[CreateAssetMenu(fileName = "Assets/Resources/FGAdjustSettings", menuName = "FunGamesSdk/Settings/MMP/Adjust Settings", order = 1000)]
public class FGAdjustSettings : ScriptableObject
{
    private static FGAdjustSettings _settings;
    public static FGAdjustSettings settings
    {
        get
        {
            if (_settings == null)
                _settings = Resources.Load<FGAdjustSettings>("FGAdjustSettings");
            return _settings;
        }
    }
    [Header("Sdk Version")]

    [Tooltip("FunGames Adjust Sdk Version")]
    [ReadOnly] public string version = "1.0";
    
    [Tooltip("Adjust App Token")]
    public string AppToken;

    [Tooltip("Adjust Log Level")]
    public AdjustLogLevel logLevel = AdjustLogLevel.Info;

    [Tooltip("Adjust Environment")]
    public AdjustEnvironment environment = AdjustEnvironment.Sandbox;
}
