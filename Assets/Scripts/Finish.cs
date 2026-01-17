using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Finish : MonoBehaviour
{
    [Header("Finish UI Var")]
    public GameObject finishUI;
    public GameObject playerUI;
    public GameObject playerCar;

    [Header("Win/Lose Status")]
    public Text status;

    private void Start()
    {
        StartCoroutine(WaitForTheFinishUI());
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            StartCoroutine(FinishZoneTimer());
            gameObject.GetComponent<BoxCollider>().enabled = false;

            status.text = "You Win";
            status.color = Color.black;
        }
        else if( other.gameObject.tag == "OpponentCar")
        {
            StartCoroutine(FinishZoneTimer());
            gameObject.GetComponent<BoxCollider>().enabled = false;

            status.text = "You Lose";
            status.color = Color.red;
        }
    }

    IEnumerator WaitForTheFinishUI()
    {
        gameObject.GetComponent<BoxCollider>().enabled = false;
        yield return new WaitForSeconds(25f);
        gameObject.GetComponent<BoxCollider>().enabled = true;
    }

    IEnumerator FinishZoneTimer()
    {
        finishUI.SetActive(true);
        playerUI.SetActive(false);
        playerCar.SetActive(false);

        yield return new WaitForSeconds(5f);
        Time.timeScale = 0f;


    }
}
