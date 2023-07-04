using System.Text.Json;
using Models.Interfaces.DomainServices.JsonSerialiser;
using Newtonsoft.Json;

namespace Domain.JsonSerialiser;

public class JsonSerialiser : IJsonSerialiser
{
  public T DeserializeObject<T>(string json)
  {
    // Deserialise the JSON into an object
    var deserialisedContent = JsonConvert.DeserializeObject<T>(json);
    return deserialisedContent;
  }

  public string SerializeObject<T>(T obj)
  {
    // JSON serialiser settings
    var jsonSerialiserSettings = new JsonSerializerSettings
    {
      NullValueHandling = NullValueHandling.Ignore,
      DefaultValueHandling = DefaultValueHandling.Ignore,
    };

    // Serialise and return the json content in string format
    var serialisedObject = JsonConvert.SerializeObject(obj, Formatting.None, jsonSerialiserSettings);
    return serialisedObject;
  }

  public string? ParseDocumentProperty(string? content, string property)
  {
    // Parse the JSON content into a JSON document & extract the property
    var jsonDocument = JsonDocument.Parse(content);
    var propertyValue = jsonDocument.RootElement.GetProperty(property).GetString();
    return propertyValue;
  }
}