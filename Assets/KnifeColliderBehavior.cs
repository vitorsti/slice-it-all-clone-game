using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeColliderBehavior : MonoBehaviour
{
    public PlayerController player;
    public float playerMass;
    public enum KnifeCollider { Blade, Handle };
    public KnifeCollider knifeCollider;

    private void Awake()
    {
        player = GetComponentInParent<PlayerController>();
        
    }

    private void Start()
    {
        //playerMass = player.myRb.mass;
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (knifeCollider)
        {
            case KnifeCollider.Blade:
                if(other.gameObject.tag == "Cut")
                {
                    player.myRb.velocity = Vector3.zero;
                    player.rotate = false;
                    player.myRb.angularVelocity = Vector3.zero;
                    //player.myRb.mass = 10;
                    Destroy(other.gameObject);
                }
                break;
            case KnifeCollider.Handle:
                break;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        switch (knifeCollider)
        {
            case KnifeCollider.Blade:
                if (other.gameObject.tag == "Cut")
                {
                   
                    //player.myRb.mass = playerMass;
                }
                break;
            case KnifeCollider.Handle:
                break;
        }
    }

}
