using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{

    public GameObject DefaultCharacter;
    public GameObject NonDefaultCharacter;
    void Start()
    {
        //GetComponent<Button>();
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        //Debug.Log("You have clicked the button!");
        if (DefaultCharacter.active)
        {
            NonDefaultCharacter.transform.position = DefaultCharacter.transform.position;
        }
        else
        {
            DefaultCharacter.transform.position = NonDefaultCharacter.transform.position;

        }
        DefaultCharacter.SetActive(!DefaultCharacter.active);
        NonDefaultCharacter.SetActive(!NonDefaultCharacter.active);


    }

}
