using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField]
    private Transform player;

    void Start()
    {
        
    }

    void Update()
    {
        transform.LookAt(new Vector3(0, player.position.y, 0));
        transform.position = Vector3.Lerp(transform.position, player.position + (-player.right * 2), .05f);
    }
}
