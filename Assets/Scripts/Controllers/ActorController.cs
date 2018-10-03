using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Collider))]
public abstract class ActorController : MonoBehaviour
{
    private NavMeshAgent agent;

    [SerializeField]
    protected Color baseColor = Color.blue;

    protected Color taggedColor = Color.red;

    private MeshRenderer renderer;

    public delegate void OnActorTagged(bool val);

    public OnActorTagged onActorTagged;

    public bool IsTagged { get; protected set; }

    public NavMeshAgent Agent
    {
        get
        {
            return agent;
        }
    }

    public int TimesTagged
    {
        get
        {
            return timesTagged;
        }

        set
        {
            timesTagged = value;
        }
    }

    private bool hasTaggedBefore = false;

    public MeshRenderer Renderer
    {
        get
        {
            return renderer;
        }
    }

    public bool LastTagged
    {
        get
        {
            return lastTagged;
        }

        set
        {
            lastTagged = value;
        }
    }

    private int timesTagged = 0;

    private bool lastTagged = false;

    private static float speed = 5;

    // Use this for initialization
    protected virtual void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        renderer = GetComponent<MeshRenderer>();
        agent.speed = speed;

        SetTagged(false);

        onActorTagged += SetTagged;

        GameController.OnGameOver += GameOver;
    }

    protected abstract Vector3 GetTargetLocation();

    protected void MoveActor()
    {
        agent.SetDestination(GetTargetLocation());
    }

    protected void OnCollisionEnter(Collision collision)
    {
        if (hasTaggedBefore)
        {
            return;
        }

        ActorController otherActor = collision.gameObject.GetComponent<ActorController>();

        if (otherActor != null)
        {
            Debug.Log("Holi");
            otherActor.hasTaggedBefore = true;

            if (this.IsTagged || otherActor.IsTagged)
            {
                if (!otherActor.LastTagged)
                {
                    GameController.Instance.NewTag(this, otherActor); 
                } 
            }
        }
    }

    protected virtual void OnDestroy()
    {
        agent = null;
        renderer = null;
        onActorTagged -= SetTagged;
    }

    public void SetTagged(bool val)
    {
        IsTagged = val;

        if (renderer)
        {
            print(string.Format("Changing color to {0}", gameObject.name));
            renderer.material.color = val ? taggedColor : baseColor;
        }

        if(IsTagged)
        {
            timesTagged++;
        }
    }

    private void GameOver()
    {
        agent.isStopped = true;
    }

    protected void OnCollisionExit(Collision collision)
    {
        ActorController otherActor = collision.gameObject.GetComponent<ActorController>();

        if (otherActor != null)
        {
            hasTaggedBefore = false;
        }
    }
}