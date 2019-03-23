using UnityEngine;
using UnityEngine.UI;

public class LoginSmallTabNavigation : OverridableMonoBehaviour
{

    public Selectable[] selectables = new Selectable[0];
    public int count;
    public int length;

    public void Start()
    {
        count = 0;
        length = selectables.Length - 1;

    }

    public override void UpdateMe(){
        if (Input.GetKeyDown(KeyCode.Tab)){

            if (count >= length)
            {
                count = 0;
            }
             else {
                count++;
            }

            selectables[count].Select();

        }

    }
}
