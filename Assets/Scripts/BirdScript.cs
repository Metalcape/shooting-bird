using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdScript : MonoBehaviour
{
    [SerializeField] private Rigidbody2D myRigidbody;
    [SerializeField] private float flapStrength;
    [SerializeField] private GameObject ceiling;
    [SerializeField] private Animator wingAnimator;

    private bool IsAlive = true;

    private AudioSource audioSource;

    public event EventHandler OnDead;
    public event EventHandler<OnPassedPipeEventArgs> OnPassedPipe;
    public class OnPassedPipeEventArgs : EventArgs
    {
        public int scoreToAdd;
    }

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsAlive)
        {
            myRigidbody.velocity = Vector2.up * flapStrength;
            wingAnimator.Play("Bird_Flap");
            audioSource.Play();
        }       
    }

    public void TriggerEventOnPassedPipe()
    {
        OnPassedPipe?.Invoke(this, new OnPassedPipeEventArgs { scoreToAdd = 1 });
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject != ceiling)
        {
            IsAlive = false;
            OnDead?.Invoke(this, EventArgs.Empty);

        }
    }
}
