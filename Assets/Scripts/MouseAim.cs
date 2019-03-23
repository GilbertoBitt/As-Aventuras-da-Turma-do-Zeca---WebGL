using UnityEngine;

public class MouseAim : MonoBehaviour{

    public Transform MouseAiming;
    public Camera MainCamera;

    private void Update(){
        //Caso Canvas esteja em moto Screen Overlay.
        MouseAiming.transform.position = Input.mousePosition;
        
        //Caso Esteja com Canvas em Screen Space Camera;
        var worldSpaceMousePosition = MainCamera.ScreenToWorldPoint(Input.mousePosition);
        worldSpaceMousePosition.z = 10f;//Setar de acordo com a profundidade da camera. por padrão as cameras da unity só exibem objetos apartir do z = 10f;
        MouseAiming.transform.position = worldSpaceMousePosition;
    }
}
