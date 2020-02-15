using System;
using UnityEngine;

namespace PlayerScripts
{
    public class BoonManager: MonoBehaviour
    {
        private Boon[] boon;
        private bool[] enabledBoons;

        public void init()
        {
            GameObject weaponSelector = GameObject.Find("WeaponSelect");
            boon = new Boon[weaponSelector.transform.childCount];
            enabledBoons = new bool[boon.Length];
            for (int i = 0; i < weaponSelector.transform.childCount; i++)
            {
                boon[i] = weaponSelector.transform.GetChild(i).GetComponent<Boon>();
                enabledBoons[i] = false;
            }
        }

        public void enableBoon(int index)
        {
            enabledBoons[index] = true;
        }

        public void disableBoon(int index)
        {
            enabledBoons[index] = false;
        }

        public void attack(int index, Vector2 position, Vector2 direction)
        {
            if (enabledBoons[index])
            {
                boon[index].attack(position, direction);
            }
        }
    }
}
