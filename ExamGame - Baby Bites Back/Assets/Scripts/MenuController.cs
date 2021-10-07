using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MenuController : MonoBehaviour
{
    public GameObject PauseMenuUI;
    public GameObject EndLevelUI;
    public static bool PausedGame = false;

    public void Start()
    {

    }
    //PauseMenu button
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            
            if (PausedGame)
            {
                Resume();
            }

            else if(!PausedGame)
            {
                Pause();
                Debug.Log("Pause");
                Debug.Log(PausedGame);
            }
        }
    }

    public void Resume()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1;
        PausedGame = false;
    }

    public void Pause()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0;
        PausedGame = true;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Level");
    }

    public void Quit()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }

    public void LevelEnd() //Will move this to end level controller eventually
    {
        EndLevelUI.SetActive(true);
        Time.timeScale = 0f;
    }

    private void OnTriggerEnter2D(Collider2D Col)
    {
        if(Col.CompareTag("Player"))
        {
            LevelEnd();
        }
    }
}
