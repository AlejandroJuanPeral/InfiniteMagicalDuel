using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public GameObject MainPanel, PlayerTypePanel, PausePanel, SelectCharacter, ExitPanel, InstructionPanel;
    public AudioSource audio, music;
    public void clickButton(string button)
    {
        audio.Play();
        switch (button)
        {
            case "Start":
                SelectCharacter.SetActive(true);
                MainPanel.SetActive(false);
                break;
            case "Instructions":
                InstructionPanel.SetActive(true);
                MainPanel.SetActive(false);
                break;
            case "Return":
                InstructionPanel.SetActive(false);
                PlayerTypePanel.SetActive(false);
                SelectCharacter.SetActive(false);
                MainPanel.SetActive(true);
                break;
            case "ExitGame":
                Application.Quit();
                break;
            case "Resume":
                PausePanel.SetActive(false);
                break;
            case "Restart":
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                break;
            case "Exit":
                ExitPanel.SetActive(true);
                break;
            case "Yes":
                SceneManager.LoadScene("Init");
                break;
            case "No":
                ExitPanel.SetActive(false);
                break;
            case "Terry":
                MainManager.Character = 1;
                SelectCharacter.SetActive(false);
                PlayerTypePanel.SetActive(true);
                break;
            case "Lili":
                MainManager.Character = 2;
                SelectCharacter.SetActive(false);
                PlayerTypePanel.SetActive(true);
                break;
            case "Players":
                music.Stop();
                SceneManager.LoadScene("players");
                break;
            case "AI":
                music.Stop();
                SceneManager.LoadScene("AI");
                break;
        }
    }
}
