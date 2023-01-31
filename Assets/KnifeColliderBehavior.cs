using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeColliderBehavior : MonoBehaviour
{
    public PlayerController player;
    //public float playerMass;
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
                if (other.gameObject.tag == "Cut")
                {
                    player.myRb.velocity = new Vector3(0, player.myRb.velocity.y, 0);
                    player.StopRotation();
                    Destroy(other.gameObject);
                }
                
                break;
            case KnifeCollider.Handle:
                player.ApplyKnockBackForce();
                //player.myRb.angularVelocity = Vector3.zero;
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
                    

                }
                break;
            case KnifeCollider.Handle:

                break;
        }
    }

}
