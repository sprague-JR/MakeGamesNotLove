﻿using System.Collections;
using System.Collections.Generic;
using EnemyScripts;
using UnityEngine;
using PlayerScripts;

namespace WeaponScripts
{
    class AoeAttack: Boon
    {
        
        private float gizmoRange = 0f;
        private Vector2 pos;
        public bool isDone;
        public ParticleSystem ps;
        public bool attackPlayer = false;

        private void Start()
        {
            ps = GetComponent<ParticleSystem>();
            ps.GetComponent<Renderer>().sortingLayerName = "Player";
            
        }

        override public float cooldown()
        {
            return 3f;
        }

        override public float range()
        {
            return 4f;
        }

        override public int damage()
        {
            return 1;
        }

        override public DamageType damageType()
        {
            return DamageType.Aoe;
        }

        override public void attack(Vector2 position, Vector2 direction)
        {
            pos = position;
            isDone = false;

            ps.Play();
            ps.startSize = range() * 1.5f;
            //start a coroutine which will grow a circle collider arround the player
            StartCoroutine("AoeCast");
        }

        IEnumerator AoeCast()
        {
            //get layer mask for overlapcircle cast
            LayerMask lm;
            if (attackPlayer)
            {
                Debug.Log("ATTACK PLAYER");
                lm = LayerMask.GetMask("Default");
            }
            else
            {
                lm = LayerMask.GetMask("Enemy");
            }
            List<GameObject> collidedObj = new List<GameObject>();

            for (float i = 0; i <= range(); i += 0.1f)
            {
                //cast a circle collider which returns an array of all enemy colliders
                Collider2D[] cols = Physics2D.OverlapCircleAll(new Vector2(pos.x, pos.y), range(), lm);
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
                        if (attackPlayer)
                        {
                            Debug.Log("ATTACK");
                            col.gameObject.GetComponent<Player>().takeDmg(damage());
                        }
                        else
                        {
                            col.gameObject.GetComponent<Enemy>().takeDmg(damage());
                        }
                    }
                }
                yield return null;
            }

            isDone = true;
            gizmoRange = 0.0f;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(transform.position, gizmoRange);
        }
    }
}
