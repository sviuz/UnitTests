using Base;
using NUnit.Framework;

namespace EditorTests {
  public class GeneralEditMode {
    public GeneralEditMode() {
      TestManager.IsTest = true;
    }

    [Test]
    public void GeneralEditModeSimplePasses() {
      AssertData(new[] { "2", "+", "2" });
      Execute();
      Assert.AreEqual(4, int.Parse(CalculateController.Result));
    }

    private static void Execute() {
      ExpressionBase.AddLastString();
      if (ExpressionBase.Parts.Count > 0) {
        CalculateController.GetResult(ExpressionBase.Parts);
      }
    }

    [Test]
    public void DivineTest() {
      AssertData(new[] { "1", "/", "0" });
      Execute();
      Assert.AreEqual("Error", CalculateController.Result);

      AssertData(new[] { "1", "/", "1" });
      Execute();
      Assert.AreEqual(1, int.Parse(CalculateController.Result));
    }


    [Test]
    public void MultiplyTest() {
      AssertData(new[] { "1", "*", "0" });
      Execute();
      Assert.AreEqual(0, int.Parse(CalculateController.Result));

      AssertData(new[] { "1", "*", "1" });
      Execute();
      Assert.AreEqual(1, int.Parse(CalculateController.Result));
    }

    [Test]
    public void SubtractionTest() {
      AssertData(new[] { "0,00000011", "-", "0,00000010" });
      Execute();
      Assert.AreNotEqual(0, float.Parse(CalculateController.Result));

      AssertData(new[] { "0,00000011", "-", "0,00000010" });
      Execute();
      Assert.AreEqual(0.00000001f, float.Parse(CalculateController.Result));
    }

    [Test]
    public void ZeroTest() {
      AssertData(new[] { "0", "0", "-", "0" });
      Execute();
      Assert.AreEqual(0, float.Parse(CalculateController.Result));

      AssertData(new[] { "0", "0", "*", "0", "0", "0", "0", "0", "0" });
      Execute();
      Assert.AreEqual(0, float.Parse(CalculateController.Result));

      AssertData(new[] { "0", "0", "0", "0", "0", "0", "0", "+", "0" });
      Execute();
      Assert.AreEqual(0, float.Parse(CalculateController.Result));

      AssertData(new[] { "0", "0", "/", "0" });
      Execute();
      Assert.AreEqual("Error", CalculateController.Result);
    }

    #region Execution Methods

    private static void AssertData(string[] list) {
      Clear();
      foreach (var obj in list) {
        InputControllerBase.AddSymbolToString(obj);
      }
    }

    private static void Clear() {
      ExpressionBase.ClearList();
      ExpressionBase.ClearPart();
      InputControllerBase.ParenthesisCount = 0;
    }

    #endregion
  }
}