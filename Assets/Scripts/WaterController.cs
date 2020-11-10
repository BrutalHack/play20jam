using System;
using Supyrb;
using UnityEngine;
using System.Collections;

public class WaterController : MonoBehaviour, IInteractable
{
    private WaterCollectedSignal _waterCollectedSignal;
    private PlayerController _playerController;

    [SerializeField]
    private float timer = 1.0f;

    public InteractionType Type
    {
        get { return InteractionType.PickUp; }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(UnityTags.Player))
        {
            return;
        }

        _playerController.AddInteractable(this);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag(UnityTags.Player))
        {
            return;
        }

        _playerController.RemoveInteractable(this);
    }

    private void Start()
    {
        _playerController = PlayerController.Instance;
        Signals.Get(out _waterCollectedSignal);
    }

    public bool Interact()
    {
        if (_playerController == null || _playerController.HasWater)
        {
            return false;
        }
        
        _waterCollectedSignal.Dispatch();
        StartCoroutine(waitForTimer());
        return true;
    }

    IEnumerator waitForTimer()
    {
        yield return new WaitForSeconds(timer);
        gameObject.SetActive(false);
    }
}