using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Vector3 followOffset;

    Vector3 currentPosition;
    Vector3 futurePosition;
    public Transform objToFollow;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        currentPosition = transform.position;
        futurePosition = Vector3.Lerp(currentPosition, objToFollow.position, 1f);

        transform.position = futurePosition + followOffset;
    }
}
