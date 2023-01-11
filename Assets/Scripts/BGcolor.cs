using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGcolor : MonoBehaviour
{
    public Color32[] colores = new Color32[4];
    public float speed = 1.0f;
    float startTime;
    public bool colorTime = false;

    // Start is called before the first frame update
    void Start()
    {
        //colores[1] = Camera.main.backgroundColor;
        startTime = Time.time;
        //Camera.main.backgroundColor = colores[0];
    }
    IEnumerator colorchange1()
    {

        yield return new WaitForSeconds(10f);
        //Camera.main.backgroundColor = colores[1];
        colorTime = true;
        //Camera.main.backgroundColor = colores[2];
    }
    // Update is called once per frame
    void Update()
    {
        float t = (Time.time - startTime) * speed;
        Camera.main.backgroundColor = colores[1];
        Camera.main.backgroundColor = Color.Lerp(colores[1], colores[2], t);
        if (colorTime)
        {
            Camera.main.backgroundColor = colores[2];
            Camera.main.backgroundColor = Color.Lerp(colores[2], colores[3], t);
        }
        StartCoroutine(colorchange1());
        //Camera.main.backgroundColor = Color.Lerp(colores[2], colores[3], t);
    }
    
}
