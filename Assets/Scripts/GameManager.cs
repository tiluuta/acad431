using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;



public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI bestTimeText;
    public GameObject artboardCube;
    public GameObject artboardCanvas;
    public TextMeshProUGUI timerText;
    //public TextMeshProUGUI countdownText;
    public Button doneButton;
    public string nextScene;

    public Texture2D[] userDifficulty1Artworks_userArtwork1;
    public Texture2D[] userDifficulty1Artworks_userArtwork2;
    public Texture2D[] userDifficulty2Artworks_userArtwork1;
    public Texture2D[] userDifficulty2Artworks_userArtwork2;
    public Texture2D[] userDifficulty3Artworks_userArtwork1;
    public Texture2D[] userDifficulty3Artworks_userArtwork2;

    private NewUserData userData;
    private float currentTime;
    public static bool isTimerRunning = true;

    private void Start()
    {
        userData = new NewUserData();
        userData.LoadThis();

        // Display the best time
        bestTimeText.text = "Best Time: " + userData.bestTime.ToString("F2");

        // Modify the size of the Artboard canvas
            switch (userData.canvasSize)
    {
            case "Small":
                artboardCube.transform.localScale = new Vector3(0.5f, 0.5f, .008f);
                break;
            case "Medium":
                artboardCube.transform.localScale = new Vector3(1f, 1f, 0.08f);
                break;
            case "Large":
                artboardCube.transform.localScale = new Vector3(2f, 2f, 0.16f);
                break;
            default:
                artboardCube.transform.localScale = new Vector3(0.5f, 0.5f, .008f);
                break;
    }

        // Load the desired artwork based on userDifficulty and userArtwork
        Texture2D selectedArtwork = GetSelectedArtwork();
        artboardCanvas.GetComponent<RawImage>().texture = selectedArtwork;

        currentTime = 0f;
        isTimerRunning = true;

        Button btn = doneButton.GetComponent<Button>();
        btn.onClick.AddListener(OnDoneButtonClicked);
    }

    private void Update()
        {
            // Debug.Log("" + currentTime);
            if(isTimerRunning){
                currentTime += Time.deltaTime;
            }

        if (currentTime > 1f)
            {
                // destroy the canvas
                // move the canvas forward
                // destroy the canvas
                Debug.Log("yayyyyy we destroyed it");
                Destroy(artboardCanvas);
                Destroy (GameObject.Find ("DaCanvas"));
            }
            UpdateTimerDisplay();
            // if the texture is set to the artboardCanvas, destroy the texture, after 1 seconds
            // Debug.Log("" + currentTime);

            if(GameObject.Find("Pause Menu") || GameObject.Find("Pause Menu(Clone)")){
                isTimerRunning = false;
            } else{
                isTimerRunning = true;
            }
        }

    public void UpdateTimerDisplay()
    {
        timerText.text = "Timer: " + currentTime.ToString("F2");
        //countdownText.text = "Countdown: " + (userData.latestTime - currentTime).ToString("F2");
    }

public void OnDoneButtonClicked()
{
    isTimerRunning = false;

    if (currentTime < userData.bestTime || userData.bestTime == 0f)
    {
        userData.bestTime = currentTime;
        userData.NewSave();
    }

    userData.latestTime = currentTime;
    userData.NewSave();

    // Load the desired scene
    SceneManager.LoadScene(nextScene);
}

    private Texture2D GetSelectedArtwork()
    {
        Texture2D[] artworks = null;

        switch (userData.userDifficulty)
        {
            case 1:
                artworks = userData.userArtwork == 1 ? userDifficulty1Artworks_userArtwork1 : userDifficulty1Artworks_userArtwork2;
                break;
            case 2:
                artworks = userData.userArtwork == 1 ? userDifficulty2Artworks_userArtwork1 : userDifficulty2Artworks_userArtwork2;
                break;
            case 3:
                artworks = userData.userArtwork == 1 ? userDifficulty3Artworks_userArtwork1 : userDifficulty3Artworks_userArtwork2;
                break;
            default:
                artworks = userDifficulty1Artworks_userArtwork1;
                break;
        }

        if (artworks != null && artworks.Length > 0)
        {
            return artworks[0];
        }

        return null;
    }



}

[System.Serializable]
public class NewUserData
{
    public float latestTime; //total time of last game
    public float bestTime; //if new time is less than latest time
    public int userDifficulty; //last selected difficulty level
    public string userControl; //last selected control type
    public int userArtwork; //last selected artwork
    public string canvasSize; //size of the canvas

    //default constructor
    public NewUserData() { }

  public NewUserData(NewUserData newData)
{
    latestTime = newData.latestTime;
    bestTime = newData.bestTime;
    userControl = newData.userControl;
    canvasSize = newData.canvasSize;

    // Convert userDifficulty to int
    if (int.TryParse(newData.userDifficulty.ToString(), out int difficulty))
    {
        userDifficulty = difficulty;
    }
    else
    {
        userDifficulty = 1;
    }

    // Convert userArtwork to int
    if (int.TryParse(newData.userArtwork.ToString(), out int artwork))
    {
        userArtwork = artwork;
    }
    else
    {
        userArtwork = 1;
    }
}

    //load from json
    public void LoadThis()
    {
        string path = Application.persistentDataPath + "/userdata.json";
        if (File.Exists(path))
        {
            string textData = File.ReadAllText(path);
            NewUserData loadedData = JsonUtility.FromJson<NewUserData>(textData);
            latestTime = loadedData.latestTime;
            bestTime = loadedData.bestTime;
            userControl = loadedData.userControl;
            userDifficulty = loadedData.userDifficulty;
            userArtwork = loadedData.userArtwork;
            canvasSize = loadedData.canvasSize;
        }
    }

    //save into json
    public void NewSave()
    {
        string path = Application.persistentDataPath + "/userdata.json";
        Debug.Log("Saved into: " + path);
        string jsonString = JsonUtility.ToJson(this, true);
        File.WriteAllText(path, jsonString);
    }
}

