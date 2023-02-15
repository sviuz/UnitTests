using System;
using System.Collections.Generic;

namespace Base {
  public static class ExpressionBase {
    public static List<string> Parts { get; set; }

    private static string _part;
    static ExpressionBase() {
      Parts = new List<string>();
      _part = String.Empty;
    }
    
    public static string GetPartString() {
      return _part;
    }

    public static List<string> GetExpressions() {
      return Parts;
    }

    public static void ClearList() {
      Parts.Clear();
    }

    public static void ClearPart() {
      _part = "";
    }

    public static void AddExpression(string s) {
      Parts.Add(s);
    }
    
    public static void AddToExpressionString(string s) {
      if (s == "+" || s == "-" || s == "*" || s == "/" || s == "(" || s == ")") {
        if (!IsPartEmpty()) {
          AddExpression(_part);
        }

        AddExpression(s);
        ClearPart();
      } else {
        _part += s;
      }

      ShowExpression(s);
    }
    
    public static bool IsPartEmpty() {
      return _part.Length == 0 ? true : false;
    }

    private static void ShowExpression(string s) {
      if (!TestManager.IsTest) {
        ShowTextController._showTextController.AddSymbolToExpression(s);
      }
    }
    
    public static void DeleteFromExpression() {
      if (IsPartEmpty()) {
        if (Parts.Count > 0) {
          if (Parts[0] == "Error") {
            ShowTextController._showTextController.ClearExpressionField();
            Parts.Clear();
          } else {
            _part = Parts[Parts.Count - 1];
            if (_part == ")") {
              InputControllerBase.ParenthesisCount++;
            }

            if (_part == "(") {
              InputControllerBase.ParenthesisCount--;
            }

            _part = _part.Remove(_part.Length - 1);
            Parts.RemoveAt(Parts.Count - 1);
          }
        }
      } else {
        _part = _part.Remove(_part.Length - 1);
      }

      if (!ShowTextController._showTextController.IsExpressionFieldEmpty()) {
        ShowTextController._showTextController.DeleteSymbolFromExpression();
      }
    }
    
    public static void AddLastString() {
      if (!IsPartEmpty()) {
        if (_part.EndsWith(",")) {
          _part = _part.Remove(_part.Length - 1);

          ShowTextController._showTextController.DeleteSymbolFromExpression();
        }

        AddExpression(_part);
        ClearPart();
      } else {
        if (Parts.Count > 0) {
          string s = Parts[Parts.Count - 1];
          if (s == "+" || s == "-" || s == "*" || s == "/" || s == "(") {
            if (s == "(") {
              InputControllerBase.ParenthesisCount--;
            }

            Parts.RemoveAt(Parts.Count - 1);

            ShowTextController._showTextController.DeleteSymbolFromExpression();
          }
        }
      }

      if (InputControllerBase.ParenthesisCount > 0) {
        for (int i = InputControllerBase.ParenthesisCount; i > 0; i--) {
          AddToExpressionString(")");
        }

        InputControllerBase.ParenthesisCount = 0;
      }
    }
  }
}