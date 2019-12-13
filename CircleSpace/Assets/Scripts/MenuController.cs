using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MenuController : MonoBehaviour
{
    [SerializeField] private PlayerMovement playMoveScript;

    [Space(10)]

    [SerializeField] private EventSystem myEventsystem;

    [Space(10)]

    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject panelPause;
    [SerializeField] private GameObject confirmExit;
    [SerializeField] private GameObject highScores;
    [SerializeField] private GameObject death;
    private bool pauseMenuActiveState;
    private bool playerIsDead;

    [Space(10)]

    [SerializeField] private GameObject[] uiButtons;

    void Start()
    {
        pauseMenuActiveState = false;
        playerIsDead = false;

        pauseMenu.SetActive(pauseMenuActiveState);
        panelPause.SetActive(false);
        confirmExit.SetActive(false);
        highScores.SetActive(false);
        death.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            PauseUnpause();
        }
    }

    public void PauseUnpause()
    {
        if (!playerIsDead)
        {
            pauseMenu.SetActive(!pauseMenuActiveState);
            pauseMenuActiveState = !pauseMenuActiveState;

            myEventsystem.SetSelectedGameObject(uiButtons[0]); // Set Continue as selected button

            if (pauseMenuActiveState == true) // PAUSED
            {
                for (int i = 0; i < playMoveScript.circles.Count; i++)
                {
                    playMoveScript.circles[i].SetActive(false);
                }

                panelPause.SetActive(true);
                Time.timeScale = 0;
            }

            else if (pauseMenuActiveState == false) // UNPAUSED
            {
                for (int i = 0; i < playMoveScript.circles.Count; i++)
                {
                    playMoveScript.circles[i].SetActive(true);
                }

                panelPause.SetActive(false);
                Time.timeScale = 1;
            }

            confirmExit.SetActive(false);
            highScores.SetActive(false);
            death.SetActive(false);
        }
    }

    public void Continue()
    {
        PauseUnpause();
        Debug.Log("Unpaused");
    }

    public void HighScore()
    {
        Debug.Log("Highscore");
        highScores.SetActive(true);
        death.SetActive(false);
        panelPause.SetActive(false);

        myEventsystem.SetSelectedGameObject(uiButtons[5]); // Set Back as selected button
    }

    public void ExitGame()
    {
        if (!playerIsDead)
        {
            Debug.Log("Exit from play");
            confirmExit.SetActive(true);
            panelPause.SetActive(false);

            myEventsystem.SetSelectedGameObject(uiButtons[3]); // Set Yes as selected button
        }

        if (playerIsDead)
        {
            Debug.Log("Exit from death");
            confirmExit.SetActive(true);
            death.SetActive(false);
            panelPause.SetActive(false);

            myEventsystem.SetSelectedGameObject(uiButtons[3]); // Set Yes as selected button
        }
    }

    public void ConfirmExit()
    {
#if (UNITY_EDITOR || DEVELOPMENT_BUILD)
        Debug.Log(name + " : " + GetType() + " : " + System.Reflection.MethodBase.GetCurrentMethod().Name);
#endif

#if (UNITY_EDITOR)
        UnityEditor.EditorApplication.isPlaying = false;
#elif (UNITY_STANDALONE) 
        Application.Quit();
#elif (UNITY_WEBGL)
        Application.OpenURL("about:blank");
#endif
    }

    public void NoGoBack()
    {
        if (!playerIsDead)
        {
            Debug.Log("No Go Back");
            panelPause.SetActive(true);
            confirmExit.SetActive(false);
            highScores.SetActive(false);

            myEventsystem.SetSelectedGameObject(uiButtons[0]); // Set Continue as selected button
        }

        if (playerIsDead)
        {
            Debug.Log("No Go Back");
            PlayerDeathPanel();
        }
    }

    public void PlayerDeath()
    {
        PauseUnpause();
        playerIsDead = true;
        PlayerDeathPanel();
    }

    public void PlayerDeathPanel()
    {
        panelPause.SetActive(false);
        confirmExit.SetActive(false);
        highScores.SetActive(false);

        death.SetActive(true);
        myEventsystem.SetSelectedGameObject(uiButtons[7]);
    }

    public void YesRestartGame()
    {
        pauseMenuActiveState = false;
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
        Debug.Log("Restart game");
    }
}
