using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteLayering : MonoBehaviour
{
    [SerializeField] SpriteRenderer sprite;
    [SerializeField] GameObject spriteObject;

    [SerializeField] int offset;
    public void AdjustLayer()
    {
        int value = (int)spriteObject.transform.position.z + offset;
        sprite.sortingOrder = -value;
    }

    private void Update()
    {
        AdjustLayer();
    }
}
