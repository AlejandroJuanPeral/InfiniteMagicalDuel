using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    Rigidbody2D rb2d;
    GameObject canon;
    [SerializeField] private float maxSpeed = 6f;
    [SerializeField] private GameObject shot, shieldObj;
    [SerializeField] private GameObject megaShot;
    [SerializeField] private CircleCollider2D col;
    [SerializeField] private GameObject HudCon;
    [SerializeField] private SpriteRenderer sprit;
    [SerializeField] private GameObject gm;
    [SerializeField] private AudioSource voice;

    public int life, damage, player;
    private bool inmunity = false, shield = false;
    public float charge, timeShield, timeDamage;

    // Start is called before the first frame update
    void Start()
    {
        life = 5;
        voice = this.gameObject.GetComponent<AudioSource>();
        rb2d = this.gameObject.GetComponent<Rigidbody2D>();
        canon = this.transform.Find("Canon").gameObject;
        charge = 0f;
        col = this.gameObject.GetComponent<CircleCollider2D>();
        damage = 1;
    }

    // Update is called once per frame
    void Update()
    {
        float xMove = Input.GetAxis("Horizontal" + player.ToString());
        float ymove = Input.GetAxis("Vertical" + player.ToString());

        rb2d.velocity = new Vector2(xMove * maxSpeed, ymove * maxSpeed);

        if (Input.GetButtonDown("Fire"+ player.ToString()))
        {
            GameObject shotgo = Instantiate(shot, canon.transform.position, Quaternion.identity);
            shotgo.GetComponent<ShotScript>().damage = damage;
        }
        else if (Input.GetButton("Fire" + player.ToString()))
        {
            charge += Time.deltaTime;
        }else if (Input.GetButtonUp("Fire" + player.ToString()))
        {
            if (charge > 3)
            {
                GameObject shotgo = Instantiate(megaShot, canon.transform.position, Quaternion.identity);
                shotgo.GetComponent<ShotScript>().damage = damage;
            }
            charge = 0;
        }
        if(damage > 1)
        {
            timeDamage -= Time.deltaTime;
            if(timeDamage <= 0)
            {
                damage = 1;
            }
        }
        if (shield)
        {
            timeShield -= Time.deltaTime;
            if (timeShield <= 0)
            {
                shield = false;
                shieldObj.SetActive(false);
            }
        }
    }
    public void takePowerUp(int n)
    {
        switch (n)
        {
            case 1:
                if (life < 5)
                {
                    life++;
                    HudCon.GetComponent<HUD>().UpdateLife(life);
                }
                break;
            case 2:
                shield = true;
                shieldObj.SetActive(true);
                timeShield = 7;
                break;
            case 3:
                if (damage < 3)
                {
                    damage++;
                    timeDamage = 5;
                }
                break;
        }
    } 
    public void receiveDamage()
    {
        if (!inmunity && !shield)
        {
            life--;
            voice.Play();
            if (life >= 1)
            {
                HudCon.GetComponent<HUD>().UpdateLife(life);
                StartCoroutine(Inmunity());
            }
            else if (life<= 0)
            {
                HudCon.GetComponent<HUD>().UpdateLife(life);
                gm.GetComponent<GameManager>().Win(player);
            }
        }
        if (shield)
        {
            shield = false;
            shieldObj.SetActive(false);
        }
    }
    IEnumerator Inmunity()
    {
        inmunity = true;
        sprit.color = new Color(1f, 1f, 1f, 0.4f);
        yield return new WaitForSeconds(3);
        sprit.color = new Color(1f, 1f, 1f, 1f);
        inmunity = false;
    }
}
