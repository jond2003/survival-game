using UnityEngine;

public class ElevatorButton : MonoBehaviour
{
    public ElevatorController elevator;
    public bool isUpButton;  // Separates the buttons by what they do

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player presses the button so long as the tag I gave it remains as Player
        if (other.CompareTag("Player"))
        {
            if (isUpButton)
            {
                elevator.MoveUp();
            }
            else
            {
                elevator.MoveDown();
            }
        }
    }
}
