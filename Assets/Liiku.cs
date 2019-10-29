using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Liiku : MonoBehaviour
{
    Rigidbody rB = new Rigidbody();
    // Start is called before the first frame update
    void Start()
    {
        rB = GetComponent<Rigidbody>();

        if (rB == null)
        {
            Debug.LogError("Pelaajan aluksella ei ole rigidbodya");
            Debug.LogError("Kappaleen nimi on: " + gameObject.name);
        }
        else
        {
            Debug.LogError("Pelaajan aluksella on rigibBody");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        rB.AddRelativeForce(0, 0, 1);
    }
}
