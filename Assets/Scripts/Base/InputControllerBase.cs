namespace Base {
  public static class InputControllerBase {
    public static int ParenthesisCount { get; set; }

    public static void AddSymbolToString(string s) {
      if (CheckKey(s)) {
        if (ExpressionBase.Parts.Count > 0) {
          if (s == "+" || s == "*" || s == "/" || s == "-") {
            string part = ExpressionBase.Parts[ExpressionBase.Parts.Count - 1];
            if (ExpressionBase.IsPartEmpty() && (part == "+" || part == "*" || part == "/" || part == "-")) {
              ExpressionBase.DeleteFromExpression();
            }
          }
        }

        if (s == "+" || s == "*" || s == "/" || s == "-" || s == "(" || s == ")") {
          if (!ExpressionBase.IsPartEmpty()) {
            if (ExpressionBase.GetPartString().EndsWith(",")) {
              ExpressionBase.DeleteFromExpression();
            }
          }
        }

        if (s == ",") {
          if (ExpressionBase.IsPartEmpty()) {
            s = "0,";
          }
        }

        if (s == "(") {
          if (ExpressionBase.Parts.Count > 0 && ExpressionBase.IsPartEmpty()) {
            string part = ExpressionBase.Parts[ExpressionBase.Parts.Count - 1];
            if (double.TryParse(part, out double d)) {
              ExpressionBase.AddToExpressionString("*");
            }
          } else {
            if (!ExpressionBase.IsPartEmpty()) {
              ExpressionBase.AddToExpressionString("*");
            }
          }

          ParenthesisCount++;
        }

        if (ExpressionBase.Parts.Count > 0 && ExpressionBase.IsPartEmpty()) {
          if (ExpressionBase.Parts[ExpressionBase.Parts.Count - 1] == ")") {
            if (s != "+" && s != "-" && s != "*" && s != "/" && s != ")") {
              ExpressionBase.AddToExpressionString("*");
            }
          }
        }

        ExpressionBase.AddToExpressionString(s);
      }
    }

    private static bool CheckKey(string key) {
      bool isOk = true;

      if (ExpressionBase.IsPartEmpty() && ExpressionBase.Parts.Count == 0) {
        if (key == "+" || key == "*" || key == "/" || key == ")") {
          isOk = false;
          return isOk;
        }
      }


      if (key == "," && !ExpressionBase.IsPartEmpty()) {
        string s = ExpressionBase.GetPartString();
        if (s.IndexOf(",") != -1) {
          isOk = false;
          return isOk;
        }
      }

      if (key == "+" || key == "*" || key == "/" || key == ")") {
        if (ExpressionBase.IsPartEmpty() && ExpressionBase.Parts[ExpressionBase.Parts.Count - 1] == "(") {
          isOk = false;
          return isOk;
        }

        if (ExpressionBase.IsPartEmpty() && ParenthesisCount > 0 && ExpressionBase.Parts[ExpressionBase.Parts.Count - 1] == "-") {
          if (ExpressionBase.Parts[ExpressionBase.Parts.Count - 2] == "(") {
            isOk = false;
            return isOk;
          }
        }
      }

      if (key == ")") {
        if (ExpressionBase.IsPartEmpty()) {
          if (ParenthesisCount > 0) {
            string part = ExpressionBase.Parts[ExpressionBase.Parts.Count - 1];
            if (part == "+" || part == "-" || part == "*" || part == "/") {
              ExpressionBase.DeleteFromExpression();
            }
          } else {
            isOk = false;
            return isOk;
          }
        }

        if (ParenthesisCount > 0) {
          ParenthesisCount--;
        } else {
          isOk = false;
          return isOk;
        }
      }

      return isOk;
    }
  }
}