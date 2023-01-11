using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicControl1 : MonoBehaviour
{
    [SerializeField] Toggle toggle;
    // Start is called before the first frame update
    void Start()
    {
        //PlayerPrefs.SetInt("JavaScript", toggle.isOn ? 1 : 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
     public void SaveButton()
    {

        PlayerPrefs.SetInt("JavaScript", toggle.isOn ? 0 : 1);
    }
}
