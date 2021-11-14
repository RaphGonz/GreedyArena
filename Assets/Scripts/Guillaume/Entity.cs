using UnityEngine;

namespace Guillaume
{
    public abstract class Entity : MonoBehaviour
    {
        protected AudioManager audioManager;
        public int maxHealth = 3;
        public int _health;

        public abstract void TakeDamage(int damage);
    }
}