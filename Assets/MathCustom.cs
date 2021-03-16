using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathCustom : MonoBehaviour
{
    public float rate = 0.1f;
    public int sign = 1;
    public Vector3 posA;
    public Vector3 posB;
    void Start()
    {

    }
    private void Update()
    {
        rate += (sign) * Time.deltaTime;
        transform.position = Vector3.Lerp(posA, posB, rate);
        if (transform.position == posA )
            sign = 1;
        if ( transform.position == posB)
            sign = -1;
    }
}
