using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeedNormal;

    [SerializeField]
    private float speed;
    [SerializeField]
    private float knockBackForce;
    [SerializeField]
    private float jumpForce;

    private bool grounded;
    public bool rotate;
    public Vector3 myCenterOfMass;
    float lerpDuration = 0.5f;

    public Rigidbody myRb;
    public MeshRenderer myMr;
    public Material defaultMaterial, knockBackEffectMaterial;




    RigidbodyConstraints defaultConstrains;
    // Start is called before the first frame update
    void Awake()
    {
        myRb = GetComponent<Rigidbody>();
        myMr = GetComponent<MeshRenderer>();

        myRb.Sleep();


    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetMouseButtonDown(0))
        {
            Move();
            //StartCoroutine(Rotation());
            Rotate();
        }
    }


    public void Move()
    {
        if (myRb.IsSleeping())
            myRb.WakeUp();

        myRb.velocity = Vector3.zero;
        myRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        myRb.AddForce(Vector3.forward * speed, ForceMode.Impulse);
    }

    public void Rotate()
    {
        myRb.angularVelocity = Vector3.zero;
        Vector3 angleVelocity = new Vector3(rotationSpeedNormal, 0, 0);
        myRb.AddTorque(angleVelocity, ForceMode.Force);

    }

    public void StopRotation()
    {
        myRb.angularVelocity = Vector3.zero;
    }

    public void ApplyKnockBackForce()
    {

        //
        myRb.velocity = Vector3.zero;
        myRb.AddForce(Vector3.back * knockBackForce, ForceMode.Impulse);

        //myRb.velocity = new Vector3(myRb.velocity.x, myRb.velocity.y, -knockBackForce);

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

    IEnumerator Rotation()
    {

        float timeElapsed = 0;
        //Quaternion startRotation = transform.rotation;
        Quaternion targetRotation = transform.rotation * Quaternion.Euler(0, 0, 80);
        while (timeElapsed < lerpDuration)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, timeElapsed / lerpDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        transform.rotation = targetRotation;

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
