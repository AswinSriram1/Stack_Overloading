using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Removerubble : MonoBehaviour
{
    public void OnCollisionEnter(Collision col)
    {
        Destroy(col.gameObject);
    }
}
