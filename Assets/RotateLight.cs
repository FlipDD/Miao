using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateLight : MonoBehaviour
{
    void Update()
    {
        transform.rotation *= Quaternion.Euler(Time.deltaTime * 20, 0, 0);
    }
}
