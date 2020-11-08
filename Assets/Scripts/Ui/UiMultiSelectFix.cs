using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Ui
{
    [RequireComponent(typeof(Selectable))]
    public class UiMultiSelectFix : MonoBehaviour, IPointerEnterHandler, IDeselectHandler
    {
        private Selectable _selectable;

        private void Awake()
        {
            _selectable = GetComponent<Selectable>();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!EventSystem.current.alreadySelecting)
            {
                EventSystem.current.SetSelectedGameObject(gameObject);
            }
        }

        public void OnDeselect(BaseEventData eventData)
        {
            _selectable.OnPointerExit(null);
        }
    }
}