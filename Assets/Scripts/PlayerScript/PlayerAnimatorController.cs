using System;
using UnityEngine;

namespace PlayerScript
{
    public class PlayerAnimatorController : MonoBehaviour
    {
        public delegate void PlayerAnimatorDelegate();

        public static PlayerAnimatorDelegate PlayerRunAnimationDelegate;
        public static PlayerAnimatorDelegate PlayerStumbleAnimationDelegate;
        
        
        private Animator _animator;
        private static readonly int PlayerRun = Animator.StringToHash("PlayerRun");
        private static readonly int PlayerStumble = Animator.StringToHash("PlayerStumble");

        private void Awake()
        {
            _animator = gameObject.GetComponent<Animator>();
            PlayerRunAnimationDelegate += PlayerRunAnimationSetActive;
            PlayerStumbleAnimationDelegate += PlayerStumbleAnimationActive;
        }

        private void Start()
        {
            PlayerRunAnimationDelegate();
        }

        private void PlayerRunAnimationSetActive()
        {
            PlayerRunAnimation(true);
        }

        private void PlayerRunAnimation(bool setActive)
        {
            _animator.SetBool(PlayerRun, setActive);
        }

        private void PlayerStumbleAnimationActive()
        {
            _animator.SetBool(PlayerStumble, true);
        }

        public void PlayerStumbleAnimationPassive()
        {
            //using by animator event system 
            _animator.SetBool(PlayerStumble, false);
        }
    }
}