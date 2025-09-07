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
                    float hp = me.Hp;
                    me.Hp -= a.Damage;
                    a.character.Exp += me.EXP * ((hp - me.Hp / me.MaxHp));
                }
                if (a.Effect!=null)
                {
                    a.Effect.Set(me);
                    StartCoroutine(a.Effect.effect());
                }
                if (other.GetComponent<PeneScript>())
                {
                    if (a.HitCount == 99)
                    {
                        other.GetComponent<PeneScript>().Hit();
                    }
                }
            }
        }
    }
}
