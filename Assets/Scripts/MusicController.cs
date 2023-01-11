using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicController : MonoBehaviour
{
    public GameObject music;

    public Toggle MusicToggle;

    // Start is called before the first frame update
    void Start()
    {
        CheckMusic();
        bool MusicBool = (PlayerPrefs.GetInt("MusicOn")==1)?true:false;
        MusicToggle.isOn = MusicBool;
    }

    public void CheckMusic()
    {
        if (PlayerPrefs.HasKey("MusicOn"))
        {
            if (PlayerPrefs.GetInt("MusicOn") > 0)
            {
                music.SetActive(true);
            }
            else
            {
               // music.SetActive(false);
            }
        }
        else
        {
            PlayerPrefs.SetInt("MusicOn", 1);
            music.SetActive(true);
        }
    }

    public void MusicBool(bool value)
    {
        if(value == true)
        {
            PlayerPrefs.SetInt("MusicOn", 1);
        }
        else
        {

            PlayerPrefs.SetInt("MusicOn", 0);

        }
        CheckMusic();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
