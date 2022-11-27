using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : SmoothCamera
{
    private void Update()
    {
        Move(Time.deltaTime);
    }
}
