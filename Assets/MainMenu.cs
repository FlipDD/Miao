using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private Text soundText;
    private bool sound;

    void Start()
    {
        Cursor.visible = true;
        GetComponent<PlayerController>().ResetDogs();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void TurnOffSound()
    {
        if (!sound)
        {
            sound = true;
            AudioListener.volume = 0;
            soundText.text = "Turn on dogs";
        }
        else if (sound)
        {
            sound = false;
            AudioListener.volume = 1;
            soundText.text = "Turn off dogs";
        }
    }
}
