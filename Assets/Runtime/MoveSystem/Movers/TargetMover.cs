using System.Threading;
using Cysharp.Threading.Tasks;
using Runtime.MoveSystem.Contracts;
using Runtime.TargetSystem.Contracts;
using UnityEngine;

namespace Runtime.MoveSystem.MoveMethods
{
    public class TargetMover<TMovedGameObject> : IMover<TMovedGameObject> where TMovedGameObject : Object, IMovingObject
    {
        readonly IMoveTrajectory _moveTrajectory;
        readonly ITargetProvider _targetProvider;

        public TargetMover(IMoveTrajectory moveTrajectory, ITargetProvider targetProvider)
        {
            _moveTrajectory = moveTrajectory;
            _targetProvider = targetProvider;
        }

        public async UniTask StartMoveAsync(TMovedGameObject obj, CancellationToken token)
        {
            var transform = obj.Transform;

            while (!token.IsCancellationRequested)
            {
                var targetDistance = _targetProvider.Position - transform.position;
                if (targetDistance.sqrMagnitude > Mathf.Epsilon)
                {
                    transform.rotation = Quaternion.LookRotation(targetDistance.normalized);
                }

                _moveTrajectory.MoveStep(transform, Time.deltaTime);

                await UniTask.Yield(PlayerLoopTiming.FixedUpdate, token);
            }
        }

        public void StopMove(CancellationTokenSource tokenSource)
        {
            tokenSource.Cancel();
            tokenSource.Dispose();
        }
    }
}