using UnityEngine;
using System.Collections;

public class Verlet : PhysicsBody {

	ArrayList constraints = new ArrayList ();

	public Dawg container;

	bool destroy = false;
	Vector3 lastPos;



	float ground;
	// Use this for initialization
	void Start () {

		mass = 1;
		base.Start ();
		GameObject g = GameObject.Find ("Ground");
		ground = g.transform.position.y + g.transform.localScale.y / 2;
		lastPos = transform.position;



	}

	public void addConstraint(Constraint c){
		constraints.Add (c);
	}

	void Update(){
		base.Update ();

		if (transform.position.y - transform.localScale.y / 2 < ground) {
			transform.position = new Vector3 (transform.position.x, ground + transform.localScale.y / 2, transform.position.z);
		}
		detectForwardCollision ();
		detectCannonCollision ();
		detectBottomCollision ();

		respectConstraints ();

	}
	void LateUpdate(){
		if (destroy) {
			destroyThis();
		}
	}

	public GameObject detectForwardCollision(){
		GameObject collision = base.detectForwardCollision();

		if (collision != null) {
			if(collision.name =="Wall" ){
				velocity = new Vector3(-velocity.x  * coeff, -velocity.y * coeff,velocity.z);

				if(forward.x > 0)
					transform.position = new Vector3(collision.transform.position.x- collision.transform.localScale.x/2 - transform.localScale.x, transform.position.y, transform.position.z);
				else
					transform.position = new Vector3(collision.transform.position.x +collision.transform.localScale.x/2+ transform.localScale.x, transform.position.y, transform.position.z);


				//forward = -forward;
			}


		}

		return collision;

	}
	public void detectCannonCollision(){
		Object[] balls = Object.FindObjectsOfType(typeof(CannonBall));

		foreach (CannonBall o in balls){
			if(Vector3.Distance(o.transform.position,transform.position) < o.transform.localScale.x/2){

				velocity+= o.velocity * 5;
			}

		}
	}


	public GameObject detectBottomCollision(){
		GameObject collision = base.detectBottomCollision ();
		if (collision != null) {
			if (collision.name == "Ground") {
				velocity = new Vector3 (velocity.x, -velocity.y * coeff, velocity.z);
					position = new Vector3 (transform.position.x, collision.transform.position.y + collision.transform.localScale.y / 2 + transform.localScale.y / 2, transform.position.z);
			}
			
		} 
		return collision;
	}


	void respectConstraints(){

		foreach(Constraint c in constraints){

			Vector3 otherPos = c.verlet.transform.position;

			float restingDistance = c.dist;


			float diffX = transform.position.x - otherPos.x;
			float diffY = transform.position.y - otherPos.y;


			float d = Mathf.Sqrt(diffX * diffX + diffY * diffY); 
			
			// difference scalar
			float difference = (restingDistance - d) / d ;

			// translation for each PointMass. They'll be pushed 1/2 the required distance to match their resting distances.
			float translateX = diffX * 0.5f * difference;
			float translateY = diffY * 0.5f * difference;

			if(Mathf.Abs (translateX) > 0.01 && Mathf.Abs(translateY) > 0.01){
				transform.position = new Vector3(transform.position.x +translateX , transform.position.y + translateY , transform.position.z);
				c.verlet.transform.position = new Vector3(c.verlet.transform.position.x - translateX , c.verlet.transform.position.y - translateY , transform.position.z);
			}
		}

		position = transform.position;
	}

	public void destroyAfter(){
		destroy = true;
	}


}
public class Constraint{
	public float dist;
	public Verlet verlet;


	public Constraint(Verlet v, float d){
		dist = d;
		verlet = v;

	}

}