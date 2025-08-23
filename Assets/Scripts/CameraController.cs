using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Character character;

    public Character[] characters;

    public int Number
    {
        get
        {
            return num;
        }
        set
        {
            if(value > 2)
            {
                value = 0;
            }
            else if(value < 0)
            {
                value = 2;
            }
            num = value;
        }
    }
    [SerializeField] private int num=0;

    
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ChangeCharacter(true);
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            ChangeCharacter(false);
        }
        transform.localRotation = Quaternion.Euler(35, 0, 0);
        character.transform.Rotate(0, Input.GetAxis("Mouse X"), 0);
    }

    public void ChangeCharacter(bool N)
    {
        character.played = false;
        if (N)
        {
            Number++;
            character = characters[Number];
        }
        else
        {
            Number--;
            character = characters[Number];
        }
        character.played=true;
            transform.position = character.transform.position + new Vector3(0, 4, -2.5f);
        transform.parent = character.transform;
    }
}
