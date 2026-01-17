using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CarSelection : MonoBehaviour
{
    

    [Header("Buttons and Canvas")]
    public Button nextButton;
    public Button previousButton;

    [Header("Cameras")]
    public GameObject cam1;
    //public GameObject cam2;
    [Header("Free Camera controller")]
    public CameraController cameraController;





    [Header("Buttons and Canvas")]
    public GameObject selectionCanvas;
    public GameObject SkipButton;
    public GameObject PlayButton;

    private int currentCar;
    private GameObject[] carList;

    private void Awake()
    {
        selectionCanvas.SetActive(false);
        PlayButton.SetActive(false);
        //cam2.SetActive(false);
        ChooseCar(0);
    }

    private void Start()
    {
        currentCar = PlayerPrefs.GetInt("CarSelected");
        // feeding a model to carList array
        carList = new GameObject[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            carList[i] = transform.GetChild(i).gameObject;
        }

        // keeping track of the currentCar
        foreach (GameObject go in carList)
        {
            go.SetActive(false);
        }
        if (carList[currentCar])
            carList[currentCar].SetActive(true);

    }

    private void ChooseCar(int index)
    {
        previousButton.interactable = (currentCar != 0);
        nextButton.interactable = (currentCar != transform.childCount - 1);
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(i == index);
        }
    }

    public void SwitchCar(int switchCars)
    {
        currentCar += switchCars;
        ChooseCar(currentCar);
    }

    public void PlayGame()
    {
        PlayerPrefs.SetInt("CarSelected", currentCar);
        SceneManager.LoadScene("scene_day");

    }

    public void skipButton()
    {
        //selectionCanvas.SetActive(true);
        //PlayButton.SetActive(true);
        //SkipButton.SetActive(false);
        //cam1.SetActive(false);
        //cam2.SetActive(true);
        //cameraController.cinemachineBrain.enabled = true;
        //cameraController.ResetView();

        // UI
        selectionCanvas.SetActive(true);
        PlayButton.SetActive(true);
        SkipButton.SetActive(false);

        // Cinemachine
        cameraController.cinemachineBrain.enabled = true;
        cameraController.selectionCanvas.SetActive(true);
        cameraController.ResetView(); // activates FollowCam
    }
}
