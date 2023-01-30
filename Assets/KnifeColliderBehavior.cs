using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeColliderBehavior : MonoBehaviour
{
    public PlayerController player;
    public enum KnifeCollider { Blade, Handle };
    public KnifeCollider knifeCollider;

    private void Awake()
    {
        player = GetComponentInParent<PlayerController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (knifeCollider)
        {
            case KnifeCollider.Blade:
                if(other.gameObject.tag == "Cut")
                {
                    player.myRb.velocity = Vector3.zero;
                    
                    Destroy(other.gameObject);
                }
                break;
            case KnifeCollider.Handle:
                break;
        }
    }

}
