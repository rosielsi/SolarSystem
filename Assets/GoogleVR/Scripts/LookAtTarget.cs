using UnityEngine;
using System.Collections;

public class LookAtTarget : MonoBehaviour {

	static public GameObject target;
	public int zoom = 20;
	public int normal = 60;
	public float smooth = 5;

	private bool isZoomed = false;
	/*void Start () {
		if (target == null) 
		{
			target = this.gameObject;
			Debug.Log ("LookAtTarget target not specified. Defaulting to parent GameObject");
		}
	}
	*/
	// Update is called once per frame
	void Update () {
		/*if (target)
		{
			transform.LookAt(target.transform);
		} */


			

			if (Input.GetMouseButtonDown(1))
			{
				
				isZoomed = !isZoomed;
			}
			if (isZoomed)
			{
				Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, zoom, Time.deltaTime * smooth);
			}
			
			else
			{
				Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, normal, Time.deltaTime * smooth);
			}
		
		
	}
}
