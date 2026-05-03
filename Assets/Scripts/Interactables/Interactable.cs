using System.Collections;
using TMPro;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public static PlayerMovement overworldMovement;
    protected SpriteRenderer spriteRenderer;
    protected virtual void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = true;
    }
    public abstract void Interact();
    public abstract void CloseDialog();
}