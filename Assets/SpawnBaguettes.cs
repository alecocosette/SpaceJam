using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBaguettes : MonoBehaviour
{
    public GameObject slow;
    public GameObject fast;
    public GameObject mid;
    public Transform location;
    public float spawnInterval = 5f;
    private static int num = 1;
    private int midCount = 0;
    private int slowCount = 0;
    private int fastCount = 0;
    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (true) // Loop indefinitely
        {
            if (num % 3 == 1)
            {
                Vector3 offset = new Vector3(midCount * 1000f, 0, 0);
                Instantiate(mid, location.position + offset, location.rotation);
                midCount++;
            }
            else if (num % 3 == 2)
            {
                Vector3 offset = new Vector3(slowCount * 800f, 0, 0);

                Instantiate(slow, location.position + offset, location.rotation);
                slowCount++;
            }
            else
            {
                Vector3 offset = new Vector3(fastCount * 1000f, 0, 0);

                Instantiate(fast, location.position+offset, location.rotation);
                num = 0;
                fastCount++;
            }
            num++;
            // Wait for the specified interval before the next iteration
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}

