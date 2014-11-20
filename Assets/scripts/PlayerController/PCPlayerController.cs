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

            pid.up      = Input.GetKeyDown(KeyCode.UpArrow   ) ? 1f : -1f;
            pid.down    = Input.GetKeyDown(KeyCode.DownArrow ) ? 1f : -1f;
            pid.left    = Input.GetKeyDown(KeyCode.LeftArrow ) ? 1f : -1f;
            pid.right   = Input.GetKeyDown(KeyCode.RightArrow) ? 1f : -1f;

            return pid;
        }
    }
}
