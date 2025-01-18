using Props;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace Player
{
    public class PlayerCamera : MonoBehaviour
    {
        private void Start()
        {
            Cam = player.cam;
        }

        private void Update()
        {
            var raycast = Physics.Raycast(Cam.transform.position, Cam.transform.forward, out var interactableHit, holdRaycastDistance, interactableLayer);
            
            if (player.input.actions["Hold"].IsPressed())
            {
                if (selectedInteractable)
                {
                    PerformForceOnInteractable(selectedInteractable);
                    
                    CounterForSnap += Time.deltaTime;

                    if (CounterForSnap >= timeTillCanSnap)
                    {
                        CounterForSnap = 0f;
                        
                        var hits = new RaycastHit[2];
                        Physics.SphereCastNonAlloc(selectedInteractable.transform.position, snapToNpcDistance, Vector3.down,
                            hits, 0f, npcLayer);
                        if (hits.Length > 0 && hits[0].transform)
                        {
                            selectedInteractable.SnapToNpc(hits[0].transform);
                            selectedInteractable = null;
                        }
                    }
                }
                else
                {
                    if (raycast)
                    {
                        hitPoint = interactableHit.point;
                        if (interactableHit.rigidbody)
                        {
                            var interactable = interactableHit.rigidbody.GetComponent<PropBehavior>();
                            if (interactable.OnInteract())
                            {
                                selectedInteractable = interactable;
                            }
                        }
                    }
                }
            }
            else if (selectedInteractable)
            {
                selectedInteractable.OnDrop();
                selectedInteractable = null;
            }
        }

        private void PerformForceOnInteractable(PropBehavior interactable)
        {
            var objectHoldIdeal = Physics.Raycast(Cam.transform.position, Cam.transform.forward, out var holdOutHit, holdDistance, mapLayer);
        
            var distanceToHold =
                objectHoldIdeal ? Vector3.Distance(holdOutHit.point, Cam.transform.position) : holdDistance;
        
            var holdAt = Cam.transform.TransformPoint(Vector3.forward * distanceToHold);
        
            var forceDirection = holdAt - interactable.transform.position;
                
            interactable.rig.AddForce(forceDirection * holdForce);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            if (Cam)
            {
                Gizmos.DrawLine(Cam.transform.position, Cam.transform.TransformPoint(Vector3.forward * holdDistance));
                Gizmos.DrawWireSphere(Cam.transform.TransformPoint(Vector3.forward * holdDistance), 1f);
            }
        }

        public PlayerController player;

        public float CounterForSnap;
        
        private Camera Cam;

    
        [Header("Values")]

        public float holdDistance = 3f;
        public float holdForce = 10f;
        public float holdRaycastDistance = 5f;
        public float snapToNpcDistance = 1f;
        public float timeTillCanSnap = 1f;
    
        [Header("Layers")]

        public LayerMask mapLayer;
        public LayerMask interactableLayer;
        public LayerMask npcLayer;

        [Header("Debug")] 
        
        public Vector3 hitPoint;

        public PropBehavior selectedInteractable;
    }
}
