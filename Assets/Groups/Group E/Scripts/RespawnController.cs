using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RespawnController : MonoBehaviour
{

    public List<GameObject> validGrounds;
    private Vector3 lastPosition;
    private Quaternion lastRotation;
    private float positionTime = 0.0f;
    private float offTrackTime = 0.0f;


    // Update is called once per frame
    void Update()
    {
        RaycastHit raycastHit;
        Vector3 raycastStart = new Vector3(transform.position.x, transform.position.y + 0.2f, transform.position.z);
        if (Physics.Raycast(raycastStart, transform.up * -1, out raycastHit, 40.0f))
        {
            if (validGrounds.Contains(raycastHit.collider.gameObject))
            {
                if(positionTime > 1.5f)
                {
                    lastPosition = transform.position;
                    lastRotation = transform.rotation;
                    offTrackTime = 0.0f;
                    positionTime = 0.0f;
                } else
                {
                    positionTime += Time.deltaTime;
                }
                
                
            }
            else
            {
                offTrackTime += Time.deltaTime;
             
            }
        } else
        {
            offTrackTime += Time.deltaTime;
        }

        
        if (offTrackTime > 5.0f)
        {
            GetComponent<CarController>().StopCar();
            transform.position = lastPosition;
            transform.rotation = lastRotation;
        }
    }
}
