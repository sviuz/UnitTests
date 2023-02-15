using System.Collections.Generic;
using UnityEngine;
using Base;

public static class CalculateController {
  private static List<string> _partsOfExpression = new List<string>();
  public static string Result;

  public static void GetResult(List<string> list) {
    _partsOfExpression = CreateCopyOfList(list);

    Result = Calculate(_partsOfExpression);
    if (!TestManager.IsTest) {
      ShowTextController._showTextController.ClearExpressionField();
      ShowTextController._showTextController.AddSymbolToExpression(Result);
    }

    ExpressionBase.ClearList();
    ExpressionBase.AddExpression(Result);
  }

  private static string Calculate(List<string> list) {
    return CheckForBrackets(list) ? CalculateArrayWhithBrackets(list) : CalculateArray(list);
  }

  private static bool CheckForBrackets(List<string> list) {
    return list.Contains("(");
  }

  private static List<string> CreateCopyOfList(List<string> list) {
    List<string> l = new List<string>();
    foreach (string s in list) {
      l.Add(s);
    }

    return l;
  }

  private static string CalculateArray(List<string> list) {
    int functionCount = 0;

    foreach (string s in list) {
      if (s == "*" || s == "/" || s == "+" || s == "-") {
        functionCount++;
      }
    }

    int indexCount = -1;

    for (int i = functionCount; i > 0; i--) {
      while (list.Contains("*") || list.Contains("/")) {
        foreach (string s in list) {
          if (s == "*" || s == "/") {
            double a = double.Parse(list[indexCount]);
            double b = double.Parse(list[indexCount + 2]);
            double res;
            if (s == "*") {
              res = Multiply(a, b);
            } else {
              if (b == 0) {
                return "Error";
              }

              res = Divide(a, b);
            }

            list.RemoveAt(indexCount + 2);
            list.RemoveAt(indexCount + 1);
            list.RemoveAt(indexCount);
            list.Insert(indexCount, res.ToString());
            functionCount--;
            indexCount = -1;
            break;
          } else {
            indexCount++;
          }
        }
      }

      indexCount = -1;

      foreach (string s in list) {
        if (s == "+" || s == "-") {
          double a = double.Parse(list[indexCount]);
          double b = double.Parse(list[indexCount + 2]);
          double res;
          if (s == "+") {
            res = Add(a, b);
          } else {
            res = Subtract(a, b);
          }

          list.RemoveAt(indexCount + 2);
          list.RemoveAt(indexCount + 1);
          list.RemoveAt(indexCount);
          list.Insert(indexCount, res.ToString());
          functionCount--;
          indexCount--;
          break;
        } else {
          indexCount++;
        }
      }
    }

    return list[0];
  }

  private static string CalculateArrayWhithBrackets(List<string> list) {
    List<string> bracketsList = new List<string>();

    int firstBracketIndex = -1;
    int lastBracketIndex = -1;
    int openBracketsCount = 0;
    int elementIndex = 0;
    int bracketsCount = 0;

    foreach (string s in list) {
      if (s == "(") {
        bracketsCount++;
      }
    }

    for (int i = bracketsCount; i > 0; i--) {
      string exp = "";
      foreach (string s in list) {
        exp += s;
      }

      Debug.Log(exp);
      foreach (string s in list) {
        if (s == "(") {
          openBracketsCount++;
          if (firstBracketIndex == -1) {
            firstBracketIndex = elementIndex;
          }
        }

        if (s == ")") {
          openBracketsCount--;
          if (openBracketsCount == 0) {
            lastBracketIndex = elementIndex;

            for (int j = firstBracketIndex + 1; j < lastBracketIndex; j++) {
              bracketsList.Add(list[j]);
            }

            bool isNegative = false;

            if (bracketsList[0] == "-") {
              if (bracketsList[1] == "(") {
                isNegative = true;
                bracketsList.RemoveAt(0);
              } else {
                bracketsList[0] = bracketsList[0] + bracketsList[1];
                bracketsList.RemoveAt(1);
              }
            }

            string res = "";
            if (bracketsList.Count == 1) {
              res = bracketsList[0];
            } else {
              if (bracketsList.Count == 2) {
                res = bracketsList[0] + bracketsList[1];
              } else {
                res = Calculate(bracketsList);

                if (!double.TryParse(res, out double d)) {
                  return res;
                }
              }
            }

            if (isNegative) {
              double d = double.Parse(res);
              d = d * -1;
              res = d.ToString();
            }

            list.RemoveRange(firstBracketIndex, lastBracketIndex - firstBracketIndex + 1);

            if (list.Count <= firstBracketIndex) {
              list.Add(res);
            } else {
              list.Insert(firstBracketIndex, res);
            }

            firstBracketIndex = -1;
            lastBracketIndex = -1;
            elementIndex = 0;
            bracketsList.Clear();
            break;
          }
        }

        elementIndex++;
      }
    }

    return list.Count > 1 ? CalculateArray(list) : list[0];
  }

  private static double Multiply(double first, double second) {
    return first * second;
  }

  private static double Divide(double first, double second) {
    return first / second;
  }

  private static double Add(double first, double second) {
    return first + second;
  }

  private static double Subtract(double first, double second) {
    return first - second;
  }
}