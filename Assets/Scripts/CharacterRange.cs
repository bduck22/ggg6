using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRange : MonoBehaviour
{
    Character me;
    void Start()
    {
        me = transform.parent.GetComponent<Character>();
    }

    void Update()
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if(!me.Target||(me.Target&&Vector3.Distance(transform.parent.position, me.Target.position)>Vector3.Distance(transform.parent.position, other.transform.position)))
            {
                me.Target = other.transform;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            me.Target = null;
        }
    }
}
