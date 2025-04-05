using UnityEngine;

namespace VictoryText
{
    public class EndText : MonoBehaviour
    {
        [SerializeField] private SqueezeAndStretch first;
        [SerializeField] private SqueezeAndStretch second;
        [SerializeField] private SqueezeAndStretch third;
        
        private void Start()
        {
            first.Trigger();
            second.Trigger();
            third.Trigger();
        }
    }
}
