using UnityEngine;

public class OpponentCarWaypoints : MonoBehaviour
{
    [Header("OpponentCar Car")]
    public OpponentCar opponentCar;
    public Waypoint currentWayPoint;

    private void Awake()
    {
        opponentCar = GetComponent<OpponentCar>();
    }

    private void Start()
    {
        opponentCar.LocateDestination(currentWayPoint.GetPosition());
    }

    private void Update()
    {
        if( opponentCar.destinationReached)
        {
            currentWayPoint = currentWayPoint.nextWaypoint;
            opponentCar.LocateDestination(currentWayPoint.GetPosition());
        }
    }
}
