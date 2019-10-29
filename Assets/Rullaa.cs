using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rullaa : MonoBehaviour
{
    bool toimi = false;
    private Rigidbody rB;
    // Start is called before the first frame update
    void Start()
    {
        rB = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!toimi && Input.GetKeyDown("space"))
        {
            toimi=true;
        }
        else if(toimi && Input.GetKeyDown("space"))
        {
            toimi = false;
        }
    }
    private void FixedUpdate()
    {
        if(toimi)
        rB.AddRelativeTorque(new Vector3(0f, 20000000f, 0f)*Time.fixedDeltaTime/10f);
    }
}
