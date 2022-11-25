using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Range Weapon/Weapon")]
public class RangeWeapon : ScriptableObject
{
    [SerializeField] private string _rangeWeaponName;
    [SerializeField] private int _rangeWeaponDamage;
    [SerializeField] private float _rangeWeaponCD;
    [SerializeField] private float _rangeWeaponMovementSpeed;
    [SerializeField] private float _rangeWeaponDistanceToRB;
    [SerializeField] private int _rangeWeaponArrowCount;
    [SerializeField] private Sprite _meeleWeaponSprite;

    public string RangeWeaponName { get { return _rangeWeaponName; } }
    public int RangeWeaponDamage { get { return _rangeWeaponDamage; } }
    public float RangeWeaponCD { get { return _rangeWeaponCD; } }
    public float RangeWeaponMovementSpeed { get { return _rangeWeaponMovementSpeed; } }
    public float RangeWeaponDistanceToRB { get { return _rangeWeaponDistanceToRB; } }
    public int RangeWeaponArrowCount { get { return _rangeWeaponArrowCount; } }
    public Sprite MeeleWeaponSprite { get { return _meeleWeaponSprite; } }


}


