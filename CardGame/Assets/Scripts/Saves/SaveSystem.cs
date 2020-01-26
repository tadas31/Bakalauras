using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;

public static class SaveSystem
{
    private static readonly string PATH = Application.persistentDataPath + "/deck.save";

    public static void SaveDeck(List<string> cardsInDeck)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(PATH, FileMode.Create);

        Deck data = new Deck(cardsInDeck);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static Deck LoadDeck()
    {
        if (File.Exists(PATH) )
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(PATH, FileMode.Open);

            Deck data = (Deck)formatter.Deserialize(stream);
            stream.Close();

            return data;
        }
        else
        {
            Debug.Log("Save file not found in " + PATH);
            return null;
        }
    }
}
