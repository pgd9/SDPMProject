using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenu : MonoBehaviour
{
    private const float CAMERA_TRANSITION_SPEED = 3.0f;

    public GameObject levelButtonPrefab;
    public GameObject levelButtonContainer;


    private Transform cameraTransform;
    private Transform cameraDesiredLookAt;

    void Awake()
    {
        if (PlayerPrefs.GetInt("MaxLevel") < 1)
        {
            PlayerPrefs.SetInt("MaxLevel", 1);
        }
        ////PlayerPrefs.SetInt("MaxLevel", 5);
    }

    private void Start()
    {
        //print("MMStart " + PlayerPrefs.GetInt("MaxLevel"));
        cameraTransform = Camera.main.transform;

        Sprite[] thumbnails = Resources.LoadAll<Sprite>("Levels");
        int count = 0;
        int MaxLevel = PlayerPrefs.GetInt("MaxLevel");
        foreach (Sprite thumbnail in thumbnails)
        {
            ++count;
            GameObject container = Instantiate(levelButtonPrefab) as GameObject;
            container.GetComponent<Image>().sprite = thumbnail;
            container.transform.SetParent(levelButtonContainer.transform, false);
            if (count <= MaxLevel)
            {
                //container.GetComponentInChildren<Text>().text = "Level " + count;
                var textElement = container.GetComponentInChildren<Text>();
                //textElement.resizeTextForBestFit = true;
                textElement.fontSize = 16;
                textElement.text = "Level " + count;
                //textElement.resizeTextForBestFit = true;

                string sceneName = thumbnail.name;
                container.GetComponent<Button>().onClick.AddListener(() => LoadLevel(sceneName));
            }
            else
            {
                var textElement = container.GetComponentInChildren<Text>();

                textElement.text = "LOCKED";
                textElement.resizeTextForBestFit = true;
            }
        }
    }

    private void Update()
    {
        if (cameraDesiredLookAt != null)
        {
            cameraTransform.rotation = Quaternion.Slerp(cameraTransform.rotation, cameraDesiredLookAt.rotation, CAMERA_TRANSITION_SPEED * Time.deltaTime);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    private void LoadLevel(string sceneName)
    {

        SceneManager.LoadScene(sceneName);
    }

    public void LookAtMenu(Transform menuTransform)
    {
        cameraDesiredLookAt = menuTransform;

    }

    // Use this for initialization
    //void Start () {}

    // Update is called once per frame
    //void Update () {}
}
