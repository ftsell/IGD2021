using System;
using System.Collections;

using UnityEngine;

public class bulletImpuls : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        creationTime = DateTime.Now;
    }
    private DateTime creationTime;
    public GameObject thisus;

    void OnCollisionEnter(Collision col)
    {
        if (!col.collider.CompareTag("Player")) return;
        if ((creationTime - DateTime.Now).TotalSeconds < 1.0) return;
        //var rb = col.gameObject.GetComponent<Rigidbody>();
        var thisRb = this.GetComponent<Rigidbody>();

        StartCoroutine(MoveSmooth(col.gameObject, thisRb.velocity.normalized, 5));
        Debug.Log("CALLED!");
        //rb.AddForce(thisRb.velocity, ForceMode.VelocityChange);
        Destroy(this);
        Destroy(thisus);
        Destroy(col.gameObject);
    }

    private IEnumerator MoveSmooth(GameObject movable, Vector3 direction, float speed) {
        float startime = Time.time;
        Vector3 start_pos = movable.transform.position;
        Vector3 end_pos = movable.transform.position + direction;

        while (start_pos != end_pos && ((Time.time - startime) * speed) < 1f) {
            float move = Mathf.Lerp(0, 1, (Time.time - startime) * speed);
            movable.transform.position += direction * move;
            yield return null;
        }
    }
}
