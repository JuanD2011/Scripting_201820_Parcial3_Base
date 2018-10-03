/// <summary>
/// Selector that succeeeds if ControlledAI is marked as 'tagged'
/// </summary>
public class ActorIsTagged : Selector
{
    protected override bool CheckCondition()
    {
        bool isTagged = false;

        if (ControlledAI.IsTagged)
        {
            isTagged = true;
        }

        return isTagged;
    }

}