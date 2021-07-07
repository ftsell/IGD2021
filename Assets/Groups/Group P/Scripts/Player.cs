using System;
using System.Collections.Generic;
using UnityEngine;

namespace GroupP {
    public class Player : MonoBehaviour
    {
        private class NoteInfo {
            public NoteInfo(Note note) {
                this.note = note;
                hitQuality = note.hitQuality;
                hit = false;
                missed = false;
            }
            public Note note;
            public HitQuality hitQuality;
            public Boolean hit;
            public Boolean missed;
        }

        public bool isAI = true;

        private bool missed;

        private List<Note> notes = new List<Note>();

        // Start is called before the first frame update
        void Start()
        {
            KeyPressHandler.instance.registerPlayer(this.gameObject);
        }

        public void addNote(Note note) {
            notes.Add(note);
            if(isAI) {
                float std = 15f;
                float rnd = getNormalRandomNumber(0f, std);
                float speed = GameManager.instance.getBpmOfActiveSong();

                float hitTime = (note.transform.localPosition.x - rnd) / speed;
                
                float callProbability = 1.0f;
                if(note.bad) {
                    callProbability = UnityEngine.Random.Range(0f, 1f);
                }

                if(callProbability > 0.7) {
                    StartCoroutine(callKeyPressMethod(hitTime, note.key));
                }
            }
        }

        System.Collections.IEnumerator callKeyPressMethod(float delay, KeyType keyType) {
            yield return new WaitForSeconds(delay);
            gameObject.GetComponent<Controller>().sendKeyPressToKeyPressHandler(keyType);
        }

        public void removeNote(Note note) {
            notes.Remove(note);
        }

        public float getNormalRandomNumber(float mean, float stdDev) {
            float u1 = UnityEngine.Random.Range(0.0001f, 1f); 
            float u2 = UnityEngine.Random.Range(0.0001f, 1f);
            double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2); //random normal(0,1)
            double randNormal = mean + stdDev * randStdNormal; //random normal(mean,stdDev^2)

            return (float)randNormal;
        }
    }
}