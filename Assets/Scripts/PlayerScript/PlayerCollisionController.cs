using UnityEngine;

namespace PlayerScript
{
    public class PlayerCollisionController : MonoBehaviour
    {
        public delegate void PlayerCollisionDelegate();
        public static PlayerCollisionDelegate DoorCollisionDelegate;
        private void OnCollisionEnter(Collision other)
        {
            switch (other.gameObject.tag)
            {
                case "door":
                    DoorCollisionDelegate();
                    break;
            }
        }
    }
}
