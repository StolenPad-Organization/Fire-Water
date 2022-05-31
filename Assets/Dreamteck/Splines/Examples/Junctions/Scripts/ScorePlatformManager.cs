using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Stolenpad.Utils;
public class ScorePlatformManager : MonoBehaviour
{
    public ScorePlatformEnding[] scorePlatforms;

    public Vector3 GetJumpPositionFromPercentage(float speed)
    {
        //0 > 1
        var index = speed < 100 ? 0 : Mathf.RoundToInt(Mathf.Lerp(1, scorePlatforms.Length - 1, Mathf.InverseLerp(100, 160, speed)));

        if (index <= 0)
            index = 0;
        if (index > scorePlatforms.Length)
            index = scorePlatforms.Length - 1;

        return scorePlatforms[index].JumpPosition.position;
    }
}
