using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Enemy, IDamageable
{
    public int Health { get; set; } //We are getting and setting the Health property from Enemy Class

    public override void Init()
    {
        base.Init();

        //assign the Health property to our Enemy health
        Health = base.health;
    }

    public void Damage()
    {
        Debug.Log("Damage()");

        //substract 1 from health
        Health--;
        anim.SetTrigger("Hit"); //Animator to set the Trigger on Hit
        isHit = true; //Enemy got Hit by Player
        anim.SetBool("InCombat", true); // After hit, Grab the Animator component

        //check if health is less than 1
        //destroy the object
        if (Health < 1)
        {
            Destroy(this.gameObject);
        }
    }
}
