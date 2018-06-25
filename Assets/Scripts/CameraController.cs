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
        if(player.GetComponent<PlayerController>().isJumping)
        {
            newPosition.y = transform.position.y;
        }

        if(SceneManager.GetActiveScene().buildIndex != 2)
        {
            if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                transform.Rotate(player.GetComponent<PlayerController>().GetRotationAngle()*-1, Space.Self);
                //transform.rotation = Quaternion.Euler(player.transform.rotation.eulerAngles);
            }
        }
        transform.position = newPosition;
        
    }
}
