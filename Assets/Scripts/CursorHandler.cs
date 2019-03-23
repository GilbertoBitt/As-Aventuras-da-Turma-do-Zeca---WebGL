using JetBrains.Annotations;
using UnityEngine;

public class CursorHandler : OverridableMonoBehaviour {

    private bool hasPressed;
    public Texture2D normalMouseState;
    public Texture2D highlightMouseState;

    public override void UpdateMe() {
        
        if(Input.GetMouseButton(0) && !hasPressed) {
            hasPressed = true;
            Cursor.SetCursor(highlightMouseState,Vector2.zero,CursorMode.Auto);
        } else if (!Input.GetMouseButton(0) && hasPressed){
            hasPressed = false;
            Cursor.SetCursor(normalMouseState, Vector2.zero, CursorMode.Auto);
        }


    }

   
    

}
