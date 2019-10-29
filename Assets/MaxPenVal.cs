using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxPenVal : MonoBehaviour
{
    public float maxDepenetrationVelocity;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().maxDepenetrationVelocity = maxDepenetrationVelocity;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
