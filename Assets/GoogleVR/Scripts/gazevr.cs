using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gazevr : MonoBehaviour
{
    private RaycastHit _hit;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    
   void Update()
    {


  

        Ray ray = Camera.main.ViewportPointToRay(new Vector3(.5f, .5f, 0f));

        if (Physics.Raycast(ray, out _hit))
        {
           
                _hit.transform.gameObject.GetComponent<teleport>().spacemanTravel();
            
        }

    }






}
