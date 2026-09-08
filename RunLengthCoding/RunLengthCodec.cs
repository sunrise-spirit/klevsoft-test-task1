using System.Text;

namespace RunLengthCoding;

/// <summary>
/// Кодирует и декодирует строки методом Run-Length Encoding (RLE).
/// Формат: за символом следует количество его повторов подряд, если оно больше 1
/// (например, "aaabbcccdde" -> "a3b2c3d2e").
/// </summary>
public static class RunLengthCodec
{
    public static string Compress(string input)
    {
        ArgumentNullException.ThrowIfNull(input);
        ValidateAlphabet(input);

        var sb = new StringBuilder();
        int i = 0;
        while (i < input.Length)
        {
            char current = input[i];
            int count = 1;
            while (i + count < input.Length && input[i + count] == current)
                count++;

            sb.Append(current);
            if (count > 1)
                sb.Append(count);

            i += count;
        }

        return sb.ToString();
    }

    public static string Decompress(string input)
    {
        ArgumentNullException.ThrowIfNull(input);

        var sb = new StringBuilder();
        int i = 0;
        while (i < input.Length)
        {
            char current = input[i];
            i++;

            int count = 0;
            while (i < input.Length && char.IsDigit(input[i]))
            {
                count = count * 10 + (input[i] - '0');
                i++;
            }

            sb.Append(current, count == 0 ? 1 : count);
        }

        return sb.ToString();
    }

    private static void ValidateAlphabet(string input)
    {
        foreach (char c in input)
        {
            if (c < 'a' || c > 'z')
                throw new ArgumentException(
                    $"Строка должна содержать только маленькие буквы латинского алфавита. Недопустимый символ: '{c}'.",
                    nameof(input));
        }
    }
}
