using UnityEngine;

namespace VampireSurvivorsClone
{
    public interface IDamageable
    {
        void TakeDamage(float damageAmount);
        Transform transform { get; }
    }
}
