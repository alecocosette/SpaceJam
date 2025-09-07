using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pointAtoB : MonoBehaviour
{
    public Transform left;
    public Transform right;
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        if(Mathf.Abs(transform.position.x - left.position.x) < 10f)
        {
            Vector3 scale = transform.localScale;
            scale.x *= -1;   // flip only on X axis
            transform.localScale = scale;
        }
        if (Mathf.Abs(right.position.x - transform.position.x) < 10f)
        {
            Vector3 scale = transform.localScale;
            scale.x *= -1;   // flip only on X axis
            transform.localScale = scale;
        }
    }
}
