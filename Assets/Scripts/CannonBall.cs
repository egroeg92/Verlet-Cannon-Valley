using UnityEngine;
using System.Collections;

public class CannonBall : PhysicsBody {

	// Use this for initialization
	void Start () {
		mass = 1;
		base.Start ();

	}
	
	// Update is called once per frame
	void Update () {
		base.Update ();
	

		GameObject collision = base.detectForwardCollision ();

		//Debug.Log (velocity);
		if (collision != null) {
			if(collision.name =="Wall"){

				velocity = new Vector3(-velocity.x  * coeff, -velocity.y * coeff ,velocity.z);
				if(forward.x > 0)
					position = new Vector3(collision.transform.position.x- collision.transform.localScale.x/2 - transform.localScale.x, transform.position.y, transform.position.z);
				else
					position = new Vector3(collision.transform.position.x +collision.transform.localScale.x/2+ transform.localScale.x, transform.position.y, transform.position.z);
				//forward = -forward;
			}
		}
		collision = base.detectBottomCollision ();

		if (collision != null) {
			if (collision.name == "Ground") {
			//	velocity = new Vector3(velocity.x  , -velocity.y * coeff ,velocity.z);	
			//	position = new Vector3 (transform.position.x, collision.transform.position.y + collision.transform.localScale.y / 2 + transform.localScale.y / 2, transform.position.z);
				destroyThis();
			}
			
		} 
		
		
		//Debug.Log (Mathf.Abs (velocity.x) + Mathf.Abs (velocity.y));
		if ((Mathf.Abs(velocity.x) + Mathf.Abs(velocity.y) < .02)||
		    transform.position.x > 10 ||
		    transform.position.x < -10 ||
		    transform.position.y < -5)
			Invoke ("destroyThis", 3);
	}

	
	
}
