using UnityEngine;

[ExecuteInEditMode]
public class CameraCircleFade : MonoBehaviour {
    public Shader shader;
    [Range(0f,0.6f)]
	public float radius;
    [Range(0f,10.6f)]
	public float radiusAndroid = 10.6f;
    float _radius;    
    public Material _mtl;
	/*Material mtl {
		get{
			if (_mtl) return _mtl; 
			else {
				_mtl = new Material(Shader.Find("Hidden/Circle"));
				return _mtl;
			}
		}
	}*/


    private void Start() {
        _mtl = new Material(shader);
    }
    // Update is called once per frame
    void Update () {
        if (Application.isMobilePlatform && _radius != radiusAndroid) {                
            _mtl.SetFloat("_Radius", radiusAndroid);
            _radius = radiusAndroid;
        } else if (!Application.isMobilePlatform && _radius != radius) {
            _mtl.SetFloat("_Radius", radius);
            _radius = radius;
        }
	}

	void OnRenderImage ( RenderTexture src, RenderTexture dst) {
		Graphics.Blit(src, dst, _mtl);
	}

    public void radiusReset() {
        if (!Application.isMobilePlatform) {
            radius = 0.6f;
        } else {
            radiusAndroid = 10.6f;
        }
    }
}



