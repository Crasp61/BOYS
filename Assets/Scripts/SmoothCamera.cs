using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCamera : MonoBehaviour
{
    [SerializeField] private Transform targetTransform;
    [SerializeField] private Vector3 offcet;
    [SerializeField] private float smoothing = 1f;
    protected GameObject playerObj;
    protected void Update()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            var nextPosition = Vector3.Lerp(transform.position, targetTransform.position + offcet, Time.deltaTime * smoothing);
            transform.position = nextPosition;
        }
    }
}
