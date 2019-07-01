using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotScript : MonoBehaviour
{
    Rigidbody2D rb2d;
    [SerializeField] private float speed = 10f;
    public int damage = 1;
    public MagicAgent IA;
    private void Start()
    {
        rb2d = this.gameObject.GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        rb2d.velocity = new Vector2(0f, speed);
        if (transform.localPosition.y > 6)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Enemy")
        {
            if(IA != null)
            {
                collision.gameObject.GetComponent<EnemyController>().receiveDamage(damage, IA);
            }
            else
            {
                collision.gameObject.GetComponent<EnemyController>().receiveDamage(damage);
            }
            if (this.tag == "Shot")
            {
                Destroy(this.gameObject);
            }
        }
        else if (collision.transform.tag == "Penalty")
        {
            if (IA != null)
            {
                collision.gameObject.GetComponent<PenaltyScript>().receiveDamage(damage, IA);
            }
            else
            {
                collision.gameObject.GetComponent<PenaltyScript>().receiveDamage(damage);
            }
            if (this.tag == "Shot")
            {
                Destroy(this.gameObject);
            }
        }
    }   
}
