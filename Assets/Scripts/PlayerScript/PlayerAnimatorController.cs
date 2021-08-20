using System;
using UnityEngine;

namespace PlayerScript
{
    public class PlayerAnimatorController : MonoBehaviour
    {
        public delegate void PlayerAnimatorDelegate();

        public static PlayerAnimatorDelegate PlayerStumbleAnimationDelegate;


        private Animator _animator;
        private static readonly int PlayerRun = Animator.StringToHash("PlayerRun");
        private static readonly int PlayerStumble = Animator.StringToHash("PlayerStumble");

        private void Awake()
        {
            _animator = gameObject.GetComponent<Animator>();
        }

        private void OnEnable()
        {
            GameManager.StartGameDelegate += PlayerRunAnimationSetActive;
            
            GameManager.GameOverDelegate += PlayerRunAnimationSetPassive;
            
            PlayerMovementController.StopPlayerMovementDelegate += PlayerRunAnimationSetPassive;
            
            GameManager.LevelCompletedDelegate += PlayerRunAnimationSetPassive;
            
            PlayerStumbleAnimationDelegate += PlayerStumbleAnimationActive;
        }

        private void PlayerRunAnimationSetActive()
        {
            _animator.SetBool(PlayerRun, true);
        }

        private void PlayerRunAnimationSetPassive()
        {
            _animator.SetBool(PlayerRun, false);
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