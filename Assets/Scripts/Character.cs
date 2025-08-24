using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterStatus
{
    None_Fight,
    Fight,
}

public enum CharacterType
{
    Knight,
    Archer,
    Mage
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

    public float Mp
    {
        get
        {
            return mp;
        }
        set
        {
            if (value <= 0)
            { 
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

    public Transform Target;

    public CharacterType Type;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        rigidbody = GetComponent<Rigidbody>();
    }
    void Update()
    {

        if (attacktime >= 1)
        {
            if (!isattack)
            {
                isattack = true;
            }
        }
        else
        {
            attacktime += Time.deltaTime * AttackRate;
        }
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

            if (Input.GetMouseButton(0)&&isattack)
            {
                attacktime = 0;
                switch (Type)
                {
                    case CharacterType.Knight: StartCoroutine(Attack(0,0.5f));
                        break;
                    case CharacterType.Archer: StartCoroutine(Attack(0.4f,1.5f));
                        break;
                    case CharacterType.Mage: StartCoroutine(Attack(0.2f,1f));
                        break;
                }
            }
        }
        else
        {
            if (Target)
            {
                if(Vector3.Distance(transform.position, Target.position) <= Range)
                {
                    animator.SetBool("Walk", false);
                    Lockon = true;
                    transform.LookAt(Target);
                    if (isattack)
                    {
                        attacktime = 0;
                        switch (Type)
                        {
                            case CharacterType.Knight:
                                StartCoroutine(Attack(0, 0.5f));
                                break;
                            case CharacterType.Archer:
                                StartCoroutine(Attack(0.4f, 1.5f));
                                break;
                            case CharacterType.Mage:
                                StartCoroutine(Attack(0.2f, 1f));
                                break;
                        }
                    }
                }
                else
                {
                    Lockon = false;
                    Walk(Target.position);
                }
            }
            else
            {
                Lockon= false;
            }
        }
    }
    public bool Lockon;

    public void Walk(Vector3 Target)
    {
        if (!Lockon)
        {
            transform.LookAt(Target);
            animator.SetBool("Walk", true);
            if (Vector3.Distance(transform.position, Target) > 2)
            {
                transform.position = Vector3.MoveTowards(transform.position, Target, Speed * Time.deltaTime);
            }
            else
            {
                animator.SetBool("Walk", false);
            }
        }
    }

    public GameObject AttackPrefab;

    IEnumerator Attack(float time, float destroytime)
    {
        animator.SetTrigger("Attack");
        isattack = false;
        yield return new WaitForSeconds(time);
        Quaternion q = transform.rotation;
        Vector3 p = transform.position;
        GameObject attackob = Instantiate(AttackPrefab, p , q);
        Attack a = attackob.GetComponent<Attack>();
        a.Damage = Damage;
        switch (Type)
        {
            case CharacterType.Knight:
                a.HitCount = 100;
                break;
            case CharacterType.Archer:
                a.HitCount = 1;
                break;
            case CharacterType.Mage:
                a.HitCount = 2;
                break;
        }
        a.character = this;
        Destroy(attackob, destroytime);
    }
}
