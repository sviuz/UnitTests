using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowTextController : MonoBehaviour {
  public static ShowTextController _showTextController;

  [SerializeField]
  private ExpressionText _expressionText;

  [SerializeField]
  private Text _historyField;

  private void Awake() {
    if (_showTextController == null) {
      _showTextController = this;
    } else {
      Destroy(gameObject);
    }

    DontDestroyOnLoad(gameObject);
  }

  public void AddSymbolToExpression(string s) {
    _expressionText.AddSymbol(s);
  }

  public void DeleteSymbolFromExpression() {
    _expressionText.DeleteSymbol();
  }

  public void ClearExpressionField() {
    _expressionText.ClearField();
  }

  public bool IsExpressionFieldEmpty() {
    return _expressionText.isFieldEmpty();
  }

  public void SetExpressionToHistory(List<string> list) {
    ClearHistoryField();

    foreach (string s in list) {
      _historyField.text += s;
    }
  }

  public void ClearHistoryField() {
    _historyField.text = "";
  }
}