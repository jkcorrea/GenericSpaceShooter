using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets
{
    public class PCPlayerController : PlayerControllerBase
    {
        void Start()
        {

        }

        partial void Update()
        {
            Debug.Log("test");
        }

        public PlayerInputData getPlayerInputData()
        {
            PlayerInputData pid = new PlayerInputData();

            pid.up      = Input.GetKeyDown(KeyCode.UpArrow   ) ? 1f : 0f;
            pid.down    = Input.GetKeyDown(KeyCode.DownArrow ) ? 1f : 0f;
            pid.left    = Input.GetKeyDown(KeyCode.LeftArrow ) ? 1f : 0f;
            pid.right   = Input.GetKeyDown(KeyCode.RightArrow) ? 1f : 0f;

            return pid;
        }
    }
}
