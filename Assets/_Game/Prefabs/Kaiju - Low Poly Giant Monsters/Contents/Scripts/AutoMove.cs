using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DistantLands
{
    public class AutoMove : MonoBehaviour
    {

        public float rotateSpeed;
        float rotAmount;


        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

            rotAmount += rotateSpeed * Time.deltaTime;

            if (rotAmount > 360)
                rotAmount -= 360;
            if (rotAmount < -360)
                rotAmount += 360;

            transform.localEulerAngles = Vector3.zero + Vector3.forward * rotAmount;


        }
    }
}
