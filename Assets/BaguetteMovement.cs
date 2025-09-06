using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class BaguetteMovement : MonoBehaviour
{
    public float frequency = 1;
    public float amplitude = 1;
    public float delay = 20;
    public float speed = 1;
    private Vector3 startPosition;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        Destroy(gameObject, delay);
    }

    // Update is called once per frame
    void Update()
    {
        float newX = startPosition.x - (speed * Time.time);
        float newY = startPosition.y + Mathf.Sin(Time.time * frequency) * amplitude;
        transform.position = new Vector3(newX, newY, startPosition.z);
    }
}
