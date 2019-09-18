using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSliding : MonoBehaviour
{
    //smaller = harder
    private float difficulty;

    void Start()
    {
        difficulty = 5;
    }

    void Update()
    {
        transform.localPosition += new Vector3(0, -Time.deltaTime/difficulty, 0);
    }
}
