using System;
using UnityEngine;

namespace PlayerScripts
{
    public class BoonManager: MonoBehaviour
    {
        private Boon[] boon;
        private bool[] enabled;

        private void Start()
        {
            GameObject weaponSelector = GameObject.Find("WeaponSelect");
            boon = new Boon[weaponSelector.transform.childCount];
            enabled = new bool[boon.Length];
            for (int i = 0; i < weaponSelector.transform.childCount; i++)
            {
                boon[i] = weaponSelector.transform.GetChild(i).GetComponent<Boon>();
                enabled[i] = false;
            }
        }

        public void enableBoon(int index)
        {
            enabled[index] = true;
        }

        public void disableBoon(int index)
        {
            enabled[index] = false;
        }

        public void attack(int index, Vector2 position, Vector2 direction)
        {
            if (enabled[index])
            {
                boon[index].attack(position, direction);
            }
        }
    }
}
