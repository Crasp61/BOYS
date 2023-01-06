using System.Collections;
using UnityEngine;

public class Wizard : Enemy
{
    [SerializeField] LayerMask playerLayer;
    [SerializeField] Transform playerPosition;
    [SerializeField] float radiusOfDetection;
    private bool readyToCast = true;
    [SerializeField] GameObject theCircleOfCurse;
    GameObject[] groundToTp;
    private bool readyToTp = true;
    GameObject[] goodGround;
    private int n=0;
    private bool ReadyToAtack()
    {
        return Physics2D.OverlapCircle(transform.position, radiusOfDetection, playerLayer);
    }
    /*private void FoundGroundToTp()
    {
        groundToTp = GameObject.FindGameObjectsWithTag("PointToTP");
        goodGround = new GameObject[groundToTp.Length];
        for (int i = 0;i < groundToTp.Length; i++)
        {
            if(Mathf.Abs(groundToTp[i].transform.position.x - transform.position.x)<radiusOfDetection && Mathf.Abs(groundToTp[i].transform.position.y - transform.position.y) < radiusOfDetection)
            {
                goodGround[n] = groundToTp[i];
                n++;
            }
        }
    }*/
    protected override void Update()
    {
        base.Update();
        if(readyToCast && ReadyToAtack())
        {
            StartCoroutine(castTheSpell());
        }
        if (ReadyToAtack() && readyToTp)
        {
            /*FoundGroundToTp();*/
            StartCoroutine(Teleport());
        }
    }

    private IEnumerator Teleport()
    {
        readyToTp = false;
        yield return new WaitForSeconds(5);
        /*int randomNumber = UnityEngine.Random.Range(0, goodGround.Length);
        transform.position = new Vector2(goodGround[randomNumber].transform.position.x, goodGround[randomNumber].transform.position.y);
        n = 0;*/
        readyToTp = true;
    }
    private IEnumerator castTheSpell()
    {
        readyToCast = false;
        yield return new WaitForSeconds(1.5f);
        Instantiate(theCircleOfCurse, playerPosition.position, playerPosition.rotation);
        yield return new WaitForSeconds(6f);
        readyToCast = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, radiusOfDetection);
    }
}
