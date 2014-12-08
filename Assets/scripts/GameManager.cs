using System;
using UnityEngine;

public class GameManager : MonoBehaviour, DeathListener
{
    public IShip playerShip;
    public EnemySpawner enemySpawner;
    public GameObject GUIObject;
    public float waitBeforeSpawn;

    public GameObject victoryText;
    public GameObject defeatText;
    
    bool panCamera = false;
    bool isPaused = false;
    bool restartPrompt = false;
    Vector3 camPlayPosition;
    Healthbar healthGUI;
    Scoreboard scoreGUI;
    GUIStyle playButtonStyle;
	bool title = true;

    void Start()
    {
        camPlayPosition = Camera.main.transform.position;
        healthGUI = GUIObject.GetComponent<Healthbar>();
        scoreGUI = GUIObject.GetComponent<Scoreboard>();

        togglePaused();
        enemySpawner.enabled = false;

        Camera.main.transform.position = new Vector3(0, 0, -50f);

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
		scoreGUI.enabled = !title;
        if (panCamera) updateCameraPos();
        if (!isPaused)
        {
            if (waitBeforeSpawn > 0) waitBeforeSpawn -= Time.deltaTime;
            else if (!enemySpawner.enabled) enemySpawner.enabled = true;
        }
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
            float x = (Screen.width / 2) - 75;
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
		title = false;
        panCamera = true;
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
        healthGUI.enabled = !isPaused;
        playerShip.enableSwitchWeapon = !isPaused;

        if (!restartPrompt)
            scoreGUI.enabled = !title;
        crossHair ch = GameObject.FindGameObjectWithTag("playerWeapon").GetComponent<crossHair>();
        if (ch != null)
            ch.enabled = !isPaused;
    }

    public void NotifyDeath(IShip deathShip)
    {
        restartPrompt = true;

        if (deathShip != playerShip) 
            victoryText.SetActive(true);
        else 
            defeatText.SetActive(true);

        togglePaused();
    }
}
