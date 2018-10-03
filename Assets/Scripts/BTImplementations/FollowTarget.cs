/// <summary>
/// Task that instructs ControlledAI to follow its designated 'target'
/// </summary>
public class FollowTarget : Task
{
    public override bool Execute()
    {
        bool sucess = false;

        if (ControlledAI.Agent.SetDestination(GetComponent<GetNearestTarget>().Do().transform.position))
        {
            ControlledAI.Agent.SetDestination(GetComponent<GetNearestTarget>().Do().transform.position);
            sucess = true;
        }

        return sucess;
    }
}