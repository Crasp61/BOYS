using System.Collections;
using UnityEngine;

public class Wizard : Enemy
{
    [SerializeField] LayerMask playerLayer;
    [SerializeField] Transform playerPosition;
    [SerializeField] float radiusOfDetection;
    private bool readyToCast = true;
    [SerializeField] GameObject theCircleOfCurse;

    private bool ReadyToAtack()
    {
        return Physics2D.OverlapCircle(transform.position, radiusOfDetection, playerLayer);
    }

    protected override void Update()
    {
        base.Update();
        if(readyToCast && ReadyToAtack())
        {
            StartCoroutine(castTheSpell());
        }
    }

    private IEnumerator castTheSpell()
    {
        readyToCast = false;
        yield return new WaitForSeconds(1.5f);
        Instantiate(theCircleOfCurse, playerPosition.position, playerPosition.rotation);
        yield return new WaitForSeconds(5f);
        readyToCast = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, radiusOfDetection);
    }
}
