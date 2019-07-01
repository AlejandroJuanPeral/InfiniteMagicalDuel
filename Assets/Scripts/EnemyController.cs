using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer renderer;
    public int health;
    public GameObject gm;
    public GameObject Player;
    [SerializeField] Sprite[] EnemiesSprites;
    int speed;
    bool isAttack;
    Rigidbody2D rg;
    Vector3 direction;
    [SerializeField] private AudioSource voice;

    // Start is called before the first frame update
    void Start()
    {
        voice = this.gameObject.GetComponent<AudioSource>();
        renderer = this.gameObject.GetComponent<SpriteRenderer>();
        rg = gameObject.GetComponent<Rigidbody2D>();
        gm = GameObject.FindGameObjectWithTag("GameManager");

        if(gm.GetComponent<GameManager>().Level == 1)
        {
            renderer.sprite = EnemiesSprites[0];
            speed = 3;
        }else if (gm.GetComponent<GameManager>().Level == 2)
        {
            renderer.sprite = EnemiesSprites[1];
            speed = 4;
        }else if (gm.GetComponent<GameManager>().Level == 3)
        {
            renderer.sprite = EnemiesSprites[2];
            speed = 6;
        }
    
        health = Random.Range(1, 5);
        changeColor();
    }
    private void Update()
    {
        if (isAttack)
        {
            transform.Translate(direction * Time.deltaTime * speed);
        }
        if (Vector3.Distance(transform.localPosition, Player.transform.localPosition)> 13)
        {
            Destroy(this.gameObject);
        }
    }
    public void Attack()
    {
        isAttack = true;
        direction = (Player.transform.position - this.transform.position).normalized;
        //rg.velocity = speed * direction;

    }
    private void changeColor()
    {
        switch (health)
        {
            case 1:
                renderer.color = Color.red;
                break;
            case 2:
                renderer.color = new Color(1f, 0.6f, 0f);
                break;
            case 3:
                renderer.color = Color.yellow;
                break;
            case 4:
                renderer.color = Color.green;
                break;
            case 5:
                renderer.color = Color.blue;
                break;
        }
    }
    public void receiveDamage(int d)
    {
        health -= d;
        if (health > 0)
        {
            changeColor();
        }
        else
        {
            if(transform.parent.tag == "Player1")
            {
                gm.GetComponent<GameManager>().point_Player1();
            }
            else if (transform.parent.tag == "Player2")
            {
                gm.GetComponent<GameManager>().point_Player2();
            }
            renderer.enabled = false;
            this.gameObject.GetComponent<CircleCollider2D>().enabled = false;
            voice.Play();
            Destroy(this.gameObject, 0.5f);
        }
    }
    public void receiveDamage(int d, MagicAgent IA)
    {
        health -= d;
        if (health > 0)
        {
            changeColor();
            IA.EnemyDamage(0.05f);
        }
        else
        {
            IA.EnemyDamage(0.1f);
            if (transform.parent.tag == "Player1")
            {
                gm.GetComponent<GameManager>().point_Player1();
            }
            else if (transform.parent.tag == "Player2")
            {
                gm.GetComponent<GameManager>().point_Player2();
            }
            renderer.enabled = false;
            this.gameObject.GetComponent<CircleCollider2D>().enabled = false;
            voice.Play();
            Destroy(this.gameObject, 0.5f);
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().receiveDamage();
        }
        else if (collision.transform.tag == "AI")
        {
            collision.gameObject.GetComponent<MagicAgent>().receiveDamage();
        }
    }
}
