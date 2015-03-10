using UnityEngine;
using System.Collections;

public class Dawg : MonoBehaviour {
	public Verlet[] vertlets = new Verlet[16];

	public Verlet b1,b2,b3,b4,t,l1,l2,l3,r1,r2,r3,h1,h2,h3,h4,h5;


	public Vector3 velocity,forward, position;
	Vector3 shift;
	bool hitGround;
	LineRenderer br;


	void addDistanceConstraints ()
	{
		//body
		b1.addConstraint (new Constraint (b2, Vector3.Distance (b1.transform.position, b2.transform.position)));
		b1.addConstraint (new Constraint (b4, Vector3.Distance (b1.transform.position, b4.transform.position)));
		b1.addConstraint (new Constraint (b3, Vector3.Distance (b1.transform.position, b3.transform.position)));
		b2.addConstraint (new Constraint (b3, Vector3.Distance (b2.transform.position, b3.transform.position)));
		b2.addConstraint (new Constraint (b1, Vector3.Distance (b1.transform.position, b2.transform.position)));
		b2.addConstraint (new Constraint (b4, Vector3.Distance (b4.transform.position, b2.transform.position)));
		b3.addConstraint (new Constraint (b2, Vector3.Distance (b2.transform.position, b3.transform.position)));
		b3.addConstraint (new Constraint (b4, Vector3.Distance (b3.transform.position, b4.transform.position)));
		b4.addConstraint (new Constraint (b1, Vector3.Distance (b1.transform.position, b4.transform.position)));
		b4.addConstraint (new Constraint (b3, Vector3.Distance (b3.transform.position, b4.transform.position)));


		//leg
		l1.addConstraint (new Constraint (b1, Vector3.Distance (b1.transform.position, l1.transform.position)));
		l1.addConstraint (new Constraint (b2, Vector3.Distance (b2.transform.position, l1.transform.position)));
		l1.addConstraint (new Constraint (b4, Vector3.Distance (b4.transform.position, l1.transform.position)));
		l1.addConstraint (new Constraint (l2, Vector3.Distance (l1.transform.position, l2.transform.position)));
		l1.addConstraint (new Constraint (l2, Vector3.Distance (l1.transform.position, l2.transform.position)));

		l2.addConstraint (new Constraint (l1, Vector3.Distance (l2.transform.position, l1.transform.position)));
		l2.addConstraint (new Constraint (l3, Vector3.Distance (l2.transform.position, l3.transform.position)));
		l2.addConstraint (new Constraint (b2, Vector3.Distance (l2.transform.position, b2.transform.position)));
		l3.addConstraint (new Constraint (l2, Vector3.Distance (l2.transform.position, l3.transform.position)));
		l3.addConstraint (new Constraint (b2, Vector3.Distance (l3.transform.position, b2.transform.position)));
		//right leg
		r1.addConstraint (new Constraint (b2, Vector3.Distance (b2.transform.position, r1.transform.position)));
		r1.addConstraint (new Constraint (b1, Vector3.Distance (b1.transform.position, r1.transform.position)));
		r1.addConstraint (new Constraint (b3, Vector3.Distance (b3.transform.position, r1.transform.position)));
		r1.addConstraint (new Constraint (r2, Vector3.Distance (r1.transform.position, r2.transform.position)));

		r2.addConstraint (new Constraint (r1, Vector3.Distance (r2.transform.position, r1.transform.position)));
		r2.addConstraint (new Constraint (r3, Vector3.Distance (r2.transform.position, r3.transform.position)));
		r2.addConstraint (new Constraint (b1, Vector3.Distance (r2.transform.position, b1.transform.position)));
		r3.addConstraint (new Constraint (r2, Vector3.Distance (r2.transform.position, r3.transform.position)));
		r3.addConstraint (new Constraint (b1, Vector3.Distance (r3.transform.position, b1.transform.position)));


		//tail
		t.addConstraint (new Constraint (b3, Vector3.Distance (t.transform.position, b3.transform.position)));
		
		t.addConstraint (new Constraint (b1, Vector3.Distance (t.transform.position, b1.transform.position)));
		t.addConstraint (new Constraint (b2, Vector3.Distance (t.transform.position, b2.transform.position)));
		t.addConstraint (new Constraint (b4, Vector3.Distance (t.transform.position, b4.transform.position)));
		//head
		h1.addConstraint (new Constraint (h2, Vector3.Distance (h1.transform.position, h2.transform.position)));
		h1.addConstraint (new Constraint (h4, Vector3.Distance (h1.transform.position, h4.transform.position)));
		h2.addConstraint (new Constraint (h3, Vector3.Distance (h2.transform.position, h3.transform.position)));
		h2.addConstraint (new Constraint (h1, Vector3.Distance (h1.transform.position, h2.transform.position)));
		h3.addConstraint (new Constraint (h2, Vector3.Distance (h2.transform.position, h3.transform.position)));
		h3.addConstraint (new Constraint (h4, Vector3.Distance (h3.transform.position, h4.transform.position)));
		h4.addConstraint (new Constraint (h1, Vector3.Distance (h1.transform.position, h4.transform.position)));
		h4.addConstraint (new Constraint (h3, Vector3.Distance (h3.transform.position, h4.transform.position)));
		h1.addConstraint (new Constraint (h5, Vector3.Distance (h1.transform.position, h5.transform.position)));
		h2.addConstraint (new Constraint (h5, Vector3.Distance (h2.transform.position, h5.transform.position)));
		h3.addConstraint (new Constraint (h5, Vector3.Distance (h3.transform.position, h5.transform.position)));
		h4.addConstraint (new Constraint (h5, Vector3.Distance (h4.transform.position, h5.transform.position)));
		h5.addConstraint (new Constraint (h1, Vector3.Distance (h1.transform.position, h5.transform.position)));
		h5.addConstraint (new Constraint (h1, Vector3.Distance (h2.transform.position, h5.transform.position)));
		h5.addConstraint (new Constraint (h1, Vector3.Distance (h3.transform.position, h5.transform.position)));
		h5.addConstraint (new Constraint (h1, Vector3.Distance (h4.transform.position, h5.transform.position)));

		h2.addConstraint (new Constraint (b4, Vector3.Distance (h2.transform.position, b4.transform.position)));
		h1.addConstraint (new Constraint (b4, Vector3.Distance (h1.transform.position, b4.transform.position)));
		h3.addConstraint (new Constraint (b4, Vector3.Distance (h3.transform.position, b4.transform.position)));
		h4.addConstraint (new Constraint (b4, Vector3.Distance (h4.transform.position, b4.transform.position)));
		h5.addConstraint (new Constraint (b4, Vector3.Distance (h5.transform.position, b4.transform.position)));

		h5.addConstraint (new Constraint (b1, Vector3.Distance (h5.transform.position, b1.transform.position)));
		h5.addConstraint (new Constraint (b2, Vector3.Distance (h5.transform.position, b2.transform.position)));
		h5.addConstraint (new Constraint (b3, Vector3.Distance (h5.transform.position, b3.transform.position)));


	}

