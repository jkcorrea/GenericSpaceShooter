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

    void Update()
    {
        controlPlayerCharacter();
    }
        
    protected void controlPlayerCharacter()
    {
        PlayerInputData pid = getPlayerInputData();
        float x = pid.up - pid.down;
        float y = pid.right - pid.left;
        ship.Accelerate(new Vector3(x, y));
    }

    public abstract PlayerInputData getPlayerInputData();
}
