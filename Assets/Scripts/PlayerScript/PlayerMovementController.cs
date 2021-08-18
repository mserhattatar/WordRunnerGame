using UnityEngine;

namespace PlayerScript
{
    public class PlayerMovementController : JoystickManager
    {
        [HideInInspector] public bool stopPlayerMovement;
        private Transform _playerT;
        private float _forwardSpeed, _movementSpeed, _targetY, _timeCount;
        private bool _isPlayerRun;
        

        private void Start()
        {
            _playerT = transform;
            _forwardSpeed = 2.4f;
            _movementSpeed = 1.7f;
            _targetY = 0.24f;
            _timeCount = 0.0f;
        }

        private void FixedUpdate()
        {
            if (_isPlayerRun) StudentMovement();
        }

        private void StudentMovement()
        {
            var pPos = _playerT.position;

            if (stopPlayerMovement)
            {
                if ((transform.position.y < 0.3f)) return;
                pPos = new Vector3(pPos.x, 0.24f, pPos.z);
                _playerT.position = pPos;
                return;
            }

            StudentMovementRotatian(_playerT);
            var targetZ = pPos.z + _forwardSpeed;
            var targetX = pPos.x + JoystickHorizontal * _movementSpeed;
            if (targetX <= -4.5f)
                targetX = -4.5f;
            else if (targetX >= 4.5f)
                targetX = 4.5f;
            var direction = new Vector3(x: targetX, _targetY, targetZ);

            transform.position = Vector3.MoveTowards(pPos, direction, 15f * Time.deltaTime);
        }


        private void StudentMovementRotatian(Transform playerT)
        {
            var direction = playerT.position + Vector3.right * (JoystickHorizontal * 10f * _timeCount);
            var lookRotation = Quaternion.LookRotation(direction);
            playerT.rotation = Quaternion.Slerp(playerT.rotation, lookRotation, _timeCount);
            _timeCount += Time.deltaTime;
        }
    }
}