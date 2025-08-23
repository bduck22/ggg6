using System.Collections.Generic;
using UnityEngine;

public enum CharacterStatus
{
    None_Fight,
    Fight,
}

public class Character : MonoBehaviour
{
    public bool played;

    Rigidbody rigidbody;

    Animator animator;

    public float Speed;
    public float Level;
    public float LevelGoal;
    public float Exp
    {
        get
        {
            return exp;
        }
        set
        {
            if (value >= LevelGoal)
            {
                Level++;
                exp = value - LevelGoal;
            }
            else
            {
                exp = value;
            }
        }
    }
    [SerializeField] private float exp;

    public float Hp
    {
        get
        {
            return hp;
        }
        set
        {
            if (value < 0)
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

    public float Mp
    {
        get
        {
            return mp;
        }
        set
        {
            if (value < 0)
            {
                Destroy(gameObject);
            }
            else if (value > MaxMp)
            {
                mp = MaxMp;
            }
            else
            {
                mp = value;
            }
        }
    }
    [SerializeField] private float mp;
    public float MaxMp;

    public float Damage;
    public float AttackRate;
    public float Citical;
    public float Range;

    public List<int> Inventory;


    public void Init()
    {
        Hp = MaxHp;
        Mp = MaxMp;
        Exp = 0;
    }

    [SerializeField] float attacktime;
    [SerializeField] bool isattack;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        rigidbody = GetComponent<Rigidbody>();
    }
    void Update()
    {
        if (played)
        {
            float x = Input.GetAxis("Vertical");
            float z = Input.GetAxis("Horizontal");
            if (x != 0 || z != 0)
            {
                animator.SetBool("Walk", true);
            }
            else if (x == 0 && z == 0)
            {
                animator.SetBool("Walk", false);
            }
            rigidbody.velocity = (transform.forward * x + transform.right * z) * Speed;
            if (Input.GetButtonUp("Vertical"))
            {
                rigidbody.velocity = new Vector3(0, rigidbody.velocity.y, rigidbody.velocity.z);
            }

            if (Input.GetButtonUp("Horizontal"))
            {
                rigidbody.velocity = new Vector3(0, rigidbody.velocity.y, rigidbody.velocity.z);
            }

            if(attacktime >= 1)
            {
                if(!isattack)
                {
                    isattack = true;
                }
            }
            else
            {
                attacktime += Time.deltaTime * AttackRate;
            }

            if (Input.GetMouseButton(0)&&isattack)
            {
                attacktime = 0;
                Attack();
            }
        }
    }
    public GameObject AttackPrefab;

    void Attack()
    {
        isattack = false;
        GameObject attackob = Instantiate(AttackPrefab, transform.position , transform.rotation);
        Attack a = attackob.GetComponent<Attack>();
        a.Damage = Damage;
        a.HitCount = 100;
        a.character = this;
        Destroy(attackob, 0.5f);
        animator.SetTrigger("Attack");
    }
}
