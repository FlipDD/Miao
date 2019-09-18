using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Transform city;
    [SerializeField]
    private Transform doggoNormal;
    [SerializeField]
    private Transform doggoHusky;
    [SerializeField]
    private Transform doggoZaza;
    [SerializeField]
    private Transform cam;
    private SpriteRenderer sprite;
    private Rigidbody rgdb;
    private Quaternion targetRotation;
    private bool running;
    private bool rotating;
    private bool hiding;
    private Animator anim;
    private static string dogoString;
    private bool grounded;

    [SerializeField]
    private TMP_Text doggos;
    internal static int doggosRescued = 0;

    private GameObject pauseMenu;
    
    void Start()
    {
        pauseMenu = FindObjectOfType<PauseMenu>().gameObject;
        pauseMenu.SetActive(false);

        if (SceneManager.GetActiveScene().name != "MainMenu")
            Cursor.visible = false;
        targetRotation = transform.rotation;
        rgdb = GetComponent<Rigidbody>();
        anim = gameObject.GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        doggos.text = "Doggos rescued: " + doggosRescued;
        Time.timeScale = 1;
        int index = Random.Range(0, 3);
        Transform doggie = null;
        if (index == 0)
            doggie = Instantiate(doggoNormal, new Vector3(Random.Range(-12f, 12f), doggoNormal.position.y + 1f, Random.Range(-12f, 12f)), Quaternion.identity);
        else if (index == 1)
            doggie = Instantiate(doggoHusky, new Vector3(Random.Range(-12f, 12f), doggoHusky.position.y + 1f, Random.Range(-12f, 12f)), Quaternion.identity);
        else if (index == 2)
            doggie = Instantiate(doggoZaza, new Vector3(Random.Range(-12f, 12f), doggoZaza.position.y + 1f, Random.Range(-12f, 12f)), Quaternion.identity);

        doggie.parent = transform.parent;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !pauseMenu.activeInHierarchy)
        {
            pauseMenu.SetActive(true);
            Cursor.visible = true;
            Time.timeScale = 0;
        }
        // else if (Input.GetKeyDown(KeyCode.Escape) && pauseMenu.activeInHierarchy)
        // {
        //     pauseMenu.SetActive(false);
        //     Cursor.visible = false;
        //     Time.timeScale = 1;
        // }
        Vector3 movement = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));

        movement = cam.TransformDirection(new Vector3(movement.x, 0, movement.z));
        if (movement.x > 0)
            sprite.flipX = false;
        else if (movement.x < 0)
            sprite.flipX = true;

        if (Input.GetKey(KeyCode.LeftShift) && !hiding)
            running = true;
        else
            running = false;

        if (movement != Vector3.zero && !running)
            anim.SetBool("Walking", true);
        else if (movement != Vector3.zero && running)
        {
            anim.SetBool("Walking", true);
            anim.speed = 10;
        }
        else
            anim.SetBool("Walking", false);
            anim.speed = 1;
        
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
            rgdb.AddForce(Vector3.up * 6.5f, ForceMode.Impulse);


        if (Input.GetKey(KeyCode.LeftControl) && !running)
        {
            hiding = true;
            anim.SetBool("Hiding", true);
            movement = Vector3.zero;
        }
        else
        {
            hiding = false;
            anim.SetBool("Hiding", false);
        }

        if (running)
            transform.position += movement * Time.deltaTime * 3.5f;
        else
            transform.position += movement * Time.deltaTime * 2.4f;
    }

    IEnumerator Rotate(Transform tf, Vector3 axis, float angle, float duration = 0.5f)
    {
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
    }

    internal void IncrementDogs()
    {
        doggosRescued += 1;
    }

    internal void ResetDogs()
    {
        doggosRescued = 0;
    }

    internal int NumberOfDogs()
    {
        return doggosRescued;
    }

    internal void SetDogType(string contained)
    {
        dogoString = contained;
    }

    internal string GetDotType()
    {
        return dogoString;
    }

    void OnCollisionEnter(Collision col)
    {
        if (!rotating)
        {
            if (col.gameObject.CompareTag("WallLeft"))
            {   
                transform.position += col.GetContact(0).normal * -2.5f;
                StartCoroutine(Rotate(city, Vector3.up, -90));
                transform.Rotate(Vector3.up, 90);
                StartCoroutine(WaitForRotation());
            }
            else if (col.gameObject.CompareTag("WallRight"))
            {
                transform.position += col.GetContact(0).normal * -2.5f;
                StartCoroutine(Rotate(city, Vector3.up, 90));
                transform.Rotate(Vector3.up, -90);
                StartCoroutine(WaitForRotation());
            }

            if (col.gameObject.CompareTag("Player"))
                SceneManager.LoadScene("Minigame");

            if (col.gameObject.CompareTag("Ground"))
                grounded = true;
        }
    }

    void OnCollisionExit(Collision col)
    {
        if (col.gameObject.CompareTag("Ground"))
                grounded = false;
    }

    IEnumerator WaitForRotation()
    {
        rotating = true;
        yield return new WaitForSeconds(.5f);
        rotating = false;
    }

    internal bool GetHiding()
    {
        return hiding;
    }
}
