using UnityEngine;
using UnityEngine.UI;

public class MusicControl : MonoBehaviour
{
    bool[] settings;

    public Toggle[] toggles;

    void Start()
    {
        settings = new bool[toggles.Length];
        for (int i = 0; i < toggles.Length; i++)
        {
            settings[i] = true;
            int index = i;

            Toggle t = toggles[i];
            t.onValueChanged.AddListener(

                (bool check) =>
                {
                    CheckBox(index, check);
                }
                );
            //saving the value
            //PlayerPrefs.SetInt(toggles[i].gameObject.name, toggles[i].isOn == true ? 1 : 0);

            //and loading it...
            //bool isOn = PlayerPrefs.GetInt(toggles[i].gameObject.name) == 1 ? true : false;
        }
    }

    public void CheckBox(int index, bool check)
    {
        settings[index] = check;

    }
    
}