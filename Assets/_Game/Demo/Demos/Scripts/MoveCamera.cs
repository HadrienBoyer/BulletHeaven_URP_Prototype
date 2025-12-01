using UnityEngine;

namespace FreeflowCombatSpace
{
    public class MoveCamera : MonoBehaviour
    {
        public Transform camera;
        // set offset values to position the camera relative to the player
        public Vector3 offset = new Vector3(0, 5, -10);

        // Search for Player tag, then follow that object with a fixed offset
        void Start()
        {
            var playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj)
            {
                transform.position = playerObj.transform.position;
                offset = camera.position - transform.position;
                transform.SetParent(playerObj.transform);

            }
            else
            {
                Debug.LogError("No object with tag 'Player' found in the scene.");
            }
        }
        void LateUpdate()
        {
            // update camera position based on player position plus offset
            camera.position = transform.position + offset;


        }
    }
}

