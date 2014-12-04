using System;
using UnityEngine;

public class GameManager : MonoBehaviour, DeathListener
{
    public IShip playerShip;
    public EnemySpawner enemySpawner;
    public GameObject GUIObject;
    
    bool panCamera = false;
    bool isPaused = false;
    bool restartPrompt = false;
    Vector3 camPlayPosition;
    Healthbar healthGUI;
    Scoreboard scoreGUI;
    GUIStyle playButtonStyle;

    void Start()
    {
        camPlayPosition = Camera.main.transform.position;
        healthGUI = GUIObject.GetComponent<Healthbar>();
        scoreGUI = GUIObject.GetComponent<Scoreboard>();

        togglePaused();

        Camera.main.transform.position = new Vector3(0, 0, -50);

        playerShip.RegisterDeathListener(this);
        
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
            float y = (Screen.height / 2) + 60;
            if (restartPrompt && GUI.Button(new Rect(x, y, 150, 80), "Restart?", playButtonStyle)) 
                restartGame();
            else if (!restartPrompt && GUI.Button(new Rect(x, y, 150, 80), "Play!", playButtonStyle))
                startGame();
        }
    }

    void startGame()
    {
        togglePaused();
        panCamera = true;
        enemySpawner.enabled = true;
        healthGUI.enabled = true;
        scoreGUI.enabled = true;
    }

    void restartGame()
    {
        Application.LoadLevel(0);
    }

    void togglePaused()
    {
        isPaused = !isPaused;
        enemySpawner.enabled = !isPaused;
        healthGUI.enabled = !isPaused;
        scoreGUI.enabled = !isPaused;
        playerShip.GetComponentInChildren<crossHair>().enabled = !isPaused;
        playerShip.enableSwitchWeapon = !isPaused;
    }

    public void NotifyDeath(IShip deathShip)
    {
        restartPrompt = true;
        togglePaused();
    }
}
