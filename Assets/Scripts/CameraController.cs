using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour
{
    public GameObject player;

    private Vector3 offset;

    void Start()
    {
        offset = transform.position - player.transform.position;
    }

    void LateUpdate()
    {

        Vector3 newPosition = player.transform.position + offset;
        if (player.GetComponent<PlayerController>().isJumping)
        {
            newPosition.y = transform.position.y;
        }

        if (SceneManager.GetActiveScene().buildIndex != 2)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                //Vector3 rotation = player.transform.rotation.eulerAngles;
                //rotation.x = 0;
                //rotation.z = 0;
                //transform.LookAt(rotation, Space.Self);
                ////transform.Rotate(rotation);
                ////transform.Rotate(player.GetComponent<PlayerController>().GetRotationAngle());
                ////transform.rotation = Quaternion.Euler(player.transform.rotation.eulerAngles);
                transform.Rotate(Vector3.up* Time.deltaTime * -100.0f, Space.Self);


            }
            if(Input.GetKey(KeyCode.RightArrow))
            {
                transform.Rotate(Vector3.up * Time.deltaTime * 100.0f, Space.Self);
            }
        }
        transform.position = newPosition;
        //newPosition.y = 0.0f;
        //transform.LookAt(newPosition);
        //Vector3 newRotation = Quaternion.LookRotation(newPosition);
    }
}
