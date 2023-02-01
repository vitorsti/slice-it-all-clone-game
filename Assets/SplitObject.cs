using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitObject : MonoBehaviour
{
    public GameObject left, right;
    MeshRenderer myMR;
    //Rigidbody lefRb, rightRb;
    // Start is called before the first frame update
    void Awake()
    {
        myMR = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CutInHalf()
    {
        myMR.enabled = false;
        Collider collider = GetComponent<Collider>();
        collider.enabled = false;
        left.SetActive(true);
        right.SetActive(true);
        left.GetComponent<Rigidbody>().useGravity = true;
        right.GetComponent<Rigidbody>().useGravity = true;
        left.GetComponent<Rigidbody>().AddForce(Vector3.left * 5, ForceMode.Impulse);
        right.GetComponent<Rigidbody>().AddForce(Vector3.right * 5, ForceMode.Impulse);
        

        Destroy(this.gameObject, 10f);
    }
}
