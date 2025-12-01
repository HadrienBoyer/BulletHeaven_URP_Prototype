//Simple navigation script to show off how the procedural animation can be used.




using UnityEngine;


namespace DistantLands.WaypointSystemSecond
{
    public class WaypointSystem : MonoBehaviour
    {

        public Transform target;
        public float changeDistance;
        public float range;
        Vector3 origin;



        // Start is called before the first frame update
        void Start()
        {

            origin = transform.position;

        }

        // Update is called once per frame
        void Update()
        {

            if (target)
                if (Vector3.Distance(transform.position, target.position) < changeDistance)
                {

                    target.position = origin + (Random.insideUnitSphere * range);


                }

        }
    }
}