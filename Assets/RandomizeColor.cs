using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeColor : MonoBehaviour
{
    MeshRenderer myMr;
    public Color random;
    // Start is called before the first frame update
    void Awake()
    {
        myMr = GetComponent<MeshRenderer>();
        RandomColor();
    }

    void RandomColor()
    {
        random = Random.ColorHSV(0f, .5f, 0f, 0.5f);
        //myMr.material.color = Random.ColorHSV(0f, .5f, 0f, 0.5f);
    }
}
