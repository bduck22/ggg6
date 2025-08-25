using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public enum BuffType
{
    Poison,
    Stun
}

public abstract class Buff
{
    public BuffType Type;
    public float Level;
    public Enemy Target;
    public abstract void Set(Enemy T);
    public abstract IEnumerator effect();
}

public class Poison : Buff
{
    float speeddown;
    float damage;
    public override void Set(Enemy T)
    {
        Type = BuffType.Poison;
        Target = T;
        speeddown=0;
        damage=0;
        switch (Level)
        {
            case 1:
                speeddown = 0.1f;
                damage = 10;
                break;
            case 2:
                speeddown = 0.1f;
                damage = 20;
                break;
            case 3:
                speeddown = 0.2f;
                damage = 20;
                break;
            case 4:
                speeddown = 0.5f;
                damage = 30;
                break;
            case 5:
                speeddown = 0.5f;
                damage = 50;
                break;
        }

    }
    public override IEnumerator effect()
    {
        Target.Speed -= speeddown;
        for (int i = 0; i < (Level<2?2:Level); i++)
        {
            Target.Hp -= damage;
            yield return new WaitForSeconds(1);
        }
        Target.Speed += speeddown;
    }
}

public class Stun : Buff
{
    float Per;
    public override void Set(Enemy T)
    {
        Target = T;
        Type = BuffType.Stun;
        Per = 0;
        switch (Level)
        {
            case 1:
            case 2:
                Per = 0.5f;
                break;
            case 3:
            case 4:
            case 5:
                Per = 1;
                break;
        }
    }

    public override IEnumerator effect()
    {
        if(Random.Range(0f, 1f)<Per)
        {
            Target.Hp += 100;
            yield return new WaitForSeconds(2);
        }
    }
}

public class Attack : MonoBehaviour
{
    public Character character;

    public float Damage;

    public float SubValue;

    public Buff Effect;

    public int HitCount;
    private void Update()
    {
        if (HitCount <= 0)
        {
            if (GetComponent<Collider>().enabled)
            {
                GetComponent<Collider>().enabled = false;
            }
        }
    }
}
