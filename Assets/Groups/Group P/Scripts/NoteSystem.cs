using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace GroupP {

    public class NoteSystem : MonoBehaviour
    {
        [System.Serializable]
        public class NoteEntry {

            public float beat;
            //public Note note;

            public List<KeyType> keys;

            public Animation animation;
        }

        public GameObject notePrefab;
        public GameObject specialPrefab;

        public float beatsPerMinute;
        private float deltaBeatS;
        private float lastBeat;
        private float tempo;

        private float songOffset;

        private int counter;

        // Probability that there is at least one note on the next beat
        float pNext = 0.75f;

        public Queue<NoteEntry> noteQueue;

        void Start()
        {
            GameObject theSong = GameManager.instance.getSong();
            tempo = theSong.GetComponent<Song>().beatsPerMinute;
            songOffset = theSong.GetComponent<Song>().offsetInSeconds;
            noteQueue = new Queue<NoteEntry>();
            

            lastBeat = 0;
            deltaBeatS = 1 / tempo;
            counter = 0;

            for(int i=5;i<theSong.GetComponent<Song>().totalNumberOfBeats;++i) {
                if(UnityEngine.Random.Range(0.0f, 1.0f) < 1.0f-pNext) { 
                    increaseP(0.1f);
                    continue;
                }
                
                List<KeyType> keys = new List<KeyType>(new KeyType[] {KeyType.UP, KeyType.DOWN, KeyType.LEFT, KeyType.RIGHT});
                
                
                int n = UnityEngine.Random.Range(1, 3);
                decreaseP(0.05f);
                for(int j=0;j<n;++j) {
                    int index = UnityEngine.Random.Range(0, keys.Count);
                    spawnNote(i, keys[index]);
                    keys.RemoveAt(index);
                    decreaseP(0.05f);
                }
                
            }
            
        }

        void increaseP(float amount) {
            pNext = Mathf.Max(pNext+amount, 1f);
        }

        void decreaseP(float amount) {
            pNext = Mathf.Max(pNext-amount, 0f);
        }

        // Update is called once per frame
        void Update()
        {
            float now = Time.time;
        
            if (now - lastBeat > deltaBeatS || now == 0) {
                
                float offset = now - lastBeat - deltaBeatS;
                
                if(counter % 4 == 0) {
                    //spawnNote(tempo * 8 - offset);
                }

                lastBeat = now - offset;

                counter += 1;
            }
        }

        GameObject spawnNote(float position, KeyType key) {
            GameObject obj;
            if(UnityEngine.Random.Range(0f, 1f) < 0.05f) {
                obj = Instantiate(specialPrefab) as GameObject;
                obj.GetComponent<Note>().special = true;
                obj.transform.SetParent(gameObject.transform);
                obj.transform.localScale = specialPrefab.transform.localScale;
            } else {
                obj = Instantiate(notePrefab) as GameObject;
                obj.transform.SetParent(gameObject.transform);
                obj.GetComponent<Note>().bad = UnityEngine.Random.Range(0f, 1f) < 0.05f;
                obj.transform.localScale = notePrefab.transform.localScale;
            }
            obj.SetActive(true);
            
            obj.GetComponent<Note>().key = key;
            
            obj.transform.localPosition = new Vector3((tempo * songOffset) + position * 60f, 0f, 0f);
            obj.GetComponent<Note>().tempo = tempo;

            return obj;
        }
    }
}