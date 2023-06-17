using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Player))]
public class PlayerAgent : Agent
{
    public UnityEvent onInit;
    private Player _player;
    private PlayerMove _playerMove;

    private Session _session;

    private void Update()
    {
        AddReward(Time.deltaTime);
    }

    public override void Initialize()
    {
        _player = GetComponent<Player>();
        _playerMove = GetComponent<PlayerMove>();
        _session = GetComponentInParent<Session>();
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(_player.transform.position.y);
        sensor.AddObservation(_player.Rigidbody2D.velocity.y);

        sensor.AddObservation(_session.ObstacleWorker.Speed);

        var obstacles = _session.ObstacleWorker.GetNearObstacle(transform.position.x);
        if (obstacles == null)
        {
            sensor.AddObservation(10f);
            sensor.AddObservation(0f);

            sensor.AddObservation(10f);
            sensor.AddObservation(0f);
        }
        else
        {
            sensor.AddObservation(obstacles[0].transform.position.x);
            sensor.AddObservation(obstacles[0].transform.position.y);

            if (obstacles.Length == 1)
            {
                sensor.AddObservation(10f);
                sensor.AddObservation(0f);
            }
            else
            {
                sensor.AddObservation(obstacles[1].transform.position.x);
                sensor.AddObservation(obstacles[1].transform.position.y);
            }
        }
    }

    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        var action = actionBuffers.DiscreteActions[0];
        switch (action)
        {
            case 1:
                _playerMove.Jump();
                break;
        }
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var discreteActions = actionsOut.DiscreteActions;
        discreteActions[0] = Input.GetKey(KeyCode.Space) ? 1 : 0;
    }

    public override void OnEpisodeBegin()
    {
        onInit?.Invoke();
    }

    public void Kill()
    {
        EndEpisode();
    }
}