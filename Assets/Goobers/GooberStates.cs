using System;
using UnityEngine;

public abstract class GooberState
{
    public abstract void EnterState(GooberContext context);
    public abstract void UpdateInput(GooberContext context);
    public abstract void Update(GooberContext context);
}

//concrete states
public class GooberIdle : GooberState
{
    void EnterState(GooberContext context)
    {
        throw (new NotImplementedException());
    }

    void UpdateInput(GooberContext context)
    {
        throw (new NotImplementedException());
    }

    void Update(GooberContext context)
    {
        throw (new NotImplementedException());
    }
}

public class GooberPathing : GooberState
{

}