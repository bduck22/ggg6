using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float Hp
    {
        get
        {
            return hp;
        }
        set
        {
            if (value <= 0)
            {
                Destroy(gameObject);
            }
            else if (value > MaxHp)
            {
                hp = MaxHp;
            }
            else
            {
                hp = value;
            }
        }
    }
    [SerializeField] private float hp;
    public float MaxHp;
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
