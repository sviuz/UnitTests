using Base;
using UnityEngine;

public class ButtonKey : MonoBehaviour {
  public void AddKeyToExpression(string key) {
    InputControllerBase.AddSymbolToString(key);
  }

  public void GetResult() {
    ExpressionBase.AddLastString();
    if (ExpressionBase.Parts.Count > 0) {
      ShowTextController._showTextController.SetExpressionToHistory(ExpressionBase.Parts);

      CalculateController.GetResult(ExpressionBase.Parts);
    }
  }

  public void DeleteSymbol() {
    ExpressionBase.DeleteFromExpression();
  }

  public void ClearAllField() {
    ShowTextController._showTextController.ClearExpressionField();
    ShowTextController._showTextController.ClearHistoryField();
    ExpressionBase.ClearList();
    ExpressionBase.ClearPart();
    InputControllerBase.ParenthesisCount = 0;
  }
}