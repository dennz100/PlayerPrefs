using System;
using System.IO;
using System.Linq;
using UnityEngine;

public class JSONExample : MonoBehaviour
{
    public string Name;
    public float Health;

    [TextArea]
    public string JsonData;

    private void OnEnable()
    {
        // JsonData = PlayerPrefs.GetString(nameof(JsonData));
        string path = Application.persistentDataPath;
        print(path);

        string filename = "savedata.game";

        using (FileStream stream = File.OpenRead($"{path}/{filename}"))
        {
            byte[] buffer = new byte[stream.Length];
            stream.Read(buffer, 0, buffer.Length);

            char[] data = new char[buffer.Length];
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = (char) buffer[i];
            }

            JsonData = new string(data);

            PlayerData playerData = JsonUtility.FromJson<PlayerData>(JsonData);

            Name = playerData.Name;
            Health = playerData.Health;
            transform.position = playerData.Position;
            transform.rotation = playerData.Rotation;
            
            // transform.rotation = playerData.GetRotation;

            // PlayerData playerData = (PlayerData) JsonUtility.FromJson(JsonData, typeof(PlayerData));
            // PlayerData playerData = JsonUtility.FromJson(JsonData, typeof(PlayerData)) as PlayerData;

            /*object deserializedData = JsonUtility.FromJson(JsonData, typeof(PlayerData));
            if (deserializedData is PlayerData playerData)
            {
                
            }*/
        }
    }

    private void OnDisable()
    {
        PlayerData playerData = new PlayerData
        {
            Name = Name,
            Health = Health,
            Position = transform.position,
            Strength = 100,
            Rotation = transform.rotation
        };

        // playerData.SetRotation(transform.rotation);

        JsonData = JsonUtility.ToJson(playerData, true);
        // PlayerPrefs.SetString(nameof(JsonData), JsonData);
        string path = Application.persistentDataPath;
        print(path);

        string filename = "savedata.game";
        using (FileStream stream = File.Create($"{path}\\{filename}"))
        {
            byte[] buffer = new byte[JsonData.Length];
            for (int i = 0; i < buffer.Length; i++)
            {
                buffer[i] = (byte) JsonData[i];
            }

            // byte[] buffer = JsonData.Select(@char => (byte)@char).ToArray();
            stream.Write(buffer, 0, buffer.Length);
        }
    }
}

public class PlayerData
{
    public string Name;
    public float Health;
    public Vector3 Position;



    [SerializeField]
    private Quaternion _rotation;

    [SerializeField]
    private int _strength;

    public int Strength
    {
        get => _strength;
        set => _strength = value;
    }

    public Quaternion Rotation
    {
        get
        {
            return _rotation;
        }
        set
        {
            _rotation = value;
        }
    }

    

    // Same as using a Property.
    /*public Quaternion GetRotation => _rotation;
    
    public void SetRotation(Quaternion rotation) => _rotation = rotation;*/
}