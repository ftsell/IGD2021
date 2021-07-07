using Boo.Lang;
using UnityEngine;
using UnityEngine.Events;

public class CameraFlightController : MonoBehaviour
{

    public GameObject positionTarget;
    public GameObject lookAtTarget;
    public float speed;
    public UnityEvent afterCameraFlightEvent;
    public float secsBeforeFlight;
    private bool finished = false;

    void Update()
    {
        secsBeforeFlight = secsBeforeFlight - Time.deltaTime;
        if (!finished && secsBeforeFlight < 0.0f)
        {
            transform.position = Vector3.Lerp(transform.position, positionTarget.transform.position, Time.deltaTime * speed * 0.25f);
            Quaternion rotation = Quaternion.LookRotation(lookAtTarget.transform.position - transform.position, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, speed * Time.deltaTime);

            if ((positionTarget.transform.position - transform.position).magnitude < 100.0 && Quaternion.Angle(lookAtTarget.transform.rotation, transform.rotation) < 10.0)
            {
                finished = true;
                afterCameraFlightEvent.Invoke();
            }
        }
    }
}
