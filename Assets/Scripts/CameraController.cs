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
                transform.Rotate(Vector3.up* Time.deltaTime * -100.0f, Space.Self);
            }
            if(Input.GetKey(KeyCode.RightArrow))
            {
                transform.Rotate(Vector3.up * Time.deltaTime * 100.0f, Space.Self);
            }
            if(Input.GetKey(KeyCode.UpArrow))
            {
                if(transform.position.y < transform.position.y + 0.50f)
                {
                    newPosition.y += 0.05f;
                }
            }
            if(Input.GetKey(KeyCode.DownArrow))
            {
                if (transform.position.y > transform.position.y - 0.50f)
                {
                    newPosition.y -= 0.05f;
                }
            }
        }
        transform.position = newPosition;

    }
}
