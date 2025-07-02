using System.Threading;
using Cysharp.Threading.Tasks;
using Runtime.MoveSystem.Contracts;
using Runtime.MoveSystem.MoveMethods;
using Runtime.MoveSystem.MoveSettings;
using Runtime.MoveSystem.MoveTrajectories;
using UnityEngine;
using VContainer;

namespace Runtime.GamePlay.MoveSystem
{
    public class SimpleMoveController : MonoBehaviour, IMovingObject
    {
        [SerializeField] private BaseTrajectorySettings moveSettings;
        
        public Transform Transform => transform;

        private IMover<SimpleMoveController> _mover;
        private CancellationTokenSource _cancellationTokenSource;
        
        private void Awake()
        {
            var trajectory = ScriptableTrajectoriesFactory.GetMethodBySettings(moveSettings);
            _mover = new SimpleMover<SimpleMoveController>(trajectory);
        }

        private void OnEnable()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            _mover.StartMoveAsync(this, _cancellationTokenSource.Token).Forget();
        }

        private void OnDisable()
        {
            _mover.StopMove(_cancellationTokenSource);
        }
    }
}