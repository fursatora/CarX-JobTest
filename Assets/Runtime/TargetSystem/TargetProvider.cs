using Runtime.TargetSystem.Contracts;
using UnityEngine;
using VContainer;

namespace Runtime.TargetSystem
{
    public class TargetProvider : MonoBehaviour, ITargetProvider
    {
        public Vector3 Position => transform.position;

        [Inject] ITargetSelector _selector;

        void OnEnable()
        {
            _selector.Register(this);
        }

        void OnDisable()
        {
            _selector.Unregister(this);
        }
    }
}