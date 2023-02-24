using UnityEditor;

namespace Editor {
  public static class BobTheBuilder {
    [MenuItem("BUILDER/Build")]
    public static void ProductionBuild() {
      BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
      buildPlayerOptions.scenes = new[] {  "Assets/Scenes/SampleScene.unity"};
      buildPlayerOptions.locationPathName = "/builds/myApp.apk";
      buildPlayerOptions.target = BuildTarget.Android;
      buildPlayerOptions.options = BuildOptions.None;
      BuildPipeline.BuildPlayer(buildPlayerOptions);
    }
  }
}