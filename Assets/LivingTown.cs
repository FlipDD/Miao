using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingTown : MonoBehaviour
{
    private bool rotating;

    void Update()
    {
        if (!rotating)
            StartCoroutine(Rotate(transform, Vector3.up, 10));
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

