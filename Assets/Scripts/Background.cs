using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField] Transform followingTarget;
    [SerializeField, Range(-1f, 1f)] float parallaxStrength = 0.1f;
    [SerializeField] bool disableVerticalParallax;
    Vector3 targetPreviousPosition;


    void Start()
    {
        if (!followingTarget)
            followingTarget = Camera.main.transform;

        targetPreviousPosition = followingTarget.position;
    }

    void Update()
    {
        var delta = followingTarget.position - targetPreviousPosition;

        if (disableVerticalParallax)
            delta.y = 0;

        targetPreviousPosition = followingTarget.position;
        
        transform.position += delta * parallaxStrength;
    }
}
