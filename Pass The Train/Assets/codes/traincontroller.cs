
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class traincontroller : MonoBehaviour {

	public bool moving=false;

	public float speed=1f;

	private bool signaledtoMove=false;
	// Use this for initialization
	void Start () {
		
	}
	void FixedUpdate(){
		if (moving && signaledtoMove) {
			this.GetComponent<Rigidbody> ().AddForce (Vector3.back * 250 * speed);
		
		}
		if (this.gameObject.transform.position.z < -520 || this.gameObject.transform.position.y < -20) {
			Destroy (this.gameObject);
		}

	}
	public void SignalToMove(){
		this.signaledtoMove = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
