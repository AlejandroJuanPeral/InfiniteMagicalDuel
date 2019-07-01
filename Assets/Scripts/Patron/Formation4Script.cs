using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Formation4Script : MonoBehaviour
{
    float timespawn;
    public Transform[] spawners, enemies;
    public GameObject Enemy;
    public GameObject Player;
    public int maxSpawns;
    public int level;
    public GameObject gm;
    bool finish;
    // Start is called before the first frame update
    void Start()
    {
        spawners = gameObject.GetComponentsInChildren<Transform>();
        maxSpawns = level * 5;
        timespawn = 2 / level;
        this.tag = transform.parent.tag;
        finish = false;
        StartCoroutine(spawn());
    }

    // Update is called once per frame
    void Update()
    {
        if (finish)
        {
            enemies = gameObject.GetComponentsInChildren<Transform>();
            if (enemies.Length < 30)
            {
                Destroy(this.gameObject);
            }
        }
    }

    IEnumerator spawn()
    {
        for ( int i = 0; i < maxSpawns; i++)
        { 
            Transform spawner = spawners[Random.Range(1, spawners.Length - 1)];
            GameObject newEnemy = Instantiate(Enemy, transform.position, transform.rotation);
            newEnemy.transform.SetParent(this.gameObject.transform);
            newEnemy.transform.localPosition = spawner.localPosition;
            newEnemy.GetComponent<EnemyController>().Player = Player;
            newEnemy.GetComponent<EnemyController>().gm = gm;
            newEnemy.GetComponent<EnemyController>().Attack();
            yield return new WaitForSeconds(timespawn);
        }
        finish = true;
    }  
}
