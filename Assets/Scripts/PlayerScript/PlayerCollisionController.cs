using System.Collections;
using UnityEngine;

namespace PlayerScript
{
    public class PlayerCollisionController : MonoBehaviour
    {
        private bool _isHitDoor;
        private void OnTriggerEnter(Collider other)
        {
            if (!_isHitDoor && other.gameObject.CompareTag("door"))
            {
                StartCoroutine(WaitForDoorCollision());
                PlayerAnimatorController.PlayerStumbleAnimationDelegate();
                WordManager.instance.FindLetterAndShow(other.gameObject.GetComponent<DoorController>().doorLetter);
                other.gameObject.GetComponent<DoorController>().SetDoor(false);
            }
        }

        private IEnumerator WaitForDoorCollision()
        {
            _isHitDoor = true;
            yield return new WaitForSeconds(1f);
            _isHitDoor = false;
        }
    }
}
