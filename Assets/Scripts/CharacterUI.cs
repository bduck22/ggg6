using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterUI : MonoBehaviour
{
    public Character character;

    public Text Level;
    public Text Hp;
    public Text Mp;
    public Text Exp;
    public Slider HpB;
    public Slider MpB;
    public Slider ExpB;
    void Start()
    {
        HpB = transform.GetChild(0).GetComponentInChildren<Slider>();
        MpB = transform.GetChild(1).GetComponentInChildren<Slider>();
        ExpB = transform.GetChild(2).GetComponentInChildren<Slider>();
        Level = transform.GetChild(3).GetComponent<Text>();

        Mp = MpB.GetComponentInChildren<Text>();
        Hp = HpB.GetComponentInChildren<Text>();
        Exp = ExpB.GetComponentInChildren<Text>();


    }
    void Update()
    {
        if (character)
        {
            Level.text = "Lv." + character.Level.ToString("#,##0");
            Hp.text = character.Hp.ToString("#,##0") + " / " + character.MaxHp.ToString("#,##0");
            Mp.text = character.Mp.ToString("#,##0") + " / " + character.MaxMp.ToString("#,##0");
            Exp.text = character.Exp.ToString("#,##0") + " / " + character.LevelGoal.ToString("#,##0");
            HpB.value = character.Hp / character.MaxHp;
            MpB.value = character.Mp / character.MaxMp;

            ExpB.value = character.Exp / character.LevelGoal;
        }
    }
}
