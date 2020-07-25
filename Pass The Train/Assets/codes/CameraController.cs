using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	// Use this for initialization
	public GameObject justin;
	bool cameraAttached = true; 
	void Start () {
		Vector3 JustinPostion = justin.transform.position;
		this.gameObject.transform.position = new Vector3 (JustinPostion.x, JustinPostion.y + 2.2f, JustinPostion.z - 4f);
		
	}
	
	// Update is called once per frame
	void Update () {
		if (cameraAttached) {
			Vector3 JustinPosition = justin.transform.position;
			this.gameObject.transform.position = new Vector3 (
				this.gameObject.transform.position.x, this.gameObject.transform.position.y,
				JustinPosition.z-4f 
			);
				}
			}
	public void AdjustCamera(float xValue ){
		this.gameObject.transform.Translate (xValue, 0, 0);
	}
	public void DetachCamera(){
		cameraAttached = false;
	}
}
