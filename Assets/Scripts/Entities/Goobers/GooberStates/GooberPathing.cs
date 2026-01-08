using System.Collections.Generic;
using UnityEngine;

public class GooberPathing : GooberState
{
    public override void EnterState(GooberContext context)
    {
        throw new System.NotImplementedException();
    }

    public override void ExitState(GooberContext context)
    {
        throw new System.NotImplementedException();
    }

    public override void Update(GooberContext context)
    {
        if (context.GetWaypoints() != null)
        {
            Vector3 targetPoint = context.GetCurrentWaypoint();
            if (Vector3.Distance(context.GetPosition(), targetPoint) > 1f) //TODO: make this 1f dynamic - probably the size of a grid cell
            {

            }
        }
    }
}
