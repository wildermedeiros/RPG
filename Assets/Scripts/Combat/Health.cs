using UnityEngine;

namespace RPG.Combat
{
    public class Health : MonoBehaviour
    {
        [SerializeField] float health = 100f;

        public void TakeDamage(float damage)
        {
            
            health -= damage;
            print(health);

            if (health <=0)
            {
                print("died");
            }
        } 
    }
}