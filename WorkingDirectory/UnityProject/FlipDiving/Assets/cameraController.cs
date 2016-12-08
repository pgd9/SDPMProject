using UnityEngine;
using System.Collections;

public class cameraController : MonoBehaviour
{

    public Transform target;
    public float distance = 7f;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = new Vector3(target.position.x, target.position.y, target.position.z - distance);
    }
}
