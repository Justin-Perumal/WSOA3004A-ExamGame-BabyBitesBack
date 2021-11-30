using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void StartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainLevel");
    }

    public void StartBossLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("BossLevel");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
