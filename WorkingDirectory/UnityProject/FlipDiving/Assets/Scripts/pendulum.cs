using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class pendulum : MonoBehaviour
{

    public float angle = 40.0f;
    public float speed = 1.5f;
    public float frictionFactor = .999f;
    public float factor = 1.0f;
    public Text DisplayAngle;

    Quaternion qStart, qEnd;

    void Start()
    {
        //qStart = Quaternion.AngleAxis(angle, Vector3.forward);
        //qEnd = Quaternion.AngleAxis(-angle, Vector3.forward);
        qStart = Quaternion.AngleAxis(angle, Vector3.forward);
        qEnd = Quaternion.AngleAxis(angle + 80, Vector3.forward);
        transform.transform.localPosition = new Controller().getHumanPosition();
        
    }

    void Update()
    {

        transform.rotation = Quaternion.Lerp(qStart, qEnd, (Mathf.Sin(Time.time * speed) * factor + 1.0f) / 2.0f);
        //factor *= frictionFactor;
        DisplayAngle.text = transform.localRotation.eulerAngles.z.ToString();
    }
}
