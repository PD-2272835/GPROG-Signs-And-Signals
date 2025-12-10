using System;
using UnityEngine;

public abstract class GooberState
{
    public abstract void EnterState(GooberContext context);
    public abstract void Update (GooberContext context);
    public abstract void ExitState(GooberContext context);
}