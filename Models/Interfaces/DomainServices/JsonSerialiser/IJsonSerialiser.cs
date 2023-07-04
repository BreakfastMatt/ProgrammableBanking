using System.Text.Json;

namespace Models.Interfaces.DomainServices.JsonSerialiser;


/// <summary>
/// The logic to serialise and deserialise JSON content
/// </summary>
public interface IJsonSerialiser
{
  /// <summary>
  /// Deserialises the supplied json string into the provided object
  /// </summary>
  /// <typeparam name="T">The model type of the object you are deserialising into</typeparam>
  /// <param name="json">The JSON file content in string format</param>
  /// <returns>The JSON content in object format</returns>
  public T DeserializeObject<T>(string json);


  /// <summary>
  /// Serialises the provided object into a JSON string
  /// </summary>
  /// <typeparam name="T">The model type of the object you are serialising from</typeparam>
  /// <param name="obj">The instance of the object you are serialising from</param>
  /// <returns>The JSON content in string format</returns>
  public string SerializeObject<T>(T obj);

  /// <summary>
  /// Parses the JSON content into a JsonDocument and extracts the provided property's string value.
  /// </summary>
  /// <param name="content">The JSON content</param>
  /// <param name="property">The JSON property value to extract</param>
  /// <returns>The parsed JSON document's property value</returns>
  public string? ParseDocumentProperty(string? content, string property);
}