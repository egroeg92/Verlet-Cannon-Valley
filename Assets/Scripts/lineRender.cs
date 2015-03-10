using UnityEngine;
using System.Collections;

public class lineRender : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public Component getRenderer()
	{
		return gameObject.GetComponent<lineRender> ();
	}
}
