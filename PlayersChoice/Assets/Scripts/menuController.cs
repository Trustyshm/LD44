using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class menuController : MonoBehaviour
{

    public bool toggleEscapeMenu;
    public GameObject escapeCanvas;
    public GameObject loadoutCanvas;

    public bool gameStart;

    public AudioSource buttonHit;

    // Start is called before the first frame update
    void Start()
    {
        gameStart = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameStart == true)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                toggleEscapeMenu = !toggleEscapeMenu;
            }

            if (toggleEscapeMenu)
            {
                escapeCanvas.SetActive(true);
                Time.timeScale = 0f;
            }
            else
            {
                escapeCanvas.SetActive(false);
                Time.timeScale = 1f;
            }
        }
        
    }

    public void LetsPlay()
    {
        SceneManager.LoadScene("MainScene");
        buttonHit.GetComponent<soundRandomizer>().PlayActive();
    }

    public void ContinueGame()
    {
        escapeCanvas.SetActive(false);
        Time.timeScale = 1f;
        toggleEscapeMenu = !toggleEscapeMenu;
    }


    public void GameStart()
    {
        gameStart = true;
    }

    public void LoadoutDone()
    {
        gameStart = true;
        loadoutCanvas.SetActive(false);
        buttonHit.GetComponent<soundRandomizer>().PlayActive();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("MainScene");
        buttonHit.GetComponent<soundRandomizer>().PlayActive();
    }

    public void QuitGame()
    {
        Application.Quit();
        buttonHit.GetComponent<soundRandomizer>().PlayActive();
    }

   
}
