using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpressionText : MonoBehaviour {
  private Text _textField;

  private void Start() {
    _textField = GetComponent<Text>();
  }

  public void AddSymbol(string s) {
    _textField.text += s;
  }

  public void DeleteSymbol() {
    _textField.text = _textField.text.Remove(_textField.text.Length - 1);
  }

  public void ClearField() {
    _textField.text = "";
  }

  public bool isFieldEmpty() {
    return _textField.text.Length == 0 ? true : false;
  }

  public string GetExpressionText() {
    return _textField.text;
  }
}