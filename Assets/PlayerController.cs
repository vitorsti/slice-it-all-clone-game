using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeedNormal;
    [SerializeField]
    private float rotationSpeedSlow;
    [SerializeField]
    private float velocity;
    [SerializeField]
    private float knockBackForce;
    [SerializeField]
    private float jumpForce;

    private bool grounded;
    public bool rotate;
    public Vector3 myCenterOfMass;

    public Rigidbody myRb;
    public MeshRenderer myMr;
    public Material defaultMaterial, knockBackEffectMaterial;

    public GameObject detector;


    RigidbodyConstraints defaultConstrains;
    // Start is called before the first frame update
    void Awake()
    {
        myRb = GetComponent<Rigidbody>();
        myMr = GetComponent<MeshRenderer>();
        //myRb.centerOfMass = myCenterOfMass;
        myRb.Sleep();
        grounded = true;

        defaultConstrains = myRb.constraints;

    }

    // Update is called once per frame
    void Update()
    {
        if (detector != null)
        {
            detector.transform.position = transform.position;
            //detector.transform.rotation = Quaternion.identity;
        }

        if (Input.GetMouseButtonDown(0))
        {

            Move();
            Rotate();

        }

        
    }


    public void Move()
    {
        if (myRb.IsSleeping())
            myRb.WakeUp();

        myRb.velocity = new Vector3(myRb.velocity.x, jumpForce, myRb.velocity.z);
        myRb.velocity = new Vector3(myRb.velocity.x, myRb.velocity.y, velocity);
        //myRb.centerOfMass = myCenterOfMass;
    }

    public void Rotate()
    {
       myRb.angularVelocity = Vector3.zero;
        Vector3 anlgeVelocity = new Vector3(rotationSpeedNormal, 0, 0);
        myRb.AddTorque(anlgeVelocity, ForceMode.Acceleration);

    }

    public void ApplyKnockBackForce()
    {
        //myRb.velocity = Vector3.zero;
        //myRb.AddForce(Vector3.back * knockBackForce, ForceMode.Force);
        myRb.velocity = new Vector3(myRb.velocity.x, myRb.velocity.y, -knockBackForce);

        KnockBackEffect();
    }

    public void KnockBackEffect()
    {
        myMr.material = knockBackEffectMaterial;

        StartCoroutine(SetDefaultMaterial());
    }

    IEnumerator SetDefaultMaterial()
    {
        yield return new WaitForSeconds(0.1f);
        myMr.material = defaultMaterial;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position + transform.rotation * myCenterOfMass, 0.1f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ground")
        {
            grounded = true;
            rotate = false;
            myRb.velocity = Vector3.zero;
            myRb.Sleep();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Ground")
        {
            grounded = false;
        }
    }
}
