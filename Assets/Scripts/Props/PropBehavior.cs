using System;
using UnityEngine;
using UnityEngine.Events;

namespace Props
{
    public class PropBehavior : MonoBehaviour
    {
        private void Awake()
        {
            if (rig) rig.Sleep();
        }

        private void Update()
        {
            if (rig.isKinematic)
            {
                OnNpcCounter += Time.deltaTime;

                if (OnNpcCounter >= timeTillCanBeGrabbed) canBeInteractedWith = true;
            }
        }
        
        public bool OnInteract()
        {
            if (!canBeInteractedWith) return false;
            
            rig.isKinematic = false;
            rig.drag = interactDrag;
            rig.angularDrag = interactDrag;
            
            interactEvent.Invoke();

            return true;
        }

        public void OnDrop()
        {
            rig.drag = dropDrag;
            rig.angularDrag = dropDrag;

            dropEvent.Invoke();
        }

        public void OnHold()
        {
            holdEvent.Invoke();
        }
        
        public void SnapToNpc(Transform npcTransform)
        {
            var npc = npcTransform.GetComponent<NpcBehavior>();
            switch (propType)
            {
                case PropType.Hat:
                    transform.position = npc.hatPos.position;
                    break;
                case PropType.Glasses:
                    transform.position = npc.glassesPos.position;
                    break;
                case PropType.NoSnap:
                    return;
            }
            
            rig.isKinematic = true;

            canBeInteractedWith = false;
        }

        public enum PropType
        {
            Hat,
            Glasses,
            NoSnap
        }

        public PropType propType;
        private float OnNpcCounter;
        public float timeTillCanBeGrabbed = 1f;
        public bool canBeInteractedWith = true;

        [Header("Rig")]
        
        public Rigidbody rig;
        
        public float interactDrag;
        public float dropDrag;

        [Header("Interact")] 
        
        public UnityEvent interactEvent;
        
        [Header("Drop")]
        
        public UnityEvent dropEvent;
        
        [Header("Hold")]
        
        public UnityEvent holdEvent;
    }
}