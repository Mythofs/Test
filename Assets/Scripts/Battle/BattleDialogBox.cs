using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum BattleState
{
    Start, PlayerAction, PlayerMove, EnemyMove, Busy
}
public class BattleDialogBox : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI dialogText;
    [SerializeField] float lettersPerSecond;
    [SerializeField] GameObject actionSelector;
    [SerializeField] GameObject moveSelector;
    [SerializeField] GameObject moveDetails;
    [SerializeField] List<TextMeshProUGUI> actionTexts;
    [SerializeField] List<TextMeshProUGUI> moveTexts;
    [SerializeField] TextMeshProUGUI ppText;
    [SerializeField] TextMeshProUGUI typeText;
    [SerializeField] Color highlightedColor;
    public IEnumerator TypeDialog(string text)
    {
        dialogText.text = "";
        foreach(var letter in text)
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(1f/lettersPerSecond);
        }
    }
    public void EnableDialogText(bool enable)
    {
        dialogText.enabled = enable;
    }
    public void EnableActionSelector(bool enable)
    {
        actionSelector.SetActive(enable);
    }
    public void EnableMoveSelector(bool enable)
    {
        moveSelector.SetActive(enable);
        moveDetails.SetActive(enable);
    }
    public void UpdateActionSelection(int selectedAction)
    {
        for(int a = 0; a < actionTexts.Count; a++)
            if(a == selectedAction)
                actionTexts[a].color = highlightedColor;
            else
                actionTexts[a].color = Color.black;

    }
    public void UpdateMoveSelection(int selectedMove, Move move)
    {
        for(int a = 0; a < moveTexts.Count; a++)
            if(a == selectedMove)
                moveTexts[a].color = highlightedColor;
            else
                moveTexts[a].color = Color.black;
        ppText.text = "PP " + move.PP + "/" + move.mBase.PP;
        typeText.text = move.mBase.Type.ToString();

    }
    public void SetMoveNames(List<Move> moves)
    {
        for(int a = 0; a < moveTexts.Count; a++)
        {
            if(a < moves.Count)
                moveTexts[a].text = moves[a].mBase.name;
            else
                moveTexts[a].text = "-";
        }
    }
}
