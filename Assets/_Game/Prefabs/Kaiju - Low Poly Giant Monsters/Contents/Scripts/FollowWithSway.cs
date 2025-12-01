//Simple navigation script to show off how the procedural animation can be used.




using UnityEngine;


namespace DistantLands
{
    public class FollowWithSway : MonoBehaviour
    {

        public Transform target;
        public float speed = 5f;
        public float angularSpeed = 15f;
        public float maxTiltHelpFactor = 1f;
        public float tiltSpeed = 15f;
        public float stoppingDistance = -1;
        public bool paused;

        public SinMove sinMove;
        Vector3 depth;
        public float maxSwayDistance;

        public float tiltFactor = 5f;
        public float maxTilt = 45f;
        float currentTilt;

        // Start is called before the first frame update
        void Awake()
        {

            depth = sinMove.depth;


        }

        // Update is called once per frame
        void Update()
        {

            if (!paused && target)
                if (Vector3.Distance(transform.position, target.position) > stoppingDistance)
                {
                    transform.position += transform.forward * speed * Time.deltaTime;

                    currentTilt = Mathf.Clamp(Mathf.MoveTowards(currentTilt, AngleDir(transform.forward, target.position - transform.position, Vector3.up) * Vector3.Angle(transform.forward, target.position - transform.position) * 0.05f * tiltFactor, tiltSpeed * Time.deltaTime), -maxTilt, maxTilt);

                    sinMove.transform.localEulerAngles = new Vector3 (0, 0, currentTilt);

                    transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(target.position - transform.position,
                        Vector3.up), (angularSpeed + Remap(Mathf.Abs(currentTilt), 0, maxTilt, 0, maxTiltHelpFactor)) * Time.deltaTime);

                }


            float j = Remap(Mathf.Clamp(Vector3.Distance(transform.position, target.position), stoppingDistance, maxSwayDistance), stoppingDistance, maxSwayDistance, 0, 1);


            sinMove.depth = (depth * j) * Remap(Mathf.Abs(currentTilt), 0, maxTilt, 1, 0);



        }
        float Remap(float s, float a1, float a2, float b1, float b2)
        {
            return b1 + (s - a1) * (b2 - b1) / (a2 - a1);
        }


        public float AngleDir(Vector3 fwd, Vector3 targetDir, Vector3 up)
        {
            Vector3 perp = Vector3.Cross(fwd, targetDir);
            float dir = Vector3.Dot(perp, up);

            if (dir > 0.0f)
            {
                return -1.0f;
            }
            else if (dir < 0.0f)
            {
                return 1.0f;
            }
            else
            {
                return 0.0f;
            }
        }
    }
}