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
        ship.Accelerate(new Vector3(ship.accelScalar * pid.x, ship.accelScalar *pid.y));
        if (pid.isFiring) ship.Fire();
        if (pid.isGettingHit) ship.ApplyDamage(1f);
    }

    public abstract PlayerInputData getPlayerInputData();
}
