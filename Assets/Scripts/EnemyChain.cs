using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChain : MonoBehaviour
{
    public Enemy[] enemies;
    public EnemyType type;

    public GameObject enemyobject;
    void Start()
    {
        
    }

    public bool spawning;

    void Update()
    {
        if(transform.childCount <= 0&&!spawning)
        {
            spawning = true;
            Invoke("Spawn", 1.5f);
        }
    }

    void Spawn()
    {
        spawning = false;
        for(int i=0;i<Random.Range(2, 6); i++)
        {
            Instantiate(enemyobject, transform.position+new Vector3(0,0,i), Quaternion.identity).transform.parent=transform;
        }
    }
}
