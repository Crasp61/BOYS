using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeWeapon : MonoBehaviour
{
    public RangeWeapon(float cd, int damage, float movementSpeed, float distanceToRb, int arrowCount)
    {
        WeaponCD = cd;
        WeaponDamage = damage;
        MovementSpeed = movementSpeed;
        DistanseToRb = distanceToRb;
        ArrowCount = arrowCount;
    }

    public float WeaponCD { get; }
    public int WeaponDamage { get; }
    public float MovementSpeed { get; }
    public float DistanseToRb { get; }
    public int ArrowCount { get; }
}

public class LongBow: RangeWeapon
{
    public LongBow(float cd, int damage, float movementSpeed, float distanceToRb, int arrowCount) : base(cd, damage, movementSpeed, distanceToRb, arrowCount)
    {

    }
}

public class ShortBow: RangeWeapon
{
    public ShortBow(float cd, int damage, float movementSpeed, float distanceToRb, int arrowCount) : base(cd, damage, movementSpeed, distanceToRb, arrowCount)
    {

    }
}

public class ClassicBow: RangeWeapon
{
    public ClassicBow(float cd, int damage, float movementSpeed, float distanceToRb, int arrowCount) : base(cd, damage, movementSpeed, distanceToRb, arrowCount)
    {

    }
}
