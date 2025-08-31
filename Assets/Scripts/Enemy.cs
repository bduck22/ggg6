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

    public float Speed;

    public float Damage;

    public bool stun;

    public Character Target;
    void Start()
    {
        InvokeRepeating("LoadTarget", 0.25f, 0.25f);
    }

    public float time;

    public bool isAttack;

    public float Range;

    void Update()
    {
        if (!stun) 
        {
            if (!isAttack&&time >= 1)
            {
                isAttack = true;
            }
            else if(time < 1)
            {
                time += Time.deltaTime;
            }
            if (Target)
            {
                transform.LookAt(Target.transform);
                if(Vector3.Distance(transform.position, Target.transform.position) <= Range)
                {
                    if (isAttack)
                    {
                        isAttack = false;
                        time = 0;
                        Attack();
                    }
                }
                else transform.position = Vector3.MoveTowards(transform.position, Target.transform.position, Speed*Time.deltaTime);   
            }
        }
    }
    void LoadTarget()
    {
        foreach(Character c in GameManager.Instance.CameraController.characters)
        {
            if (Target)
            {
                if (Vector3.Distance(transform.position, c.transform.position) < Vector3.Distance(transform.position, Target.transform.position))
                {
                    Target = c;
                }
            }
            else Target = c;
        }
    }

    public void Attack()
    {

    }
}
