using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class ChaserController : MonoBehaviour
{
    [SerializeField]
    private Transform cam;
    [SerializeField]
    private Transform player;
    [SerializeField]
    private GameObject lostText;
    private PlayerController playerController;
    private NavMeshAgent agent;
    private bool finding;
    private SpriteRenderer spriteRend;

    void Start()
    {
        playerController = player.GetComponent<PlayerController>();
        agent = GetComponent<NavMeshAgent>();
        agent.destination = player.position;
        agent.speed += playerController.NumberOfDogs() * .3f;
        spriteRend = GetComponent<SpriteRenderer>();
        lostText.SetActive(false);
    }

    void Update()
    {
        transform.LookAt(new Vector3(cam.position.x, 0, cam.position.z));
        if (!finding)
            StartCoroutine(FollowFrequency());

        float dist = Vector3.Distance(transform.position, player.position);
        if (dist < 0)
            spriteRend.flipX = true;
        else if (dist > 0)
            spriteRend.flipX = false;
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("WallLeft"))
            transform.position += col.GetContact(0).normal * -2.5f;
        else if (col.gameObject.CompareTag("WallRight"))
            transform.position += col.GetContact(0).normal * -2.5f;

        if (col.gameObject.CompareTag("Playa"))
            StartCoroutine(GoMenu());
    }

    IEnumerator FollowFrequency()
    {
        finding = true;
        yield return new WaitForSeconds(3);
        if (!playerController.GetHiding())
            agent.destination = player.position;
        else
            agent.destination = new Vector3(Random.Range(-12.5f, 12.5f), transform.position.y, Random.Range(-12.5f, 12.5f));
        finding = false;
    }

    IEnumerator GoMenu()
    {
        lostText.SetActive(true);
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(2.5f);
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
}
