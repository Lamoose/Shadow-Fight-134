using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCon : MonoBehaviour
{
    

     public Transform player1;
     public Transform player2;
    public float yOffset;
     public float cameraSpeed = 5f;
    [SerializeField] private float minDisatance = 5f;

    void Update()
     {

         float distance = Vector3.Distance(player1.position, player2.position);
         Vector3 midpoint = (player1.position + player2.position) / 2f;      
         if (distance < minDisatance)    
         {
            AdjustCameraPosition();
            Vector3 targetPosition = new Vector3(midpoint.x, yOffset, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * cameraSpeed);
            Camera.main.orthographicSize = Mathf.Max(distance / 2f, minDisatance);
         }


        if (distance > minDisatance)
        {
            AdjustCameraPosition();
            Vector3 targetPosition = new Vector3(midpoint.x, yOffset, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * cameraSpeed);
            Camera.main.orthographicSize = Mathf.Max(distance / 2f, minDisatance);
        }
         void AdjustCameraPosition()
         {
             // Calculate the midpoint between player1 and player2
             Vector3 midpoint = (player1.position + player2.position) / 2f;

             // Lock the Y position of the midpoint
             midpoint.y = yOffset;

             // Adjust the camera position based on the midpoint
             transform.position = new Vector3(midpoint.x, midpoint.y, transform.position.z);
         }

     }
}

