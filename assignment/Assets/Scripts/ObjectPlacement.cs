using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPlacement : MonoBehaviour
{
    private Ray ray;
    private RaycastHit detect;


    // Start is called before the first frame update
    void Start()
    {
        DetectCollider();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DetectCollider()
    {
        ray = new Ray(transform.position, -transform.up);

        if (Physics.Raycast(ray, out detect)) transform.position = new Vector3(detect.point.y, detect.point.z);

        else
        {
            ray = new Ray(transform.position, transform.up);

            if (Physics.Raycast(ray, out detect)) transform.position = new Vector3(detect.point.y, detect.point.z);
        }
    }
}
