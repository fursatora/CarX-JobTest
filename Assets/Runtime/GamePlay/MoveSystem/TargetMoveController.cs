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
        [SerializeField] private BaseTrajectorySettings trajectorySO;

        [Inject] private ScriptableTrajectoriesFactory _factory;

        private TargetReceiver _receiver;
        private IMoveTrajectory _trajectory;
        private IMover<TargetMoveController> _mover;
        private CancellationTokenSource _cts;

        public Transform Transform => transform;

        void Awake()
        {
            _receiver = GetComponent<TargetReceiver>();
            _trajectory = _factory.GetMethodBySettings(trajectorySO);
            _mover = new TargetMover<TargetMoveController>(_trajectory, _receiver);
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
