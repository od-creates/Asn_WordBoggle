using System.Collections.Generic;
using UnityEngine;

public static class ScoreTable
{
    // Define point values for each letter
    private static readonly Dictionary<char, int> table = new Dictionary<char, int>()
    {
        { 'A', 0 }, { 'B', 1 }, { 'C', 1 }, { 'D', 1 },
        { 'E', 0 }, { 'F', 2 }, { 'G', 3 }, { 'H', 2 },
        { 'I', 0 }, { 'J', 3 }, { 'K', 2 }, { 'L', 2 },
        { 'M', 2 }, { 'N', 2 }, { 'O', 0 }, { 'P', 1 },
        { 'Q', 3 }, { 'R', 1 }, { 'S', 1 }, { 'T', 2 },
        { 'U', 0 }, { 'V', 3 }, { 'W', 2 }, { 'X', 3 },
        { 'Y', 2 }, { 'Z',3 }
    };

    /// <summary>
    /// Returns the point value for the given letter.
    /// Defaults to 1 if the letter isn't in the table.
    /// </summary>
    public static int GetValue(char letter)
    {
        letter = char.ToUpperInvariant(letter);
        if (table.TryGetValue(letter, out var val))
            return val;
        // fallback for unexpected characters
        Debug.LogWarning($"ScoreTable: no entry for '{letter}', defaulting to 0 point.");
        return 0;
    }
}