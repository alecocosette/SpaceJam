using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScript : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            //Game Over Screen
        }
        if (collision.CompareTag("Sheep"))
        {
            //Game Over Screen
        }

    }
}
