using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHit : MonoBehaviour
{
    Character me;
    void Start()
    {
        me = transform.parent.GetComponent<Character>();
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }
}
