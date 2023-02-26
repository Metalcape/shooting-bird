using System;
using System.Collections;
using System.Collections.Generic;
using Mono.Cecil;
using UnityEngine;

public class PipeMiddleScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            GameObject bird = collision.gameObject;
            BirdScript birdScript = bird.GetComponent<BirdScript>();
            birdScript.TriggerEventOnPassedPipe();
        }
        
    }
}
