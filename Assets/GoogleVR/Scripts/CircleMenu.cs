using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleMenu : MonoBehaviour
{
    public GameObject CircleMenuPanel;

    public void ShowHideMenu()
    {
        if(CircleMenuPanel != null)
        {
            Animator animator = CircleMenuPanel.GetComponent<Animator>();
            if(animator != null)
            {
                bool isOpen = animator.GetBool("show");
                animator.SetBool("show", !isOpen);
            }
        }
    }
}
