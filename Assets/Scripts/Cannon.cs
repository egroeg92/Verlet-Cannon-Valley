﻿using UnityEngine;
using System.Collections;

public class Cannon : MonoBehaviour {


	public CannonBall cannonBall;
	public Verlet verlet;
	public Dawg dawg;

	Transform tip;
	float shootSpeed;
	float shootPowerRange;
	Vector3 forward;
	Quaternion identity;
	bool left;
	Valley valley;

	// Use this for initialization
	void Start () {
		valley = GameObject.Find("Valley").GetComponent<Valley> ();
		shootPowerRange = valley.shootPowerRange;


		shootSpeed = valley.shootSpeed;
		identity = transform.rotation;
		left = true;
		forward = new Vector3 (1, 0, 0);
		if (identity != Quaternion.identity) {
			left = false;
			forward = new Vector3(-1,0,0);

		}
		tip = transform.Find ("Barrel") ;
		tip = tip.Find ("front");
		tip = tip.Find ("Tip");


	
	}
	
	// Update is called once per frame
	void Update () {
		shootPowerRange = valley.shootPowerRange;

		shootSpeed = valley.shootSpeed;
		if (Input.GetKeyDown ("space") && !left) {
			shoot();
			setAngle();
		
		}
		if (Input.GetKeyDown ("tab") && left) {
			shoot();
			setAngle();
			
		}
			
	}

	private void shoot(){

		float range = Random.Range (shootSpeed - (shootPowerRange), (shootSpeed + shootPowerRange));

		Vector3 velocity = (transform.rotation * new Vector3(1,0,0)) * range;

		if (left) {
			Dawg Dawg = generatePoints();
			Dawg.forward = forward;
			Dawg.velocity = velocity;

		} else {
			PhysicsBody projectile = (PhysicsBody)Instantiate (cannonBall, tip.transform.position - new Vector3 (cannonBall.transform.localScale.x, 0, 0), Quaternion.identity);
			projectile.forward = forward;
			projectile.velocity = velocity;
		}
	}

	private void setAngle(){
		Quaternion rot = identity;
		
		float angle = Random.Range (15,60);
		rot *= Quaternion.Euler(Vector3.forward * angle);
		transform.rotation = rot;

	}

	public Dawg generatePoints(){
		Vector3 pos = new Vector3 (0, 0, 0);

		Dawg cont =  (Dawg)Instantiate (dawg, tip.transform.position + pos, Quaternion.identity);

		return cont;
		
		
		
	}
	float getAngle(Vector3 A, Vector3 B){
		//Debug.Log (v1.transform.position + "," + v2.transform.position);
		Vector3 targetDir = A - B;
		Vector3 forward = A;
		float angle = Vector3.Angle(targetDir, forward);
		
		if(A.y < B.y)
			return 360 - angle;
		
		
		return angle;
	}
}
