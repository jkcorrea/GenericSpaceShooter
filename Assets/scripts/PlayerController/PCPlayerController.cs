using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets
{
    public class PCPlayerController : IPlayerController
    {
        public override PlayerInputData getPlayerInputData()
        {
            PlayerInputData pid = new PlayerInputData();
            pid.x = Input.GetAxis("Horizontal");
            pid.y = Input.GetAxis("Vertical");
            pid.isFiring = Input.GetButton("Fire1") || Input.GetKeyDown(KeyCode.Space);
            return pid;
        }
    }
}
