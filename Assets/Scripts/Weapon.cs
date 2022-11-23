using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeeleWeapon : MonoBehaviour
{
    public  MeeleWeapon (float cd, int damage, float range)
    {
        WeaponCD = cd;
        WeaponDamage = damage;
        WeaponAttackrange = range;
    }

    public float WeaponCD { get; }
    public virtual int WeaponDamage { get; }
    public float WeaponAttackrange { get; }

}

public class Axe: MeeleWeapon
{ 
    public Axe (float cd, int damage, float range): base (cd,damage,range)
    {
       
    }
}

public class Sword: MeeleWeapon
{
    
    public Sword(float cd, int damage, float range) : base(cd, damage, range)
    {
        
    }

   
      
}

public class Dagger : MeeleWeapon
{
    public Dagger(float cd, int damage, float range) : base(cd, damage, range)
    {

    }
}

