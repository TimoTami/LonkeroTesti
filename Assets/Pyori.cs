using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pyori : MonoBehaviour
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
        if (!toimi && Input.GetKeyDown("return"))
        {
            toimi = true;
        }
        else if (toimi && Input.GetKeyDown("return"))
        {
            toimi = false;
        }
    }
    private void FixedUpdate()
    {
        if (toimi)
            rB.AddRelativeTorque(new Vector3(0f, 2000000000f, 0f) * Time.fixedDeltaTime / 10f);
    }
}
