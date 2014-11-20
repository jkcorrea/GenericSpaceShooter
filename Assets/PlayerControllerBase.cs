using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets
{
    public abstract class PlayerControllerBase : MonoBehaviour
    {
        GameObject playerCharacter;

        partial void Start()
        {
            playerCharacter = this.gameObject;
        }

        partial void Update()
        {
            controlPlayerCharacter();
        }

        public abstract PlayerInputData getPlayerInputData();

        
        protected void controlPlayerCharacter()
        {
            PlayerInputData pid = getPlayerInputData();
            float vert = pid.up - pid.down;
            float hori = pid.right - pid.left;
            playerCharacter.rigidbody.AddForce(hori, vert, 0, ForceMode.Acceleration);
        }
    }
}
