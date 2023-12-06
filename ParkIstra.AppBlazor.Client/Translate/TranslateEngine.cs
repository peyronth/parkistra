public class TranslateEngine {
    public static Dictionary<string, string> GlobalDictionary { get; set; } = new Dictionary<string, string>
    {
        { "helloWorld", "Hello World!" },
        { "helloWorld2", "Hello World 2!" }
    };
}