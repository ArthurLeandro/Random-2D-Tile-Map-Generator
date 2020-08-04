using UnityEngine;

[RequireComponent(typeof(Camera))]
public class PixelPerfectCamera : MonoBehaviour {
  [SerializeField] static float pixelsToUnits = 1f;
  [SerializeField]  static float scale = 1f;
  [SerializeField] Vector2 nativeResolution = new Vector2(160,144);
  /// <summary>
  /// Awake is called when the script instance is being loaded.
  /// </summary>
  void Awake(){
      Camera camera = GetComponent<Camera>();
      if(camera.orthographic){
        var dir = Screen.height;
        var res = nativeResolution.y;
        scale = dir/res;
        pixelsToUnits *=  scale;
        camera.orthographicSize = (dir/2.0f)/pixelsToUnits;
      }
  }
}