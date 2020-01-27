using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;

public static class SaveSystem
{
    private static readonly string DECK_PATH = Application.persistentDataPath + "/deck.dt";

    //saves cards in deck
    public static void SaveDeck(List<string> cardsInDeck)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(DECK_PATH, FileMode.Create);

        Deck data = new Deck(cardsInDeck);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    //loads cards that are in deck from save file
    public static Deck LoadDeck()
    {
        if (File.Exists(DECK_PATH) )
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(DECK_PATH, FileMode.Open);

            Deck data = (Deck)formatter.Deserialize(stream);
            stream.Close();

            return data;
        }
        else
        {
            Debug.Log("Save file not found in " + DECK_PATH);
            return null;
        }
    }
}
