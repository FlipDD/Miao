using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnNotes : MonoBehaviour
{
    [SerializeField]
    private List<Transform> keys;
    //0 - left
    //1 - up
    //2 - down
    //3 - right
    [SerializeField]
    private Transform leftLine, upLine, downLine, rightLine;
    private Vector3 scale;
    private static float difficulty;
    private bool spawning;
    private bool left, right, up, down;

    void Start()
    {
        difficulty = 3f;
        scale = new Vector3(8.1447f, 0.06223061f, 0.082196f);
    }

    void Update()
    {
        difficulty -= Time.deltaTime / 20f;

        if (!spawning)
            StartCoroutine(SpawnNote(Random.Range(difficulty/2 - 1f, difficulty/2 + 1f)));
    }

    IEnumerator SpawnNote(float time, float delay = 0.01f)
    {
        spawning = true;
        yield return new WaitForSeconds(delay);
        int index = Random.Range(0, 4);
        if ((left == true && index == 0) ||
            (right == true && index == 3) ||
            (down == true && index == 2) ||
            (up == true && index == 1))
        {
            StartCoroutine(SpawnNote(.1f, .1f));
            yield break;
        }

        Transform note = Instantiate(keys[index], transform.localPosition, Quaternion.identity);
        if (index == 0)
        {
            note.parent = leftLine;
            StartCoroutine(SetLineBool(0));
        }
        else if (index == 1)
        {
            note.parent = upLine;
            StartCoroutine(SetLineBool(1));
        }
        else if (index == 2)
        {
            note.parent = downLine;
            StartCoroutine(SetLineBool(2));
        }    
        else if (index == 3)
        {
            note.parent = rightLine;
            StartCoroutine(SetLineBool(3));
        }   

        note.localScale = scale;
        note.localPosition = new Vector3(0, 0.5f, -0.5f);
        note.localRotation = Quaternion.identity;

        yield return new WaitForSeconds(time);
        spawning = false;
    }

    IEnumerator SetLineBool(int value)
    {
        if (value == 0)
            left = true;
        else if (value == 1)
            up = true;
        else if (value == 2)
            down = true;
        else if (value == 3)
            right = true;
        yield return new WaitForSeconds(1.2f);
        if (value == 0)
            left = false;
        else if (value == 1)
            up = false;
        else if (value == 2)
            down = false;
        else if (value == 3)
            right = false;

    }
}
