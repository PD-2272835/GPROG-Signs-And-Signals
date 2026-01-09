using System.Collections.Generic;
using UnityEngine;

public class GooberPathing : GooberState
{
    private List<Vector3> _pathWaypoints;
    private int _currentWaypointIndex;
    private Vector3 _targetPosition = Vector3.negativeInfinity; //negativeInfinity will never be reachable by pathing so effectively represents a non-assigned _targetPosition


    public override void EnterState(GooberContext context)
    {
        Debug.Log("started pathing " + context.name);
        _currentWaypointIndex = 0;
        if (_targetPosition != Vector3.negativeInfinity)
        {
            _pathWaypoints = Pathfinding.Instance.FindPath(context.GetPosition(), _targetPosition);
        }
        else
        {
            context.ChangeState(context.Idle);
        }
    }

    public override void ExitState(GooberContext context)
    {
        _pathWaypoints = null;
        _targetPosition = Vector3.negativeInfinity;
    }

    public override void Update(GooberContext context)
    {
        if (_pathWaypoints != null)
        {
            Vector3 targetPoint = _pathWaypoints[_currentWaypointIndex];
            if (Vector3.Distance(context.GetPosition(), targetPoint) > 1f) //TODO: make this 1f dynamic - probably the size of a grid cell from somewhere
            {
                Vector3 direction = (targetPoint - context.GetPosition()).normalized;
                context.UpdatePosition(direction * context.movementSpeed * Time.deltaTime);
            } else
            {
                _currentWaypointIndex++;
                if (_currentWaypointIndex >= _pathWaypoints.Count)
                {
                    context.ChangeState(context.Idle); //TODO - this should change to whatever action this goober should be performing (based on the target position)
                }
            }
        } 
    }


    public void SetTarget(Vector3 targetPosition)
    {
        _targetPosition = targetPosition;
    }
}
