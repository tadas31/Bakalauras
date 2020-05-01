using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;

public static class SaveSystem
{
    public static readonly string DECK_PATH = Application.persistentDataPath + "/deck.dt";
    public static readonly string PUZZLE_PATH = Application.persistentDataPath + "/puzzle.dt";

    // Saves cards in deck
    public static void SaveDeck(List<DeckFormat> cardsInDeck)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(DECK_PATH, FileMode.Create);

        Deck data = new Deck(cardsInDeck);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    // Loads cards that are in deck from save file
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
            Debug.Log("Deck save file not found in " + DECK_PATH);
            return null;
        }
    }

    // Saves completed puzzles
    public static void SaveCompletedPuzzles(int completedPuzzles)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(PUZZLE_PATH, FileMode.Create);

        CompletedPuzzles data = new CompletedPuzzles(completedPuzzles);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    // Loads completed puzzles
    public static CompletedPuzzles LoadCompletedPuzzles()
    {
        if (File.Exists(PUZZLE_PATH) )
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(PUZZLE_PATH, FileMode.Open);

            CompletedPuzzles data = (CompletedPuzzles)formatter.Deserialize(stream);
            stream.Close();

            return data;
        }
        else
        {
            Debug.Log("Puzzle save file not found in " + PUZZLE_PATH);
            return null;
        }
    }
}
