//Simple script that rotates an object by a sine wave.




using UnityEngine;


namespace DistantLands
{
    public class SinMove : MonoBehaviour
    {

        public Vector3 depth;
        public Vector3 offset;
        public Vector3 width = Vector3.one * 10;
        private Vector3 originalRot;


        // Start is called before the first frame update
        void Start()
        {

            originalRot = transform.localPosition;

        }

        // Update is called once per frame
        void Update()
        {

            float sinPosX = Mathf.Sin((Time.time - offset.x) / width.x * 30) * depth.x;
            float sinPosY = Mathf.Sin((Time.time - offset.y) / width.y * 30) * depth.y;
            float sinPosZ = Mathf.Sin((Time.time - offset.z) / width.z * 30) * depth.z;

            Vector3 sinPos = new Vector3(sinPosX, sinPosY, sinPosZ);

            transform.localPosition = originalRot + (sinPos);


        }
    }
}