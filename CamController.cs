
using UnityEngine;

public class CamController : MonoBehaviour
{

    // public float panSpeed = 20f;
    // public float panBorderThickness = 10f;
    // public Vector2 panLimit;

    public Transform target;
    public float zLevel, zLevel2;
    public float yLevel;
    public float zTest;

    public float xLimit1, xLimit2;

    //public void CameraRoom(float zValue,float zValue2)
    //{
    //    zLevel = zValue;
    //    zLevel2 = zValue2;
    //    return;
    //}



    void Start()
    {
        //// set the desired aspect ratio (the values in this example are
        //// hard-coded for 16:9, but you could make them into public
        //// variables instead so you can set them at design time)
        //float targetaspect = 16.0f / 9.0f;

        //// determine the game window's current aspect ratio
        //float windowaspect = (float)Screen.width / (float)Screen.height;

        //// current viewport height should be scaled by this amount
        //float scaleheight = windowaspect / targetaspect;

        //// obtain camera component so we can modify its viewport
        //Camera camera = GetComponent<Camera>();

        //// if scaled height is less than current height, add letterbox
        //if (scaleheight < 1.0f)
        //{
        //    Rect rect = camera.rect;

        //    rect.width = 1.0f;
        //    rect.height = scaleheight;
        //    rect.x = 0;
        //    rect.y = (1.0f - scaleheight) / 2.0f;

        //    camera.rect = rect;
        //}
        //else // add pillarbox
        //{
        //    float scalewidth = 1.0f / scaleheight;

        //    Rect rect = camera.rect;

        //    rect.width = scalewidth;
        //    rect.height = 1.0f;
        //    rect.x = (1.0f - scalewidth) / 2.0f;
        //    rect.y = 0;

        //    camera.rect = rect;
        //}
    }


    void Update()
    {
        Vector3 pos = target.position;
        pos.x = Mathf.Clamp(pos.x, xLimit1, xLimit2);
        pos.z = Mathf.Clamp(pos.z + zTest, zLevel2, zLevel);


        transform.position = new Vector3(pos.x, yLevel, pos.z);


        //    pos.x = Mathf.Clamp(pos.x, -panLimit.x, panLimit.x);
        //    pos.z = Mathf.Clamp(pos.z, -panLimit.y, panLimit.y - 25);

        //    transform.position = pos;



        //    Vector3 pos = transform.position;

        //    if (Input.GetKey("w") || Input.GetKey(KeyCode.UpArrow) || Input.mousePosition.y >= Screen.height - panBorderThickness)
        //    {
        //        pos.z += panSpeed * Time.deltaTime;
        //    }
        //    if (Input.GetKey("s") || Input.GetKey(KeyCode.DownArrow) || Input.mousePosition.y <= panBorderThickness)
        //    {
        //        pos.z -= panSpeed * Time.deltaTime;
        //    }
        //    if (Input.GetKey("d") || Input.GetKey(KeyCode.RightArrow) || Input.mousePosition.x >= Screen.width - panBorderThickness)
        //    {
        //        pos.x += panSpeed * Time.deltaTime;
        //    }
        //    if (Input.GetKey("a") || Input.GetKey(KeyCode.LeftArrow) || Input.mousePosition.x <= panBorderThickness)
        //    {
        //        pos.x -= panSpeed * Time.deltaTime;
        //    }



    }
}
