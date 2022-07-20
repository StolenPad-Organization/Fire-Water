using System;
using UnityEngine;

public static class EventManager
{
    #region Player Events
    public static Func<PlayerWeapon> GetFrostWeapon;
    public static Func<PlayerWeapon> GetFireWeapon;
    public static Action<bool> OpenFireWeapon;
    public static Action<bool> OpenFrostWeapon;
    public static Action<int, Transform> AddMoney;
    #endregion

    #region Ending Events
    public static Action PlayerDeath;
    public static Action<Element> DeactivateJetpack;
    public static Action<int> UpdateScore;
    #endregion

    #region Level Events
    public static Action TriggerWin;
    public static Action TriggerLose;
    public static Action loadNextScene;
    public static Action loadSameScene;
    public static Action loadOpeningScene;
    #endregion
}