using UnityEngine;

namespace Guillaume
{
    public abstract class Entity : MonoBehaviour
    {
        public int maxHealth;
        private int _health;

        protected Entity()
        {
            _health = maxHealth;
        }

        public abstract void TakeDamage(int damage);
    }
}