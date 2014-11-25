using UnityEngine;
using System.Collections;

public class crossHair : MonoBehaviour {

	bool drawCrosshair = true;
	Color crosshairColor = Color.white;

	float width = 3.0f;      //Crosshair width
	float height = 35.0f;     //Crosshair height
	// Use this for initialization
	
    Texture2D tex;
	
	GUIStyle lineStyle;

	public Vector2 crossHairLoc;

	Camera mainCam;

	public class spreading{
		public float spread = 20.0f;          //Adjust this for a bigger or smaller crosshair
		public float maxSpread = 60.0f;
		public float minSpread = 20.0f;
		public float spreadPerSecond = 30.0f;
		public float decreasePerSecond = 25.0f;
	}
	
	spreading spread;
	
	void Awake (){
		tex = new Texture2D(1,1);

		SetColor(tex, crosshairColor); //Set color
		
		lineStyle = new GUIStyle();
		lineStyle.normal.background = tex;
		spread = new spreading ();
		mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
		crossHairLoc = Vector2.zero;
	}

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetButton("Fire1") || Input.GetKeyDown(KeyCode.Space)){
			spread.spread += spread.spreadPerSecond * Time.deltaTime;       //Incremente the spread
			//Fire();
		}else{
			spread.spread -= spread.decreasePerSecond * Time.deltaTime;      //Decrement the spread        
		}
		
		spread.spread = Mathf.Clamp(spread.spread, spread.minSpread, spread.maxSpread);  

 	}

	void OnGUI(){
		Vector2 centerPoint = new Vector2(Screen.width / 2, Screen.height / 2);
//		Vector3 screenPos = mainCam.WorldToScreenPoint(transform.position);
//		Vector2 centerPoint = new Vector2 (screenPos.x, screenPos.y);
		//Vector2 centerPoint = new Vector2(crossHairLoc.x, crossHairLoc.y);
		
		//if(drawCrosshair && crossHairLoc != Vector2.zero){
		if(drawCrosshair){

			GUI.Box(new Rect(centerPoint.x - width / 2, centerPoint.y - (height + spread.spread), width, height), "", lineStyle);
			GUI.Box(new Rect(centerPoint.x - width / 2, centerPoint.y + spread.spread, width, height), "", lineStyle);
			GUI.Box(new Rect(centerPoint.x + spread.spread, (centerPoint.y - width / 2), height , width), "", lineStyle);
			GUI.Box(new Rect(centerPoint.x - (height + spread.spread), (centerPoint.y - width / 2), height , width), "", lineStyle);

		}   
	}

	void SetColor(Texture2D myTexture, Color myColor){
		for (int y = 0; y < myTexture.height; ++y){
			for (int x = 0; x < myTexture.width; ++x){
				myTexture.SetPixel(x, y, myColor);
			}
		}	
		myTexture.Apply();
	}
}



//Applies color to the crosshair

