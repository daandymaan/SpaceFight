﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public State currentState;
    public State globalState;
    public State previousState;

    private IEnumerator coroutine;

    public int updatesPerSecond = 5;

    void Start() 
    {
        
    }

    private void OnEnable() 
    {
        StartCoroutine(Think());
    }
    public void ChangeStateDelayed(State newState, float delay)
    {
        coroutine = ChangeStateCoroutine(newState, delay);
        StartCoroutine(coroutine);
    }

    public void CancelDelayedStateChange()
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }
    }

    IEnumerator ChangeStateCoroutine(State newState, float delay)
    {
        yield return new WaitForSeconds(delay);
        ChangeState(newState);
    }

    public void RevertToPreviousState()
    {
        ChangeState(previousState);
    }

    public void ChangeState(State newState)
    {
        
        if (currentState != null)
        {
            currentState.Exit();
        }
        if (this.previousState == null || previousState.GetType() != this.previousState.GetType())
        {
            this.previousState = currentState;
        }
        currentState = newState;
        currentState.owner = this;
        currentState.Enter();
    }

    System.Collections.IEnumerator Think()
    {
        yield return new WaitForSeconds(Random.Range(0, 0.5f));
        while (true)
        {
            if (globalState != null)
            {
                globalState.Think();
            }
            if (currentState != null)
            {
                currentState.Think();
            }
            
            yield return new WaitForSeconds(1.0f / (float)updatesPerSecond);
        }
    }

    public void SetGlobalState(State state)
    {
        if (globalState != null)
        {
            globalState.Exit();
        }
        globalState = state;
        if (globalState != null)
        {
            globalState.owner = this;
            globalState.Enter();
        }        
    }
}
