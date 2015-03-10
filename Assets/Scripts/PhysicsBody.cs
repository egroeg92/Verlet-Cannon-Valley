using UnityEngine;
using System.Collections;

public class PhysicsBody : MonoBehaviour {

	protected Valley valley;
	protected float coeff = .5f;

	public float mass ;
	public Vector3 position;
	public Vector3 acceleration;
	public Vector3 velocity;

	public Vector3 forward;

	float prevX;

	public static float gravity = 0.005f;
	float forceGravity = 1;

	// Use this for initialization
	protected void Start () {
		valley = GameObject.Find("Valley").GetComponent<Valley> ();
		valley.AddBody (this);
		position = transform.position;
		forceGravity = mass * -gravity;
		acceleration = new Vector3 (0, forceGravity, 0);
		prevX = velocity.x;

	}
	
	// Update is called once per frame
	protected void Update () {
		if(velocity.x / (Mathf.Abs(velocity.x)) != prevX/(Mathf.Abs(prevX)))
        	forward = -forward;
		
 		prevX = velocity.x;
		if (velocity.x != 0) {
			acceleration.x = -(valley.airRes * velocity.x) ; 
		}
		if (velocity.y != 0) {
			acceleration.y = -(valley.airRes * velocity.y) ; 
		}
		acceleration.y += mass * -gravity;
		velocity += acceleration;
		position += velocity;
		transform.position = position;

	}

	/// <summary>
	/// Destroies the this.
	/// </summary>
	protected void destroyThis(){
		valley.removeBody (this);	
		Destroy (this.gameObject);	
		Destroy (this);
	}
	/// <summary>
	/// 	/// </summary>
	/// <returns>The forward collision.</returns>
	protected GameObject detectForwardCollision(){
		GameObject collision = null;
		RaycastHit hit;
		if (Physics.Raycast (transform.position, forward, out hit))
		{
			float xDist;
			if(forward.x > 0)
				xDist = Mathf.Abs(hit.transform.position.x - hit.transform.localScale.x/2 - (transform.position.x + transform.localScale.x/2 ));
			else
				xDist = Mathf.Abs(hit.transform.position.x + hit.transform.localScale.x/2 - (transform.position.x - transform.localScale.x/2 ));

			if(xDist < transform.localScale.x/2  )
				collision = hit.transform.gameObject;
			

		}

		return collision;
	}
	/// <summary>
	///  </summary>
	/// <returns>The backward collision.</returns>
	protected GameObject detectBackwardCollision(){
		GameObject collision = null;
		RaycastHit hit;
		if (Physics.Raycast (transform.position, -forward, out hit))
		{
			float xDist;
			if(forward.x > 0)
				xDist = Mathf.Abs(hit.transform.position.x - hit.transform.localScale.x/2 - (transform.position.x + transform.localScale.x/2 ));
			else
				xDist = Mathf.Abs(hit.transform.position.x + hit.transform.localScale.x/2 - (transform.position.x - transform.localScale.x/2 ));
			
			
			if(xDist < transform.localScale.x/2  || xDist < 0.06)
				collision = hit.transform.gameObject;
			
			
		}
		
		return collision;
	}

	/// <summary>
	/// Detects the bottom collision.
	/// </summary>
	/// <returns>The bottom collision.</returns>
	protected GameObject detectBottomCollision(){
		GameObject collision = null;
		RaycastHit hit;
		if (Physics.Raycast (transform.position, Vector3.down, out hit)) {

			float yDist = Mathf.Abs( hit.transform.position.y + hit.transform.localScale.y/2 - (transform.position.y));

			if(yDist < transform.localScale.y/2)
			{
				collision = hit.transform.gameObject;

			}
		}
		return collision;
	}
}
