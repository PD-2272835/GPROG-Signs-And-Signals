using UnityEngine;

//concrete states
public class GooberIdle : GooberState
{
    private Vector3 _randomPos;
    private float _wanderWaitTime;

    public override void EnterState(GooberContext context)
    {
        Debug.Log(context.ToString() + " entered Idle State");
        _randomPos = GenerateNewWanderTarget(context.GetPosition());
        _wanderWaitTime = UnityEngine.Random.Range(0f, context.maxWanderWaitPeriod);
    }
    public override void ExitState(GooberContext context) { }

    public override void Update(GooberContext context)
    {
        if (_wanderWaitTime <= 0f)
        {
            if (UnityEngine.Random.Range(0, 100) <= context.wanderChance*100) //better verbosity makes using random more explicit (system also defines a random method)
            {
                Debug.Log("idle exit condition met");
                context.PathTo(_randomPos);
            }
            _wanderWaitTime = UnityEngine.Random.Range(0f, context.maxWanderWaitPeriod);
        }
        _wanderWaitTime -= Time.deltaTime;
    }

    private Vector3 GenerateNewWanderTarget(Vector3 originPosition)
    {
        int randomXDelta = UnityEngine.Random.Range(0, 3) * GeneratePositiveOrNegative();
        int randomYDelta = UnityEngine.Random.Range(0, 3) * GeneratePositiveOrNegative();
        Vector3 randomPosition = new Vector3(originPosition.x + randomXDelta, originPosition.y + randomYDelta);
        
        if (Pathfinding.Instance.Grid.IsInGrid(randomPosition)) return randomPosition;
        else return GenerateNewWanderTarget(originPosition);
    }

    private int GeneratePositiveOrNegative()
    {
        return UnityEngine.Random.Range(0, 2) * 2 - 1; //randomly generate either +1 or -1
    }
}
