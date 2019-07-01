using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenaltyScript : MonoBehaviour
{
    public int health, p;
    SpawnerScript  Spawner;
    float speed;
    GameObject Player;
    Vector3 direction;
    bool isAttack;
    // Start is called before the first frame update
    void Start()
    {
        health = 5;
    }
    void Update()
    {
        if (transform.localPosition.y < -7)
        {
            Destroy(this.gameObject);
        }
        if (isAttack) {
            transform.Translate(direction * Time.deltaTime * speed);
         }
    
        if (Vector3.Distance(transform.localPosition, Player.transform.localPosition)> 12)
        {
            Destroy(this.gameObject);
        }
    }
    public void SetStats(int pl, float s, SpawnerScript spawn,GameObject playObj)
    {
        p = pl;
        Spawner = spawn;
        speed = s;
        Player = playObj;
        direction = (Player.transform.position - this.transform.position).normalized;
        isAttack = true;
    }
    public void receiveDamage(int d)
    {
        health -= d;
        if (health <= 0)
        {
            if (p == 1)
            {
                Spawner.returnPenalties(2, speed + 0.2f);
            }
            else if (p == 2)
            {
                Spawner.returnPenalties(1, speed + 0.2f);
            }
            Destroy(this.gameObject);
        }
    }
    public void receiveDamage(int d, MagicAgent IA)
    {
        health -= d;
        if (health <= 0)

        {
            if (p == 1)
            {
                Spawner.returnPenalties(2, speed + 0.2f);
            }
            else if (p == 2)
            {
                Spawner.returnPenalties(1, speed + 0.2f);
            }
            IA.EnemyDamage(0.3f);
            Destroy(this.gameObject);
        }
        else
        {
            IA.EnemyDamage(0.05f);
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().receiveDamage();
        }
        else if (collision.transform.tag == "AI")
        {
            collision.gameObject.GetComponent<MagicAgent>().receiveDamage();
        }
    }
}
