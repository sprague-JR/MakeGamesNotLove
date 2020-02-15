using System;
using UnityEngine;

namespace PlayerScripts
{
    class BoonManager: MonoBehaviour
    {
        public Boon[] boon;


        private void Start()
        {
            GameObject weaponSelector = GameObject.Find("WeaponSelect");
            boon = new Boon[weaponSelector.transform.childCount];
            for (int i = 0; i < weaponSelector.transform.childCount; i++)
            {
                boon[i] = weaponSelector.transform.GetChild(i).GetComponent<Boon>();
            }
        }

        public void enableBoon(int index)
        {
            //set boon active here
        }

        public void disableBoon(int index)
        {
            //set boon inactive here
        }
    }
}
