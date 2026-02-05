using System.Reflection;

namespace TechStore.Infra.Fake;

public static class FakeEntitySetter
{
    public static void SetPrivateId<T>(T entity, int id)
        where T : class => SetPrivateProperty(entity, "Id", id);

    public static void SetPrivateProperty<T>(T entity, string propName, object? value)
        where T : class
    {
        if (entity is null)
            throw new ArgumentNullException(nameof(entity));

        var prop = typeof(T).GetProperty(
            propName,
            BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic
        );

        if (prop is null)
            throw new InvalidOperationException(
                $"Propriedade '{propName}' não encontrada em {typeof(T).Name}."
            );

        prop.SetValue(entity, value);
    }
}
