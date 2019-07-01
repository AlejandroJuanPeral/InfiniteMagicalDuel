using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MagicAgent : Agent
{
    public int player;
    public float Speed = 1f;
    float shootTime = 2f;
    float timer;
    GameObject canon;
    List<GameObject> shots;
    //Rigidbody2D rb2d;
    //[SerializeField] private float maxSpeed = 6f;
    [SerializeField] private GameObject shot, shieldObj;
    [SerializeField] private GameObject megaShot;
    [SerializeField] private GameObject HudCon;
    [SerializeField] private GameObject gm;
    [SerializeField] private SpriteRenderer sprit;
    [SerializeField] private GameObject enemies;
    [SerializeField] private AudioSource voice;

    public int life, damage;
    private bool inmunity = false, shield = false;
    int lastCombo;
    public float  timeShield, timeDamage;
    void Start()
    {
        life = 5;
        //rb2d = this.gameObject.GetComponent<Rigidbody2D>();
        canon = this.transform.Find("Canon").gameObject;
        voice = this.gameObject.GetComponent<AudioSource>();
        shots = new List<GameObject>();
        if (player == 1)
        {
            lastCombo = gm.GetComponent<GameManager>().comboPlayer1;
        }
        else
        {
            lastCombo = gm.GetComponent<GameManager>().comboPlayer2;
        }
        damage = 1;
    }

    public override void AgentReset()
    {
        life = 5;
        this.transform.localPosition = new Vector3(-4.5f, 0f, 0f);
        HudCon.GetComponent<HUD>().UpdateLife(life);
        for (int i = shots.Count - 1; i >= 0; i--)
        {
            if (shots[i] == null)
            {
                shots.RemoveAt(i);
            }
            else
            {
                Destroy(shots[i]);
                shots.RemoveAt(i);

            }
        }
        shots = new List<GameObject>();
        if (player == 1)
        {
            lastCombo = gm.GetComponent<GameManager>().comboPlayer1;
        }
        else
        {
            lastCombo = gm.GetComponent<GameManager>().comboPlayer2;
        }
        damage = 1;
    }

    public override void CollectObservations()
     {
        AddVectorObs(transform.position.x);
        AddVectorObs(transform.position.y);
        if (inmunity)
        {
            AddVectorObs(1);
        }
        else{
            AddVectorObs(0);
        }
        AddVectorObs(life);
        AddVectorObs(damage);
        AddVectorObs(lastCombo);

       /* for (int i = 19; i >= 0; i--)
        {
            if(i < shots.Count)
            {
                if (shots[i] == null)
                {
                    shots.RemoveAt(i);
                    AddVectorObs(0);
                    AddVectorObs(-120);
                }
                else
                {
                    AddVectorObs(shots[i].transform.position.x);
                    AddVectorObs(shots[i].transform.position.y);
                }
            }
            else
            {
                AddVectorObs(0);
                AddVectorObs(-120);
            }
        }
        */
        Transform[] ts = enemies.GetComponentsInChildren<Transform>();
        List<Transform> ene = new List<Transform>();
        List<Transform> pen = new List<Transform>();
        bool powered = false;
        foreach (Transform t in ts)
        {
            if (t.tag == "Enemy")
            {
                ene.Add(t);
            }
            else if (t.tag == "Penalty")
            {
                pen.Add(t);
            }
            else if (t.tag == "PoweUp" && !powered)
            {
                AddVectorObs(t.position.x);
                AddVectorObs(t.position.y);
                powered = true;
            }
        }
        if (!powered)
        {
            AddVectorObs(0);
            AddVectorObs(-20);
        }
        for (int i = 0; i < 25; i++)
        {
            if(ene.Count > i)
            {
                AddVectorObs(ene[i].position.x);
                AddVectorObs(ene[i].position.y);
                AddVectorObs(ene[i].gameObject.GetComponent<EnemyController>().health);
            }
            else
            {
                AddVectorObs(0);
                AddVectorObs(-20);
                AddVectorObs(0);
            }
        }
        for (int i = 0; i < 4; i++)
        {
            if (pen.Count > i)
            {
                AddVectorObs(pen[i].position.x);
                AddVectorObs(pen[i].position.y);
                AddVectorObs(pen[i].gameObject.GetComponent<PenaltyScript>().health);
            }
            else
            {
                AddVectorObs(0);
                AddVectorObs(-20);
                AddVectorObs(0);
            }
        }
        /*for (int i = 0; i < 25; i++)
        {
            if(ts.Length > i && ts[i] != null)
            {
                AddVectorObs(ts[i].position.x);
                AddVectorObs(ts[i].position.y);
                if(ts[i].tag == "Enemy")
                {
                    AddVectorObs(ts[i].gameObject.GetComponent<EnemyController>().health);
                }
                else if(ts[i].tag == "Penalty")
                {
                    AddVectorObs(ts[i].gameObject.GetComponent<PenaltyScript>().health);
                }
                else if(ts[i].tag == "PoweUp")
                {
                    AddVectorObs(-1);
                } else
                {
                    AddVectorObs(0);
                }
            }
            else
            {
                AddVectorObs(0);
                AddVectorObs(-20);
                AddVectorObs(0);
            }
        }*/
    }
    private void Update()
    {
        if (life <= 0)
        {
            Lose();
        }
        if (damage > 1)
        {
            timeDamage -= Time.deltaTime;
            if (timeDamage <= 0)
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
                inmunity = false;
                shieldObj.SetActive(false);
            }
        }
    }
    public override void AgentAction(float[] vectorAction, string textAction)
    {

        if (brain.brainParameters.vectorActionSpaceType == SpaceType.continuous)
        {
            float newX = transform.localPosition.x + (vectorAction[1] * Speed * Time.deltaTime);
            newX = Mathf.Clamp(newX, -7.5f, -0.9f);
            float newY = transform.localPosition.y + (vectorAction[0] * Speed * Time.deltaTime);
            newY = Mathf.Clamp(newY, -4.8f, 4.8f);

            transform.localPosition = new Vector3(newX, newY, 0);
            float distance = Vector3.Distance(transform.localPosition, new Vector3(-4.5f, 0f, 0f));
            if (distance > 4.5f)
            {
                AddReward(-0.002f * distance);
            }
            else
            {
                AddReward(0.001f);
            }
            bool near = false;
            Transform[] ts = enemies.GetComponentsInChildren<Transform>();
            foreach (Transform t in ts)
            {
                if (t != null && (t.tag == "Enemy" || t.tag == "Penalty"))
                {
                    if (Vector3.Distance(transform.position, t.position) < 1.5f)
                    {
                        near = true;
                        break;
                    }
                }
            }
            if (near)
            {
                AddReward(-0.1f);
            }
            else
            {
                AddReward(0.0005f);
            }
            timer += 0.01f;
            shootTime += 0.01f;
            if (shootTime > 3f && vectorAction[2] > 0)
            {
                GameObject newshot = Instantiate(megaShot, canon.transform.position, Quaternion.identity);
                newshot.GetComponent<ShotScript>().IA = this;
                newshot.GetComponent<ShotScript>().damage = damage;
                shots.Add(newshot);
                shootTime = 0;
            }
            else if (vectorAction[2] > 0 && shootTime > 0.06f)
            {
                GameObject newshot = Instantiate(shot, canon.transform.position, Quaternion.identity);
                newshot.GetComponent<ShotScript>().IA = this;
                newshot.GetComponent<ShotScript>().damage = damage;
                shots.Add(newshot);
                shootTime = 0;
            }

        }
        AddReward(0.01f);
        ComboReward();
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
                inmunity = true;
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
        AddReward(0.5f);
    }
    public void EnemyDamage(float i)
    {
        AddReward(i);
    }
    void ComboReward()
    {
        if (player == 1)
        {
            if (lastCombo > gm.GetComponent<GameManager>().comboPlayer1)
            {
                AddReward(0.05f);
            }
            else if (lastCombo < gm.GetComponent<GameManager>().comboPlayer1)
            {
                AddReward(-0.1f);
            }
            lastCombo = gm.GetComponent<GameManager>().comboPlayer1;
        }
        else
        {
            if (lastCombo > gm.GetComponent<GameManager>().comboPlayer2)
            {
                AddReward(0.05f);
            }
            else if (lastCombo < gm.GetComponent<GameManager>().comboPlayer2)
            {
                AddReward(-0.1f);
            }
            lastCombo = gm.GetComponent<GameManager>().comboPlayer2;
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
                life--;
                HudCon.GetComponent<HUD>().UpdateLife(life);
                StartCoroutine(Inmunity());
                AddReward(-1f);
            }
        }
        if (shield)
        {
            AddReward(-0.4f);
            shield = false;
            inmunity = false;
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
    public void Lose()
    {
        gm.GetComponent<GameManager>().Win(player);
        AddReward(-1f);
        Done();
    }
    public void Win()
    {
        AddReward(1f);
    }
}

