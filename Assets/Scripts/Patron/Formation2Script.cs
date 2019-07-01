using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Formation2Script : MonoBehaviour
{
    int speed;
    int direction;
    bool moving = false;
    Vector3 destination;
    Transform[] enemies;
    public GameObject Player;
    public int level;
    public GameObject gm;
    // Start is called before the first frame update
    void Start()
    {
        enemies = gameObject.GetComponentsInChildren<Transform>();
        foreach (Transform t in enemies)
        {
            if (t != null && t.tag == "Enemy")
            {
                t.GetComponent<EnemyController>().Player = Player;
                t.GetComponent<EnemyController>().gm = gm;
            }
        }
        this.tag = transform.parent.tag;
    }

    // Update is called once per frame
    void Update()
    {
        if (moving)
        {
            float step = speed * Time.deltaTime;
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, destination, step);
            if(Vector3.Distance(transform.localPosition, destination) < 0.001f)
            {
                StartCoroutine(Attack());
                moving = false;
            }
        }
        enemies = gameObject.GetComponentsInChildren<Transform>();
        if(enemies.Length < 2)
        {
            Destroy(this.gameObject);
        }
    }
    public void SetStats(int s, int dir)
    {
        speed = s;
        direction = dir;
        if(dir == 1)
        {
            destination = new Vector3(-7f, 0f, 0f);
        }
        else
        {
            destination = new Vector3(-1.5f, 0f, 0f);
        }
        moving = true;
    }
    IEnumerator Attack()
    {
        foreach(Transform e in enemies)
        {
            if(e != null && e.tag == "Enemy")
            {
                e.GetComponent<EnemyController>().Attack();
                yield return new WaitForSeconds(1 / level);

            }
        }

    }
}
