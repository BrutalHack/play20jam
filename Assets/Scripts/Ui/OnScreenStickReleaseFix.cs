using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Ui
{
    public class OnScreenStickReleaseFix : MonoBehaviour, IPointerUpHandler
    {
        private PlayerController _playerController;

        private void Awake()
        {
            _playerController = PlayerController.Instance;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _playerController.StopMovement();
        }
    }
}