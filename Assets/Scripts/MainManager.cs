using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    public static int Character;
    public AudioSource music;
    public AudioClip[] songs;
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    private void Start()
    {
        int r = Random.Range(0, 2);
        music.clip = songs[r];
        music.Play();
    }
}
