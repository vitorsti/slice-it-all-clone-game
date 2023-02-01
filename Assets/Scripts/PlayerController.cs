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
    private float speed;
    [SerializeField]
    private float knockBackForce;
    [SerializeField]
    private float jumpForce;

    private Touch touch;
    //private bool grounded;
    public bool rotate;

    public Rigidbody myRb;
    public MeshRenderer myMr;
    public Material defaultMaterial, knockBackEffectMaterial;
    public AudioSource flipAs, cableHitAs, cutAs;

    public GameObject detector;

    [Header("Debugger")]
    [SerializeField]
    bool stopRotation;
    [SerializeField]
    bool stopMovement;

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
        //Debug.LogError(transform.eulerAngles.z);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!stopMovement)
            {
                Move();
            }

            if (!stopRotation)
            {
                Rotate();
            }

            PlayFlipAudio();
        }

        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Ended)
            {
                Move();
                Rotate();
                PlayFlipAudio();
            }
        }

        if (rotate)
        {

            if (transform.eulerAngles.z > -10.0f && transform.eulerAngles.z < 5.0f || transform.eulerAngles.z > 150 && transform.eulerAngles.z < 5.0f)
            {
                Debug.DrawRay(detector.transform.position, detector.transform.forward * 1000, Color.red, 0.1f);
                //Debug.Log("hello");
                //SlowRotate();
                RaycastHit hit;

                if (Physics.Raycast(detector.transform.position, detector.transform.forward, out hit, 1000))
                {
                    //Rotate();
                    SlowRotate();
                    //rotate = false;
                    Debug.Log(hit.transform.name);
                }
                //SlowRotate();
            }
        }


    }

    public void PlayFlipAudio()
    {
        flipAs.Play();

    }
    public void PlayCableHitAudio()
    {
       cableHitAs.Play();

    }

    public void PlayCutAudio()
    {
        cutAs.Play();

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
        rotate = true;
        myRb.angularVelocity = Vector3.zero;
        Vector3 angleVelocity = new Vector3(rotationSpeedNormal, 0, 0);
        myRb.AddTorque(angleVelocity, ForceMode.Force);

    }

    public void SlowRotate()
    {
        Debug.Log("passou aqui");
        myRb.angularVelocity = Vector3.zero;
        Vector3 angleVelocity = new Vector3(rotationSpeedSlow, 0, 0);
        myRb.AddTorque(angleVelocity, ForceMode.Force);
        //rotate = false;

    }

    /*public void StopRotation()
    {
        myRb.angularVelocity = Vector3.zero;
    }*/

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ground")
        {
            //grounded = true;
            rotate = false;
            myRb.velocity = Vector3.zero;
            myRb.Sleep();
        }

        if (other.gameObject.tag == "Game Over")
        {
            rotate = false;
            myRb.velocity = Vector3.zero;
            myRb.Sleep();
            GameManager.GameOver();
        }

        if (other.gameObject.tag == "Win")
        {
            rotate = false;
            myRb.velocity = Vector3.zero;
            myRb.Sleep();
            GameManager.Win();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Ground")
        {
            //grounded = false;
        }
    }

    /*private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position + transform.rotation * myCenterOfMass, 0.1f);
    }*/

}
