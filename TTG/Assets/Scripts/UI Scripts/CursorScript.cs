using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorScript : MonoBehaviour
{
    [SerializeField]
    Camera cam;

    Vector2 mousePos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //getting position of cursor by taking its position on the screen and then putting that into the game world
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        transform.position = mousePos;

        //REMINDER Gray out cursor when you need to/are reloading
    }
}
