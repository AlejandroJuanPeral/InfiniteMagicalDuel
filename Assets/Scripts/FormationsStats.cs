using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormationsStats : MonoBehaviour
{
    public GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        Transform[] ts = gameObject.GetComponentsInChildren<Transform>();
        foreach (Transform t in ts)
        {
            if (t != null && t.tag == "Enemy")
            {
                t.GetComponent<EnemyController>().Player = Player;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
