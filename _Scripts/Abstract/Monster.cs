using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Monster {

    public int hp;
    public int gold;
    public string name;
    public int targetCaravanID;

    public int Power;
    public int Haste; // % value
    public float attackSpeed;
    public int critChance; // % value
    public int critDamage; // % value

    public bool hasSpecialMove;

    public bool isDead;

    public int uniqueID; // for stopping one of multiple instances of the same monster

    public Monster()
    {
        this.hp = 200;
        this.gold = 100;
        this.name = "Dirty Dan";
        this.targetCaravanID = -1;

        this.Power = 10;
        this.Haste = 0;
        this.attackSpeed = 1.2f;
        this.critChance = 10;
        this.critDamage = 0;

        this.hasSpecialMove = false;

        this.uniqueID = Random.Range(99999, 100000000) * Random.Range(99999, 100000000);
    }

    public double calculateDamage()
    {
        float rand = Random.Range(0, 100);
        if (rand <= critChance)
        {
            return ((double)Power) * (1.5 + (((double)critDamage) / 100));
        }
        else
        {
            return (double)Power;
        }
    }
}
