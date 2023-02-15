using System.Collections;
using Base;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace PlayMode {
  public class NewTestScript {
    public NewTestScript() {
      SceneManager.LoadScene("SampleScene");
    }

    #region broken tests

    [UnityTest]
    public IEnumerator _LetterTest() {
      //Заранее сломанный тест
      yield return new WaitForSeconds(0.1f);

      AssertData(new[] { "w", "/", "0" });
      Execute();
      Assert.AreEqual("Error", CalculateController.Result);
    }

    [UnityTest]
    public IEnumerator _OperandsTest() {
      //Заранее сломанный тест
      yield return new WaitForSeconds(0.1f);

      AssertData(new[] { "+", "-", "0,00000010" });
      Execute();
      Assert.AreNotEqual(0.00000010f, float.Parse(CalculateController.Result));
    }

    #endregion

    [UnityTest]
    public IEnumerator SumTest() {
      yield return new WaitForSeconds(0.1f);
      AssertData(new[] { "1", "+", "1" });
      Execute();

      Assert.AreEqual(2, int.Parse(CalculateController.Result));
    }

    [UnityTest]
    public IEnumerator DivineTest() {
      yield return new WaitForSeconds(0.1f);

      AssertData(new[] { "1", "/", "0" });
      Execute();
      Assert.AreEqual("Error", CalculateController.Result);

      AssertData(new[] { "1", "/", "1" });
      Execute();
      Assert.AreEqual(1, int.Parse(CalculateController.Result));
    }


    [UnityTest]
    public IEnumerator MultiplyTest() {
      yield return new WaitForSeconds(0.1f);

      AssertData(new[] { "1", "*", "0" });
      Execute();
      Assert.AreEqual(0, int.Parse(CalculateController.Result));

      AssertData(new[] { "1", "*", "1" });
      Execute();
      Assert.AreEqual(1, int.Parse(CalculateController.Result));
    }

    [UnityTest]
    public IEnumerator SubtractionTest() {
      yield return new WaitForSeconds(0.1f);

      AssertData(new[] { "0,00000011", "-", "0,00000010" });
      Execute();
      Assert.AreNotEqual(0, float.Parse(CalculateController.Result));

      AssertData(new[] { "0,00000011", "-", "0,00000010" });
      Execute();
      Assert.AreEqual(0.00000001f, float.Parse(CalculateController.Result));
    }


    [UnityTest]
    public IEnumerator OperandsTest() {
      yield return new WaitForSeconds(0.1f);

      AssertData(new[] { "-", "+", "*", "*" });
      Execute();
      Debug.Log(CalculateController.Result);
      Assert.AreEqual(true, float.TryParse(CalculateController.Result, out float result));
    }

    [UnityTest]
    public IEnumerator ZeroTest() {
      yield return new WaitForSeconds(0.1f);

      AssertData(new[] { "0", "0", "-", "0" });
      Execute();
      Assert.AreEqual(0, float.Parse(CalculateController.Result));
      
      AssertData(new[] { "0", "0", "*", "0","0","0","0","0","0" });
      Execute();
      Assert.AreEqual(0, float.Parse(CalculateController.Result));
      
      AssertData(new[] { "0", "0","0","0","0","0","0", "+", "0" });
      Execute();
      Assert.AreEqual(0, float.Parse(CalculateController.Result));
      
      AssertData(new[] { "0", "0", "/", "0" });
      Execute();
      Assert.AreEqual("Error", CalculateController.Result);
    }

    #region Executing Methods

    private static void Execute() {
      ExpressionBase.AddLastString();
      if (ExpressionBase.Parts.Count > 0) {
        ShowTextController._showTextController.SetExpressionToHistory(ExpressionBase.Parts);

        CalculateController.GetResult(ExpressionBase.Parts);
      }
    }

    private static void Clear() {
      ShowTextController._showTextController.ClearExpressionField();
      ShowTextController._showTextController.ClearHistoryField();
      ExpressionBase.ClearList();
      ExpressionBase.ClearPart();
      InputControllerBase.ParenthesisCount = 0;
    }

    private void AssertData(string[] list) {
      Clear();
      foreach (var obj in list) {
        InputControllerBase.AddSymbolToString(obj);
      }
    }

    #endregion
  }
}