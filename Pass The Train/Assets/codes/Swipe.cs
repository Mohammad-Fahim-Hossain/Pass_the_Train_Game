﻿using UnityEngine;
using System.Collections;

public class Swipe : MonoBehaviour {

	private float fingerStartTime  = 0.0f;
	private Vector2 fingerStartPos = Vector2.zero;
	
	private bool isSwipe = false;
	private float minSwipeDist  = 50.0f;
	private float maxSwipeTime = 0.5f;
	
	public enum SwipeDirection{
		none,
		up,
		down,
		right,
		left
	}
	
	public static SwipeDirection swipe;
	
	void Start () {
		swipe = SwipeDirection.none;
	}
	
	public static void ResetSwipe(){
		swipe = SwipeDirection.none;
	}
	
	
	// Update is called once per frame
	void Update () {
		
		if (Input.touchCount > 0){
			
			foreach (Touch touch in Input.touches)
			{
				switch (touch.phase)
				{
				case TouchPhase.Began :
					/* this is a new touch */
					isSwipe = true;
					fingerStartTime = Time.time;
					fingerStartPos = touch.position;
					break;
					
				case TouchPhase.Canceled :
					/* The touch is being canceled */
					isSwipe = false;
					break;
					
				case TouchPhase.Ended :
					
					float gestureTime = Time.time - fingerStartTime;
					float gestureDist = (touch.position - fingerStartPos).magnitude;
					
					if (isSwipe && gestureTime < maxSwipeTime && gestureDist > minSwipeDist){
						Vector2 direction = touch.position - fingerStartPos;
						Vector2 swipeType = Vector2.zero;
						
						if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y)){
							// the swipe is horizontal:
							swipeType = Vector2.right * Mathf.Sign(direction.x);
						}else{
							// the swipe is vertical:
							swipeType = Vector2.up * Mathf.Sign(direction.y);
						}
						
						if(swipeType.x != 0.0f){
							if(swipeType.x > 0.0f){
								// MOVE RIGHT
								swipe = SwipeDirection.right;
							}else{
								// MOVE LEFT
								swipe = SwipeDirection.left;
							}
						}
						
						if(swipeType.y != 0.0f ){
							if(swipeType.y > 0.0f){
								// MOVE UP
								swipe = SwipeDirection.up;
							}else{
								// MOVE DOWN
								swipe = SwipeDirection.down;
							}
						}
						
					}
					
					break;
				}
			}
		}
		
	}

}
