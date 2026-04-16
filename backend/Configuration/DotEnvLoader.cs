namespace backend.Configuration;

public static class DotEnvLoader
{
    public static void Load(string filePath)
    {
        if (!File.Exists(filePath))
        {
            return;
        }

        foreach (var rawLine in File.ReadLines(filePath))
        {
            var trimmedLine = rawLine.TrimStart();
            if (string.IsNullOrWhiteSpace(trimmedLine) || trimmedLine.StartsWith('#'))
            {
                continue;
            }

            var content = trimmedLine.StartsWith("export ", StringComparison.OrdinalIgnoreCase)
                ? trimmedLine[7..]
                : trimmedLine;

            var separatorIndex = content.IndexOf('=');
            if (separatorIndex <= 0)
            {
                continue;
            }

            var key = content[..separatorIndex].Trim();
            if (string.IsNullOrWhiteSpace(key))
            {
                continue;
            }

            var value = content[(separatorIndex + 1)..];
            Environment.SetEnvironmentVariable(key, value);
        }
    }
}