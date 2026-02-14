using System.Reflection;
using System.Resources;
using NUnit.Framework;
using Phnx.Shared.Constants;

namespace Phnx.Tests.LanguageTests;

public class LanguageConstantsResourceTests
{
    private const string ResourceBaseName = "Phnx.Shared.Resources.Language";

    [Test]
    public void AllLanguageConstants_ShouldExistInResourceFile()
    {
        var resourceManager = new ResourceManager(ResourceBaseName, typeof(LanguageConstants).Assembly);
        var constantFields = GetAllLanguageConstantFields();

        var missingKeys = new List<string>();
        var allKeys = new List<string>();

        foreach (var field in constantFields)
        {
            var keyValue = field.GetValue(null)?.ToString();
            if (string.IsNullOrEmpty(keyValue))
                continue;

            allKeys.Add(keyValue);

            try
            {
                var resourceValue = resourceManager.GetString(keyValue);
                if (string.IsNullOrEmpty(resourceValue))
                {
                    missingKeys.Add(keyValue);
                }
            }
            catch (MissingManifestResourceException)
            {
                missingKeys.Add(keyValue);
            }
        }

        Assert.That(
            missingKeys.Count == 0,
            $"Missing resource keys ({missingKeys.Count}/{allKeys.Count}):\n" +
            $"{string.Join("\n", missingKeys.Select(k => $"  - {k}"))}"
        );
    }

    [Test]
    public void AllLanguageConstants_ShouldHaveNonEmptyValues()
    {
        var resourceManager = new ResourceManager(ResourceBaseName, typeof(LanguageConstants).Assembly);
        var constantFields = GetAllLanguageConstantFields();

        var emptyValueKeys = new List<string>();

        foreach (var field in constantFields)
        {
            var keyValue = field.GetValue(null)?.ToString();
            if (string.IsNullOrEmpty(keyValue))
                continue;

            try
            {
                var resourceValue = resourceManager.GetString(keyValue);
                if (string.IsNullOrWhiteSpace(resourceValue))
                {
                    emptyValueKeys.Add(keyValue);
                }
            }
            catch
            {
            }
        }

        Assert.That(
            emptyValueKeys.Count == 0,
            $"Keys with empty or whitespace values:\n" +
            $"{string.Join("\n", emptyValueKeys.Select(k => $"  - {k}"))}"
        );
    }

    [Test]
    public void LanguageConstants_ShouldFollowNamingConvention()
    {
        var constantFields = GetAllLanguageConstantFields();
        var invalidConstants = new List<string>();

        foreach (var field in constantFields)
        {
            var fieldName = field.Name;
            var fieldValue = field.GetValue(null)?.ToString();

            if (fieldName != fieldValue)
            {
                invalidConstants.Add($"{fieldName} = {fieldValue}");
            }
        }

        Assert.That(
            invalidConstants.Count == 0,
            $"Constants not following nameof() pattern:\n" +
            $"{string.Join("\n", invalidConstants.Select(c => $"  - {c}"))}"
        );
    }

    [Test]
    public void LanguageConstants_ShouldHaveUniqueValues()
    {
        var constantFields = GetAllLanguageConstantFields();
        var duplicates = new List<string>();
        var valueGroups = constantFields
            .Select(f => new { Field = f.Name, Value = f.GetValue(null)?.ToString() })
            .Where(x => !string.IsNullOrEmpty(x.Value))
            .GroupBy(x => x.Value)
            .Where(g => g.Count() > 1);

        foreach (var group in valueGroups)
        {
            var fields = string.Join(", ", group.Select(x => x.Field));
            duplicates.Add($"Value '{group.Key}' used by: {fields}");
        }

        Assert.That(
            duplicates.Count == 0,
            $"Duplicate constant values found:\n" +
            $"{string.Join("\n", duplicates.Select(d => $"  - {d}"))}"
        );
    }

    private static IEnumerable<FieldInfo> GetAllLanguageConstantFields()
    {
        return typeof(LanguageConstants)
            .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
            .Where(f => f.IsLiteral && !f.IsInitOnly && f.FieldType == typeof(string));
    }
}
