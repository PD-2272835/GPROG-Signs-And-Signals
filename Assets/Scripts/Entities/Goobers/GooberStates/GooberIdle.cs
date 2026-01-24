using UnityEngine;

//concrete states
public class GooberIdle : GooberState
{
    private Vector3 _WanderTarget;
    private float _WanderWaitTime;

    public override void EnterState(GooberContext context)
    {
        _WanderTarget = GenerateNewWanderTarget(context.GetPosition());
        _WanderWaitTime = Random.Range(0f, context.MaxWanderWaitPeriod);
    }
    public override void ExitState(GooberContext context) { }

    public override void Update(GooberContext context)
    {
        if (_WanderWaitTime <= 0f)
        {
            if (Random.Range(0, 100) <= context.WanderChance*100)
            {
                context.PathTo(_WanderTarget);
            }
            _WanderWaitTime = Random.Range(0f, context.MaxWanderWaitPeriod);
        }
        _WanderWaitTime -= Time.deltaTime;
    }

    private Vector3 GenerateNewWanderTarget(Vector3 originPosition)
    {
        int randomXDelta = Random.Range(0, 3) * GeneratePositiveOrNegative();
        int randomYDelta = Random.Range(0, 3) * GeneratePositiveOrNegative();
        Vector3 randomPosition = new Vector3(originPosition.x + randomXDelta, originPosition.y + randomYDelta);
        
        if (Pathfinding.GetInstance().Grid.IsInGrid(randomPosition)) return randomPosition;
        else return GenerateNewWanderTarget(originPosition);
    }

    private int GeneratePositiveOrNegative()
    {
        return Random.Range(0, 2) * 2 - 1; //randomly generate either +1 or -1
    }
}
