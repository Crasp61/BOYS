using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCamera : MonoBehaviour
{
    [SerializeField] private Transform targetTransform;
    [SerializeField] private Vector3 offcet;
    [SerializeField] private float smoothing = 1f;

    protected private void Move(float deltaTime)
    {
        var nextPosition = Vector3.Lerp(transform.position, targetTransform.position + offcet, deltaTime * smoothing);

        transform.position = nextPosition;
    }
}
