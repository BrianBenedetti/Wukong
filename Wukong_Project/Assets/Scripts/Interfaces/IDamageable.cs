using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable<T1, T2>
{
    void TakeDamage(int damageTaken, DamageTypes damageType);
}
