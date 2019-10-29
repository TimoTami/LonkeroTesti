using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ignoraa : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Physics.IgnoreLayerCollision(8, 8);
        //Physics.IgnoreLayerCollision(9, 8);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
