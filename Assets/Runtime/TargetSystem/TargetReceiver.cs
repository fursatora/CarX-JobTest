using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Runtime.TargetSystem.Contracts;
using UnityEngine;

namespace Runtime.TargetSystem
{
    public class TargetReceiver : MonoBehaviour, ITargetReceiver
    {
        [SerializeField] private float reachedDistance;
        [SerializeField] private float trackingCheckTime;
        
        private ITargetProvider _target;
        private CancellationTokenSource _tokenSource;
        

        private void OnEnable()
        {
            SetTarget();
            StartTrack(_tokenSource.Token).Forget();
        }

        private void OnDisable()
        {
            StopTrack();
        }

        public event Action<ITargetReceiver> OnTargetReached;
        public bool IsReached { get; private set; }

        public ITargetProvider GetTarget()
        {
            return _target;
        }

        private void SetTarget()
        {
            _tokenSource = new CancellationTokenSource();
            _target = TargetSelector.GetNearest(transform.position);
        }

        private async UniTask StartTrack(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                _target = TargetSelector.GetNearest(transform.position);

                if (_target != null)
                {
                    var currentDistance = Vector3.Distance(transform.position, _target.Position);

                    if (IsReached == false && currentDistance <= reachedDistance)
                    {
                        IsReached = true;
                        OnTargetReached?.Invoke(this);
                        _target = null;
                    }
                    else if (IsReached && currentDistance > reachedDistance)
                    {
                        IsReached = false;
                    }
                }

                await UniTask.Delay(TimeSpan.FromSeconds(trackingCheckTime), cancellationToken: token);
            }
        }

        private void StopTrack()
        {
            _tokenSource.Cancel();
            _tokenSource.Dispose();
            _target = null;
        }
    }
}