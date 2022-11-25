using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Meele Weapon/Weapon")]
public class MeeleWeapon : ScriptableObject
{
    [SerializeField] private string _meeleWeaponName;
    [SerializeField] private int _meeleWeaponDamage;
    [SerializeField] private float _meeleWeaponCD;
    [SerializeField] private float _meeleWeaponAttackRange;
    [SerializeField] private Sprite _meeleWeaponSprite;

    public string MeeleWeaponName {get {return _meeleWeaponName;} }
    public int MeeleWeaponDamage { get { return _meeleWeaponDamage; } }
    public float MeeleWeaponCD { get { return _meeleWeaponCD; } }
    public float MeeleWeaponAttackRange { get { return _meeleWeaponAttackRange; } }
    public Sprite MeeleWeaponSprite { get { return _meeleWeaponSprite; } }
}

/*[CreateAssetMenu(fileName = "New Axe", menuName = "Meele Weapon/Axe")]
public class Axe : MeeleWeapon
{

}
[CreateAssetMenu(fileName = "New Sword", menuName = "Meele Weapon/Sword")]
public class Sword : MeeleWeapon
{
    [SerializeField] private float _crtiticalMeeleDamageChance;
    public float CrititicalMeeleDamageChance { get { return _crtiticalMeeleDamageChance; } }
}
[CreateAssetMenu(fileName = "New Dagger", menuName = "Meele Weapon/Dagger")]
public class Dagger : MeeleWeapon
{
   
}*/

