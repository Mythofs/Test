using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class BattleSystem : MonoBehaviour
{
    [SerializeField] BattleUnit playerUnit;
    [SerializeField] BattleHUD playerHUD;
    [SerializeField] BattleUnit enemyUnit;
    [SerializeField] BattleHUD enemyHUD;
    [SerializeField] BattleDialogBox dialogBox;
    BattleState state;
    private int currentAction;
    private int currentMove;
    private InputAction downAction = new InputAction(type: InputActionType.Button, binding: "<Keyboard>/downArrow");
    private InputAction upAction = new InputAction(type: InputActionType.Button, binding: "<Keyboard>/upArrow");

    private InputAction leftAction = new InputAction(type: InputActionType.Button, binding: "<Keyboard>/leftArrow");
    private InputAction rightAction = new InputAction(type: InputActionType.Button, binding: "<Keyboard>/rightArrow");

    private InputAction cAction = new InputAction(type: InputActionType.Button, binding: "<Keyboard>/c");
    void Start()
    {
        StartCoroutine(SetupBattle());
    }
    private void OnEnable()
    {
        downAction.Enable();
        upAction.Enable();
        leftAction.Enable();
        rightAction.Enable();
        cAction.Enable();
    }
    private void OnDiable()
    {
        downAction.Disable();
        upAction.Enable();
        leftAction.Disable();
        rightAction.Enable();
        cAction.Enable();
    }
    public IEnumerator SetupBattle()
    {
        playerUnit.setup();
        playerHUD.setData(playerUnit.Pokemon);
        enemyUnit.setup();
        enemyHUD.setData(enemyUnit.Pokemon);
        dialogBox.SetMoveNames(playerUnit.Pokemon.Moves);
        yield return dialogBox.TypeDialog($"A wild {enemyUnit.Pokemon.Base.Name} appeared.");
        yield return new WaitForSeconds(1f);
        PlayerAction();
    }
    void PlayerAction()
    {
        state = BattleState.PlayerAction;
        StartCoroutine(dialogBox.TypeDialog("Choose an action"));
        dialogBox.EnableActionSelector(true);
    }
    void PlayerMove()
    {
        state = BattleState.PlayerMove;
        dialogBox.EnableActionSelector(false);
        dialogBox.EnableDialogText(false);
        dialogBox.EnableMoveSelector(true);
    }
    private void Update()
    {
        if(state == BattleState.PlayerAction)
            HandleActionSelection();
        if(state == BattleState.PlayerMove)
            HandleMoveSelection();
    }
    void HandleActionSelection()
    {
        if(downAction.WasPressedThisFrame() && currentAction < 1)
            currentAction++;
        else if(upAction.WasPressedThisFrame() && currentAction > 0)
            currentAction--;
        dialogBox.UpdateActionSelection(currentAction);
        if(cAction.WasPressedThisFrame())
            if(currentAction == 0)
            {
                PlayerMove();
            }
            else if(currentAction == 1)
            {
                //run
            }
    }
    void HandleMoveSelection()
    {
        if(rightAction.WasPressedThisFrame() && currentMove < playerUnit.Pokemon.Moves.Count - 1)
            currentMove++;
        else if(leftAction.WasPressedThisFrame() && currentMove > 0)
            currentMove--;
        else if(upAction.WasPressedThisFrame() && currentMove > 1)
            currentMove -= 2;
        else if(downAction.WasPressedThisFrame() && currentMove < playerUnit.Pokemon.Moves.Count - 2)
            currentMove += 2;
        dialogBox.UpdateMoveSelection(currentMove, playerUnit.Pokemon.Moves[currentMove]);
    }
}
