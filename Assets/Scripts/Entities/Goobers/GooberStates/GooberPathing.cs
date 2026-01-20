using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GooberPathing : GooberState
{
    private List<Vector3> _PathWaypoints = new List<Vector3>();
    private int _CurrentWaypointIndex = 0;


    public override void EnterState(GooberContext context)
    {
        Debug.Log("entered Pathing and started pathing " + context.name + " to cell at " + context.TargetPosition);
        if (Pathfinding.Instance != null)
        {
            Debug.Log("target position is: " + context.TargetPosition.ToString());
            _PathWaypoints = Pathfinding.Instance.FindPath(context.GetPosition(), context.TargetPosition);
            Debug.Log("is path waypoints empty or null? :" + (_PathWaypoints.Any() != true));
        }
        else
        {
            context.ChangeState(context.Idle); // path wasn't set or there is no instance of pathfinding singleton to use, return to idle
        }
    }

    public override void ExitState(GooberContext context)
    {
        _PathWaypoints = null;
        _CurrentWaypointIndex = 0;
    }

    public override void Update(GooberContext context)
    {
        if (_PathWaypoints?.Any() != true) //ensure pathWaypoints is not null or empty
        {
            context.ChangeState(context.Idle);
        }
        else
        {
            //if there is a path, move through it
            Vector3 currentWaypoint = _PathWaypoints[_CurrentWaypointIndex];
            if (Vector3.Distance(context.GetPosition(), currentWaypoint) > context.PathVariance)
            {

                Vector3 direction = (currentWaypoint - context.GetPosition()).normalized;
                context.UpdatePosition(context.MovementSpeed * Time.deltaTime * direction);
            }
            else
            {
                _CurrentWaypointIndex++;

                //if path fully traversed, exit pathing state
                if (_CurrentWaypointIndex >= _PathWaypoints.Count) 
                {
                    IGooberInteractable destinationOccupier = (IGooberInteractable)WorldManager.Instance.GetOccupierFromWorldPos(context.GetPosition());
                    
                    if (destinationOccupier != null) context.ChangeState(context.Working);
                   
                    context.ChangeState(context.Idle);
                }
            }
        }
    }

}