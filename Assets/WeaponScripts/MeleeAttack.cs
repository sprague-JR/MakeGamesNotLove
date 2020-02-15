using System.Collections;
using System.Collections.Generic;
using EnemyScripts;
using UnityEngine;
using PlayerScripts;


class MeleeAttack : Boon
{

    override public uint damage()
    {
        return 10;
    }

    override public DamageType damageType()
    {
        return DamageType.Melee;
    }

    override public void attack(Vector2 position, Vector2 direction)
    {
        RaycastHit2D hit;
        LayerMask lm = LayerMask.GetMask("Enemy");

        hit = Physics2D.Raycast(new Vector2(position.x, position.y), new Vector2(direction.x, direction.y).normalized, range, lm);

        if (hit)
        {
            Color color = Color.white;
            float duration = 2.0f;
            Debug.DrawLine(transform.position, hit.transform.position, color, duration);
            SetLaserPos(hit);
        }

    }

    void SetLaserPos(RaycastHit2D hit)
    {
        if(hit.collider)
        {
            hit.collider.gameObject.GetComponent<Enemy>().takeDmg(damage());
        }
    }

}



