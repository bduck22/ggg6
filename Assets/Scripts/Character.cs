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

public enum SkillType
{
    Q,
    E,
    R,
    F
}

[System.Serializable]
public class Skill
{
    public SkillType Type;
    public int Level
    {
        get
        {
            return level;
        }
        set
        {
            if(value > 5)
            {
                level = 5;
            }
            else
            {
                level = value;
            }
        }
    }
    [SerializeField] private int level;

    public float time;

    public Buff buff;

    public GameObject effect;

    public float[] Damage = new float[5];

    public float[] SubValue = new float[5];

    public float[] cool = new float[5];

    public float[] Mp = new float[5];

    public bool Levelup()
    {
        if(Level >= 5)
        {
            return false;
        }
        else
        {
            Level++;
            return true;
        }
    }

    public bool IsUsing(float Mp)
    {
        if(cool[Level-1] <= time && level > 0 && Mp >= this.Mp[Level-1])
        {
            return true;
        }
        else
        {
            return false;
        }
    }
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
                skillPoint++;
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
                if(value < hp)
                {
                    StopCoroutine(NoFight());
                    StartCoroutine(NoFight());
                }
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
    public int skillPoint;

    public List<int> Inventory;


    public void Init()
    {
        Status = CharacterStatus.None_Fight;
        Hp = MaxHp;
        Mp = MaxMp;
        Exp = 0;
    }

    [SerializeField] float attacktime;
    [SerializeField] bool isattack;

    public Transform Target;

    public CharacterStatus Status;

    public CharacterType Type;

    public Skill[] skills;

    void Start()
    {
        Status = CharacterStatus.None_Fight;
        animator = GetComponentInChildren<Animator>();
        rigidbody = GetComponent<Rigidbody>();

        InvokeRepeating("MpHeal", 1, 1);
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
            isattack = false;
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

            if (Input.GetKeyDown(KeyCode.Q))
            {
                UseSkill(0);
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                UseSkill(1);
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                UseSkill(2);
            }
            if (Input.GetKeyDown(KeyCode.F))
            {
                UseSkill(3);
            }
        }
        foreach(Skill s in skills)
        {
            if(s.Level!=0&&s.time < s.cool[s.Level-1])
            {
                s.time += Time.deltaTime;
            }
        }

        if (healtime >= 1)
        {
            healtime = 0;
            Hp += MaxHp * 0.05f;
        }
        else
        {
            healtime += Time.deltaTime;
        }
    }
    public float healtime;
    public IEnumerator NoFight()
    {
        Status = CharacterStatus.Fight;
        yield return new WaitForSeconds(5);
        Status = CharacterStatus.None_Fight;
    }

    void MpHeal()
    {
        Mp += MaxMp * 0.01f;
    }
    public bool UseSkill(int n)
    {
        Skill skill = skills[n];
        if (skill.IsUsing(Mp))
        {
            StopCoroutine(NoFight());
            StartCoroutine(NoFight());
            Mp -= skill.Mp[skill.Level-1];
            animator.SetTrigger("Skill");

            skill.time = 0;

            Attack attack = Instantiate(skill.effect, transform.position, transform.rotation).GetComponent<Attack>();
            attack.character = this;
            switch (Type)
            {
                case CharacterType.Knight:
                    switch (skills[n].Type)
                    {
                        case SkillType.Q:
                            attack.HitCount = 1;
                            break;
                        case SkillType.E:
                            attack.HitCount = (int)skill.SubValue[skill.Level-1];
                            break;
                        case SkillType.R:
                            attack.HitCount = 100;
                            attack.SubValue = skill.SubValue[skill.Level - 1]*Damage;
                            break;
                        case SkillType.F:
                            attack.HitCount = (int)skills[n].SubValue[skill.Level - 1];
                            Buff B = new Stun();
                            B.Level = skill.Level;
                            attack.Effect = B;
                            break;
                    }
                    attack.Damage = skill.Damage[skill.Level-1] * Damage;
                    break;
                case CharacterType.Archer:
                    break;
                case CharacterType.Mage:
                    break;
            }
            return true;
        }
        else return false;
    }

    private void FixedUpdate()
    {
        if (!played)
        {
            if (Target)
            {
                if (Vector3.Distance(transform.position, Target.position) <= Range)
                {
                    animator.SetBool("Walk", false);
                    Lockon = true;
                    transform.LookAt(Target);
                    if (isattack)
                    {
                        attacktime = 0;
                        bool skill = false;
                        for (int i = 3; i >= 0; i--)
                        {
                            if (!skill)
                            {
                                skill = UseSkill(i);
                            }
                            else
                            {
                                break;
                            }
                        }

                        if (!skill)
                        {
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
                }
                else
                {
                    Lockon = false;
                    Walk(Target.position);
                }
            }
            else
            {
                Lockon = false;
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
        StopCoroutine(NoFight());
        StartCoroutine(NoFight());
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
