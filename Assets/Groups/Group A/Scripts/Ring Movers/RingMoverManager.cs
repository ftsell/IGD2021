using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingMoverManager : MonoBehaviour
{
    public static RingMoverManager instance = null;
    public float bl_exit, br_exit, tl_exit, tr_exit, bl_enter, br_enter, tl_enter, tr_enter;
    public bool bl_active, br_active, tl_active, tr_active;
    public GameObject bottom_left, bottom_right, top_left, top_right;
    public float cooldown, speed, dur;
    // Start is called before the first frame update
    private void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);
    }

    void Start()
    {
        cooldown = 5f;
        speed = 0.2f;
        dur = 5f;
        bl_active = true;
        br_active = true;
        tl_active = true;
        tr_active = true;
    }

    // Update is called once per frame
    void Update()
    {
        float t = Time.time;
        if ((!bl_active) && (t >= bl_exit + cooldown))
        {
            bottom_left.SetActive(true);
            bl_active = true;
        }
        if ((!bl_active) && (t <= bl_exit + cooldown)) bottom_left.SetActive(false);
        if ((!br_active) && (t >= br_exit + cooldown))
        {
            bottom_right.SetActive(true);
            br_active = true;
        }
        if ((!br_active) && (t <= br_exit + cooldown)) bottom_right.SetActive(false);
        if ((!tl_active) && (t >= tl_exit + cooldown))
        {
            top_left.SetActive(true);
            tl_active = true;
        }
        if ((!tl_active) && (t <= tl_exit + cooldown)) top_left.SetActive(false);
        if ((!tr_active) && (t >= tr_exit + cooldown))
        {
            top_right.SetActive(true);
            tr_active = true;
        }
        if ((!tr_active) && (t <= tr_exit + cooldown)) top_right.SetActive(false);
    }

    
}