	// Use this for initialization
	void Start () {
		hitGround = false;

		addDistanceConstraints ();

		br = gameObject.AddComponent<LineRenderer>();
		br.SetWidth(0.02F, 0.02F);
		br.SetVertexCount (24);

		position = new Vector3 (0, 0, 0);
		foreach (Verlet v in vertlets) {
			v.container = this;
			v.forward = forward;
			v.velocity = velocity;
		}


	}
	void updateLine(){
		int i = 0;

		br.SetPosition(0, h2.transform.position);
		br.SetPosition(1, h3.transform.position);
		br.SetPosition(2, h4.transform.position);
		br.SetPosition(3, h1.transform.position);
		br.SetPosition(4, h2.transform.position);

		
		br.SetPosition(5, b4.transform.position);
		br.SetPosition(6, b1.transform.position);
		br.SetPosition(7, l1.transform.position);
		br.SetPosition(8, l2.transform.position);
		br.SetPosition(9, l3.transform.position);
		br.SetPosition(10, l2.transform.position);
		br.SetPosition(11, l1.transform.position);
		br.SetPosition(12, b1.transform.position);

		
		br.SetPosition(13, b2.transform.position);
		br.SetPosition(14, r1.transform.position);
		br.SetPosition(15, r2.transform.position);
		br.SetPosition(16, r3.transform.position);
		br.SetPosition(17, r2.transform.position);
		br.SetPosition(18, r1.transform.position);
		br.SetPosition(19, b2.transform.position);

		
		br.SetPosition(20, b3.transform.position);
		br.SetPosition(21, t.transform.position);
		br.SetPosition(22, b3.transform.position);
		
		br.SetPosition(23, b4.transform.position);

	}
	// Update is called once per frame
	void Update () {
		if (vertlets.Length == 0) {
			Destroy (this.gameObject);	
			Destroy (this);
		} else {

			Vector3 totalVel = new Vector3 (0, 0, 0);

			foreach (Verlet v in vertlets) {

				totalVel += v.velocity;
				position += v.transform.position;

			}

			position = position/vertlets.Length;
			totalVel = totalVel/vertlets.Length;
			if ((Mathf.Abs (totalVel.x) + Mathf.Abs (totalVel.y) < .03) || 
			    (position.x < -10) ||
			    (position.x > 10) ||
			    position.y < 0) {
					Invoke ("destroyAll", 5);
			}
		}
	}
	void LateUpdate(){
		updateLine ();
	}
	void destroyAll(){
		foreach (Verlet v in vertlets) {
			v.destroyAfter();

		}
	
		vertlets = new Verlet[0];
	}
}
