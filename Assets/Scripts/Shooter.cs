using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private GameObject gun;

    private AttackerSpawner myLaneSpawner;
    private Animator animator;
    
    private void Start()
    {
        SetLaneSpawner();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (IsAttackerInLane())
        {
            animator.SetBool("isAttacking", true);
        }
        else
        {
            animator.SetBool("isAttacking", false);
        }
    }
    
    private void SetLaneSpawner()
    {
        AttackerSpawner[] spawners = FindObjectsOfType<AttackerSpawner>();
        foreach (AttackerSpawner spawner in spawners)
        {
            float tolerance = 0.11f;
            bool isCloseEnough = Mathf.Abs(spawner.transform.position.y - transform.position.y) <= tolerance;
            if (isCloseEnough)
            {
                myLaneSpawner = spawner;
                return;
            }
        }
    }
    
    private bool IsAttackerInLane()
    {
        if (myLaneSpawner == null || myLaneSpawner.transform.childCount <= 0)
            return false;

        foreach (Transform attacker in myLaneSpawner.transform)
        {
            if (attacker.transform.position.x > transform.position.x)
                return true;
        }
        return false;
    }

    public void Fire()
    {
        Instantiate(projectile, gun.transform.position, transform.rotation);
    }
}