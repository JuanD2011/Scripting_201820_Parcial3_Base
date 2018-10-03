using UnityEngine;
using UnityEngine.AI;

public class AIController : ActorController
{
    [SerializeField]
    private float moveRadius = 50F;

    [SerializeField]
    private Root btRootNode;

    public void MoveAI()
    {
        MoveActor();
    }

    protected override void Start()
    {
        base.Start();

        if (btRootNode != null)
        {
            btRootNode.SetControlledAI(this);
        }

        AIMoveTest.Instance.onAIMoveIssued += MoveAI;
    }

    protected override void OnDestroy()
    {
        AIMoveTest.Instance.onAIMoveIssued -= MoveAI;
        base.OnDestroy();
    }

    protected override Vector3 GetTargetLocation()
    {
        Vector3 result = transform.position;

        Vector3 randomDirection = Random.insideUnitSphere * moveRadius;
        randomDirection += transform.position;

        NavMeshHit hit;

        if (NavMesh.SamplePosition(randomDirection, out hit, moveRadius, 1))
        {
            result = hit.position;
        }

        return result;
    }

    private void Update()
    {
        if(Time.frameCount % 60 == 0)
        {
            btRootNode.Execute();
        }
    }
}