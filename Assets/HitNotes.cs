using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class HitNotes : MonoBehaviour
{
    [SerializeField]
    private Transform noteExplosion;
    [SerializeField]
    private Transform ballExplosion;
    [SerializeField]
    private Transform cam;
    private Vector3 initPosition;
    [SerializeField]
    private TMPro.TMP_Text loseText;
    [SerializeField]
    private TMPro.TMP_Text winText;
    [SerializeField]
    private TMPro.TMP_Text healthText;
    [SerializeField]
    private TMPro.TMP_Text timeText;
    private float time = 30;
    private int healthPoints = 3;
    internal bool left, up, down, right, clicked, checking, hit;
    private AudioManager a;

    void Start()
    {
        loseText.gameObject.SetActive(false);
        winText.gameObject.SetActive(false);
        initPosition = cam.transform.position;
        a = FindObjectOfType<AudioManager>();
        Time.timeScale = 1;
    }

    void Update()
    {
        if (Time.timeSinceLevelLoad > 2)
            HandleInput();
    }

    void HandleInput()
    {
        time -= Time.deltaTime;
        timeText.text = "Time left: " + (int) time;
        if (time <= 0)
            StartCoroutine(WinState());

        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown("a"))
        {
            StartCoroutine(SwitchBool(0));
            StartCoroutine(CheckClick());
            a.Play("Miao1");
            // up = false;
            // down = false;
            // right = false;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown("w"))
        {
            StartCoroutine(SwitchBool(1));
            StartCoroutine(CheckClick());
            a.Play("Miao2");
            // left = false;
            // down = false;
            // right = false;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown("s"))
        {
            StartCoroutine(SwitchBool(2));
            StartCoroutine(CheckClick());
            a.Play("Miao3");
            // left = false;
            // up = false;
            // right = false;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown("d"))
        {
            StartCoroutine(SwitchBool(3));
            StartCoroutine(CheckClick());
            a.Play("Miao4");
            // left = false;
            // up = false;
            // down = false;
        }
    }
    
    void OnCollisionStay(Collision col)
    {
        if ((col.gameObject.CompareTag("Left") && left) || 
            (col.gameObject.CompareTag("Up") && up) || 
            (col.gameObject.CompareTag("Down") && down) || 
            (col.gameObject.CompareTag("Right") && right))
        {
            if (!hit)
                StartCoroutine(Hit());

            Destroy(col.gameObject);
            Instantiate(noteExplosion, col.gameObject.transform.position, Quaternion.identity);
        }
    }

    internal void Damage()
    {
        a.Play("DogMad");
        healthPoints -= 1;
        healthText.text = "Tries: " + healthPoints;
        if (healthPoints == 0)
        {
            StartCoroutine(LoseState());
        }
        else
            StartCoroutine(CameraShaker(.5f, 0.08f, 0.08f, 0.08f));
    }

    IEnumerator LoseState()
    {
        loseText.gameObject.SetActive(true);
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(2);
        Time.timeScale = 1;
        SceneManager.LoadScene("SampleScene");
    }

    IEnumerator WinState()
    {
        winText.gameObject.SetActive(true);
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(3);
        FindObjectOfType<PlayerController>().IncrementDogs();
        Time.timeScale = 1;
        SceneManager.LoadScene("SampleScene");
    }

    IEnumerator SwitchBool(int index)
    {
        if (index == 0)
            left = true;
        else if (index == 1)
            up = true;
        else if (index == 2)
            down = true;
        else if (index == 3)
            right = true;

        yield return new WaitForSeconds(.1f);

        if (index == 0)
            left = false;
        else if (index == 1)
            up = false;
        else if (index == 2)
            down = false;
        else if (index == 3)
            right = false;
    }

    IEnumerator CheckClick()
    {
        checking = true;
        yield return new WaitForSeconds(.05f);
        if (!hit)
            Damage();

        checking = false;
    }

    IEnumerator Hit()
    {
        hit = true;
        yield return new WaitForSeconds(.05f);
        hit = false;
    }

    internal IEnumerator CameraShaker(float duration, float magnitudeX, float magnitudeY, float magnitudeZ)
	{
		float timer = 0;
		while (timer < duration)
		{
			float x = UnityEngine.Random.Range(-1f, 1f) * magnitudeX;
			float y = UnityEngine.Random.Range(1f, 1f) * magnitudeY;
            float z = UnityEngine.Random.Range(1f, 1f) * magnitudeY;

			cam.transform.position	 = new Vector3(initPosition.x + x, initPosition.y + y, initPosition.z + z);

			timer += Time.deltaTime;
			
			yield return null;
		
		}
		cam.transform.position = initPosition;
	}
}
