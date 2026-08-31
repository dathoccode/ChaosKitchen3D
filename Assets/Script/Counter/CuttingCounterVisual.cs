using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class CuttingCounterVisual : MonoBehaviour
{
    private const string Cut = "Cut";

    [SerializeField] private CuttingCounter cuttingCounter;

    private Animator animator;
    

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        cuttingCounter.OnCut += CuttingCounter_OnCut;
    }

    private void CuttingCounter_OnCut(object sender, EventArgs e)
    {
        animator.SetTrigger(Cut);
    }
}
