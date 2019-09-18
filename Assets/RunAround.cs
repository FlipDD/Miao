using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunAround : MonoBehaviour
{
    private bool moving;
    private bool left;
    private Vector3 op;
    private Vector3 refVel = Vector3.zero;

    void Start()
    {
        op = transform.position;        
    }

    void Update()
    {
        if (!moving)
        {
            if (!left)
            {
                StartCoroutine(Move(1));
                left = true;
            }
            else if (left)
            {
                StartCoroutine(Move(-1));
                left = false;
            }
        }
    }

    IEnumerator Move(float mult)
    {
        moving = true;
        transform.position = Vector3.Lerp(transform.position, new Vector3(op.x, op.y + 10 * mult, op.z), .05f);
        yield return new WaitForSeconds(Random.Range(1f, 3f));
        moving = false;
    } 
}
