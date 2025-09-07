using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BaguetteTakeRelease : MonoBehaviour
{
    public Transform positionToShoot;
    private static int howManyBaguettes = 0;
    public GameObject projectile;
    public AudioSource intake;
    public AudioSource release;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("BaguetteItem"))
        {
            Instantiate(intake);
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
            Instantiate(release);
            Instantiate(projectile, positionToShoot.position, positionToShoot.rotation);
            howManyBaguettes--;
        } 
    }
}
