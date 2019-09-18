    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ActivateMinigame : MonoBehaviour
{
    private GameObject cameraObject;
    private Transform cam;
    private GameObject playa;
    private PlayerController playaController;
    private GameObject ct;
    private GameObject chaser;
    private SpriteRenderer chaserSp;
    private TMPro.TMP_Text convinceText;
    private bool isWatching;
    private Rigidbody rgdb;
    private bool jumping;

    void Start()
    {
        ct = GameObject.Find("ConvinceDoggo");
        chaser = GameObject.Find("Chaser");
        chaserSp = chaser.GetComponent<SpriteRenderer>();
        ct.SetActive(false);

        cameraObject = GameObject.Find("Main Camera");
        cam = cameraObject.transform;

        playa = GameObject.Find("Player");
        playaController = playa.GetComponent<PlayerController>();
        playaController.SetDogType(gameObject.name);

        rgdb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        transform.LookAt(new Vector3(cam.position.x, 0, cam.position.z));

        if (chaserSp.isVisible)
            isWatching = true;
        else
            isWatching = false;

        if (!jumping)
            StartCoroutine(Jump());

    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Playa"))
            StartCoroutine(StartMinigame());
    }

    IEnumerator StartMinigame()
    {
        ct.SetActive(true);
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(2);
        SceneManager.LoadScene("Minigame");
    }

    IEnumerator Jump()
    {
        jumping = true;
        rgdb.AddForce(Vector3.up * 125, ForceMode.Force);
        yield return new WaitForSeconds(.3f);
        jumping = false;
    }
}
