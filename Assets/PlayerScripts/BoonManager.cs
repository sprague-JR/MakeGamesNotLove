using System;
using UnityEngine;

namespace PlayerScripts
{
    class BoonManager: MonoBehaviour
    {
        Boon[] boon;


        private void Start()
        {
            GameObject weaponSelector = GameObject.Find("WeaponSelect");
            for (int i = 0; i < weaponSelector.transform.childCount; i++)
            {
                Debug.Log(weaponSelector.transform.GetChild(i).GetComponent<Boon>());
                boon[i] = weaponSelector.transform.GetChild(i).GetComponent<Boon>();
            }
        }
    }
}
