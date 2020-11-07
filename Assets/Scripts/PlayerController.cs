using UnityEngine;
using UnityEngine.InputSystem;

namespace UnityTemplateProjects
{
    public class PlayerController : MonoBehaviour
    {
        public void OnMove(InputAction.CallbackContext context)
        {
            Debug.Log("Move" + context);
        }

        public void OnAction(InputAction.CallbackContext context)
        {
            Debug.Log("Action + " + context.phase);
        }

        public void OnMenu(InputAction.CallbackContext context)
        {
            //TODO Menu
        }
    }
}