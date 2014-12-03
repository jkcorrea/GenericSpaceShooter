using System;
using System.Collections.Generic;
using UnityEngine;

public class Scoreboard : MonoBehaviour, DeathListener
{
    public GUIStyle style;
    public int barToScreenRatio = 6;

    float scoreboardLength;
    float score;

    void Start()
    {
        score = 0f;
        scoreboardLength = Screen.width / barToScreenRatio;
    }

    void OnGUI()
    {
        GUI.Box(new Rect(Screen.width - (10 + scoreboardLength), 10, scoreboardLength, 20), score.ToString());
    }

    public void NotifyDeath(IShip deadShip)
    {
        Debug.Log("test!! " + deadShip.score);
        incrementScore(deadShip.score);
    }

    void incrementScore(float value)
    {
        score += value;
    }

	public float GetScore() {
		return score;
	}
}
