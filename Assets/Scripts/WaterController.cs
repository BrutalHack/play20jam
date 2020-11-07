using System;
using Supyrb;
using UnityEngine;

public class WaterController : MonoBehaviour, IInteractable
{
    private WaterCollectedSignal _waterCollectedSignal;
    private PlayerController _playerController;

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
        _playerController = GameObject.FindWithTag(UnityTags.Player).GetComponent<PlayerController>();
        Signals.Get(out _waterCollectedSignal);
    }

    public bool Interact()
    {
        if (_playerController == null)
        {
            return false;
        }
        
        _waterCollectedSignal.Dispatch();
        gameObject.SetActive(false);
        return true;
    }
}