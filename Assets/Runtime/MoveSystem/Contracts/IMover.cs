using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Runtime.MoveSystem.Contracts
{
    public interface IMover<TMovedGameObject> where TMovedGameObject : Object, IMovingObject
    {
        UniTask StartMoveAsync(TMovedGameObject obj, CancellationToken token);
        void StopMove(CancellationTokenSource tokenSource);
    }
}