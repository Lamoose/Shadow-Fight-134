using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCon : MonoBehaviour
{
    public Transform player1;
    public Transform player2;
    public float cameraSpeed = 5f;
    public float minDistance = 2f;

    void Update()
    {
      
        float distance = Vector3.Distance(player1.position, player2.position);

        Vector3 midpoint = (player1.position + player2.position) / 2f;

       AdjustCameraPosition();
       Vector3 targetPosition = new Vector3(midpoint.x, midpoint.y, transform.position.z);
       transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * cameraSpeed);
       Camera.main.orthographicSize = Mathf.Max(distance / 2f, minDistance);

        void AdjustCameraPosition()
        {
            // Calculate the midpoint between player1 and player2
            Vector3 midpoint = (player1.position + player2.position) / 2f;

            // Lock the Y position of the midpoint
            midpoint.y = transform.position.y;

            // Adjust the camera position based on the midpoint
            transform.position = new Vector3(midpoint.x, midpoint.y, transform.position.z);
        }

    }
    /* public GameObject p1;
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


     }*/
}

