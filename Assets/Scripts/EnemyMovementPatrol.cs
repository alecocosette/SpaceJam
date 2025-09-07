using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class EnemyMovementPatrol : MonoBehaviour
    {
        public Transform pointA;
        public Transform pointB;
        private Rigidbody2D rb;
        //private Animator anim;
        //hi
        private Transform currentPoint;
        public float speed;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            //anim = GetComponent<Animator>();
            currentPoint = pointB;
            //anim.SetBool("isRunning", true);
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            Vector2 direction = new Vector2(currentPoint.position.x - transform.position.x, 0).normalized;
            rb.velocity = direction * speed;

            // Switch points when close enough
            if (Mathf.Abs(transform.position.x - currentPoint.position.x) < 5f)
            {
                currentPoint = currentPoint == pointA ? pointB : pointA;

            }
            if (direction.x > 0)
                transform.localScale = new Vector3(3, 3, 1);
            else if (direction.x < 0)
                transform.localScale = new Vector3(-3, 3, 1);
        }
        
    }
}