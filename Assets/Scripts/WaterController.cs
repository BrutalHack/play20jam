using System;
using Supyrb;
using UnityEngine;

public class WaterController : MonoBehaviour, IInteractable
{
    private WaterCollectedSignal _waterCollectedSignal;
    private PlayerController _playerController;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(UnityTags.Player))
        {
            return;
        }

        _playerController.SetInteractable(this);
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

    public void Interact()
    {
        if (_playerController == null)
        {
            return;
        }

        _playerController.RemoveInteractable(this);

        _waterCollectedSignal.Dispatch();
        gameObject.SetActive(false);
    }
}