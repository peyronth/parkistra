namespace ParkIstra.Libraries.EF;

public class SqlScriptParser
{
    public List<string> GetSqlBatches(string fileName)
    {
        using TextReader reader = File.OpenText(fileName);
        var script = reader.ReadToEnd();

        return script.Split("GO")
            .Select(s => s.Trim(new[] { '\n', '\r', '\t' }))
            .Where(s => !(string.IsNullOrEmpty(s) ||
                s.StartsWith("use", StringComparison.InvariantCultureIgnoreCase)))
            .ToList();
    }
}
