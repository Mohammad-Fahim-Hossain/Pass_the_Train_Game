using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour {

	bool alive=true;
	bool ground=true;
	int jump=0;
	int maxJumpCycles=15;
	int left=0;
	int maxLeftCycles=5;
	int right=0;
	int maxRightCycles=5;
	public Camera mainCamera;
	public Material skybox;




	// Use this for initialization
	void Start () {
		jump = maxJumpCycles;
		left = maxLeftCycles;
		right = maxRightCycles;
		
	}
	
	// Update is called once per frame
	void Update () {
		if((Input.GetKeyUp(KeyCode.RightArrow))||
			Swipe.swipe==Swipe.SwipeDirection.right){
			Swipe.ResetSwipe();
			right=0;
		}
		if((Input.GetKeyUp(KeyCode.LeftArrow))||
			Swipe.swipe==Swipe.SwipeDirection.left){
			Swipe.ResetSwipe();
			left=0;
		}
		if(((Input.GetKeyUp(KeyCode.Space))||
			Swipe.swipe==Swipe.SwipeDirection.up)&& ground){
			Swipe.ResetSwipe();
			jump=0;
		}
		ActivateTrainsInRange ();
		CheckifDied ();
		UpDateSkybox ();

	}
	void UpDateSkybox(){
		skybox.SetFloat ("_AtmosphereThickness", Mathf.Repeat (Time.time / 20, 5f));
	
	}



	private void ActivateTrainsInRange(){
		Collider[] colliders = Physics.OverlapSphere(
			this.gameObject.transform.position,50f);
		foreach(Collider c in colliders ){
			if(c.gameObject.GetComponent<traincontroller>() !=null) {
				c.gameObject.GetComponent<traincontroller> ().SignalToMove ();

			}

		}
	
	
	}

	void FixedUpdate(){
		if (alive) {
			this.GetComponent<Rigidbody> ().velocity = new Vector3 (0, 0, 10);
		} 
		if (jump < maxJumpCycles) {
			this.GetComponent<Animator> ().speed = 0;
			this.GetComponent<Rigidbody> ().AddForce (0, 600, 500);
			jump++;
			ground = false;
		}
		if (left < maxLeftCycles) {
			this.GetComponent<Rigidbody> ().MovePosition (
				new Vector3 (
					this.gameObject.transform.position.x - (2.7f / maxLeftCycles),
					this.gameObject.transform.position.y,
					this.gameObject.transform.position.z)
			);
			mainCamera.SendMessage ("AdjustCamera", -(2.7f / maxLeftCycles));

			left++;
		
		}
		if(right<maxRightCycles){
			this.GetComponent<Rigidbody>().MovePosition(
				new Vector3(
					this.gameObject.transform.position.x+(2.7f/maxRightCycles),
					this.gameObject.transform.position.y,
					this.gameObject.transform.position.z)
			);
			mainCamera.SendMessage ("AdjustCamera", (2.7f / maxRightCycles)); 

				right++;

		}
	}

	void CheckifDied(){
	
		if (this.gameObject.transform.position.y < -5) {
		
			Application.LoadLevel (0);
		
		
		}
	}




				void OnCollisionEnter(Collision collision){


		if (collision.collider.gameObject.layer == LayerMask.NameToLayer ("Obstacle")) {
			alive = false;
			this.GetComponent<Animator> ().speed = 0;
			mainCamera.GetComponent<CameraController> ().DetachCamera ();
			this.GetComponent<Rigidbody> ().velocity = new Vector3 (0, 0, 0);
			this.GetComponent<Rigidbody> ().AddForce (-500f, 1500f, -1000f);
		
		
		
		
		}



		if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Ground")) {
			ground = true;
			this.GetComponent<Animator> ().speed = 1;
					
					}
				
				} 
}
