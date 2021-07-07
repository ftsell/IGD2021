using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pulse : MonoBehaviour
{

    private static float beatsPerSecond;

    private static float songOffset;

    private static int numberOfBeats;

    private static bool playing = false;

    private bool started = false;
    private bool lastPulseWasScaleUp = false;

    private float timeAtLastPulse = 0;
    private float timeAtNextPulse = 0;

    float before, after;

    public static void setBPM(float beatsPerMinute) {
        beatsPerSecond = beatsPerMinute / 60f;
    }

    public static void setOffset(float offset) {
        songOffset = offset;
    }

    public static void setNumberOfBeats(int beats) {
        numberOfBeats = beats;
    }

    public static void play() {
        playing = true;
    }

    public static void stop() {
        playing = false;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!started && playing) {
            timeAtLastPulse = Time.time;
            started = true;
            //StartCoroutine("pulsate");
            timeAtLastPulse = Time.time;
            timeAtNextPulse = timeAtLastPulse + songOffset;
        }
        if(started && !playing) {
            playing = false;
        }
        if(started && playing) {
            float now = Time.time;
            if(now < timeAtNextPulse) { return; }

            if(lastPulseWasScaleUp) {
                scaleDown();
            } else {
                scaleUp();
            }
            lastPulseWasScaleUp = !lastPulseWasScaleUp;

            timeAtLastPulse = timeAtNextPulse;

            timeAtNextPulse = timeAtLastPulse + 1f / beatsPerSecond / 2f; 
        }
    }

    private void scaleUp() {
        transform.localScale = new Vector3(1.07f, 1.07f, 1f);
    }

    private void scaleDown() {
        transform.localScale = new Vector3(1f, 1f, 1f);
    }


}
