using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Sound : MonoBehaviour
{
    public AudioSource audio;



    // Use this for initialization
    void Start()
    {
        //audio = GetComponent<AudioSource>();
        var v = GetComponent<Toggle>();
        v.onValueChanged.AddListener(musicOnOff);
    }

    public void musicOnOff(bool value)
    {
        audio.mute = !audio.mute;
        PlayerPrefs.SetInt("IsMute", audio.mute ? 1 : 0);
        //if (audio.mute == true)
        //    audio.mute = false;

        //if (audio.mute == false)
        //    audio.mute = true;

    }

    // Update is called once per frame
    void Update()
    {

    }
}
