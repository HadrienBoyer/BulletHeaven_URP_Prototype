//Simple script that scales an object by a sine wave.




using System.Collections;
using UnityEngine;


namespace DistantLands
{
    public class Autoblink : MonoBehaviour
    {

        public Vector3 blinkDirection;
        Vector3 originalRot;

        public Vector2 timeBetweenBlinks;
        float blinkTime;
        bool blinking;

        public AnimationCurve blinkCurve;

        // Start is called before the first frame update
        void Start()
        {

            originalRot = transform.localScale;



        }

        // Update is called once per frame
        void Update()
        {

            if (blinkTime <= 0 && ! blinking)
                StartCoroutine(Blink());
            else
                blinkTime -= Time.deltaTime;


        }
        
        public IEnumerator Blink ()
        {
            blinking = true;
            float t = 0;
            Keyframe lastframe = blinkCurve[blinkCurve.length - 1];
            float lastTime = lastframe.time;


            while (t < lastTime)
            {

                transform.localScale = originalRot - blinkDirection * blinkCurve.Evaluate(t);


                t += Time.deltaTime;
                yield return null;

            }

            blinking = false;
            blinkTime = Random.Range(timeBetweenBlinks.x, timeBetweenBlinks.y);


            yield break;

        }
    }
}