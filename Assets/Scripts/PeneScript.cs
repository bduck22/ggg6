using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeneScript : MonoBehaviour
{
    public Attack me;
    void Start()
    {
        me = GetComponent<Attack>();
    }

    
    void Update()
    {
        
    }
    public void Hit()
    {
        me.Damage = me.SubValue;
    }
}
