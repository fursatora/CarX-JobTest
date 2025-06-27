using Runtime.TargetSystem.Contracts;
using UnityEngine;
using VContainer;

namespace Runtime.TargetSystem
{
    public class TargetProvider : MonoBehaviour, ITargetProvider
    {
        public Vector3 Position => transform.position;
        
        [Inject] private ITargetSelector _selector;

        private void OnEnable()
        {
            _selector.Register(this);
        }

        private void OnDisable()
        {
            _selector.Unregister(this);
        }
    }
}