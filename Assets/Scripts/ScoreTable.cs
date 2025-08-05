using System.Collections.Generic;
using UnityEngine;

public static class ScoreTable
{
    // Define point values for each letter
    private static readonly Dictionary<char, int> table = new Dictionary<char, int>()
    {
        { 'A', 1 }, { 'B', 3 }, { 'C', 3 }, { 'D', 2 },
        { 'E', 1 }, { 'F', 4 }, { 'G', 2 }, { 'H', 4 },
        { 'I', 1 }, { 'J', 8 }, { 'K', 5 }, { 'L', 1 },
        { 'M', 3 }, { 'N', 1 }, { 'O', 1 }, { 'P', 3 },
        { 'Q',10 }, { 'R', 1 }, { 'S', 1 }, { 'T', 1 },
        { 'U', 1 }, { 'V', 4 }, { 'W', 4 }, { 'X', 8 },
        { 'Y', 4 }, { 'Z',10 }
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
        Debug.LogWarning($"ScoreTable: no entry for '{letter}', defaulting to 1 point.");
        return 1;
    }
}