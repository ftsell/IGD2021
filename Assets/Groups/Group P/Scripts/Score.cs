using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GroupP
{
    public class Score : MonoBehaviour
    {
        public int score;
        public int hitStreak;
        public int multiplier;
        public int specialMultiplier;
        int[] multiplierThresholds;

        public GameObject goodEffect, perfectEffect, missedEffect;
        private GameObject lastMissedEffect;

        public AudioSource badSound;
        public AudioSource specialSound;

        public Vector3 effectPosition = new Vector3(0f, 0f, 0f);
        public GameObject canvas;

        public Text scoreText;
        public Text multiplierText;

        public ParticleSystem particles;


        // Start is called before the first frame update
        void Start()
        {
            score = 0;
            multiplier = 1;
            specialMultiplier = 0;
            hitStreak = 0;

            multiplierThresholds = new int[] { 3, 6, 9 };
            multiplierText = scoreText.transform.GetChild(0).gameObject.GetComponent<Text>();
        }

        // Update is called once per frame
        void Update()
        {
            scoreText.text = score.ToString();
            multiplierText.text = "x" + multiplier.ToString();
            if(specialMultiplier > 0)
            {
                multiplierText.text += "+" + specialMultiplier.ToString();
            }
        }

        public void Hit(HitQuality points)
        {
            switch(points) {
                case HitQuality.NORMAL:
                    break;
                case HitQuality.GOOD:
                    spawnEffect(goodEffect, new Color(0.8f, 0.8f, 0.15f));
                    break;
                case HitQuality.PERFECT:
                    particles.Play();
                    spawnEffect(perfectEffect, new Color(1f,1f, 0.2f));
                    break;
            }
            hitStreak++;
            if (multiplier <= multiplierThresholds.Length && hitStreak >= multiplierThresholds[multiplier - 1])
            {
                multiplier++;
            }
            
            score += multiplier * (int)points + specialMultiplier * (int)points;
        }

        public void Missed()
        {
            if(lastMissedEffect == null)
            {
                lastMissedEffect = spawnEffect(missedEffect, new Color(1f,1f,1f));
            }
            multiplier = 1;
            hitStreak = 0;
        }

        public void SpecialMissed() {
            Missed();
            specialMultiplier = 0;
        }

        public void BadHit() {
            badSound.Play();
            gameObject.GetComponent<Controller>().badHit();
            multiplier = 1;
            specialMultiplier = 0;
            hitStreak = 0;
            score -= 10;
        }

        public void SpecialHit(HitQuality points) {
            specialSound.Play();
            if (specialMultiplier <= multiplierThresholds.Length) {
                specialMultiplier++;
            }
            Hit(points);

        }

        public GameObject spawnEffect(GameObject effectPrefab, Color color) {
            GameObject effect = Instantiate(effectPrefab);
            
            effect.transform.SetParent(GameObject.Find("CanvasP").transform, false);
            effect.transform.localPosition = scoreText.transform.localPosition;
            effect.transform.localScale = effectPrefab.transform.localScale;

            effect.GetComponent<SpriteRenderer>().color = color;

            effect.SetActive(true);

            return effect;
        }
    }
}