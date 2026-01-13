using UnityEngine;

//concrete states
public class GooberIdle : GooberState
{
    private int _moveChance = 20;

    public override void EnterState(GooberContext context)
    {

    }

    public override void Update(GooberContext context)
    {
        if (UnityEngine.Random.Range(0, 100) <= _moveChance) //better verbosity makes using random more explicit (system defines a random method)
        {
            Vector3 currentPos = context.GetPosition();
            int randomXDelta = UnityEngine.Random.Range(-3, 3);
            int randomYDelta = UnityEngine.Random.Range(-3, 3);
            Vector3 randomPosition = new Vector3(currentPos.x + randomXDelta, currentPos.y + randomYDelta);

            context.Pathing.SetTarget(randomPosition);
            context.ChangeState(context.Pathing);
        }
    }

    public override void ExitState(GooberContext context)
    {
        
    }
}
