using System.Threading;
using Cysharp.Threading.Tasks;
using Runtime.MoveSystem.Contracts;
using UnityEngine;

namespace Runtime.MoveSystem.MoveMethods
{
    public class SimpleMover<TMovedGameObject> : IMover<TMovedGameObject> where TMovedGameObject : Object, IMovingObject
    {
        private readonly IMoveTrajectory _moveTrajectory;

        public SimpleMover(IMoveTrajectory moveTrajectory)
        {
            _moveTrajectory = moveTrajectory;
        }

        public async UniTask StartMoveAsync(TMovedGameObject obj, CancellationToken token)
        {
            var transform = obj.Transform;

            while (!token.IsCancellationRequested)
            {
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