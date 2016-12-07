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
        PlayerPrefs.SetInt("MaxLevel", 1);
    }

    private void Start()
    {

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
                container.GetComponentInChildren<Text>().text = "Level " + count;
                string sceneName = thumbnail.name;
                container.GetComponent<Button>().onClick.AddListener(() => LoadLevel(sceneName));
            }
            else
            {
                container.GetComponentInChildren<Text>().text = "LOCKED";
            }
        }
    }

    private void Update()
    {
        if (cameraDesiredLookAt != null)
        {
            cameraTransform.rotation = Quaternion.Slerp(cameraTransform.rotation, cameraDesiredLookAt.rotation, CAMERA_TRANSITION_SPEED * Time.deltaTime);
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
