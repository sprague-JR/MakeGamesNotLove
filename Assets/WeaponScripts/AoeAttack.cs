using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerScripts;

namespace WeaponScripts
{
    class AoeAttack: Boon
    {

        private float gizmoRange = 0f;
        private Vector2 pos;
      
        override public God god()
        {
            return null;
        }

        override public uint damage()
        {
            return 0;
        }

        override public DamageType damageType()
        {
            return 0;
        }

        override public void attack(Vector2 position, Vector2 direction)
        {

            pos = position;
            Debug.Log("aoe cast");

            //start a coroutine which will grow a circle collider arround the player
            StartCoroutine("AoeCast");

            
            
        }

        IEnumerator AoeCast()
        {
            //get layer mask for overlapcircle cast
            LayerMask lm = LayerMask.GetMask("Enemy");
            List<GameObject> collidedObj = new List<GameObject>();

            for (float i = 0; i <= range; i += 0.1f)
            {
                //cast a circle collider which returns an array of all enemy colliders
                Collider2D[] cols = Physics2D.OverlapCircleAll(new Vector2(pos.x, pos.y), range, lm);
                gizmoRange = i; //update gizmo range
                
                foreach (Collider2D col in cols)
                {
                    //check if gameObject has already been detected
                    int count = 0;
                    foreach(GameObject obj in collidedObj)
                    {
                        
                        if(col.gameObject == obj)
                        {
                            count++;
                        }
                    }
                    if (count == 0)
                    {
                        collidedObj.Add(col.gameObject);
                        
                        //add the inflict damage method here
                    }
                    
                }
                yield return null;
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(transform.position, gizmoRange);

        }

    }
}
