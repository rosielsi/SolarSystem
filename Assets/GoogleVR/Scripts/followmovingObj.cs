using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followmovingObj : MonoBehaviour
{
    public Transform targetObj;
    public Vector3 cameraOffset;
    public float sm = 0.5f;
    public bool targetlook = false;
    // Start is called before the first frame update
    void Start()
    {
        cameraOffset = transform.position - targetObj.transform.position;    
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 newPosition = targetObj.transform.position + cameraOffset;
        transform.position = Vector3.Slerp(transform.position,newPosition,sm);

        //camera pos changee
        if (targetlook) {
            transform.LookAt(targetObj);
        }
    }
    void Stop() {
        cameraOffset = transform.position + targetObj.transform.position;

    }
}
