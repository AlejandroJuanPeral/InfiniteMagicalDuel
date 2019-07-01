using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{

    [SerializeField] private GameObject pausePanel;
    bool pause;
    void Start()
    {
        pause = false;
        pausePanel.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pause)
            {
                ContinueGame();
            }
            else
            {
                PauseGame();
            }
        }
    }
    private void PauseGame()
    {
        Time.timeScale = 0;
        pause = true;
        pausePanel.SetActive(true);
        //Disable scripts that still work while timescale is set to 0
    }
    public void ContinueGame()
    {
        Time.timeScale = 1;
        pause = false;
        pausePanel.SetActive(false);
        //enable the scripts again
    }

}
