using UnityEngine;

namespace Player
{
    public class PlayerGrounder : MonoBehaviour
    {
        private void OnCollisionEnter(Collision col)
        {
            var hits = new RaycastHit[2];
            Physics.SphereCastNonAlloc(transform.position, 1f, Vector3.down, hits, leeway, groundMask);
            if (hits.Length > 0 && hits[0].collider == col.collider)
            {
                hitCollider = hits[0].collider;
                player.grounded = true;
            }
        }
        
        private void OnCollisionExit(Collision col)
        {
            if (hitCollider == col.collider) player.grounded = false;
        }
        

        public PlayerController player;

        public Collider hitCollider;
        
        public LayerMask groundMask;
        
        public float leeway = 0.4f;
    }
}