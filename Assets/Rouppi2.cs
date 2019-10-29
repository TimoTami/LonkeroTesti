using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(LineRenderer))]

public class Rouppi2 : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform target;
    Vector3 targetPoint;
    GameObject parentGameObject;
    GameObject targetPointGameobject;
    public float resolution = 0.5F;
    public float ropeDrag = 0.1F;
    public float ropeAngDrag;
    public float ropeMass = 0.1F;
    public float ropeColRadius = 0.5F;
    public float maxDepenetrationVelocity;
    private Vector3[] segmentPos;
    private GameObject[] joints;
    private LineRenderer line;
    LineRenderer lineri;
    private int segments = 0;
    private bool rope = false;
    bool velocityNollaan = false;
    bool lengthen = false;
    bool hiiri = false;
    bool lineriPoikkinaiselle = true;
    private bool poikki = false;

    //Joint Settings
    public Vector3 axis;
    public Vector3 swingAxis;
    public float lowTwistLimit;
    public float highTwistLimit;
    public float swing1Limit;
    public float swing2Limit;
    public float spring;
    public float damper;
    public bool autoAnchor;
    public bool autoAnchorTarget;
    public bool enableProcessing;
    public bool enableProcessingTarget;
    public bool enableProjection;
    public bool enableCollision;
    public float projectionDistance;
    public float projectionAngle;
    public float contactDistance1;
    public float contactDistance2;
    public float contactDistance3;
    public float contactDistance4;
    public float breakForce;
    public float breakForceTarget;
    public float breakTorque;
    public float breakTorqueTarget;


    float original;
    Vector3 dis;
    Vector3 conAnkkuri;
    Vector3 ankkuri;

    GameObject kamera;
    Hiiri2 kameraScript;


    void Awake()
    {
        line = gameObject.GetComponent<LineRenderer>();
        //BuildRope();
        kamera = Camera.main.gameObject;
        kameraScript = kamera.GetComponent<Hiiri2>();

    }

    void Update()
    {
        // Put rope control here!
        if (rope)
        {
            UpdateSegmentPos();
        }
        //Destroy Rope Test	(Example of how you can use the rope dynamically)
        if (rope && hiiri == true && (Input.GetMouseButtonDown(1) || Input.GetKeyDown("5")))
        {
            hiiri = false;
            poikki = false;
            var ff = joints;
            DestroyRope();
            Destroy(targetPointGameobject);
        }
        else if (!rope&& hiiri == false&& (Input.GetMouseButtonDown(1)||Input.GetKeyDown("5")))
        {
            kameraScript.GetTarget2(ref targetPoint, ref parentGameObject);

            //targetPoint = kameraScript.place2;
            //parentGameObject = kameraScript.placeGameObject2;
            targetPointGameobject = new GameObject();
            targetPointGameobject.transform.position = targetPoint;
            targetPointGameobject.transform.parent = parentGameObject.transform;
            targetPointGameobject.AddComponent<JointBrake>();
            targetPointGameobject.AddComponent<Rigidbody>();
            targetPointGameobject.GetComponent<Rigidbody>().isKinematic = true;
            hiiri = true;
            BuildRope();
        }
        if (rope && Input.GetKey("d"))
        {
            LengthenRope();
            //LengthenRope1();
        }
        else if (rope && Input.GetKey("e"))
        {
            //velocityNollaan = true;
            //StartCoroutine(VelocityOdotus());
            ShortenRope();
        }
        else if (Input.GetKeyUp("3"))
        {

            //velocityNollaan = false;
        }
    }
    void LateUpdate()
    {
        // Does rope exist? If so, update its position
        if (rope)
        {
            for (int i = 0; i < joints.Length; i++)
            {
                if (i < joints.Length && joints[i].GetComponent<CharacterJoint>() == null)
                {
                    //line.positionCount = i;
                    poikki = true;
                    int laskuri = 0;
                    Vector3[] listi = new Vector3[joints.Length - i];

                    Vector3[] poistetutJointit = new Vector3[joints.Length - i];

                    //for (int g = i; g < joints.Length; g++)
                    //{


                    //    int numIdx = Array.IndexOf(joints, g);


                    //    List<GameObject> tmp = new List<GameObject>(joints);
                    //    poistetutJointit[g - 1] = joints[g].transform.position;
                    //    tmp.RemoveAt(g);
                    //    joints = tmp.ToArray();

                    //}

                    for (int p = i; p < joints.Length; p++)
                    {
                        listi[p - i] = joints[p].transform.position;

                    }

                    for (int a = 0; a < i; a++)
                    {
                        if (joints[a].GetComponent<CharacterJoint>() != null)
                        {
                            laskuri++;

                        }
                    }
                    Vector3[] lista = new Vector3[laskuri];
                    for (int a = 0; a < i; a++)
                    {
                        if (joints[a].GetComponent<CharacterJoint>() != null)
                        {

                            lista[a] = joints[a].transform.position;
                            line.positionCount = lista.Length;
                        }
                        
                    }

                    if (lineriPoikkinaiselle)
                    {
                        GameObject lineriGameObject = new GameObject("LinerendererPoikkinaiselle");
                        lineri = lineriGameObject.AddComponent<LineRenderer>();
                        lineriPoikkinaiselle = false;
                    }


                    lineri.startWidth = line.startWidth;
                    lineri.endWidth = line.endWidth;

                    //lineri.materials[0] = Resources.Load("Koysi Material1") as Material;
                    lineri.material = line.material;

                    //lineri.SetWidth(start: 0.025f, end: 0.025f);
                    lineri.positionCount = listi.Length;
                    lineri.SetPositions(listi);


                    line.SetPositions(lista);


                }
                else if (lineriPoikkinaiselle && i == 0)
                {
                    line.SetPosition(i, transform.position);
                }
                else if (lineriPoikkinaiselle && i == joints.Length - 1)
                {
                    line.SetPosition(i, joints[i].transform.position);
                }
                else if (lineriPoikkinaiselle && i < joints.Length - 1)
                {
                    line.SetPosition(i, joints[i - 1].transform.position);
                }
            }
            line.enabled = true;
            if (!lineriPoikkinaiselle)
            {
                lineri.enabled = true;
            }

        }
        else
        {
            line.enabled = false;
            if (!lineriPoikkinaiselle)
            {
                lineri.enabled = false;
                lineriPoikkinaiselle = true;
            }

        }
    }

    private void FixedUpdate()
    {
        if (velocityNollaan)
        {
            transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }


    IEnumerator VelocityOdotus()
    {
        //print(Time.time);
        //yield return new WaitForSeconds(5);
        //print(Time.time);

        // Start control of the three cubes.
        velocityNollaan = true;

        // Move the first cube up or down.
        //yield return new WaitForSeconds(0.5f);
        //transform.GetComponent<Rigidbody>().velocity = Vector3.zero;


        // Pause for a second.
        yield return new WaitForSeconds(0.5f);

        velocityNollaan = false;
    }

    void UpdateSegmentPos()
    {
        for (int i = 0; i < joints.Length - 1; i++)
        {
            segmentPos[i] = joints[i].transform.position;
        }
    }
    void BuildRope()
    {
        segments = (int)(Vector3.Distance(transform.position, targetPoint) * resolution);
        line.positionCount = segments;
        segmentPos = new Vector3[segments];
        joints = new GameObject[segments];
        segmentPos[0] = transform.position;
        segmentPos[segments - 1] = targetPoint;
        var seperation = ((targetPoint - transform.position) / segments);
        original = segments;

        for (int s = 1; s < segments; s++)
        {
            Vector3 vector = (seperation * s) + transform.position;
            segmentPos[s] = vector;
            AddJointPhysics(s - 1);
        }

        ankkuri = joints[1].GetComponent<CharacterJoint>().connectedAnchor;
        AddTargetJoint();
    }

    void AddTargetJoint()
    {
        CharacterJoint end = targetPointGameobject.AddComponent<CharacterJoint>();
        end.enablePreprocessing = enableProcessingTarget;
        end.enableProjection = enableProjection;



        end.autoConfigureConnectedAnchor = false;
        end.connectedAnchor = ankkuri;


        end.anchor = Vector3.zero;
        end.connectedBody = joints[joints.Length - 2].GetComponent<Rigidbody>();
        end.gameObject.layer = 8;
        end.swingAxis = swingAxis;
        end.axis = axis;
        end.projectionDistance = projectionDistance;
        end.projectionAngle = projectionAngle;
        end.breakForce = breakForceTarget;
        end.breakTorque = breakTorqueTarget;

        SoftJointLimit limit_setter = end.lowTwistLimit;
        SoftJointLimitSpring setti = end.swingLimitSpring;
        SoftJointLimit highTwist = end.highTwistLimit;
        highTwist.contactDistance = contactDistance1;
        SoftJointLimit swing1 = end.swing1Limit;
        swing1.contactDistance = contactDistance3;
        swing1.limit = swing1Limit;
        SoftJointLimit swing2 = end.swing2Limit;
        swing2.contactDistance = contactDistance4;
        swing2.limit = swing2Limit;
        end.swing1Limit = swing1;
        end.swing2Limit = swing2;
        end.highTwistLimit = highTwist;
        limit_setter.contactDistance = contactDistance2;
        setti.damper = damper;
        setti.spring = spring;
        end.twistLimitSpring = setti;
        end.swingLimitSpring = setti;

        limit_setter.limit = lowTwistLimit;
        end.lowTwistLimit = limit_setter;
        limit_setter = end.highTwistLimit;
        limit_setter.limit = highTwistLimit;
        end.highTwistLimit = limit_setter;
        limit_setter = end.swing1Limit;
        limit_setter.limit = swing1Limit;
        end.swing1Limit = limit_setter;

        rope = true;
        joints[joints.Length - 1] = targetPointGameobject;
        conAnkkuri = end.connectedAnchor;
        lengthen = false;
    }

    void AddJointPhysics(int n)
    {
        joints[n] = new GameObject("Joint_" + (n));
        joints[n].transform.parent = transform;
        if (n > 2)
        {
            joints[n].layer = 9;
        }
        else
        {
            joints[n].layer = 8;
        }
        joints[n].AddComponent<Ignoraa>();
        joints[n].AddComponent<JointBrake>();
        Rigidbody rigid = joints[n].AddComponent<Rigidbody>();
        //rigid.useGravity = false;
        //rigid.mass = 0.01f;
        rigid.angularDrag = ropeAngDrag;
        rigid.interpolation = RigidbodyInterpolation.Interpolate;
        rigid.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        rigid.maxDepenetrationVelocity = maxDepenetrationVelocity;
        SphereCollider col = joints[n].AddComponent<SphereCollider>();
        CharacterJoint ph = joints[n].AddComponent<CharacterJoint>();

        ph.enableCollision = enableCollision;
        ph.breakForce = breakForce;
        ph.breakTorque = breakTorque;
        ph.autoConfigureConnectedAnchor = autoAnchor;

        if (!autoAnchor && !lengthen)
        {

            ph.connectedAnchor = Vector3.zero;
        }
        else if (lengthen)
        {
            ph.autoConfigureConnectedAnchor = false;
            ph.connectedAnchor = ankkuri;
        }

        ph.enablePreprocessing = enableProcessing;
        ph.enableProjection = enableProjection;
        ph.swingAxis = swingAxis;
        ph.axis = axis;
        ph.projectionDistance = projectionDistance;
        ph.projectionAngle = projectionAngle;
        SoftJointLimit limit_setter = ph.lowTwistLimit;
        SoftJointLimitSpring setti = ph.swingLimitSpring;
        SoftJointLimit highTwist = ph.highTwistLimit;
        highTwist.contactDistance = contactDistance1;
        SoftJointLimit swing1 = ph.swing1Limit;
        swing1.contactDistance = contactDistance3;
        swing1.limit = swing1Limit;
        SoftJointLimit swing2 = ph.swing2Limit;
        swing2.contactDistance = contactDistance4;
        swing2.limit = swing2Limit;
        ph.swing1Limit = swing1;
        ph.swing2Limit = swing2;
        ph.highTwistLimit = highTwist;
        setti.damper = damper;
        setti.spring = spring;
        limit_setter.contactDistance = contactDistance2;
        ph.twistLimitSpring = setti;
        ph.swingLimitSpring = setti;

        limit_setter.limit = lowTwistLimit;
        ph.lowTwistLimit = limit_setter;
        limit_setter = ph.highTwistLimit;
        limit_setter.limit = highTwistLimit;
        ph.highTwistLimit = limit_setter;
        limit_setter = ph.swing1Limit;
        limit_setter.limit = swing1Limit;
        ph.swing1Limit = limit_setter;



        if (lengthen)
        {

            joints[n].transform.position = segmentPos[n];
        }
        else
        {
            joints[n].transform.position = segmentPos[n];
        }

        rigid.drag = ropeDrag;
        rigid.mass = ropeMass;
        col.radius = ropeColRadius;

        if (lengthen)
        {
            //ph.anchor = Vector3.zero;
            //ph.autoConfigureConnectedAnchor = true;
            //ph.connectedAnchor = Vector3.zero;
            ph.connectedBody = joints[n - 1].GetComponent<Rigidbody>();
            lengthen = false;
        }
        else if (n == 0)
        {
            ph.connectedBody = transform.GetComponent<Rigidbody>();
        }

        else
        {
            ph.connectedBody = joints[n - 1].GetComponent<Rigidbody>();
        }

    }

    void DestroyRope()
    {
        rope = false;
        for (int dj = 0; dj < joints.Length; dj++)
        {
            if (joints[dj] != targetPointGameobject)
            {
                Destroy(joints[dj]);
            }

        }

        Destroy(targetPointGameobject.GetComponent<CharacterJoint>());
        segmentPos = new Vector3[0];
        joints = new GameObject[0];
        segments = 0;
    }

    void LengthenRope()
    {
        if (joints.Length < 120 && poikki == false)
        {
            Debug.Log("joints size1:" + joints.Length);
            var sf = joints;
            var kj = joints.Length;
            //var pp = joints[kj-1];

            Destroy(targetPointGameobject.GetComponent<CharacterJoint>());
            //joints[joints.Length-1] = new GameObject("Joint_" + (joints.Length));

            joints[joints.Length - 1] = null;

            Array.Resize(ref segmentPos, segmentPos.Length + 1);
            UpdateSegmentPos();


            segments = segmentPos.Length;

            var seperation = ((targetPoint - transform.position) / segments);
            var dis2 = joints[joints.Length - 2].transform.position - joints[joints.Length - 3].transform.position;
            var yh = seperation * (segments - 1);
            dis = targetPoint - segmentPos[segmentPos.Length - 3];
            var testi = segmentPos[segmentPos.Length - 2] + (dis / segments);
            var vaari = (seperation * (segments)) + transform.position;
            Vector3 vector = (seperation * (segments - 1)) + transform.position;

            segmentPos[segments - 2] = testi;
            //segmentPos[segments - 2] = vector;
            segmentPos[segments - 1] = targetPoint;


            lengthen = true;
            AddJointPhysics(joints.Length - 1);
            var gh = joints[joints.Length - 1];

            Array.Resize(ref joints, joints.Length + 1);
            Debug.Log("joints size2:" + joints.Length);

            lengthen = true;
            AddTargetJoint();
            line.positionCount = joints.Length;
            UpdateSegmentPos();
        }

    }

    void AddJointPhysicsLengthen(int n)
    {
        joints[n] = new GameObject("Joint_" + (n));
        joints[n].transform.parent = transform;
        joints[n].layer = 8;
        joints[n].AddComponent<Ignoraa>();
        joints[n].AddComponent<JointBrake>();
        Rigidbody rigid = joints[n].AddComponent<Rigidbody>();
        //rigid.useGravity = false;
        //rigid.mass = 0.01f;
        rigid.angularDrag = ropeAngDrag;
        rigid.interpolation = RigidbodyInterpolation.Interpolate;
        rigid.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        rigid.maxDepenetrationVelocity = maxDepenetrationVelocity;
        SphereCollider col = joints[n].AddComponent<SphereCollider>();
        CharacterJoint ph = joints[n].AddComponent<CharacterJoint>();

        ph.breakForce = breakForce;
        ph.breakTorque = breakTorque;
        ph.autoConfigureConnectedAnchor = autoAnchor;
        ph.enablePreprocessing = enableProcessing;
        ph.enableProjection = enableProjection;
        ph.swingAxis = swingAxis;
        ph.axis = axis;
        ph.projectionDistance = projectionDistance;
        ph.projectionAngle = projectionAngle;
        SoftJointLimit limit_setter = ph.lowTwistLimit;
        SoftJointLimitSpring setti = ph.swingLimitSpring;
        SoftJointLimit highTwist = ph.highTwistLimit;
        highTwist.contactDistance = contactDistance1;
        SoftJointLimit swing1 = ph.swing1Limit;
        swing1.contactDistance = contactDistance3;
        swing1.limit = swing1Limit;
        SoftJointLimit swing2 = ph.swing2Limit;
        swing2.contactDistance = contactDistance4;
        swing2.limit = swing2Limit;
        ph.swing1Limit = swing1;
        ph.swing2Limit = swing2;
        ph.highTwistLimit = highTwist;
        setti.damper = damper;
        setti.spring = spring;
        limit_setter.contactDistance = contactDistance2;
        ph.twistLimitSpring = setti;
        ph.swingLimitSpring = setti;

        limit_setter.limit = lowTwistLimit;
        ph.lowTwistLimit = limit_setter;
        limit_setter = ph.highTwistLimit;
        limit_setter.limit = highTwistLimit;
        ph.highTwistLimit = limit_setter;
        limit_setter = ph.swing1Limit;
        limit_setter.limit = swing1Limit;
        ph.swing1Limit = limit_setter;

        //ph.breakForce = ropeBreakForce; <--------------- TODO

        joints[n].transform.position = segmentPos[n];

        rigid.drag = ropeDrag;
        rigid.mass = ropeMass;
        col.radius = ropeColRadius;

        if (n == 0)
        {
            ph.connectedBody = transform.GetComponent<Rigidbody>();
        }
        else
        {
            ph.connectedBody = joints[n - 1].GetComponent<Rigidbody>();
        }

    }

    void ShortenRope()
    {
        if (joints.Length > 5 && poikki == false)
        {
            Destroy(joints[0]);
            var tt = joints[0];
            segments -= 1;
            int numToRemove = 0;
            int numIdx = Array.IndexOf(joints, numToRemove);
            List<GameObject> tmp = new List<GameObject>(joints);
            tmp.RemoveAt(0);
            joints = tmp.ToArray();
            var kj = joints[0];
            UpdateSegmentPos();

            int saf = Array.IndexOf(segmentPos, numToRemove);
            List<Vector3> sdg = new List<Vector3>(segmentPos);
            sdg.RemoveAt(0);
            segmentPos = sdg.ToArray();
            segmentPos[0] = transform.position;

            Debug.Log("Poistettu: " + tmp);
            //segmentPos[0] =;
            var gs = segmentPos;
            //line.positionCount = joints.Length;


            CharacterJoint joint = joints[0].GetComponent<CharacterJoint>();
            if (joint != null)
            {
                joint.connectedBody = transform.GetComponent<Rigidbody>();
                joint.autoConfigureConnectedAnchor = false;
                joint.connectedAnchor = Vector3.zero;
                //segmentPos[1]=
            }
            CharacterJoint joint3 = joints[2].GetComponent<CharacterJoint>();
            if (joint != null)
            {

                joint3.gameObject.layer = 8;
                //segmentPos[1]=
            }


        }


    }
}
