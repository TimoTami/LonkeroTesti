using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Liikkuminen : MonoBehaviour
{
    private Rigidbody rb;

    private float xForce;
    private float zForce;
    private Vector3 force;

    private float pitch = 0.0F;
    private float yaw = 0.0F;

    //private Vector2 mD;

    //private Transform myBody;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //rb.constraints = RigidbodyConstraints.FreezeRotationZ;
    }

    // Update is called once per frame
    void Update()
    {
        //Vector2 mC=new Vector2(Input.GetAxisRaw("Mouse X"),Input.GetAxisRaw("Mouse Y"));
        //mD += mC;

        //Vector3 mouse = Camera.main.ScreenToViewportPoint(Input.mousePosition);

        //transform.position = new Vector3(mouse.x, mouse.y, transform.position.z);




       
            //Vector3 inputDir = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
            //Quaternion rotateInputDir = Quaternion.Euler(0f, 0f, 0f);//rotate me <3
            //rb.velocity=(rotateInputDir * inputDir * 10f);


        

    }

    void FixedUpdate()
    {
        //rb.velocity = Vector3.zero;
        xForce = Input.GetAxis("Horizontal");
        zForce = Input.GetAxis("Vertical");

        pitch += Input.GetAxis("Mouse Y");
        yaw += Input.GetAxis("Mouse X");

        //Debug.LogError("FixedUpdate;; xForce: " + xForce + " zForce :" + zForce + " pitch: " + pitch + " yaw: " + yaw);

    }

    void LateUpdate()
    {

        Vector3 t = new Vector3(-pitch, yaw,0);
        Vector3 k = t + Vector3.forward;
        //transform.LookAt(k, Vector3.up);

        //Quaternion q = Quaternion.AngleAxis(-pitch, Vector3.right)* Quaternion.AngleAxis(yaw, Vector3.up);

        force = new Vector3(xForce, 0.0F, zForce);



        // rotate object to face mouse direction
        //rb.angularVelocity = new Vector3(-pitch, yaw, 0);
        //rb.AddRelativeTorque(new Vector3(pitch ,0 , -yaw ),ForceMode.Impulse);
        //Debug.LogError("rB velocity: " + rb.angularVelocity + " pitch: " + pitch + " yaw: " + yaw);
        //transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, 0);


        // move object in facing direction relative to local (AddRelative) not world (AddForce) coordinates
        rb.AddForce(force);

        //Debug.LogError("LateUpDate;; xForce: " + xForce + "zForce :" + zForce + "pitch: " + pitch + "yaw: " + yaw);


        //transform.rotation =q;

    }





    //public float speed=1f;
    //private Vector2 mousePos;
    //private Vector3 screenPos;
    //private Vector3 pos;
    //private Vector3 euleri;
    //private Transform cam;
    //private Rigidbody rB;


    //private float dx;
    //private float dz;
    //private float theta;

    //void Start()
    //{
    //    rB = this.GetComponent<Rigidbody>();
    //    cam= transform.Find("Lonkero Camera");
    //    pos = transform.position;
    //    euleri = transform.rotation.eulerAngles;
    //}

    //void Update()
    //{
    //    Move();
    //}

    //void Move()
    //{
    //    var sf= transform.rotation.eulerAngles.z * 3.14 / 180.0;
    //    mousePos = Input.mousePosition;
    //    screenPos = cam.GetComponent<Camera>().ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, transform.position.z - cam.GetComponent<Camera>().transform.position.z));
    //    euleri.z = Mathf.Atan2((screenPos.y - transform.position.y), (screenPos.x - transform.position.x)) * Mathf.Rad2Deg;
    //    theta = euleri.z * 3.14f / 180.0f;
    //    dx = Mathf.Cos(theta);
    //    dz = Mathf.Sin(theta);
    //    pos.x = transform.position.x + dx * speed;
    //    pos.z = transform.position.z + dz * speed;
    //    rB.AddRelativeForce (pos);

    //    Debug.LogError("Jutut: pos.x: " + pos.x+" pos.z: "+pos.z+" pos: "+pos);
    //    Debug.LogError("speed: " + speed);

    //}
}
