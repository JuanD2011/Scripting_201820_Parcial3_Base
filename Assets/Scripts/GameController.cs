using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public delegate void GameOver();
    public static event GameOver OnGameOver;

    private static GameController instance;

    private void Awake()
    {
        instance = this;
    }

    private ActorController[] players;

    [SerializeField]
    private float gameTime = 25F;

    public float CurrentGameTime { get; private set; }

    public static GameController Instance
    {
        get
        {
            return instance;
        }
    }

    [Range(1, 4)]
    [SerializeField] private int aiAmount;

    [SerializeField]
    private GameObject aiTemplate;

    [SerializeField]
    private GameObject playerTemplate;

    [SerializeField]
    private Collider aiSpawnerCollider;

    private bool finished;


    // Use this for initialization
    private IEnumerator Start()
    {
        CurrentGameTime = gameTime;

        SpawnPlayers();

        // Sets the first random tagged player
        players = FindObjectsOfType<ActorController>();

        yield return new WaitForSeconds(0.5F);

        players[Random.Range(0, players.Length)].onActorTagged(true);
    }

    private void Update()
    {
        if (CurrentGameTime <= 0F && !finished)
        {
            finished = true;
            OnGameOver();
            CheckWinner();
        }
        else if(!finished)
        {
            CurrentGameTime -= Time.deltaTime;
        }
    }

    private Vector3 GetSpawnPoint()
    {
        Vector3 point = Vector3.zero;

        point = new Vector3(Random.Range(aiSpawnerCollider.bounds.min.x, aiSpawnerCollider.bounds.max.x), Random.Range(aiSpawnerCollider.bounds.min.y, aiSpawnerCollider.bounds.max.y), Random.Range(aiSpawnerCollider.bounds.min.z, aiSpawnerCollider.bounds.max.z));

        return point;
    }

    public void NewTag(ActorController actor1, ActorController actor2)
    {
        foreach(ActorController player in players)
        {
            player.LastTagged = false;
        }

        if (actor1.IsTagged || actor2.IsTagged)
        {
            if (actor1.IsTagged)
            {
                actor1.SetTagged(false);
                actor1.LastTagged = true;
                actor2.SetTagged(true);
            }
            else if (actor2.IsTagged)
            {
                actor1.SetTagged(true);
                actor2.SetTagged(false);
                actor2.LastTagged = true;
            }
        }
    }

    private void SpawnPlayers()
    {
        GameObject player = Factory.Instace.FactoryObject(playerTemplate);

        if(player != null)
        {
            player.transform.position = GetSpawnPoint();
        }

        for (int i = 0; i < aiAmount; i++)
        {
            GameObject ai = Factory.Instace.FactoryObject(aiTemplate);

            if (ai != null)
            {
                ai.transform.position = GetSpawnPoint();
            }
        }
    }

    private void CheckWinner()
    {
        List<ActorController> winners = new List<ActorController>();

        int lowestTags = 100;

        foreach(ActorController actor in players)
        {
            if(actor.TimesTagged < lowestTags)
            {
                lowestTags = actor.TimesTagged;
            }
        }

        foreach(ActorController actor in players)
        {
            if(actor.TimesTagged == lowestTags)
            {
                winners.Add(actor);
            }
        }

        foreach (ActorController actor in winners)
        {
            actor.Renderer.material.color = Color.magenta;
            actor.transform.localScale = new Vector3(2, 2, 2);
        }
    }
}
