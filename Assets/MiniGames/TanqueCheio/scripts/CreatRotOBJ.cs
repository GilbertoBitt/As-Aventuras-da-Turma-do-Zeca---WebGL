using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatRotOBJ : MonoBehaviour {

    public Transform target;//set target from inspector instead of looking in Update
    public float speed = 3f;
    private Transform myTransform;
    void Start () {
        myTransform = this.GetComponent<Transform>();

    }

    // Update is called once per frame
    void Update () {
        //rotate to look at the player
        transform.LookAt(target.position);
        transform.Rotate(new Vector3(0, -90, -90), Space.Self);//correcting the original rotation



        //myTransform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);



    }/*
    void OnDrawGizmos() {
        Color color;
        color = Color.green;
        // local up
        DrawHelperAtCenter(this.transform.up, color, 2f);

        color.g -= 0.5f;
        // global up
        DrawHelperAtCenter(Vector3.up, color, 1f);

        color = Color.blue;
        // local forward
        DrawHelperAtCenter(this.transform.forward, color, 2f);

        color.b -= 0.5f;
        // global forward
        DrawHelperAtCenter(Vector3.forward, color, 1f);

        color = Color.red;
        // local right
        DrawHelperAtCenter(this.transform.right, color, 2f);

        color.r -= 0.5f;
        // global right
        DrawHelperAtCenter(Vector3.right, color, 1f);
    }

    private void DrawHelperAtCenter(
                       Vector3 direction, Color color, float scale) {
        Gizmos.color = color;
        Vector3 destination = transform.position + direction * scale;
        Gizmos.DrawLine(transform.position, destination);
    }
    */
}
