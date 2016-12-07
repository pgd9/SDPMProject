using UnityEngine;
using System.Collections;

public class GameplaySound : MonoBehaviour
{
    public bool IsMute = false;
    // Use this for initialization
    void Start()
    {
        IsMute = PlayerPrefs.GetInt("IsMute") == 1 ? true : false;
        var audio = GetComponent<AudioSource>();
        audio.mute = IsMute;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
