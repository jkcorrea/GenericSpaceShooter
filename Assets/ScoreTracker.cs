﻿using UnityEngine;
using System.Collections;

public class ScoreTracker : MonoBehaviour {

	float score;
	GUIText guiText;

	// Use this for initialization
	void Start () {
		score = 0;
		guiText = GetComponent<GUIText>();
		guiText.text = "" + score;
	}
	
	// Update is called once per frame
	void Update () {
		guiText.text = "" + score;
	}

	public void IncrementScore(float value) {
		score += value;
	}

}
