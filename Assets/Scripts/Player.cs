using UnityEngine;

public class Player : MonoBehaviour
{
    public string Name;
    public float Health;

    private void OnEnable()
    {
        // Name = PlayerPrefs.GetString("PlayerName");
        Name = PlayerPrefs.GetString($"{nameof(Player)}{nameof(Name)}");
        Health = PlayerPrefs.GetFloat($"{nameof(Player)}{nameof(Health)}");

        Vector3 position = Vector3.zero;
        for (int i = 0; i < 3; i++)
        {
            position[i] =
                PlayerPrefs.GetFloat($"{nameof(Player)}Position{i}", 0);
        }

        transform.position = position;
    }


    private void OnDisable()
    {
        PlayerPrefs.SetString($"{nameof(Player)}{nameof(Name)}", Name);
        PlayerPrefs.GetFloat($"{nameof(Player)}{nameof(Health)}", Health);

        for (int i = 0; i < 3; i++)
        {
            PlayerPrefs.SetFloat($"{nameof(Player)}Position{i}", transform.position[i]);
        }
    }
}
