using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class ShootBaguette : MonoBehaviour
{
    public Transform positionToShoot;
    public float delay = 5;
    public float speed = 5f;
    private Vector3 direction;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, delay);
        direction = transform.position;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
            DestroyImmediate(gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
            transform.Translate(Vector3.right * speed * Time.deltaTime, Space.World);
        
        
    }
}
