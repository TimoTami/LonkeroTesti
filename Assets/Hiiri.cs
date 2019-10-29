using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hiiri : MonoBehaviour
{
    public GameObject target;
    Ray ray;
    float rotationSpeed = 1f;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = target.transform.position + new Vector3(0, 0, -2);
        transform.LookAt(target.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        float z = transform.eulerAngles.z;
        transform.Rotate(0, 0, -z);
        float XaxisRotation = Input.GetAxis("Mouse X") * rotationSpeed;
        float YaxisRotation = Input.GetAxis("Mouse Y") * rotationSpeed;

       
            transform.RotateAround(target.transform.position, Vector3.up, XaxisRotation);
        
        
            transform.RotateAround(target.transform.position, Vector3.right, -YaxisRotation);
        
        
        

        //transform.RotateAround(Vector3.down, XaxisRotation);
        //transform.RotateAround(Vector3.right, YaxisRotation);

        //transform.position = target.transform.position + new Vector3(0, 0, -2f);
        Vector3 aimPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //aimPos.z = 0;

        
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log(aimPos);
            if (Physics.Raycast(transform.position, aimPos))
            {

            }
        }


    }
}
