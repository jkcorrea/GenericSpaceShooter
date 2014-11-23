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
	
	
	// Use this for initialization
	void Start () {
		lineRenderer = GetComponent<LineRenderer>();
		myTransform = transform;
		lineRenderer.SetWidth(laserWidth, laserWidth);
		lineRenderer.SetVertexCount((int)maxLength);
	}
	
	// Update is called once per frame
	void Update () {
		//Fire ();
	}

	override
	public bool Fire() {

		CheckCollsion();
		int i = 0;
		while (i < length) {
			Vector3 pos = new Vector3(myTransform.position.x, myTransform.position.y , i * 0.5F);
			lineRenderer.SetPosition(i, pos);
			i++;
		}
		return true;
	}

	void CheckCollsion(){
		RaycastHit[] hit;
		hit = Physics.RaycastAll(myTransform.position, myTransform.forward, maxLength);
		int i = 0;
		while(i < hit.Length){
			//Check to make sure we aren't hitting triggers but colliders
			if(!hit[i].collider.isTrigger)
			{
				length = (int)Mathf.Round(hit[i].distance)+2;
				position = new Vector3[length];
				lineRenderer.SetVertexCount(length);

				return;
			}
			i++;
		}

		length = (int)maxLength;
		position = new Vector3[length];
		lineRenderer.SetVertexCount(length);
	}
	
	
}
