using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    //variable to determine if the Damage function can be called
    private bool _canDamage = true;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Hit: " + other.name); //Amount of Hits that Player is hitting to Enemy

        //Calling Damage Function
        //checking with Interface hit and check if we have IDamageable interface and stored and hit
        IDamageable hit = other.GetComponent<IDamageable>();

        if (hit != null)
        {
            //if canDamage is true then hit.Damage will call
            if (_canDamage == true)
            {
                hit.Damage(); // Call the function of Damage if hits
                _canDamage = false;
                StartCoroutine(ResetDamage()); //we will start the Coroutine and Initialize it
            }
        }
    }

    //coroutine to reset the variable after 0.5f
    IEnumerator ResetDamage()
    {
        yield return new WaitForSeconds(0.5f); //Will wait for the 0.5 seconds
        _canDamage = true; //Initially we Falsed it, Now it will true 
    }
}
