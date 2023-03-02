using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleAudioImpact : MonoBehaviour
{
    public AudioSource CollisionSound;
    private float unityVelocityThreshold = 1.0f;

    private float calculatedVelocityThreshold = .1f;
    ArticulationBody localAB = null;
    Rigidbody localRB = null;

    private void Start()
    {
        gameObject.TryGetComponent<Rigidbody>(out localRB);
        gameObject.TryGetComponent<ArticulationBody>(out localAB);
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.relativeVelocity.magnitude > 0) // sometimes unity calculates speed for us
        {
            if(collision.relativeVelocity.magnitude > unityVelocityThreshold)
                CollisionSound.Play();
        } else if (collision.body != null && (localRB != null || localAB != null)){ // sometimes we have to calculate

            Vector3 ownSpeed = localRB != null ? localRB.velocity : localAB.velocity;
            Vector3 colliderSpeed = collision.rigidbody != null ? collision.rigidbody.velocity : collision.articulationBody.velocity;

            if((ownSpeed - colliderSpeed).magnitude > calculatedVelocityThreshold)
                CollisionSound.Play();
        }
    }
}
