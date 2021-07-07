using UnityEngine;

namespace Groups.Group_S
{
    /**
 * Implements the functionality for a player figure to grab a part of a kart.
 * Player must have a Collider component and part must be tagged with "KartPart", have a Collider and a RigidBody component.
 *
 * Freeze all rotations and positions on the part to avoid parts floating around.
 *
 * Grabbing and dropping is triggered by pressing east (e.g. Q with WASD).
 */
    public class GrabKartPart : MonoBehaviour
    {
        private GameObject _grabbable;
        private GameObject _grabbed;
        private void OnCollisionEnter(Collision other)
        {
            // Object is only grabbable when it's a part and not parented by anything, especially not a player
            if (other.gameObject.CompareTag("KartPart") && (other.transform.parent.name == "KartParts" || other.transform.parent == null || !other.transform.parent.CompareTag("Player") ))
            {
                _grabbable = other.gameObject;
            }
        }

        private void OnCollisionExit(Collision other)
        {
            // When there is no longer a collision with the current grabbable object, it is not grabbable anymore
            if (other.gameObject.CompareTag("KartPart") && other.gameObject == _grabbable)
            {
                _grabbable = null;
            }
        }


        void OnEastPress() {
            if (_grabbed != null) {
                //Drop old part
                DropPart();
            } else {
                //Grab new part
                GrabPart();
            }
        }

        public void DropPart() {
            if (_grabbed != null) {
                _grabbed.transform.parent = null;
                _grabbed = null;
            }
        }

        public void GrabPart()
        {

            if (_grabbable != null && _grabbed == null)
            {
                _grabbed = _grabbable;
                _grabbable.transform.parent = transform;
            }
        }
    }
}
