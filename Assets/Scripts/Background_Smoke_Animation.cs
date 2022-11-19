using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background_Smoke_Animation : MonoBehaviour
{
    public float StartX;
    public float EndX;
    void Update()
    {
        if (transform.position.x < EndX)
        {
            transform.position = (Vector3)new Vector2(StartX, transform.position.y);
        }
    }
}
