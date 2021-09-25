using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changePos : MonoBehaviour
{
    private RaycastHit _hit;
    void Start()
    {




        Ray ray = Camera.main.ViewportPointToRay(new Vector3(.5f, .5f, 0f));

        if (Physics.Raycast(ray, out _hit))
        {

            _hit.transform.gameObject.GetComponent<teleport>().spacemanTravel();

        }

    }
}
