using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField] private List<GameObject> _weaponInChest;
    private bool _onTriggerChest = false;

    private void Update()
    {
        OpenChest();
    }
    private void OpenChest()
    {
        if (_onTriggerChest)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Instantiate(_weaponInChest[Random.Range(0, _weaponInChest.Count)], transform.position, Quaternion.identity);
                Destroy(gameObject);
                
            }
        }
    }



    private void OnTriggerStay2D(Collider2D collision)
    {
       if(collision.GetComponent<Player>() != null)
            _onTriggerChest = true;
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
            _onTriggerChest = false;
        
    }
}
