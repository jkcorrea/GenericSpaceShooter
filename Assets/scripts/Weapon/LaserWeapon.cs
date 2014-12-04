using UnityEngine;
using System.Collections;

public class LaserWeapon : IWeapon {

	public float laserWidth = 1.0f;
	public float noise = 1.0f;
	public float maxLength = 50.0f;
	public Color color = Color.red;
    public float damage = 50;
    public crossHair laserCrossHair;

//	Camera mainCam;
	LineRenderer lineRenderer;
	int length;
//	Vector3[] position;
	//Cache any transforms here
	Transform myTransform;
	Transform endEffectTransform;
	//The particle system, in this case sparks which will be created by the Laser
	public ParticleSystem endEffect;
	Vector3 offset;
	Material LaserMaterialRef;
	bool isShowingLaser;
<<<<<<< HEAD
	bool isPlayerOrNot;
=======
	crossHair laserCrossHair;
//	bool isPlayerOrNot;
>>>>>>> 1a00b4f2719944cbfd59c138f397c581ce6485c4
	
	
	// Use this for initialization
	void Start () {
//		isPlayerOrNot = false;
		isShowingLaser = false;
		lineRenderer = gameObject.AddComponent("LineRenderer") as LineRenderer;
		lineRenderer.SetWidth(laserWidth, laserWidth);
		lineRenderer.SetVertexCount((int)maxLength);
		LaserMaterialRef = (Material)Resources.LoadAssetAtPath("Assets/Materials/laser1.mat", typeof(Material));
		lineRenderer.material = LaserMaterialRef;
		lineRenderer.material.mainTextureOffset = new Vector2 (0, Time.time);
		lineRenderer.enabled = false;

//		mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
		if (gameObject.tag.Equals ("Player")) {
//			isPlayerOrNot = true;
			laserCrossHair = gameObject.AddComponent<crossHair>();
			laserCrossHair.shooting = false;
		}

	}
	
	// Update is called once per frame
	void Update () {
	}

	override
	public GameObject Fire() {
		StopCoroutine ("showLaser");
		StartCoroutine ("showLaser");
		audio.Play ();
		return null;
	}

	void CheckCollsion(){
		RaycastHit[] hit;
		hit = Physics.RaycastAll(transform.position, transform.up, maxLength);
		int i = 0;
		while(i < hit.Length){
			//Check to make sure we aren't hitting triggers but colliders
			if(!hit[i].collider.isTrigger)
//		if(Physics.Raycast(transform.position, transform.forward, out hit, maxLength))
			{
				length = (int)Mathf.Round(hit[i].distance)+2;
//				position = new Vector3[length];
				lineRenderer.SetVertexCount(length);
				//notify enemy die
				hit[i].collider.gameObject.SendMessage("ApplyDamage", damage, SendMessageOptions.DontRequireReceiver);

				return;
			}
			i++;
		}
	}

	IEnumerator showLaser(){
		if (isShowingLaser) {
			yield return true;
		}
		isShowingLaser = true;
		lineRenderer.enabled = true;
		generateLaser ();
//		audio.Play ();
		yield return new WaitForSeconds (.1f);
		resetLaser();
		isShowingLaser = false;
	}

	void generateLaser() {

//		if (isPlayerOrNot) {
//			laserCrossHair.shooting = true;
//			Vector3 posit = new Vector3 (transform.position.x, transform.position.y, length * 0.5F);
//			Vector3 screenPos = mainCam.WorldToScreenPoint (posit);
//			laserCrossHair.crossHairLoc = screenPos;
//		}

		int i = 0;
		length = (int)maxLength;
//		position = new Vector3[length];
		lineRenderer.SetVertexCount(length);
		while (i < length) {
			Vector3 pos = new Vector3(transform.position.x, transform.position.y , i * 0.5F);
			lineRenderer.SetPosition(i, pos);
			i++;
		}
	
//		Debug.Log("laser End Position" + posit.x.ToString() + " " +  posit.y.ToString()+ " " + posit.z.ToString());
//		Debug.Log("laser End Position Screen" + screenPos.x.ToString() + " " +  screenPos.y.ToString()+ " " + screenPos.z.ToString());
//
		CheckCollsion ();
	}

	void resetLaser()
	{    
		lineRenderer.enabled = false;
	}
	
	
}
