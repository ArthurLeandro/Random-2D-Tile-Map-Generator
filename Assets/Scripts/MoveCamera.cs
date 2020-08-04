using UnityEngine;

public class MoveCamera : MonoBehaviour {
  [SerializeField] float speed=4f;
  Vector3 startPosition;
  bool moving;
  /// <summary>
  /// LateUpdate is called every frame, if the Behaviour is enabled.
  /// It is called after all Update functions have been called.
  /// </summary>
  void LateUpdate(){
    if(Input.GetMouseButtonDown(1)){
      startPosition = Input.mousePosition;
      moving = true;
    }
    if(Input.GetMouseButtonUp(1) && moving){
      moving = false;
    }
    if(moving){
      Vector3 position = Camera.main.ScreenToViewportPoint(Input.mousePosition-startPosition);
      Vector3 move = new Vector3(position.x*speed,position.y*speed,0);
      this.transform.Translate(move,Space.Self);
    }
  }
}