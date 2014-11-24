using UnityEngine;
using System.Collections;

public class LaserWeapon : IWeapon {

	public float laserWidth = 1.0f;
	public float noise = 1.0f;
	public float maxLength = 50.0f;
	public Color color = Color.red;

	
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
	
	
	// Use this for initialization
	void Start () {
		lineRenderer = gameObject.AddComponent("LineRenderer") as LineRenderer;
		lineRenderer.SetWidth(laserWidth, laserWidth);
		lineRenderer.SetVertexCount((int)maxLength);
		LaserMaterialRef = (Material)Resources.LoadAssetAtPath("Assets/Materials/laser2.mat", typeof(Material));
		lineRenderer.material = LaserMaterialRef;
		lineRenderer.material.mainTextureOffset = new Vector2 (0, Time.time);
	}
	
	// Update is called once per frame
	void Update () {
		//Fire ();
	}

	override
	public bool Fire() {
		int i = 0;
		length = (int)maxLength;
		position = new Vector3[length];
		lineRenderer.SetVertexCount(length);
		while (i < length) {
			Vector3 pos = new Vector3(transform.position.x, transform.position.y , i * 0.5F);
			lineRenderer.SetPosition(i, pos);
			i++;
		}
		CheckCollsion();
		return true;
	}

	void CheckCollsion(){
		RaycastHit[] hit;
		hit = Physics.RaycastAll(transform.position, transform.forward, maxLength);
		int i = 0;
		while(i < hit.Length){
			//Check to make sure we aren't hitting triggers but colliders
			if(!hit[i].collider.isTrigger)
			{
				length = (int)Mathf.Round(hit[i].distance)+2;
				position = new Vector3[length];
				lineRenderer.SetVertexCount(length);
				//notify enemy die
				return;
			}
			i++;
		}

		//no collision,destory the laser
//		length = (int)maxLength;
//		position = new Vector3[length];
//		lineRenderer.SetVertexCount(length);

		lineRenderer.SetPosition (0, transform.position);
	}
	
	
}
