using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Scripts.Crafting
{
    public class CraftingMovement : MonoBehaviour
    {
        private PlayerControl control;
        private Vector2 input;
        private Action<InputAction.CallbackContext> onCancelInput;
        private int index = 0;
        private float delay = 0.1f;
        private float lastMove = 0;
        [SerializeField] private TextMeshProUGUI sideItemName;
        [SerializeField] private Image sideItemSprite;
        [SerializeField] private List<Image> materialImages;
        [SerializeField] private List<TextMeshProUGUI> materialTexts;
        [SerializeField] private List<Image> craftingOptions;
        private void Awake()
        {
            control = new PlayerControl();
            onCancelInput = ctx =>
            {
                input = Vector2.zero;
            };
            transform.position = craftingOptions[0].transform.position;
        }
        private void OnEnable()
        {
            control.Enabled();
            control.UI.Move.performed += 
        }
    }
}
