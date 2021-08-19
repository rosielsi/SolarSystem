using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotation : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform trans_target;
    public int speed;

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(trans_target.transform.position,trans_target.transform.up,speed*Time.deltaTime);
    }
}
