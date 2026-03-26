using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static void SaveGame(int health, int coins, Vector3 position)
    {
        PlayerPrefs.SetInt("health", health);
        PlayerPrefs.SetInt("coins", coins);
        PlayerPrefs.SetFloat("posX", position.x);
        PlayerPrefs.SetFloat("posY", position.y);
        PlayerPrefs.Save();
    }

    public static bool HasSave()
    {
        return PlayerPrefs.HasKey("health");
    }

    public static int LoadHealth() => PlayerPrefs.GetInt("health", 3);
    public static int LoadCoins() => PlayerPrefs.GetInt("coins", 0);
    public static Vector3 LoadPosition()
    {
        float x = PlayerPrefs.GetFloat("posX", 0);
        float y = PlayerPrefs.GetFloat("posY", 0);
        return new Vector3(x, y, 0);
    }

    public static void DeleteSave()
    {
        PlayerPrefs.DeleteAll();
    }
}