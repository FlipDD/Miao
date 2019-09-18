using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingTree : MonoBehaviour
{
    private bool rotating;
    private bool lefti;
    [SerializeField]
    private float angle;

    void Start()
    {
        
    }

    void Update()
    {
        if (!rotating)
            if (!lefti)
            {
                StartCoroutine(Rotate(transform, Vector3.up, angle, Random.Range(1f, 2f)));
                lefti = true;
            }
            else if (lefti)
            {
                StartCoroutine(Rotate(transform, Vector3.up, -angle, Random.Range(1f, 2f)));
                lefti = false;
            }
    }

    IEnumerator Rotate(Transform tf, Vector3 axis, float angle, float duration = 0.5f)
    {
        rotating = true;
        Quaternion from = tf.rotation;
        Quaternion to = tf.rotation;
        to *= Quaternion.Euler( axis * angle );
    
        float elapsed = 0.0f;
        while(elapsed < duration)
        {
            tf.rotation = Quaternion.Slerp(from, to, elapsed / duration );
            elapsed += Time.deltaTime;
            yield return null;
        }
        tf.rotation = to;
        rotating = false;
    }
}
