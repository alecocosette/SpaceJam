using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BaguetteTakeRelease : MonoBehaviour
{
    public Transform positionToShoot;
    private static int howManyBaguettes = 0;
    public GameObject projectile;
    public AudioClip intake;
    public AudioClip release;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("BaguetteItem"))
        {
            audioSource.PlayOneShot(intake);
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
            audioSource.PlayOneShot(release);
            Instantiate(projectile, positionToShoot.position, positionToShoot.rotation);
            howManyBaguettes--;
        } 
    }
}
