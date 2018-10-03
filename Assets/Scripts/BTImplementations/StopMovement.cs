/// <summary>
/// Task that instructs ControlledAI to stop its current movement.
/// </summary>
public class StopMovement : Task
{
    public override bool Execute()
    {
        bool sucess = false;
        Stop();

        if(ControlledAI.Agent.remainingDistance <= ControlledAI.Agent.stoppingDistance)
        {
            sucess = true;
        }

        return sucess;
    }

    private void Stop()
    {
        ControlledAI.Agent.SetDestination(transform.localPosition);
        print("Quieto");
    }
}