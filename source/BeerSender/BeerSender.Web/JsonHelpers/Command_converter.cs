using System.Text.Json;
using System.Text.Json.Serialization;
using BeerSender.Domain;

namespace BeerSender.Web.JsonHelpers;

public class Command_converter : JsonConverter<Command_message>
{
    private static readonly Dictionary<string, Type> TypeLookup = new();

    static Command_converter()
    {
        var command_types = typeof(Command)
            .Assembly
            .GetTypes()
            .Where(type => !type.IsAbstract && typeof(Command).IsAssignableFrom(type));

        foreach (var command_type in command_types)
        {
            TypeLookup[command_type.Name] = command_type;
        }
    }

    public override bool CanConvert(Type type)
    {
        return typeof(Command_message).IsAssignableFrom(type);
    }

    public override Command_message? Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.StartObject)
        {
            throw new JsonException();
        }

        if (!reader.Read()
            || reader.TokenType != JsonTokenType.PropertyName
            || reader.GetString()?.ToLower() != "$type")
        {
            throw new JsonException();
        }

        if (!reader.Read() || reader.TokenType != JsonTokenType.String)
        {
            throw new JsonException();
        }

        var typeDiscriminator = reader.GetString();
        var commandType = TypeLookup[typeDiscriminator!];

        if (!reader.Read()
            || reader.TokenType != JsonTokenType.PropertyName
            || reader.GetString()?.ToLower() != "aggregate_id")
        {
            throw new JsonException();
        }

        if (!reader.Read() || reader.TokenType != JsonTokenType.String)
        {
            throw new JsonException();
        }

        var aggregateIdString = reader.GetString();
        Guid.TryParse(aggregateIdString, out Guid aggregateId);

        if (!reader.Read() || reader.GetString()?.ToLower() != "command")
        {
            throw new JsonException();
        }

        if (!reader.Read() || reader.TokenType != JsonTokenType.StartObject)
        {
            throw new JsonException();
        }

        var command = (Command)JsonSerializer.Deserialize(ref reader, commandType)!;

        if (!reader.Read() || reader.TokenType != JsonTokenType.EndObject)
        {
            throw new JsonException();
        }

        return new Command_message(aggregateId, command);
    }

    public override void Write(
        Utf8JsonWriter writer,
        Command_message value,
        JsonSerializerOptions options)
    {
        writer.WriteStartObject();

        writer.WriteString("$type", value.GetType().Name);
        writer.WriteString("aggregate_id", value.Aggregate_id);
        writer.WritePropertyName("command");
        JsonSerializer.Serialize(writer, value, value.GetType());

        writer.WriteEndObject();
    }
}