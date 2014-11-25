using UnityEngine;
using System.Collections;

public class LaserWeapon : IWeapon {

	public float laserWidth = 1.0f;
	public float noise = 1.0f;
	public float maxLength = 50.0f;
	public Color color = Color.red;
	public float damage = 50;

	Camera mainCam;
	LineRenderer lineRenderer;
	int length;
	Vector3[] position;
	//Cache any transforms here
	Transform myTransform;
	Transform endEffectTransform;
	//The particle system, in this case sparks which will be created by the Laser
	public ParticleSystem endEffect;
	Vector3 offset;
	Material LaserMaterialRef;
	bool isShowingLaser;
	crossHair laserCrossHair;
	
	
	// Use this for initialization
	void Start () {
		isShowingLaser = false;
		lineRenderer = gameObject.AddComponent("LineRenderer") as LineRenderer;
		lineRenderer.SetWidth(laserWidth, laserWidth);
		lineRenderer.SetVertexCount((int)maxLength);
		LaserMaterialRef = (Material)Resources.LoadAssetAtPath("Assets/Materials/laser2.mat", typeof(Material));
		lineRenderer.material = LaserMaterialRef;
		lineRenderer.material.mainTextureOffset = new Vector2 (0, Time.time);
		lineRenderer.enabled = false;

		mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
		laserCrossHair = gameObject.GetComponent<crossHair> ();
	}
	
	// Update is called once per frame
	void Update () {
	}

	override
	public bool Fire() {
		StopCoroutine ("showLaser");
		StartCoroutine ("showLaser");
		return true;
	}

	void CheckCollsion(){
		RaycastHit[] hit;
		hit = Physics.RaycastAll(transform.position, transform.forward, maxLength);
		int i = 0;
		while(i < hit.Length){
			//Check to make sure we aren't hitting triggers but colliders
			if(!hit[i].collider.isTrigger)
//		if(Physics.Raycast(transform.position, transform.forward, out hit, maxLength))
			{
				length = (int)Mathf.Round(hit[i].distance)+2;
				position = new Vector3[length];
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
		audio.Play ();
		yield return new WaitForSeconds (.1f);
		resetLaser();
		isShowingLaser = false;
	}

	void generateLaser() {
		int i = 0;
		length = (int)maxLength;
		position = new Vector3[length];
		lineRenderer.SetVertexCount(length);
		while (i < length) {
			Vector3 pos = new Vector3(transform.position.x, transform.position.y , i * 0.5F);
			lineRenderer.SetPosition(i, pos);
			i++;
		}
		Vector3 posit = new Vector3(transform.position.x, transform.position.y , i * 0.5F);
		Vector3 screenPos = mainCam.WorldToScreenPoint(posit);
		Debug.Log("laser End Position" + posit.x.ToString() + " " +  posit.y.ToString()+ " " + posit.z.ToString());
		Debug.Log("laser End Position Screen" + screenPos.x.ToString() + " " +  screenPos.y.ToString()+ " " + screenPos.z.ToString());

		//laserCrossHair.crossHairLoc = new Vector2 (screenPos.x, screenPos.y);
		CheckCollsion ();
	}

	void resetLaser()
	{    
		lineRenderer.enabled = false;
	}
	
	
}
