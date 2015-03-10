using UnityEngine;
using System.Collections;

public class pointMass : MonoBehaviour {


	public ArrayList constraints = new ArrayList();
	public pointMass s1;

	public Vector3 veloc;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += veloc;
		/*
		Vector3 targetDir = s1.transform.position - transform.position;
		Vector3 forward = transform.forward;
		float angle = Vector3.Angle(targetDir, veloc);
		if (angle < 5.0F)
			print("close");
		*/


		//dirrection of velocity
		Debug.Log (veloc.normalized);





	}
}

public class con{
	public float dist;
	public pointMass verlet;

	public con(pointMass v, float d){
		dist = d;
		verlet = v;
	}
}
