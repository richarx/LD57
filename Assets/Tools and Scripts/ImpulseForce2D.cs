using InspectorAttribute;
using UnityEngine;

namespace Tools_and_Scripts
{
    public class ImpulseForce2D : MonoBehaviour
    {
        [ConditionalHide("!" + nameof(onStart))] 
        [SerializeField] private Vector2 force;

        public bool onStart = true;
        
        private void Start()
        {
            if (onStart)
                Trigger(force);
        }

        public void Trigger(Vector2 impulseForce)
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.AddForce(impulseForce, ForceMode2D.Impulse);
        }
    }
}
