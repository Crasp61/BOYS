using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public  Weapon (float cd, int damage, float range)
    {
        WeaponCD = cd;
        WeaponDamage = damage;
        WeaponAttackrange = range;
    }

    public float WeaponCD { get; }
    public int WeaponDamage { get; }
    public float WeaponAttackrange { get; }

}

public class Axe: Weapon
{ 
    public Axe (float cd, int damage, float range): base (cd,damage,range)
    {
       
    }
}

public class Sword: Weapon
{
    public Sword(float cd, int damage, float range) : base(cd, damage, range)
    {

    }


}

public class Dagger : Weapon
{
    public Dagger(float cd, int damage, float range) : base(cd, damage, range)
    {

    }
}

