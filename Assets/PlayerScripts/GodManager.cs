using System;
using PlayerScripts.gods;
using PlayerScripts.oaths;
using UnityEngine;

namespace PlayerScripts
{
    public class GodManager: MonoBehaviour
    {
        private God[] gods;
        private bool[] enabledGods;

        public void init()
        {
            GameObject godSelector = GameObject.Find("GodSelector");
            gods = new God[godSelector.transform.childCount];
            enabledGods = new bool[gods.Length];
            for (int i = 0; i < godSelector.transform.childCount; i++)
            {
                gods[i] = godSelector.transform.GetChild(i).GetComponent<God>();
                enabledGods[i] = false;
            }
        }

        public void enableGods(int index)
        {
            enabledGods[index] = true;
        }

        public void disableGod(int index)
        {
            enabledGods[index] = false;
        }

        public void attack(int index, Vector2 position, Vector2 direction)
        {
            if (enabledGods[index])
            {
                gods[index].boon().attack(position, direction);
            }

            for (var i = 0; i < enabledGods.Length; i++)
            {
                if (!(gods[i] is PacifistGod)) continue;
                enabledGods[i] = false;
                break;
            }
        }
    }
}
