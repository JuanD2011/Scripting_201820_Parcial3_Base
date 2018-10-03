using UnityEngine;
/// <summary>
/// Selector that succeeds if 'tagged' player is within a 'acceptableDistance' radius.
/// </summary>
public class IsTaggedActorNear : Selector
{
    [SerializeField]
    private float acceptableDistance = 0F;

    private bool sucess;

    protected override bool CheckCondition()
    {
        sucess = false;

        ActorController taggedActor = Do();

        if (taggedActor != null)
        {
            sucess = true;
        }
        return sucess;
    }

    public ActorController Do()
    {
        ActorController taggedActor = null;

        RaycastHit[] hits = Physics.SphereCastAll(ControlledAI.transform.position, acceptableDistance, Vector3.forward);

        foreach(RaycastHit hit in hits)
        {
            if(hit.transform.gameObject.GetComponent<ActorController>())
            {
                if(hit.transform.gameObject.GetComponent<ActorController>() != ControlledAI && hit.transform.gameObject.GetComponent<ActorController>().IsTagged && !ControlledAI.LastTagged)
                {
                    taggedActor = hit.transform.gameObject.GetComponent<ActorController>();
                }
            }
        }

        return taggedActor;
    }
}