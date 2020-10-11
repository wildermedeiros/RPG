using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class WeaponPickup : MonoBehaviour
    {
        [SerializeField] Weapon weaponPickup;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnTriggerEnter(Collider other)
        {
            //var player = GameObject.FindWithTag("Player");
            if (other.CompareTag("Player"))
            {
                other.GetComponent<Fighter>().EquipWeapon(weaponPickup);
                Destroy(gameObject);
            }
        }
    }

}