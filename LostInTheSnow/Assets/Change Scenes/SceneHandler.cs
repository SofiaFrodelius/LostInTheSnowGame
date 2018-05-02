using UnityEngine.SceneManagement;

public static class SceneHandler
{
    //private static int currentSceneIndex = 0;

    public static void ChangeScene(int index)
    {
        SceneManager.LoadSceneAsync(index);
    }

    public static void ChangeSceneByName(string name)
    {
        SceneManager.LoadSceneAsync(name);
    }

    public static void NextScene()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public static void PreviousScene()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
