using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BaguetteTakeRelease : MonoBehaviour
{
    private static int howManyBaguettes = 0;
    public GameObject projectile;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("BaguetteItem"))
        {
            howManyBaguettes++;
            Destroy(collision.gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(howManyBaguettes > 0 && Input.GetKeyDown(KeyCode.E))
        {
            // cambia esta vaina cuando ya tengas el script;
            Instantiate(projectile);
            howManyBaguettes--;
        } 
    }
}
