using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHit : MonoBehaviour
{
    Character me;
    void Start()
    {
        me = transform.GetComponent<Character>();
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }
}
