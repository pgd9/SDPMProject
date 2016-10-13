using UnityEngine;
using System.Collections;
using Assets.Scripts.HelperScripts;
//using UnityStandardAssets.Characters.ThirdPerson;

public class Controller : MonoBehaviour
{
    private Rigidbody rb;
    private Cubemap c;
    private Animator ani;
    private int count = 0;
    public float diveAngle = 60;
    public string State = Constants.State_Idle;
    public GameObject angleDisplayObject;
    public GameObject coin;

    void Awake()
    {
        ani = GetComponent<Animator>();
        ani.applyRootMotion = false;
    }

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        rb.useGravity = false;
        ani = GetComponent<Animator>();
        ani.applyRootMotion = false;
        angleDisplayObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if ((Input.GetMouseButtonDown(0) || Input.touchCount > 0) && State == Constants.State_Idle)
        {
            State = Constants.State_Ready;
            angleDisplayObject.SetActive(true);
            // Trigger Angle Animation 
            // Get an angle - set diveAngle = getAngle from animation
        }

        else if ((Input.GetMouseButtonDown(1) || Input.touchCount == 0) && State == Constants.State_Ready)
        { // Input.GetKeyUp(KeyCode.Space) && count == 0

            angleDisplayObject.SetActive(false);
            diveAngle= angleDisplayObject.transform.localRotation.eulerAngles.z;
            State = Constants.State_Jump;
            jump();
        }
        else if ((Input.GetMouseButtonDown(0) || Input.touchCount > 0) && State == Constants.State_Jump)
        { // Input.GetKeyUp(KeyCode.Space) && count == 1
            State = Constants.State_Tuck;
            tuck();
        }

        else if ((Input.GetMouseButtonUp(1) || Input.touchCount == 0) && State == Constants.State_Tuck)
        {
            State = Constants.State_End;
            //End of Jump;
            // Evaluate fair jump;
            rb.ResetCenterOfMass();
            rb.angularVelocity = Vector3.zero;
            ani.SetBool("Crouch", false);
        }

    }

    private void FixedUpdate()
    {
        if (transform.localRotation.eulerAngles.x > 179 && State == Constants.State_Jump)
        {
            rb.angularVelocity = Vector3.zero;
        }
        //if (count == 1)
        //{

        //    rb.AddTorque(new Vector3(0, 0, 10) * -100, ForceMode.Force);
        //    //rb.centerOfMass = new Vector3(rb.centerOfMass.x, rb.centerOfMass.y + 0.25f);
        //    ani.SetBool("Crouch", true);
        //    print(1);
        //}
    }

    private void tuck()
    {
        count++;
        rb.AddTorque(new Vector3(0, 0, 10) * -100, ForceMode.Force);
        //rb.centerOfMass = new Vector3(rb.centerOfMass.x, rb.centerOfMass.y + 0.25f);
        ani.SetBool("Crouch", true);
    }

    private void jump()
    {
        count++;
        rb.isKinematic = false;
        rb.useGravity = true;
        Vector3 dir = Quaternion.AngleAxis(diveAngle, Vector3.forward) * Vector3.right;
        //rb.AddForce(dir * 250, ForceMode.Force);
        rb.velocity = new Vector3(5f, 5f, 0);
        rb.AddTorque(new Vector3(0, 0, 1) * -100);
    }




    // Position to jump
    public Vector3 getHumanPosition()
    {
        return Vector3.zero;
        //return rb.position;
    }
}
