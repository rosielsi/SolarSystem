using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gazevr : MonoBehaviour
{
    private RaycastHit _hit;
    public Image imagegaze;
    public float toaltime = 2;
    bool gvrstatus;
    float gvrtimer;
    public int distanceOfRay = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    
   void Update()
    {


        if (gvrstatus)
        {
            gvrtimer += Time.deltaTime;
            imagegaze.fillAmount = gvrtimer / toaltime;
        }

        Ray ray = Camera.main.ViewportPointToRay(new Vector3(.5f, .5f, 0f));

        if (Physics.Raycast(ray, out _hit, distanceOfRay))
        {
            if (imagegaze.fillAmount == 1 && _hit.transform.CompareTag("teleport"))
            {
                _hit.transform.gameObject.GetComponent<teleport>().spacemanTravel();
            }
        }

    }





    public void GRon() {

        gvrstatus = true;
    }


    public void GRoff()
    {
        gvrstatus = false;
        gvrtimer = 0;
        imagegaze.fillAmount = 0;
    }
}
