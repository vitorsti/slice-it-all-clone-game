using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitObject : MonoBehaviour
{
    public GameObject left, right;
    MeshRenderer myMR;
    public Color randomColor;

    [SerializeField]
    private int scoreToAdd;
    // Start is called before the first frame update
    void Awake()
    {
        myMR = GetComponent<MeshRenderer>();
        if (this.gameObject.name == "GoldBlock")
            SetGoldColor();
        else
            RandomColor();

    }

    // Update is called once per frame
    void Start()
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

        GameManager.AddScore(scoreToAdd);

        Destroy(this.gameObject, 5f);
    }

    void RandomColor()
    {
        randomColor = Random.ColorHSV(0f, 1f, 1f, 1f, 0f, 1);

        myMR.material.color = randomColor;

        left.GetComponent<MeshRenderer>().material.color = randomColor;
        right.GetComponent<MeshRenderer>().material.color = randomColor;
    }

    void SetGoldColor()
    {
        Color color;
        if (ColorUtility.TryParseHtmlString("#ffdf00", out color))
        {
            randomColor = color;
        }

        myMR.material.color = randomColor;

        left.GetComponent<MeshRenderer>().material.color = randomColor;
        right.GetComponent<MeshRenderer>().material.color = randomColor;
    }
}
