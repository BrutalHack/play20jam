using UnityEngine;
using Random = UnityEngine.Random;

public class StoneController : MonoBehaviour
{
    [SerializeField] private float _offSetSize;

    private void Awake()
    {
        var spriteRendererTransform = GetComponentInChildren<SpriteRenderer>().transform;
        var transformPosition = spriteRendererTransform.position;
        var zOffset = (Random.value - 0.5f) * _offSetSize;
        spriteRendererTransform.position =
            new Vector3(transformPosition.x, transformPosition.y, transformPosition.z += zOffset);
    }
}