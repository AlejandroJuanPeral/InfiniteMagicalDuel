using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    [SerializeField] GameObject[] patrons, powerUps;
    [SerializeField] GameObject[] spawnPenaltyCoords1, spawnPenaltyCoords2;
    [SerializeField] GameObject penalty, gm;
    [SerializeField] GameObject player1Enemies, player2Enemies, player1, player2;
    float spawntime, spawnTimePower;
    List<GameObject> patronsSpawned1 = new List<GameObject>();
    List<GameObject> penaltiesSpawned1 = new List<GameObject>();
    List<GameObject> patronsSpawned2 = new List<GameObject>();
    List<GameObject> penaltiesSpawned2 = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        spawntime = 2;
        spawnTimePower = Random.Range(5, 20);
    }

    // Update is called once per frame
    void Update()
    {
        spawntime -= Time.deltaTime;
        spawnTimePower -= Time.deltaTime;

        if (spawntime <= 0)
        {
            spawntime = 12 / gm.GetComponent<GameManager>().Level;
            spawnFormations();
        }
        if (spawnTimePower <= 0)
        {
            spawnTimePower = Random.Range(5, 20);
            spawnPower();
        }
    }

    void spawnFormations()
    {
        int r = Random.Range(1, 5);
        GameObject newPatron;
        Vector3 position;
        switch (r)
        {
            case 0:
                newPatron = Instantiate(patrons[0], transform.position, transform.rotation);
                patronsSpawned2.Add(newPatron);
                break;
            case 1:
                position = new Vector3(-9f, 0f, 0f);
                newPatron = Instantiate(patrons[1], transform.position, transform.rotation);
                newPatron.transform.SetParent(player2Enemies.transform);
                newPatron.GetComponent<Formation2Script>().Player = player2;
                newPatron.GetComponent<Formation2Script>().gm = gm;
                newPatron.transform.localPosition = position;
                newPatron.GetComponent<Formation2Script>().level = gm.GetComponent<GameManager>().Level;
                newPatron.GetComponent<Formation2Script>().SetStats(3 + (gm.GetComponent<GameManager>().Level - 1 * 2), 1);
                patronsSpawned2.Add(newPatron);
                break;
            case 2:
                position = new Vector3(0.5f, 0f, 0f);
                newPatron = Instantiate(patrons[1], transform.position, transform.rotation);
                newPatron.transform.SetParent(player2Enemies.transform);
                newPatron.GetComponent<Formation2Script>().Player = player2;
                newPatron.GetComponent<Formation2Script>().gm = gm;
                newPatron.transform.localPosition = position;
                newPatron.GetComponent<Formation2Script>().level = gm.GetComponent<GameManager>().Level;
                newPatron.GetComponent<Formation2Script>().SetStats(3 + (gm.GetComponent<GameManager>().Level - 1 * 2), -1);
                patronsSpawned2.Add(newPatron);
                break;
            case 3:
                position = new Vector3(-4.25f, 5.5f, 0f);
                newPatron = Instantiate(patrons[2], transform.position, transform.rotation);
                newPatron.transform.SetParent(player2Enemies.transform);
                newPatron.GetComponent<Formation3Script>().Player = player2;
                newPatron.GetComponent<Formation3Script>().gm = gm;
                newPatron.transform.localPosition = position;
                newPatron.GetComponent<Formation3Script>().level = gm.GetComponent<GameManager>().Level;
                newPatron.GetComponent<Formation3Script>().SetStats(3 + (gm.GetComponent<GameManager>().Level - 1 * 2), -1);
                patronsSpawned2.Add(newPatron);
                break;
            case 4:
                position = new Vector3(-4.25f, -5.5f, 0f);
                newPatron = Instantiate(patrons[2], transform.position, transform.rotation);
                newPatron.transform.SetParent(player2Enemies.transform);
                newPatron.GetComponent<Formation3Script>().Player = player2;
                newPatron.GetComponent<Formation3Script>().gm = gm;
                newPatron.transform.localPosition = position;
                newPatron.GetComponent<Formation3Script>().level = gm.GetComponent<GameManager>().Level;
                newPatron.GetComponent<Formation3Script>().SetStats(3 + (gm.GetComponent<GameManager>().Level - 1 * 2), 1);
                patronsSpawned2.Add(newPatron);
                break;
            case 5:
                position = new Vector3(-4.25f, 0f, 0f);
                newPatron = Instantiate(patrons[3], transform.position, transform.rotation);
                newPatron.transform.SetParent(player2Enemies.transform);
                newPatron.GetComponent<Formation4Script>().gm = gm;
                newPatron.GetComponent<Formation4Script>().Player = player2;
                newPatron.transform.localPosition = position;
                newPatron.GetComponent<Formation4Script>().level = gm.GetComponent<GameManager>().Level;
                patronsSpawned2.Add(newPatron);
                break;
        }

        switch (r)
        {
            case 0:
                newPatron = Instantiate(patrons[0], transform.position, transform.rotation);
                patronsSpawned1.Add(newPatron);
                break;
            case 1:
                position = new Vector3(-9f, 0f, 0f);
                newPatron = Instantiate(patrons[1], transform.position, transform.rotation);
                newPatron.transform.SetParent(player1Enemies.transform);
                newPatron.GetComponent<Formation2Script>().Player = player1;
                newPatron.GetComponent<Formation2Script>().gm = gm;
                newPatron.transform.localPosition = position;
                newPatron.GetComponent<Formation2Script>().level = gm.GetComponent<GameManager>().Level;
                newPatron.GetComponent<Formation2Script>().SetStats(3 + (gm.GetComponent<GameManager>().Level - 1 * 2), 1);
                patronsSpawned1.Add(newPatron);
                break;
            case 2:
                position = new Vector3(0.5f, 0f, 0f);
                newPatron = Instantiate(patrons[1], transform.position, transform.rotation);
                newPatron.transform.SetParent(player1Enemies.transform);
                newPatron.GetComponent<Formation2Script>().Player = player1;
                newPatron.GetComponent<Formation2Script>().gm = gm;
                newPatron.transform.localPosition = position;
                newPatron.GetComponent<Formation2Script>().level = gm.GetComponent<GameManager>().Level;
                newPatron.GetComponent<Formation2Script>().SetStats(3 + (gm.GetComponent<GameManager>().Level - 1 * 2), -1);
                patronsSpawned1.Add(newPatron);
                break;
            case 3:
                position = new Vector3(-4.25f, 5.5f, 0f);
                newPatron = Instantiate(patrons[2], transform.position, transform.rotation);
                newPatron.transform.SetParent(player1Enemies.transform);
                newPatron.GetComponent<Formation3Script>().gm = gm;
                newPatron.GetComponent<Formation3Script>().Player = player1;
                newPatron.transform.localPosition = position;
                newPatron.GetComponent<Formation3Script>().level = gm.GetComponent<GameManager>().Level;
                newPatron.GetComponent<Formation3Script>().SetStats(3 + (gm.GetComponent<GameManager>().Level - 1 * 2), -1);
                patronsSpawned1.Add(newPatron);
                break;
            case 4:
                position = new Vector3(-4.25f, -5.5f, 0f);
                newPatron = Instantiate(patrons[2], transform.position, transform.rotation);
                newPatron.transform.SetParent(player1Enemies.transform);
                newPatron.GetComponent<Formation3Script>().gm = gm;
                newPatron.GetComponent<Formation3Script>().Player = player1;
                newPatron.transform.localPosition = position;
                newPatron.GetComponent<Formation3Script>().level = gm.GetComponent<GameManager>().Level;
                newPatron.GetComponent<Formation3Script>().SetStats(3 + (gm.GetComponent<GameManager>().Level - 1 * 2), 1);
                patronsSpawned1.Add(newPatron);
                break;
            case 5:
                position = new Vector3(-4.25f, 0f, 0f);
                newPatron = Instantiate(patrons[3], transform.position, transform.rotation);
                newPatron.transform.SetParent(player1Enemies.transform);
                newPatron.GetComponent<Formation4Script>().gm = gm;
                newPatron.GetComponent<Formation4Script>().Player = player1;
                newPatron.transform.localPosition = position;
                newPatron.GetComponent<Formation4Script>().level = gm.GetComponent<GameManager>().Level;
                patronsSpawned1.Add(newPatron);
                break;
        }
    }

    public void spawnPower()
    {
        int r = Random.Range(1, 5);
        float x = Random.Range(-7f, -1f);
        Vector3 position = new Vector3(x, 9f, 0f); ;
        GameObject newPower;
        switch (r)
        {
            case 1:
                newPower = Instantiate(powerUps[0], transform.position, transform.rotation);
                newPower.transform.SetParent(player2Enemies.transform);
                newPower.transform.localPosition = position;
                break;
            case 2:
            case 3:
                newPower = Instantiate(powerUps[1], transform.position, transform.rotation);
                newPower.transform.SetParent(player2Enemies.transform);
                newPower.transform.localPosition = position;
                break;
            case 4:
            case 5:
                newPower = Instantiate(powerUps[2], transform.position, transform.rotation);
                newPower.transform.SetParent(player2Enemies.transform);
                newPower.transform.localPosition = position;
                break;         
        }

        switch (r)
        {
            case 1:
                newPower = Instantiate(powerUps[0], transform.position, transform.rotation);
                newPower.transform.SetParent(player1Enemies.transform);
                newPower.transform.localPosition = position;
                break;
            case 2:
            case 3:
                newPower = Instantiate(powerUps[1], transform.position, transform.rotation);
                newPower.transform.SetParent(player1Enemies.transform);
                newPower.transform.localPosition = position;
                break;
            case 4:
            case 5:
                newPower = Instantiate(powerUps[2], transform.position, transform.rotation);
                newPower.transform.SetParent(player1Enemies.transform);
                newPower.transform.localPosition = position;
                break;
        }

    }
    public void SpawnPenalties(int n, int p)
    {
        if (p == 1)
        {
            StartCoroutine(spawnPP1(n));
        }else if (p == 2)
        {
            StartCoroutine(spawnPP2(n));
        }
    }
    IEnumerator spawnPP1(int n)
    {
        for (int i = 0; i < n; i++)
        {
            GameObject newPenalty = Instantiate(penalty, transform.position, transform.rotation);
            newPenalty.transform.SetParent(player2Enemies.transform);
            GameObject spawner = spawnPenaltyCoords2[Random.Range(1, spawnPenaltyCoords2.Length - 1)];
            newPenalty.transform.localPosition = spawner.transform.localPosition;
            newPenalty.GetComponent<PenaltyScript>().SetStats(2, 4f, this, player2);
            penaltiesSpawned2.Add(newPenalty);
            yield return new WaitForSeconds(1);
        }
    }
    IEnumerator spawnPP2(int n)
    {
        for (int i = 0; i < n; i++)
        {
            GameObject newPenalty = Instantiate(penalty, transform.position, transform.rotation);
            newPenalty.transform.SetParent(player1Enemies.transform);
            GameObject spawner = spawnPenaltyCoords1[Random.Range(1, spawnPenaltyCoords2.Length - 1)];
            newPenalty.transform.localPosition = spawner.transform.localPosition;
            newPenalty.GetComponent<PenaltyScript>().SetStats(1 ,4f, this, player1);
            penaltiesSpawned1.Add(newPenalty);
            yield return new WaitForSeconds(1);
        }
    }
    public void returnPenalties(int p, float s)
    {
        if (p == 1)
        {
            GameObject newPenalty = Instantiate(penalty, transform.position, transform.rotation);
            newPenalty.transform.SetParent(player1Enemies.transform);
            GameObject spawner = spawnPenaltyCoords1[Random.Range(1, spawnPenaltyCoords2.Length - 1)];
            newPenalty.transform.localPosition = spawner.transform.localPosition;
            newPenalty.GetComponent<PenaltyScript>().SetStats(1, +0.3f, this, player1);
            penaltiesSpawned2.Add(newPenalty);
        }
        else if (p == 2)
        {
            GameObject newPenalty = Instantiate(penalty, transform.position, transform.rotation);
            newPenalty.transform.SetParent(player2Enemies.transform);
            GameObject spawner = spawnPenaltyCoords2[Random.Range(1, spawnPenaltyCoords2.Length - 1)];
            newPenalty.transform.localPosition = spawner.transform.localPosition;
            newPenalty.GetComponent<PenaltyScript>().SetStats(2, s + 0.3f, this, player2);
            penaltiesSpawned1.Add(newPenalty);
        }
    }
    public void restart(int p)
    {
        if (p == 1)
        {
            while (patronsSpawned1.Count > 0)
            {
                GameObject remPatron = patronsSpawned1[0];
                patronsSpawned1.Remove(remPatron);
                Destroy(remPatron);

            }
            while (penaltiesSpawned1.Count > 0)
            {
                if (penaltiesSpawned1[0] != null)
                {
                    GameObject remPenalty = penaltiesSpawned1[0];
                    penaltiesSpawned1.Remove(remPenalty);
                    Destroy(remPenalty);
                }
                else
                {
                    GameObject remPenalty = penaltiesSpawned1[0];
                    penaltiesSpawned1.Remove(remPenalty);
                }
            }
        }
        if (p == 2)
        {
            while (patronsSpawned2.Count > 0)
            {
                GameObject remPatron = patronsSpawned2[0];
                patronsSpawned2.Remove(remPatron);
                Destroy(remPatron);

            }
            while (penaltiesSpawned2.Count > 0)
            {
                if (penaltiesSpawned2[0] != null)
                {
                    GameObject remPenalty = penaltiesSpawned2[0];
                    penaltiesSpawned2.Remove(remPenalty);
                    Destroy(remPenalty);
                }
                else
                {
                    GameObject remPenalty = penaltiesSpawned2[0];
                    penaltiesSpawned2.Remove(remPenalty);
                }
            }
        }
        spawntime = 12 / gm.GetComponent<GameManager>().Level;

    }
}
