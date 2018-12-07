using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Updates UI for displaying relevant data about the agents
/// </summary>
public class StatUpdate : MonoBehaviour
{
    /// <summary>
    /// Reference to ant population display text
    /// </summary>
    Text AntPop;

    /// <summary>
    /// Reference to avg. ant fitness display text
    /// </summary>
    Text AntFit;

    /// <summary>
    /// Reference to fungi population display trext
    /// </summary>
    Text FungiPop;

    void Start()
    {
        AntPop = transform.GetChild(0).GetComponent<Text>();
        AntFit = transform.GetChild(1).GetComponent<Text>();
        FungiPop = transform.GetChild(2).GetComponent<Text>();
    }

    void Update()
    {
        AntPop.text = "Ant population: " + (FindObjectsOfType<WorkerAnt>().Length + FindObjectsOfType<QueenAnt>().Length);

        //Get all ants
        WorkerAnt[] ants = FindObjectsOfType<WorkerAnt>();
        QueenAnt[] queens = FindObjectsOfType<QueenAnt>();

        //calculate fitness sum
        float fitnessSum = 0;
        for (int i = 0; i < ants.Length; i++)
            fitnessSum += ants[i].dna.GetFitness();
        for (int i = 0; i < queens.Length; i++)
            fitnessSum += queens[i].dna.GetFitness();

        AntFit.text = "Avg. ant fitness: " + (fitnessSum / (FindObjectsOfType<WorkerAnt>().Length + FindObjectsOfType<QueenAnt>().Length));

        FungiPop.text = "Fungi population: " + FindObjectsOfType<Fungi>().Length;
    }
}
