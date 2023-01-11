using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomColor : MonoBehaviour
{
    float timeLeft;
    Color targetColor;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timeLeft <= Time.deltaTime)
        {
            Camera.main.backgroundColor = targetColor;
            targetColor = new Color(Random.value, Random.value, Random.value);
            timeLeft = 2.6f;
        }
        else
        {
            Camera.main.backgroundColor = Color.Lerp(Camera.main.backgroundColor, targetColor, Time.deltaTime / timeLeft);
            timeLeft -= Time.deltaTime;
        }
        
    }
}
