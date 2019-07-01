using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpScript : MonoBehaviour
{
    public int type;
    public float speed = 2;
    Vector3 destination;
    [SerializeField] private AudioSource voice;

    // Start is called before the first frame update
    void Start()
    {
        voice = this.gameObject.GetComponent<AudioSource>();
        destination = new Vector3(transform.localPosition.x, -10f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        float step = speed * Time.deltaTime;
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, destination, step);
        if (transform.localPosition.y < -7f)
        {
            Destroy(this.gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().takePowerUp(type);
            this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            this.gameObject.GetComponent<CircleCollider2D>().enabled = false;
            voice.Play();
            Destroy(this.gameObject, 0.5f);
        }
        else if (collision.transform.tag == "AI")
        {
            collision.gameObject.GetComponent<MagicAgent>().takePowerUp(type);
            this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            this.gameObject.GetComponent<CircleCollider2D>().enabled = false;
            voice.Play();
            Destroy(this.gameObject, 0.5f);
        }
    }
}
