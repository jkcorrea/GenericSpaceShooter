using System;
using UnityEngine;
using System.Collections.Generic;

public class TouchPlayerController : IPlayerController
{
    public override PlayerInputData getPlayerInputData()
    {
#if UNITY_IPHONE || UNITY_ANDROID
        var x = Input.acceleration;
        PlayerInputData pid = new PlayerInputData();
        pid.x = Input.acceleration.x;
        pid.y = Input.acceleration.y;
        foreach (Touch t in Input.touches) {
            if (t.phase == TouchPhase.Began) {
                pid.isFiring = true;
                break;
            }
        }
        return pid;
#endif
    }
}