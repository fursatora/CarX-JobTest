using System.Threading;
using Cysharp.Threading.Tasks;
using Runtime.MoveSystem.Contracts;
using Runtime.TargetSystem.Contracts;
using UnityEngine;

namespace Runtime.MoveSystem.MoveMethods
{
    public class TargetMover<TMovedGameObject> : IMover<TMovedGameObject> where TMovedGameObject : Object, IMovingObject
    {
        private readonly IMoveTrajectory _moveTrajectory;
        private readonly ITargetReceiver _targetReceiver;

        public TargetMover(IMoveTrajectory moveTrajectory, ITargetReceiver targetReceiver)
        {
            _moveTrajectory = moveTrajectory;
            _targetReceiver = targetReceiver;
        }

        public async UniTask StartMoveAsync(TMovedGameObject obj, CancellationToken token)
        {
            var transform = obj.Transform;

            while (!token.IsCancellationRequested)
            {
                if (_targetReceiver.IsReached)
                {
                    await PauseMoveAsync(token);
                    continue;
                }

                var targetProvider = _targetReceiver.GetTarget();

                if (targetProvider == null)
                {
                    await UniTask.Yield(PlayerLoopTiming.FixedUpdate, token);
                    continue;
                }

                var targetDistance = targetProvider.Position - transform.position;

                if (targetDistance.sqrMagnitude > Mathf.Epsilon && targetDistance != Vector3.zero)
                    transform.rotation = Quaternion.LookRotation(targetDistance);

                _moveTrajectory.MoveStep(transform, Time.deltaTime);

                await UniTask.Yield(PlayerLoopTiming.Update, token);
            }
        }

        public void StopMove(CancellationTokenSource tokenSource)
        {
            tokenSource.Cancel();
            tokenSource.Dispose();
        }

        private async UniTask PauseMoveAsync(CancellationToken token)
        {
            while (_targetReceiver.IsReached && !token.IsCancellationRequested)
                await UniTask.Yield(PlayerLoopTiming.Update, token);
        }
    }
}