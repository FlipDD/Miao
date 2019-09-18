using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyNotes : MonoBehaviour
{
    [SerializeField]
    private HitNotes player;
    [SerializeField]
    private Transform cam;
    private Vector3 initPosition;

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Left") || 
            col.gameObject.CompareTag("Up") || 
            col.gameObject.CompareTag("Down") || 
            col.gameObject.CompareTag("Right"))
        {
            Destroy(col.gameObject);
            player.Damage();
        }
    }

}
