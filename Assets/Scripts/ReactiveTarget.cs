using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactiveTarget : MonoBehaviour
{
    private SceneController _controller;
    [SerializeField]
    private Animator _animator;
    public void ReactToHit()
    {
        WanderingAI behavior = GetComponent<WanderingAI>();
        if (behavior != null)
        {
            behavior.SetAlive(false);
        }
        StartCoroutine(Die());
    }
    private IEnumerator Die()
    {
        //this.transform.Rotate(-75, 0, 0);
        _animator.SetTrigger("Die");

        GetComponent<WanderingAI>().enabled = false;

        yield return new WaitForSeconds(3.5f);

        _controller.EnemyDie(this.gameObject);

        Destroy(this.gameObject);
    }

    public SceneController controller
    {
        set
        {
            _controller = value;
        }
    }
}
