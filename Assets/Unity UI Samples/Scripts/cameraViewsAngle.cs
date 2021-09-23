using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraViewsAngle : MonoBehaviour
{
    public GameObject Tcam;
    public GameObject FCam1;
    public GameObject FCam2;
    public GameObject FCam3;

    // Update is called once per frame
    void Update()
    {
        Tcam.SetActive(true);
        FCam1.SetActive(false);
        FCam2.SetActive(false);
        FCam3.SetActive(false);
    }
}
