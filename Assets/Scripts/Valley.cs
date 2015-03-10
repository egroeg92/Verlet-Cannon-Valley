using UnityEngine;
using System.Collections;

public class Valley : MonoBehaviour {

	public float windRange;
	public float airRes;
	Vector3 wind ;

	public GameObject windMeter;
	public float rangePercent;
	Vector3 defaultPos;
	float posMag;
	GameObject meter,hand;
	ArrayList bodies = new ArrayList();


	// Use this for initialization
	void Start () {

		InvokeRepeating ("changeWind",0, 0.5f);
		meter = windMeter.transform.Find ("Meter").gameObject;
		hand = windMeter.transform.Find ("Hand").gameObject;

		defaultPos = hand.transform.position;
		posMag = meter.transform.localScale.x / 2;

		rangePercent = rangePercent / 100;

	}
	void changeWind(){
		float f = Random.Range (-windRange, windRange);
		wind = new Vector3 (f, 0, 0);
		foreach (PhysicsBody b in bodies) {
			b.velocity += wind;
		}

		if (wind.x == 0) {
			hand.transform.position = defaultPos;
		}else{
			float percent = f / windRange;

			hand.transform.position = new Vector3(defaultPos.x + (posMag * percent),defaultPos.y,defaultPos.z);

		}

	}
	// Update is called once per frame
	void Update () {

	}

	public void AddBody(PhysicsBody b){
			bodies.Add (b);
	}
	public void removeBody(PhysicsBody b){
			bodies.Remove (b);
	}
}
