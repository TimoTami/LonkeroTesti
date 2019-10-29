using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PallonLiikutus : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {


        float xForce = Input.GetAxis("Horizontal")*50;
        float zForce = Input.GetAxis("Vertical")*50;

        Vector3 force = new Vector3(xForce, 0.0F, zForce);
        gameObject.GetComponent<Rigidbody>().AddForce(force);
    }
}
