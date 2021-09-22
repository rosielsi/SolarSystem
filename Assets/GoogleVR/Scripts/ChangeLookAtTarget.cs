using UnityEngine;
using System.Collections;

public class ChangeLookAtTarget : MonoBehaviour {

	public GameObject target;

	void Start() {
		if (target == null) 
		{
			target = this.gameObject;
			Debug.Log ("ChangeLookAtTarget target not specified. Defaulting to parent GameObject");
		}
	}


	// Called when MouseDown on this gameObject
	void OnMouseDown () {

			LookAtTarget.target = target;
				if (target)
				{
			
					Camera.main.fieldOfView = Mathf.Clamp(60 * target.transform.localScale.x, 1, 50);

				}

	}





}
