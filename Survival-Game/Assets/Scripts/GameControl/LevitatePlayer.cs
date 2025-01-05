using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevitatePlayer : MonoBehaviour
{

    [SerializeField] private float levitationTime = 5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //disable renderers in the checkpoint
            Renderer[] renderersInRobot = GetComponentsInChildren<Renderer>();
            foreach (Renderer renderer in renderersInRobot)
            {
                renderer.enabled = false;
            }


            //disable child text
            foreach (Transform child in this.gameObject.transform)
            {
                child.gameObject.SetActive(false);
            }

            StartCoroutine(FloatPlayer(other.gameObject));

        }
    }

    private IEnumerator FloatPlayer(GameObject player)
    {
        CharacterController characterController = player.GetComponent<CharacterController>();
        if (characterController != null)
        {
            float floatTime = 0f;
            PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
            playerMovement.gravityForce = 0f; //disable gravity for floating
            while (floatTime < levitationTime) //levitate for this many secs
            {
                Vector3 vectorToMove = Vector3.up * 10f * Time.deltaTime;
                characterController.Move(vectorToMove);
                floatTime += Time.deltaTime;
                yield return null;

            }
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadSceneAsync("WinScene");
        }
    }
}
