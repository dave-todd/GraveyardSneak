using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureAnimationScript : MonoBehaviour
{

    public void KillCreature()
    {
        transform.parent.gameObject.GetComponent<CreatureScript>().StartDeath();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") { transform.parent.gameObject.GetComponent<CreatureScript>().StartCollide(); }
    }

}
