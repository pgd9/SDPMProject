using UnityEngine;
using System.Collections;
using Assets.Scripts.HelperScripts;
using UnityEngine.UI;
//using UnityStandardAssets.Characters.ThirdPerson;

public class Controller : MonoBehaviour
{
    private Rigidbody rb;
    private Cubemap c;
    private Animator ani;
    private int count = 0;
    private bool hasTuck;
    private bool validLanding;
    private Quaternion originalrotation;
    private Vector3 originalposition;
    private Vector3 lastposition;
    private int score;
    private int currentElevation = 1;

    private float diveAngle = 60;
    public string State = Constants.State_Idle;
    public GameObject angleDisplayObject;
    public GameObject coin;
    public Text JumpMsg;
    public Text countText;
    public int Elevations;
    public int JumpForce = 250;

    void Awake()
    {
        ani = GetComponent<Animator>();
        ani.applyRootMotion = false;
        originalrotation = transform.rotation;
        originalposition = transform.position;
        lastposition = originalposition;
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
        score = 0;
        SetCountText();
    }

    // Update is called once per frame
    void Update()
    {

        if ((Input.GetKeyDown(KeyCode.Alpha1) || Input.touchCount > 0) && State == Constants.State_Idle)
        {
            State = Constants.State_Ready;
            //angleDisplayObject.SetActive(true);
            setAngleIndicator();
            JumpMsg.text = "";
            // Trigger Angle Animation 
            // Get an angle - set diveAngle = getAngle from animation
        }

        else if ((Input.GetKeyUp(KeyCode.Alpha2)) && State == Constants.State_Ready) // || Input.touchCount == 0
        { // Input.GetKeyUp(KeyCode.Space) && count == 0

            angleDisplayObject.SetActive(false);
            diveAngle = angleDisplayObject.transform.localRotation.eulerAngles.z;
            jump();
            State = Constants.State_Jump;
        }
        else if ((Input.GetKeyDown(KeyCode.Alpha3) || Input.touchCount > 0) && State == Constants.State_Jump)
        { // Input.GetKeyUp(KeyCode.Space) && count == 1
            State = Constants.State_Tuck;
            tuck();
        }

        else if ((Input.GetKeyUp(KeyCode.Alpha4)) && State == Constants.State_Tuck) // || Input.touchCount == 0
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
            //rb.angularVelocity = Vector3.zero;
        }



        //if (count == 1)
        //{

        //    rb.AddTorque(new Vector3(0, 0, 10) * -100, ForceMode.Force);
        //    //rb.centerOfMass = new Vector3(rb.centerOfMass.x, rb.centerOfMass.y + 0.25f);
        //    ani.SetBool("Crouch", true);
        //    print(1);
        //}
    }

    void OnCollisionEnter(Collision other)
    {



        if (other.collider.gameObject.name == "Floor")
        {
            print(Quaternion.Angle(originalrotation, transform.rotation));
            //CheckValidLanding(transform.eulerAngles.x);
            CheckValidLanding(Quaternion.Angle(originalrotation, transform.rotation));
            if (validLanding && hasTuck && State.Equals(Constants.State_End))
            {
                // Code on valid landing
                print("Valid Jump");
                transform.position = getNewposition();
            }
            else
            {
                if (!hasTuck)
                {
                    JumpMsg.text = "Did not tuck";
                }
                else if (!State.Equals(Constants.State_End))
                {
                    JumpMsg.text = "Still tucking";
                }
                else if (transform.eulerAngles.x > 0 && transform.eulerAngles.x < 180)
                {
                    JumpMsg.text = "Belly flop";
                }
                else
                {
                    JumpMsg.text = "Back flop";
                }
                transform.position = originalposition;
                lastposition = originalposition;
                print("Invalid jump");
            }
            State = Constants.State_Idle;
            ani.SetBool("Crouch", false);
            currentElevation = 1;
            transform.rotation = originalrotation;
            print("collision");
            rb.useGravity = false;
            rb.isKinematic = true;
            //this.gameObject.SetActive(false);
            //GetComponent<GameObject>().SetActive(false);// gameObject.SetActive(false);
            //Destroy(currentObject);
        }
        else if (!(State.Equals(Constants.State_Idle)))
        {
            validLanding = false;
            JumpMsg.text = "Got hit";
            State = Constants.State_Idle;
            lastposition = originalposition;
            currentElevation = 1;
            transform.position = originalposition;
            ani.SetBool("Crouch", false);
            transform.rotation = originalrotation;
            rb.useGravity = false;
            rb.isKinematic = true;
        }
    }

    private void setAngleIndicator()
    {
        angleDisplayObject.transform.position = transform.position + new Vector3(1f, +3.5f, 0f);
        angleDisplayObject.SetActive(true);
    }

    private Vector3 getNewposition()
    {
        if (currentElevation < Elevations)
        {
            lastposition = new Vector3(lastposition.x - 0.5f, lastposition.y + 6.5f, lastposition.z);
            currentElevation++;
        }
        return lastposition;
    }

    private void CheckValidLanding(float angle)
    {
        // print(angle);
        //if ((140 < angle && angle < 220) || (320 < angle || angle < 40))
        //{
        //    validLanding = true;
        //}
        if (angle < 45 || 135 < angle)
        {
            validLanding = true;
        }
        else
        {
            validLanding = false;
        }
    }

    private void tuck()
    {
        count++;
        rb.AddTorque(new Vector3(0, 0, 10) * -100, ForceMode.Force);
        //rb.centerOfMass = new Vector3(rb.centerOfMass.x, rb.centerOfMass.y + 0.25f);
        ani.SetBool("Crouch", true);
        hasTuck = true;
    }

    private void jump()
    {
        count++;
        rb.isKinematic = false;
        rb.useGravity = true;
        Vector3 dir = Quaternion.AngleAxis(diveAngle, Vector3.forward) * Vector3.right;
        rb.AddForce(dir * JumpForce, ForceMode.Force);
        //rb.velocity = new Vector3(5f, 5f, 0);
        rb.AddTorque(new Vector3(0, 0, 1) * -100);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            other.gameObject.SetActive(false);
            score = score + 1;
            SetCountText();
        }
    }

    void SetCountText()
    {
        countText.text = "Score: " + score.ToString();
    }
    // Position to jump
    public Vector3 getHumanPosition()
    {
        return Vector3.zero;
        //return rb.position;
    }
}
