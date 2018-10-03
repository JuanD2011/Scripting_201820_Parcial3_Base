using UnityEngine;
using UnityEngine.AI;
/// <summary>
/// Task that instructs ControlledAI to move away from 'tagged' player
/// </summary>
public class FleeFromTagged : Task
{
    public override bool Execute()
    {
        bool sucess = false;

        if(ControlledAI.Agent.SetDestination(Flee()))
        {
            ControlledAI.Agent.SetDestination(Flee());
            sucess = true;
        }

        return sucess;
    }

    private Vector3 Flee()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - GetComponent<IsTaggedActorNear>().Do().transform.position);

        Vector3 fleeTo = transform.position + transform.forward * 5;

        NavMeshHit hit;
        NavMesh.SamplePosition(fleeTo, out hit, 5, NavMesh.AllAreas);

        return hit.position;
    }
}