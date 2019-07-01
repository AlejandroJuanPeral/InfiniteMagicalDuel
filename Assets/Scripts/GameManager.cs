using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int comboPlayer1, comboPlayer2, Level;

    float timerGame, timer1, timer2;
    bool combo1, combo2;
    int player1Score, player2Score;
    public GameObject Player1, Player2;
    public GameObject spawner;
    public Text scoreText1, scoreText2, comboText1,comboText2, PlayerWinText;
    public GameObject FinishPanel;
    public Sprite terry, lili;
    public AudioClip[] songs;
    public AudioClip[] voice;
    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        Level = 1;
        comboPlayer1 = 0;
        comboPlayer2 = 0;
        timer1 = 0;
        timer2 = 0;
        timerGame = 0;
        combo1 = false;
        combo2 = false;
        player1Score = 0;
        player2Score = 0;
        scoreText1.text = "Score: " + player1Score.ToString("000000");
        scoreText2.text = "Score: " + player2Score.ToString("000000");
        comboText1.text = "x" + comboPlayer2.ToString();
        comboText2.text = "x" + comboPlayer2.ToString();
        if (MainManager.Character == 1)
        {
            Player1.GetComponent<SpriteRenderer>().sprite = terry;
            Player1.GetComponent<AudioSource>().clip = voice[0];
            Player2.GetComponent<SpriteRenderer>().sprite = lili;
        }
        int r = Random.Range(0, 5);
        audioSource.clip = songs[r];
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        timerGame += Time.deltaTime;
        if (timerGame > 120 && Level == 1)
        {
            Level = 2;
            changeLevel(2);
        }else if(timerGame > 240 && Level == 2)
        {
            changeLevel(3);
            Level = 3;
        }

        if (combo1)
        {
            if(timer1 > 0)
            {
                timer1 -= Time.deltaTime;
            }
            else
            {
                comboPlayer1 = 0;
                comboText1.text = "x" + comboPlayer1.ToString();
                combo1 = false;
            }
        }
        if (combo2)
        {
            if (timer2 > 0)
            {
                timer2 -= Time.deltaTime;
            }
            else
            {
                comboPlayer2 = 0;
                comboText2.text = "x" + comboPlayer2.ToString();
                combo2 = false;
            }
        }
    }

    public void Win(int p)
    {
        FinishPanel.SetActive(true);

        if (p == 1)
        {
            PlayerWinText.text = "Player 2 Wins";
            if (Player2.tag == "AI")
            {
                Player2.GetComponent<MagicAgent>().Win();
            }
            if (Player1.tag == "AI")
            {
                restart(1);
            }
        }
        else if (p == 2)
        {
            PlayerWinText.text = "Player 1 Wins";
            if (Player1.tag == "AI")
            {
                Player1.GetComponent<MagicAgent>().Win();
            }
            if (Player2.tag == "AI")
            {
                restart(2);
            }
        }
        Time.timeScale = 0;
    }

    public void point_Player1()
    {
        player1Score += 100;
        combo1 = true;
        timer1 = 5f;
        comboPlayer1 += 1;
        scoreText1.text = "Score: " + player1Score.ToString("000000");
        comboText1.text = "x" + comboPlayer1.ToString();
        if (comboPlayer1 % 5 == 0)
        {
            spawner.GetComponent<SpawnerScript>().SpawnPenalties(comboPlayer1 / 5, 1);
        }
    }
    public void point_Player2()
    {
        player2Score += 100;
        combo2 = true;
        timer2 = 5f;
        comboPlayer2 += 1;
        scoreText2.text = "Score: " + player2Score.ToString("000000");
        comboText2.text = "x" + comboPlayer2.ToString();
        if (comboPlayer2 % 5 == 0)
        {
            spawner.GetComponent<SpawnerScript>().SpawnPenalties(comboPlayer2 / 5, 2);
        }
    }

    void changeLevel(int l)
    {
        audioSource.Stop();
        int r = Random.Range(0, 5);
        audioSource.clip = songs[r];
        audioSource.Play();
    }

    void restart(int p)
    {
        if(p == 1)
        {
            timer1 = 0;
            comboPlayer1 = 0;
            player1Score = 0;
            scoreText1.text = "Score: " + player1Score.ToString("000000");
            combo1 = false;
        }
        else if (p == 2)
        {
            timer2 = 0;
            comboPlayer2 = 0;
            player2Score = 0;
            scoreText2.text = "Score: " + player2Score.ToString("000000");
            combo2 = false;
        }

        spawner.GetComponent<SpawnerScript>().restart(p);
    }
}

