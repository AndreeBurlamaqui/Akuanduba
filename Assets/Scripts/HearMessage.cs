using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HearMessage : MonoBehaviour
{

    public ParticleSystem part;
    public List<ParticleCollisionEvent> collisionEvents;

    void Start()
    {
        part = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("HearTarget"))
            other.GetComponent<Patrol>().AttackPlayer();


    }

}
