using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] bool XRotate = false;
    [SerializeField] bool YRotate = false;
    [SerializeField] bool ZRotate = false;
    [SerializeField] float rotateSpeed = 1f;


    // Update is called once per frame
    void Update()
    {
        if (XRotate == true)
        {
            transform.Rotate(Vector3.right, rotateSpeed * Time.deltaTime);
        }
        if (YRotate == true)
        {
            transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);
        }
        if (ZRotate == true)
        {
            transform.Rotate(Vector3.forward, rotateSpeed * Time.deltaTime);
        }

    }
}
