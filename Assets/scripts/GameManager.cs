using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public IShip playerShip;
    public EnemySpawner enemySpawner;
    public GameObject GUIObject;
    
    bool panCamera;
    bool isPaused;
    Vector3 camPlayPosition;
    Healthbar healthGUI;
    Scoreboard scoreGUI;
    GUIStyle playButtonStyle;

    void Start()
    {
        isPaused = true;
        panCamera = false;
        camPlayPosition = Camera.main.transform.position;
        healthGUI = GUIObject.GetComponent<Healthbar>();
        scoreGUI = GUIObject.GetComponent<Scoreboard>();

        enemySpawner.enabled = false;
        healthGUI.enabled = false;
        scoreGUI.enabled = false;
        Camera.main.transform.position = new Vector3(0, 0, -50);
        
        // Enable the proper player controller script
#if UNITY_ANDROID || UNITY_IPHONE
        playerShip.GetComponent<TouchPlayerController>().enabled = true;
        playerShip.GetComponent<PCPlayerController>().enabled = false;
#else
        playerShip.GetComponent<TouchPlayerController>().enabled = false;
        playerShip.GetComponent<PCPlayerController>().enabled = true;
#endif
    }

    void Update()
    {
        if (panCamera) updateCameraPos();
    }

    void updateCameraPos()
    {
        if (Vector3.Equals(Camera.main.transform.position, camPlayPosition))
            panCamera = false;
        else
            Camera.main.transform.position = Vector3.Slerp(Camera.main.transform.position, camPlayPosition, Time.deltaTime);
    }

    void OnGUI()
    {
        if (playButtonStyle == null)
        {
            playButtonStyle = new GUIStyle(GUI.skin.button);
            playButtonStyle.fontSize = 36;
        }

        if (isPaused)
        {
            float x = (Screen.width / 2) - 50;
            float y = (Screen.height / 2) + 20;
            if (GUI.Button(new Rect(x, y, 150, 80), "Play!", playButtonStyle))
                startGame();
        }
    }

    void startGame()
    {
        isPaused = false;
        panCamera = true;
        enemySpawner.enabled = true;
        healthGUI.enabled = true;
        scoreGUI.enabled = true;

    }
}
