using Microsoft.Extensions.Localization;

public class TranslationService : ITranslationService
{
    private readonly IStringLocalizer<TranslationService> _localizer;

    public TranslationService(IStringLocalizer<TranslationService> localizer)
    {
        _localizer = localizer;
    }

    public string Translate(string key)
    {
        return _localizer[key];
    }
}
