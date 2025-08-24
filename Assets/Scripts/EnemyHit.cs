using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHit : MonoBehaviour
{
    Enemy me;
    void Start()
    {
        me = transform.GetComponent<Enemy>();
    }

    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Attack"))
        {
            if (other.GetComponent<Attack>())
            {
                Attack a = other.GetComponent<Attack>();
                if (a.HitCount > 0)
                {
                    a.HitCount--;
                    me.Hp -= a.Damage;
                }
            }
        }
    }
}
