using System;
using PlayerScripts.gods;
using PlayerScripts.oaths;
using UnityEngine;

namespace PlayerScripts
{
    public class GodManager: MonoBehaviour
    {
        public bool notPacifist;

        private God[] gods;
        private bool canAttack;

        public void init()
        {
            canAttack = true;
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
            if (!gods[index].oath().hasBeenBroken() && canAttack)
            {
                canAttack = false;
                gods[index].boon().attack(position, direction);
                Invoke("resetCooldown", gods[index].boon().cooldown());
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

        void resetCooldown()
        {
            canAttack = true;
        }
    }
}
