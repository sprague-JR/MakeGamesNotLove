﻿using System;
using PlayerScripts.gods;
using PlayerScripts.oaths;
using UnityEngine;

namespace PlayerScripts
{
    public class GodManager: MonoBehaviour
    {
        private God[] gods;
        private bool notPacifist;

        public void init()
        {
            GameObject godSelector = GameObject.Find("GodSelect");
            gods = new God[godSelector.transform.childCount];
            for (int i = 0; i < godSelector.transform.childCount; i++)
            {
                gods[i] = godSelector.transform.GetChild(i).GetComponent<God>();
            }
        }

        public void enableGods(int index)
        {
            gods[index].oath().forceBreak(false);
        }

        public void disableGod(int index)
        {
            gods[index].oath().forceBreak(true);
        }

        public void attack(int index, Vector2 position, Vector2 direction)
        {
            if (!gods[index].oath().hasBeenBroken())
            {
                gods[index].boon().attack(position, direction);
            }

            if (notPacifist) return;
            foreach (var god in gods)
            {
                if (!(god is PacifistGod g)) continue;
                ((PacifistOath) g.oath()).playerAttack(true);
                notPacifist = true;
                break;
            }
        }
    }
}
