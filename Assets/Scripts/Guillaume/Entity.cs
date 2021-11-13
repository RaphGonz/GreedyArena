using UnityEngine;

namespace Guillaume
{
    public abstract class Entity : MonoBehaviour
    {
        public int maxHealth;
        public int _health;

        public abstract void TakeDamage(int damage);
    }
}