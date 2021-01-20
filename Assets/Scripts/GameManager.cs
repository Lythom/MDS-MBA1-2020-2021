using UnityEngine;

public class GameManager : MonoBehaviour
{
    public MonoBehaviour[] ImplementedGames;
    
    private int nextGameIdx = 0;

    private void Start()
    {
        EventManager<MonoBehaviour>.StartListening("GameFinished", SwitchGame);
        ImplementedGames[nextGameIdx].enabled = true;
    }

    private void SwitchGame(MonoBehaviour previousGame)
    {
        previousGame.enabled = false;
        nextGameIdx = (nextGameIdx + 1) % (ImplementedGames.Length - 1);
        ImplementedGames[nextGameIdx].enabled = true;
    }
}