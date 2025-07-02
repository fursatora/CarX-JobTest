using System.Threading;
using Cysharp.Threading.Tasks;
using Runtime.MoveSystem.Contracts;
using Runtime.MoveSystem.MoveMethods;
using Runtime.MoveSystem.MoveSettings;
using Runtime.MoveSystem.MoveTrajectories;
using Runtime.TargetSystem;
using UnityEngine;
using VContainer;

namespace Runtime.GamePlay.MoveSystem
{
    [RequireComponent(typeof(TargetReceiver))]
    public class TargetMoveController : MonoBehaviour, IMovingObject
    {
        [SerializeField] private BaseTrajectorySettings moveSettings;
        
        private TargetReceiver _receiver;
        private IMover<TargetMoveController> _mover;
        private CancellationTokenSource _cts;

        public Transform Transform => transform;

        void Awake()
        {
            var trajectory = ScriptableTrajectoriesFactory.GetMethodBySettings(moveSettings);
            _receiver = GetComponent<TargetReceiver>();
            _mover = new TargetMover<TargetMoveController>(trajectory, _receiver);
        }

        void OnEnable()
        {
            _cts = new CancellationTokenSource();
            _mover.StartMoveAsync(this, _cts.Token);
        }

        void OnDisable()
        {
            _mover.StopMove(_cts);
            _cts = null;
        }
    }
}
