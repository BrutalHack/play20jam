using System;
using Supyrb;
using UnityEngine;

public class PlantWallController : MonoBehaviour, IInteractable
{
    private PlantWallDestroyedSignal _plantWallDestroyedSignal;
    private PlayerController _playerController;

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
        Signals.Get(out _plantWallDestroyedSignal);
    }

    public InteractionType Type => InteractionType.WaterIt;

    public bool Interact()
    {
        if (_playerController == null)
        {
            return false;
        }

        if (!_playerController.HasWater)
        {
            //TODO No Water to interact?
            return false;
        }

        _playerController.RemoveWater();
        _plantWallDestroyedSignal.Dispatch();
        gameObject.SetActive(false);
        return true;
    }
}