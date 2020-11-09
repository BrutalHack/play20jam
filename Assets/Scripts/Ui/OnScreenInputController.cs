using UnityEngine;
using UnityEngine.InputSystem;

namespace Ui
{
    public class OnScreenInputController : MonoBehaviour
    {
        private void Start()
        {
            if (Touchscreen.current != null && Touchscreen.current.enabled)
            {
                Debug.Log("I can has touch");
            }
            else
            {
                Debug.Log("I can has no touch");
                gameObject.SetActive(false);
            }
        } 
    }
}