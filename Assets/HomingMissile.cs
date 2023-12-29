using UnityEngine;
using System.Collections.Generic;
using System.Collections;
public class HomingMissile : MonoBehaviour
{
    public float speed = 10f;
    public float rotationSpeed = 5f;
    public string targetTag = "Enemy"; // Set the target tag in the Unity Editor
    public Transform target;
    public AudioClip audioClip = null;
    protected virtual void Update()
    {
        FindTarget(); // Automatically find the target

        if (Time.deltaTime > 0f)
        {
            if (target != null)
            {
                Vector3 direction = (target.position - transform.position).normalized;
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
            }

            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
    }

    // Automatically find a target with the specified tag
    private void FindTarget()
    {
        if (target == null)
        {
            GameObject[] potentialTargets = GameObject.FindGameObjectsWithTag(targetTag);

            float closestDistance = Mathf.Infinity;
            foreach (GameObject potentialTarget in potentialTargets)
            {
                float distance = Vector3.Distance(transform.position, potentialTarget.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    target = potentialTarget.transform;
                }
            }
        }
    }

    // Handle collisions with other objects
    protected virtual void OnTriggerEnter(Collider other)
    {
        // Implement collision logic (e.g., damage, explosion)
        if (other.CompareTag(targetTag))
        {
            StopAllCoroutines();
            StartCoroutine("timingExplode");
        }
    }
    IEnumerator timingExplode()
    {
        AudioSource A = GetComponent<AudioSource>();
        A.clip=audioClip;
        A.Play();
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
