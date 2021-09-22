using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class profiling : MonoBehaviour
{
    GameObject dummy;
    void Update()
    {
        dummy = new GameObject("Dummy");
        Transform t = dummy.GetComponent<Transform>();
    }

    // Update is called once per frame
    void LaTeUpdate()
    {
        Destroy(dummy);
    }
}
