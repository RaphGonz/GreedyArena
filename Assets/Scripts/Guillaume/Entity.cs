using UnityEngine;

namespace Guillaume
{
    public abstract class Entity : MonoBehaviour
    {
        public int maxHealth = 3;
        public int _health;

        public abstract void TakeDamage(int damage);
    }
}