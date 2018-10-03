using UnityEngine;

public class GetNearestTarget : Task
{
    private ActorController closestActor = null;

    public override bool Execute()
    {
        bool sucess = false; 
        ActorController closest = null;
        closest = Do();

        if(closest != null)
        {
            sucess = true;
        }

        return sucess;
    }

    public ActorController Do()
    {
        float distanceToClosestEnemy = Mathf.Infinity;
        ActorController[] players = FindObjectsOfType<ActorController>();
        foreach (ActorController actor in players)
        {
            float distance = Vector3.Distance(transform.localPosition, actor.transform.localPosition);
            if (distance < distanceToClosestEnemy && actor != ControlledAI && !actor.LastTagged)
            {
                distanceToClosestEnemy = distance;
                closestActor = actor;
            }
        }

        return closestActor;
    }
}

