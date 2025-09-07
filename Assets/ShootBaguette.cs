using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

namespace Assets
{
    public class ShootBaguette : MonoBehaviour
    {
        public float delay = 0;
        public float speed = 5f;
        // Start is called before the first frame update
        void Start()
        {
            Destroy(gameObject, delay);
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            //    if (collision != null)
            //    {
            //        Destroy(gameObject);
            //    }
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Enemy"))
            {
                Destroy(collision.gameObject);
                Destroy(gameObject);
            }
        }
        // Update is called once per frame
        void Update()
        {

            transform.Translate(Vector3.right * speed * Time.deltaTime, Space.Self);


        }
    }
}