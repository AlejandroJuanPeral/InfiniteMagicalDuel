using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    private int score;
    private int life;
    [SerializeField] private GameObject[] hearts;

    // Start is called before the first frame update
    private void Start()
    {
        UpdateLife(5);
    }

    // Update is called once per frame
    public void UpdateLife(int l)
    {
        life = l;
        for(int i = 0; i < hearts.Length; i++)
        {
            if(i < life)
            {
                hearts[i].GetComponent<Image>().color = Color.red;
            }
            else
            {
                hearts[i].GetComponent<Image>().color = Color.gray;

            }
        }
    }
}
