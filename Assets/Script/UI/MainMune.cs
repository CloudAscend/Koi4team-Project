using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMune : MonoBehaviour
{
    Animator animator;
    [SerializeField]
    GameObject UI;
    private void Awake()
    {
        animator= GetComponent<Animator>();
    }
    public void ChoseOption(int option)
    {
        animator.SetBool("Not Chose", false);
        animator.SetInteger("Chose", option);
    }
    public void GameStart()
    {
        animator.SetTrigger("Start");
    }
    public void ExitMainMune()
    {
        gameObject.SetActive(false);
        UI.SetActive(true);
    }
}
