using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TurtleManager : MonoBehaviour
{
    public static TurtleManager inst;

    [SerializeField] private List<Transform> spawns = new();
    [SerializeField] private GameObject turtlePrefab;

    private readonly float turtleY = -1;
    public List<Turtle> Turtles = new();

    public event UnityAction Dive;
    public event UnityAction Resurface;

    private void Awake()
    {
        inst = this;
    }

    private void OnEnable()
    {
        EventManager.OnCorrectAnswer += MassDive;
    }

    private void OnDisable()
    {
        EventManager.OnCorrectAnswer -= MassDive;
        EventManager.OnIncorrectAnswer -= MassDive;
    }

    public static void MassDive()
    {
        inst.Dive?.Invoke();
    }

    public static void MassResurface()
    {
        inst.Resurface?.Invoke();
    }

    public bool SpawnTurtles(int turtsToSpawn)
    {
        EventManager.OnIncorrectAnswer -= MassDive;

        if (Turtles.Count != 0)
        {
            for (int i = Turtles.Count-1; i > -1; i--)
            {
                Pooler.Despawn(Turtles[i].gameObject);
                EventManager.OnIncorrectAnswer -= Turtles[i].HandleDive;
                Turtles.RemoveAt(i);
            }
        }

        List<Transform> spawnsLeft = new();
        spawnsLeft.AddRange(spawns);
        
        for (int i = 0; i < turtsToSpawn; i++)
        {
            int index = Random.Range(0, spawnsLeft.Count);
            Vector3 turtPosition = new(spawnsLeft[index].position.x, turtleY, spawnsLeft[index].position.z);
            Pooler.Spawn(turtlePrefab, turtPosition, Quaternion.identity);

            spawnsLeft.RemoveAt(index);
        }

        Turtles.AddRange(FindObjectsOfType<Turtle>());

        if (MatchLetterManager.inst.Difficulty == Difficulty.Hard)
            EventManager.OnIncorrectAnswer += MassDive;

        return true;
    }
}
