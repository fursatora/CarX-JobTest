using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Runtime.TargetSystem.Contracts;
using UnityEngine;
using VContainer;

namespace Runtime.TargetSystem
{
    public class TargetReceiver : MonoBehaviour, ITargetReceiver
    {
        [SerializeField] private float reachedDistance;
        [SerializeField] private float trackingCheckTime;

        [Inject] ITargetSelector _selector;

        public event Action<ITargetReceiver> OnTargetReached;
        public bool IsReached => _isReached;

        private ITargetProvider _target;
        private CancellationTokenSource _tokenSource;
        private bool _isReached = false;

        private void OnEnable()
        {
            SetTarget();
            StartTrack(_tokenSource.Token).Forget();
        }

        private void OnDisable()
        {
           StopTrack();
        }

        public ITargetProvider GetTarget()
        {
            return _target;
        }

        private void SetTarget()
        {
            _tokenSource = new CancellationTokenSource();
            _target = _selector.GetNearest(transform.position);
        }

        private async UniTask StartTrack(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                _target = _selector.GetNearest(transform.position);

                if (_target != null)
                {
                    var currentDistance = Vector3.Distance(transform.position, _target.Position);
                    
                    if (_isReached == false && currentDistance <= reachedDistance)
                    {
                        _isReached = true;
                        OnTargetReached?.Invoke(this);
                        _target = null;
                    }
                    else if (_isReached && currentDistance > reachedDistance)
                    {
                        _isReached = false;
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