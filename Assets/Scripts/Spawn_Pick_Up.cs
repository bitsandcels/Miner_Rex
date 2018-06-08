using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Pick_Up : MonoBehaviour {

    public int max_active = 2;
    public int max_spawned = 10;
    public float delay_activation = 1;
    public float delay_multipuleSpawn = 2;
    public GameObject pick_ups;
    internal System.Collections.Generic.List<GameObject> pickUps;

    private Vector3 spawnPoint;
    private bool invoked = false;
    private int count = 0;

    public int Range;

	// Use this for initialization
	void Start ()
    {
        pickUps = new System.Collections.Generic.List<GameObject>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(!invoked && pickUps.Count < max_active)
        {
            InvokeRepeating("SpawnPickUp", delay_activation, delay_multipuleSpawn);
            invoked = true;
        }
	}

    void SpawnPickUp()
    {
        //find/confirm player instance
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if(player != null)
        {
            if (count < max_spawned)
            {
                //Vector3 pos_player = player.transform.position;
                Vector3 rand_pos = Random.insideUnitSphere * Range;
                spawnPoint.y = transform.position.y;
                spawnPoint.x = rand_pos.x;
                spawnPoint.z = rand_pos.z;

                GameObject PickUpObj = Instantiate(pick_ups, spawnPoint, Quaternion.identity);
                
                ++count;

                if (count >= max_spawned)
                {
                    CancelInvoke();
                }

            }
        }
    }

}
