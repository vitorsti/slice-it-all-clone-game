using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float rotationVelocity;
    [SerializeField]
    private float velocity;

    [SerializeField]
    private float jumpForce;

    private bool grounded;
    public bool rotate;
    public Vector3 myCenterOfMass;
    Vector3 m_EulerAngleVelocity;
    public Rigidbody myRb;
    // Start is called before the first frame update
    void Awake()
    {
        myRb = GetComponent<Rigidbody>();
        myRb.centerOfMass = myCenterOfMass;
        myRb.Sleep();
        grounded = true;


        m_EulerAngleVelocity = new Vector3(0, 0, rotationVelocity);
    }

    // Update is called once per frame
    void Update()
    {
        //myRb.centerOfMass = myCenterOfMass;
        if (Input.GetMouseButtonDown(0))
        {
            if (myRb.IsSleeping())
                myRb.WakeUp();

            myRb.velocity = new Vector3(myRb.velocity.x, jumpForce, myRb.velocity.z);
            myRb.velocity = new Vector3(myRb.velocity.x, myRb.velocity.y, velocity);



            rotate = true;
        }



    }

    private void FixedUpdate()
    {
        if (!grounded)
        {
            //transform.Translate(Vector3.forward * velocity * Time.deltaTime, Space.World);

        }
        if (rotate)
        {
            Quaternion deltaRotation = Quaternion.Euler(m_EulerAngleVelocity * Time.deltaTime);
            myRb.MoveRotation(myRb.rotation * deltaRotation);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position + transform.rotation * myCenterOfMass, 0.1f);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Ground")
        {
            grounded = true;
            rotate = false;
            myRb.velocity = Vector3.zero;
            myRb.Sleep();
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "Ground")
        {
            grounded = false;
        }
    }
}
