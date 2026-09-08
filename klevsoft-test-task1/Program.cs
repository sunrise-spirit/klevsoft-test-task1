using RunLengthCoding;

Console.WriteLine("Введите строку (маленькие буквы латинского алфавита)");
string input = Console.ReadLine() ?? string.Empty;

string compressed = RunLengthCodec.Compress(input);
Console.WriteLine($"Сжатая строка: {compressed}");

string decompressed = RunLengthCodec.Decompress(compressed);
Console.WriteLine($"Распакованная строка: {decompressed}");


