using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kitkaa : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("collisionEnter");

        Rigidbody rb= transform.GetComponent<Rigidbody>();
        if ( (rb.velocity.magnitude)>0)
        {
            rb.velocity = Vector3.zero;
            Debug.Log("collisionEnter, velocityMag "+ rb.velocity.magnitude);
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        Debug.Log("collisionStay");
        Rigidbody rb = transform.GetComponent<Rigidbody>();
        if ((rb.velocity.magnitude) > 0)
        {
            rb.velocity = Vector3.zero;
            Debug.Log("collisionStay, velocityMag " + rb.velocity.magnitude);
        }
    }
    
}
