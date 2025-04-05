using UnityEngine;

namespace Tools_and_Scripts
{
    public class ImpulseForce2D : MonoBehaviour
    {
        [SerializeField] private Vector2 force;
        
        private void Start()
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.AddForce(force, ForceMode2D.Impulse);
        }
    }
}
