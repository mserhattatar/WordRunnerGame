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
                WordManager.İnstance.FindLetterAndShow(other.gameObject);
            }
        }

        private IEnumerator WaitForDoorCollision()
        {
            _isHitDoor = true;
            yield return new WaitForSeconds(0.2f);
            _isHitDoor = false;
        }
    }
}