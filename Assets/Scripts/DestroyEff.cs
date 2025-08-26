using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEff : MonoBehaviour
{
    void Update()
    {
        if (!GetComponent<Collider>().enabled)
        {
            Destroy(gameObject);
        }
    }
}
