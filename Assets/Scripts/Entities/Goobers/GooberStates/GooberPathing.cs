using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GooberPathing : GooberState
{
    private List<Vector3> _pathWaypoints = new List<Vector3>();
    private int _currentWaypointIndex = 0;


    public override void EnterState(GooberContext context)
    {
        Debug.Log("entered Pathing and started pathing " + context.name + " to cell at " + context.targetPosition);
        if (Pathfinding.Instance != null)
        {
            Debug.Log("target position is: " + context.targetPosition.ToString());
            _pathWaypoints = Pathfinding.Instance.FindPath(context.GetPosition(), context.targetPosition);
            Debug.Log("is path waypoints empty or null? :" + (_pathWaypoints.Any() != true));
        }
        else
        {
            context.ChangeState(context.Idle); // path wasn't set or there is no instance of pathfinding singleton to use, return to idle
        }
    }

    public override void ExitState(GooberContext context)
    {
        _pathWaypoints = null;
        _currentWaypointIndex = 0;
    }

    public override void Update(GooberContext context)
    {
        if (_pathWaypoints?.Any() != true) //ensure pathWaypoints is not null or empty
        {
            context.ChangeState(context.Idle);
        }
        else
        {
            Debug.Log("the number of waypoints left is " + _pathWaypoints.Count.ToString());
            Vector3 currentWaypoint = _pathWaypoints[_currentWaypointIndex];
            Debug.Log("pathing to waypoint " + currentWaypoint.ToString() + "which is " + Vector3.Distance(context.GetPosition(), currentWaypoint) + " away");
            if (Vector3.Distance(context.GetPosition(), currentWaypoint) > 0.1f) //TODO: make this .1f a variable - probably the size of a grid cell from somewhere
            {
                Vector3 direction = (currentWaypoint - context.GetPosition()).normalized;
                context.UpdatePosition(context.movementSpeed * Time.deltaTime * direction);
            }
            else
            {
                _currentWaypointIndex++;
                if (_currentWaypointIndex >= _pathWaypoints.Count) //path fully traversed, exit pathing state
                {
                    context.ChangeState(context.Idle); //TODO - this should change to whatever action this goober should be performing (based on the target position)
                }
            }
        }
    }

}