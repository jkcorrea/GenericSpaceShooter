using System;
using System.Collections.Generic;
using UnityEngine;

class Healthbar : MonoBehaviour
{
    public IShip playerShip = null;
    public GUIStyle fgStyle = null;
	public GUIStyle fgStyleLowHealth = null;
	public GUIStyle bgStyle = null;
    public int barToScreenRatio = 6;

    float healthBarLengthOriginal;
    float healthBarLength;
    bool lowHealth;

    void Start()
    {
        healthBarLengthOriginal = Screen.width / barToScreenRatio;
        healthBarLength = healthBarLengthOriginal;
    }

    void Update()
    {
        float health = playerShip.getHealth();

        if (health < 0) 
            return;
        else if (!lowHealth && health / playerShip.initialHealth < 0.35f)
            lowHealth = true;

        healthBarLength = (Screen.width / barToScreenRatio) * (playerShip.getHealth() / playerShip.initialHealth);
    }

    void OnGUI()
    {
        GUI.Box(new Rect(10, 10, healthBarLengthOriginal, 50), /*playerShip.getHealth() + "/" + playerShip.initialHealth*/ "", bgStyle);
        GUI.Box(new Rect(10, 10, healthBarLength, 50), "", lowHealth ? fgStyleLowHealth : fgStyle);
    }
}
