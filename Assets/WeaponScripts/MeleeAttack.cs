using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerScripts;


class MeleeAttack : MonoBehaviour, Boon
{
    public float range = 2.0f;

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
                Debug.Log("Attack ennemy");
                
            }
        
    }

}



