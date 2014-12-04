using System;
using UnityEngine;
using System.Collections.Generic;

public class TouchPlayerController : IPlayerController
{
    public override PlayerInputData getPlayerInputData()
    {
        var acc = Input.acceleration;
        PlayerInputData pid = new PlayerInputData();
        pid.x = acc.x;
        pid.y = acc.y;
        foreach (Touch t in Input.touches) {
            if (t.phase == TouchPhase.Began) {
                pid.isFiring = true;
                break;
            }
        }
        return pid;
    }
}