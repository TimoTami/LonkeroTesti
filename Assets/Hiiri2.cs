using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hiiri2 : MonoBehaviour
{
    public Transform target;
    public float distance = 5.0f;
    public float xSpeed = 120.0f;
    public float ySpeed = 120.0f;

    public float yMinLimit = -20f;
    public float yMaxLimit = 80f;

    public float distanceMin = .5f;
    public float distanceMax = 15f;

    public float offset;

    public Vector3 place;
    public GameObject placeGameObject;

    public Vector3 place2;
    public GameObject placeGameObject2;
    public bool onEste = false;
    public bool onEste2 = false;
    bool tahtain = false;

    int layerMask = 1 << 8;
    


    Rigidbody rigidbody;

    float x = 0.0f;
    float y = 0.0f;


    public Texture2D crossi;
    public Texture2D eiOsuCrossi;
    public Texture2D osuuCrossi;
    public float crosshairScale = 1;
    void OnGUI()
    {

        //if not paused
        if (Time.timeScale != 0)
        {
            if (crossi != null)
            {
                
                    GUI.DrawTexture(new Rect((Screen.width - crossi.width * crosshairScale) / 2, (Screen.height - crossi.height * crosshairScale) / 2, crossi.width * crosshairScale, crossi.height * crosshairScale), crossi);
                
                
                
            }
            else
            {
                Debug.Log("No crosshair texture set in the Inspector");
            }
        }
    }

    // Use this for initialization
    void Start()
    {


        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;

        rigidbody = GetComponent<Rigidbody>();

        // Make the rigid body not change rotation
        if (rigidbody != null)
        {
            rigidbody.freezeRotation = true;
        }

        layerMask = ~layerMask;
    }

    public void GetTarget1(ref Vector3 point, ref GameObject parentGameObject)
    {
        float flo = Vector3.Distance(transform.position, target.transform.position);
        onEste = false;
        RaycastHit raycastHit;
        if (Physics.Raycast(transform.position + transform.forward * flo, transform.forward, out raycastHit, 20f, layerMask))
        {
            onEste = true;
            //Debug.DrawRay(transform.position + new Vector3(0, 0, 2.2f), transform.forward*20f, Color.blue, 1000f);
            Debug.DrawRay(transform.position + transform.forward * flo, transform.forward * 20f, Color.green, 50f);
            //place = raycastHit.point;
            //placeGameObject = raycastHit.transform.gameObject;
            Debug.Log("point: " + raycastHit.point + "object: " + raycastHit.transform.gameObject);



            if (raycastHit.point != null&& raycastHit.transform.gameObject != null)
            {
                point = raycastHit.point;
                parentGameObject = raycastHit.transform.gameObject;
            }

        }

    }
    public void GetTarget2(ref Vector3 point, ref GameObject parentGameObject)
    {
        float flo = Vector3.Distance(transform.position, target.transform.position);
        onEste = false;
        RaycastHit raycastHit;
        if (Physics.Raycast(transform.position + transform.forward * flo, transform.forward, out raycastHit, 20f, layerMask))
        {
            onEste = true;
            //Debug.DrawRay(transform.position + new Vector3(0, 0, 2.2f), transform.forward*20f, Color.blue, 1000f);
            Debug.DrawRay(transform.position + transform.forward * flo, transform.forward * 20f, Color.green, 50f);
            //place = raycastHit.point;
            //placeGameObject = raycastHit.transform.gameObject;
            Debug.Log("point: " + raycastHit.point + "object: " + raycastHit.transform.gameObject);



            if (raycastHit.point != null && raycastHit.transform.gameObject != null)
            {
                point = raycastHit.point;
                parentGameObject = raycastHit.transform.gameObject;
            }

        }

    }
    //public void getTarget2( )
    //{
    //    float flo = Vector3.Distance(transform.position, target.transform.position);
    //    onEste2 = false;
    //    RaycastHit raycastHit;
    //    if (Physics.Raycast(transform.position + transform.forward * flo, transform.forward, out raycastHit, 20f, layerMask))
    //    {
    //        onEste2 = true;
    //        //Debug.DrawRay(transform.position + new Vector3(0, 0, 2.2f), transform.forward * 20f, Color.yellow, 1000f);
    //        place2 = raycastHit.point;
    //        placeGameObject2 = raycastHit.transform.gameObject;
    //        Debug.Log("point: " + place2 + "object: " + placeGameObject2);

    //    }
    //}

    private void Update()
    {
        float flo = Vector3.Distance(transform.position, target.transform.position);

        if (Physics.Raycast(transform.position + transform.forward * flo, transform.forward, 20f, layerMask))
        {
            crossi = osuuCrossi;
        }
        else
        {
            crossi = eiOsuCrossi;
        }
        if (Input.GetKeyDown("space"))
        {
           
                Screen.fullScreen = Screen.fullScreen;
            


            if (Input.GetKeyDown(KeyCode.G))
                Screen.fullScreen = !Screen.fullScreen;

        }
        //if (Input.GetMouseButtonDown(0)||Input.GetKeyDown("4"))
        //{
        //    onEste = false;

            
            
        //    RaycastHit raycastHit;
        //    if (Physics.Raycast(transform.position + transform.forward * flo, transform.forward, out raycastHit, 20f,layerMask))
        //    {
        //        onEste=true;
        //        //Debug.DrawRay(transform.position + new Vector3(0, 0, 2.2f), transform.forward*20f, Color.blue, 1000f);
        //        Debug.DrawRay(transform.position + transform.forward * flo, transform.forward * 20f, Color.green, 50f);
        //        place = raycastHit.point;
        //        placeGameObject = raycastHit.transform.gameObject;
        //        Debug.Log("point: " + place + "object: " + placeGameObject);

        //    }
        //    else if (Physics.Raycast(transform.position + transform.forward * 2.2f, transform.forward, out raycastHit, 20f))
        //    {
                
        //        //Debug.DrawRay(transform.position + new Vector3(0, 0, 2.2f), transform.forward * 20f, Color.blue, 1000f);
        //        //Debug.DrawRay(transform.position + transform.forward * 2.2f, transform.forward * 20f, Color.green, 1000f);
        //        Debug.Log("point: " + raycastHit.point + "object: " + raycastHit.transform.gameObject);

        //    }
        //}

        //else if (Input.GetMouseButtonDown(1) || Input.GetKeyDown("5"))
        //{
        //    onEste2 = false;
        //    RaycastHit raycastHit;
        //    if (Physics.Raycast(transform.position+ transform.forward * 2.2f, transform.forward, out raycastHit, 20f,layerMask))
        //    {
        //        onEste2 = true;
        //        //Debug.DrawRay(transform.position + new Vector3(0, 0, 2.2f), transform.forward * 20f, Color.yellow, 1000f);
        //        place2 = raycastHit.point;
        //        placeGameObject2 = raycastHit.transform.gameObject;
        //        Debug.Log("point: " + place2 + "object: " + placeGameObject2);

        //    }
        //}
    }
    void LateUpdate()
    {
        if (target)
        {
            x += Input.GetAxis("Mouse X") * xSpeed * distance * 0.02f;
            y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;

            y = ClampAngle(y, yMinLimit, yMaxLimit);

            Quaternion rotation = Quaternion.Euler(y, x, 0);

            distance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel") * 5, distanceMin, distanceMax);

            //RaycastHit hit;
            //if (Physics.Linecast(target.position, transform.position, out hit))
            //{
            //    distance -= hit.distance;
            //}
            Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
            Vector3 position = rotation * negDistance + target.position;
            position.y += offset;

            transform.rotation = rotation;
            transform.position = position;
        }
    }

    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }
}
