using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Runtime.TargetSystem;
using Runtime.TargetSystem.Contracts;
using UnityEngine;

namespace Runtime.GamePlay.RotationSystem
{
    [RequireComponent(typeof(TargetReceiver))]
    public class TargetRotationController : MonoBehaviour
    {
        [SerializeField] float rotationSpeed;

        private TargetReceiver _targetReceiver;
        private CancellationTokenSource _cancellationTokenSource;
        
        void Awake()
        {
            _targetReceiver = GetComponent<TargetReceiver>();
        }

        private void OnEnable()
        {
           StartRotating();
        }
        
        private void OnDisable()
        {
            StopRotating();
        }

        private void StartRotating()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            RotateToTarget(_cancellationTokenSource.Token).Forget();
        }

        private void StopRotating()
        {
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource.Dispose();
            _cancellationTokenSource = null;
        }

        private async UniTask RotateToTarget(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                ITargetProvider target = _targetReceiver.GetTarget();

                if (target != null)
                {
                    Vector3 dir = target.Position - transform.position;
                    dir.y = 0f;
                    if (dir.sqrMagnitude > float.Epsilon)
                    {
                        Quaternion desired = Quaternion.LookRotation(dir);
                        transform.rotation = Quaternion.RotateTowards(transform.rotation, desired, 
                            rotationSpeed * Time.deltaTime);
                    }
                }

                await UniTask.Yield(PlayerLoopTiming.Update, token);
            }
        }
    }
}