using UnityEngine;
using System.Collections;

public class TerrainGenerator : MonoBehaviour {

	public float roughness = 0.00f;

	public GameObject Cannon;

	public Valley valley;


	private Terrain terrain;
	public int mapWidth; 
	public int mapHeight;

	int width = 10; 
	int slopeWidth; 
	int slopeHeight;


	// Use this for initialization
	void Start () {
		valley = gameObject.GetComponent<Valley> ();
		GenerateHeights();

	
	}
	private void createCannon(GameObject cube, bool left){

		Vector3 pos = cube.transform.position + new Vector3 (0, cube.transform.localScale.y / 2, 0);
		Quaternion rot = Quaternion.identity;
		if(left)
			rot *= Quaternion.Euler(Vector3.up * 180);


		Cannon cannon = Instantiate (Cannon, pos, rot) as Cannon; 


	}

	private void GenerateHeights()
	{

		float secWidth = width/(float)mapWidth;
		int slopeWidth = mapWidth / 5;
		int slopeHeight = mapHeight;


		float[] heights = new float[mapWidth];


		for (int i = 0; i < slopeWidth ; i++)
		{

			heights[i] = slopeHeight;
			heights[i+(2*slopeWidth)] = 0;
			heights[i+(4*slopeWidth)] = slopeHeight;
		}


		int left = slopeWidth;
		int right = left + slopeWidth - 1;
		int mid = ((right-left) / 2) + left;

		int left2 = slopeWidth * 3;
		int right2 = left2 + slopeWidth - 1;
		int mid2 = ((right2-left2) / 2) + left2;
		

		heights[left] = slopeHeight ;
		heights[right] = 0;
			
		heights[left2] = 0 ;
		heights[right2] = slopeHeight;
		
		midPointBisection (left, mid, right, heights);
		midPointBisection (left2, mid2, right2, heights);

		float p = 0;


		int leftPos = (int)(heights.Length/5  + ((heights.Length / 5) / 3)-1);
		int rightPos = (int)(4 * heights.Length / 5 - ((heights.Length / 5) / 3)-1);



		int index = 0;
		foreach (float f in heights) {
			GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
			cube.name = "Wall";
			if(f == 0)
				cube.name = "Ground";
			cube.transform.localScale = new Vector3(secWidth,f+secWidth,1);
			cube.transform.position = new Vector3(p,f/2,0);
			p += cube.transform.localScale.x;

			if(index == leftPos){
				createCannon(cube,false);
			}
			if(index == rightPos){
				createCannon(cube,true);
			}
			index += 1;

		}

	}

	private void midPointBisection(int left, int mid, int right, float[] heights){

		if (left == mid || right == mid) {

			return;
		}

		float midHeight = Mathf.Abs(heights[left] + ((heights[right]-heights[left])/2));

		float rand = Random.Range (-roughness,roughness);

		heights[mid] = midHeight + rand;


		int midLeft = ((mid - left)/2)+left;
		int midRight = ((right - mid)/2)+mid;

		midPointBisection(left,midLeft,mid,heights);
		midPointBisection (mid, midRight, right, heights);

	}
	

}
