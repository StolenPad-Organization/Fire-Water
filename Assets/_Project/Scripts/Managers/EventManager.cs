using System;
using UnityEngine;

public static class EventManager
{
    #region Player Events
    public static Func<PlayerWeapon> GetFrostWeapon;
    public static Func<PlayerWeapon> GetFireWeapon;
    #endregion

    #region Ending Events

    #endregion

    #region Level Events
    public static Action TriggerWin;
    public static Action TriggerLose;
    public static Action loadNextScene;
    public static Action loadSameScene;
    public static Action loadOpeningScene;
    #endregion
}