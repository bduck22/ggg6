using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public CameraController CameraController;
    void Start()
    {
        Instance = this;

    }

    public List<rank_data> list;

    void Update()
    {
        
    }
}

[System.Serializable]
public class rank_data
{
    public float Score;
}
