using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEnemyAnimStart : MonoBehaviour
{
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.speed = Random.Range(0, 2000);
        StartCoroutine(resetSpeedToNormal());
    }


    private IEnumerator resetSpeedToNormal()
    {
        yield return new WaitForSeconds(.1f);
        animator.speed = 1;
    }
}
