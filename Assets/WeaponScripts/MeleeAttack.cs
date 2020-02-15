using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerScripts;


class MeleeAttack : MonoBehaviour, Boon
{


    public God god()
    {
        return null;
    }

    public uint damage()
    {
        return 0;
    }

    public DamageType damageType()
    {
        return 0;
    }

    public void attack(Vector2 position, Vector2 direction)
    {
        RaycastHit hit;
        //LayerMask lm = LayerMask.GetMask("Characters");

        Physics.Raycast(new Vector3(position.x, position.y, 0), new Vector3(direction.x, direction.y, 0).normalized, out hit, 2.0f);
        Gizmos.DrawRay(transform.position, new Vector3(direction.x, direction.y, 0).normalized * 5f);
        SetLaserPos(hit);
    }

    void SetLaserPos(RaycastHit hit)
    {

        Debug.Log("I hit something 2 units in front of me");
    }

}



