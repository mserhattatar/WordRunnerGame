using UnityEngine;

namespace PlayerScript
{
    public class PlayerAnimatorController : MonoBehaviour
    {
        private Animator _animator;
        private static readonly int PlayerRun = Animator.StringToHash("PlayerRun");
        private static readonly int PlayerStumble = Animator.StringToHash("PlayerStumble");

        private void Awake()
        {
            _animator = gameObject.GetComponent<Animator>();
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