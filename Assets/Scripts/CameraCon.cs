using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCon : MonoBehaviour
{

    public GameObject p1;
    public GameObject p2;

    public float yOffset = 2f;
    public float minDistance = 7.5f;

    private float Xmin, Xmax;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void LateUpdate()
    {
        Xmin = Xmax = p1.transform.position.x;
        
        if (p1.transform.position.x < Xmin) Xmin = p1.transform.position.x;
        if (p2.transform.position.x < Xmin) Xmin = p2.transform.position.x;

        if (p1.transform.position.x > Xmax) Xmax = p1.transform.position.x;
        if (p2.transform.position.x > Xmax) Xmax = p2.transform.position.x;

        float xMiddle = (Xmax + Xmin) / 2;

        float Distance = (Xmax - Xmin);

        if (Distance < minDistance) Distance = minDistance;

        transform.position = new Vector3(xMiddle, yOffset, -Distance);


    }
}
