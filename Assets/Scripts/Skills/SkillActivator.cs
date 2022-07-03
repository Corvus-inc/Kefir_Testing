using UnityEngine;

namespace Skills
{
    public class SkillActivator
    {
        private readonly Animator _animator;

        private int _animSleepingId;
        private int _animLyingId;
        private int _animSittingId;
        private int _animReadyId;
        private int _animHipHopId;
        private int _animSalsaId;
        private int _animWaveId;

        public bool CanSleeping;
        public bool CanLying;
        public bool CanSitting;
        public bool CanReady;
        public bool CanHipHop;
        public bool CanSalsa;
        public bool CanWave;

        public SkillActivator(Animator animator)
        {
            _animator = animator;
            HashAnimations();
        }
        
        public bool Sleeping
        {
            get => _animator is not null && _animator.GetBool(_animSleepingId);
            set
            {
                if (CanSleeping) _animator.SetBool(_animSleepingId, value);
            }
        }

        public bool Lying
        {
            get => _animator is not null && _animator.GetBool(_animLyingId);
            set
            {
                if (CanLying) _animator?.SetBool(_animLyingId, value);
            }
        }

        public bool Sitting
        {
            get => _animator is not null && _animator.GetBool(_animSittingId);
            set
            {
                if (CanSitting) _animator.SetBool(_animSittingId, value);
            }
        }

        public bool Ready
        {
            get => _animator is not null && _animator.GetBool(_animReadyId);
            set
            {
                if (CanReady) _animator.SetBool(_animReadyId, value);
            }
        }

        public bool HipHop
        {
            get => _animator is not null && _animator.GetBool(_animHipHopId);
            set
            {
                if (CanHipHop) _animator.SetBool(_animHipHopId, value);
            }
        }

        public bool Salsa
        {
            get => _animator is not null && _animator.GetBool(_animSalsaId);
            set
            {
                if (CanSalsa) _animator.SetBool(_animSalsaId, value);
            }
        }

        public bool Wave
        {
            get => _animator is not null && _animator.GetBool(_animWaveId);
            set
            {
                if (CanWave) _animator.SetBool(_animWaveId, value);
            }
        }


        private void HashAnimations()
        {
            _animSleepingId = Animator.StringToHash("Sleeping");
            _animLyingId = Animator.StringToHash("Lying");
            _animSittingId = Animator.StringToHash("Sitting");
            _animReadyId = Animator.StringToHash("Ready");
            _animHipHopId = Animator.StringToHash("HipHop");
            _animSalsaId = Animator.StringToHash("Salsa");
            _animWaveId = Animator.StringToHash("Wave");
        }
    }
}