using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public enum BuffType
{
    Poison,
    Stun
}

public abstract class Buff : MonoBehaviour
{
    public BuffType Type;
    public float Level;
    public Enemy Target;
    public abstract void Set();
}

public class Poison : Buff
{
    public override void Set()
    {
        Type = BuffType.Poison;
        float speeddown=0;
        float damage=0;
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

        StartCoroutine(effect(speeddown, damage));
    }
    IEnumerator effect(float speeddown, float Damage)
    {
        yield return new WaitForSeconds(Damage);
    }
}

public class Attack : MonoBehaviour
{
    public Character character;

    public float Damage;

    public Buff Effect;

    public int HitCount;
    private void Update()
    {
        if (HitCount <= 0)
        {
            Destroy(gameObject);
        }
    }

}
