using UnityEngine;
using System.Collections;
//using UnityStandardAssets.Characters.ThirdPerson;

public class Controller : MonoBehaviour
{
    private Rigidbody rb;
    private Cubemap c;
    private Animator ani;
    private int count = 0;
    private int diveAngle = 60;

    void Awake(){
        ani = GetComponent<Animator>();
        ani.applyRootMotion = false;
    }

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //c = GetComponent<Cubemap>();
        rb.isKinematic = true;
        rb.useGravity = false;
        ani = GetComponent<Animator>();
        ani.applyRootMotion = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space) && count == 0)
        {
            count++;
            rb.isKinematic = false;
            rb.useGravity = true;
            Vector3 dir = Quaternion.AngleAxis(diveAngle, Vector3.forward) * Vector3.right;
            //rb.AddForce(dir * 250, ForceMode.Force);
            rb.velocity = new Vector3(5f, 5f, 0);
            rb.AddTorque(new Vector3(0, 0, 1) * -100);
        }
        else if (Input.GetKeyUp(KeyCode.Space) && count == 1)
        {
            count++;
            rb.AddTorque(new Vector3(0, 0, 10) * -100);
            rb.centerOfMass = new Vector3(rb.centerOfMass.x, rb.centerOfMass.y + 0.25f);
            ani.SetBool("Crouch", true);

        }
        else if (Input.GetKeyUp(KeyCode.Space) && count == 2)
        {
            rb.ResetCenterOfMass();
            rb.angularVelocity = Vector3.zero;
            ani.SetBool("Crouch", false);
        }
    }

    private void FixedUpdate()
    {
        
    }
}
