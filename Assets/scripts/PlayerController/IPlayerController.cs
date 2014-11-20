using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public abstract class IPlayerController : MonoBehaviour
{
    IShip ship;

    void Start()
    {
        ship = GetComponent<IShip>();
    }

    void FixedUpdate()
    {
        controlPlayerCharacter();
    }
        
    protected void controlPlayerCharacter()
    {
        PlayerInputData pid = getPlayerInputData();
        ship.Accelerate(new Vector3(4*pid.x, 4*pid.y));
        if (pid.isFiring) ship.Fire();
    }

    public abstract PlayerInputData getPlayerInputData();
}
