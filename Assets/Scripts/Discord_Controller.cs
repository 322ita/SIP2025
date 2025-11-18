using Discord;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Discord_Controller : MonoBehaviour
{
    public long applicationID;
    [Space]
    public string details = "Currently playing the game";
    public string state = "Current Scene: ";
    [Space]
    public string largeImage = "game_logo";
    public string largeText = "Project Shadow";


    private long time;

    private static bool instanceExists;
    public Discord.Discord discord;
    void IFisEditor()
    {
        if (Application.isEditor)
        {
            details = "Currently testing the game";
            state = "Current Scene: ";
        }
    }
    void Awake()
    {
        IFisEditor();
        // Transition the GameObject between scenes, destroy any duplicates
        if (!instanceExists)
        {
            instanceExists = true;
            DontDestroyOnLoad(gameObject);
        }
        else if (FindObjectsByType(GetType(), FindObjectsInactive.Include, FindObjectsSortMode.None).Length > 1)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Log in with the Application ID
        discord = new Discord.Discord(applicationID, (System.UInt64)Discord.CreateFlags.NoRequireDiscord);
        time = System.DateTimeOffset.Now.ToUnixTimeMilliseconds();

        UpdateStatus();
    }

    void Update()
    {
        // Destroy the GameObject if Discord isn't running
        try
        {
            discord.RunCallbacks();
        }
        catch
        {
            Destroy(gameObject);
        }
    }

    void LateUpdate()
    {
        UpdateStatus();
    }

    void UpdateStatus()
    {
        // Update Status every frame
        try
        {
            var activityManager = discord.GetActivityManager();
            var activity = new Discord.Activity
            {
                Details = details,
                State = state + SceneManager.GetActiveScene().name,
                Assets =
                {
                    LargeImage = largeImage,
                    LargeText = largeText
                },
                Timestamps =
                {
                    Start = time
                }
            };

            activityManager.UpdateActivity(activity, (res) =>
            {
                if (res != Discord.Result.Ok) Debug.LogWarning("Failed connecting to Discord!");
            });
        }
        catch
        {
            // If updating the status fails, Destroy the GameObject
            Destroy(gameObject);
        }
    }
    private void OnApplicationQuit()
    {
        discord.Dispose();
    }
    
}