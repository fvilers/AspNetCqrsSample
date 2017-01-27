using Newtonsoft.Json;
using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;

namespace BookStore.Core.Serialization
{
    public class JsonTextSerializer : ITextSerializer
    {
        private readonly JsonSerializer _serializer;
        private static readonly JsonSerializerSettings DefaultSettings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All,
            TypeNameAssemblyFormat = FormatterAssemblyStyle.Simple
        };

        public JsonTextSerializer()
            : this(DefaultSettings)
        {
        }

        private JsonTextSerializer(JsonSerializerSettings settings)
            : this(CreateSerializer(settings))
        {
        }

        private JsonTextSerializer(JsonSerializer serializer)
        {
            if (serializer == null) throw new ArgumentNullException(nameof(serializer));
            _serializer = serializer;
        }

        public void Serialize(TextWriter writer, object graph)
        {
            if (writer == null) throw new ArgumentNullException(nameof(writer));
            if (graph == null) throw new ArgumentNullException(nameof(graph));

            var jsonWriter = new JsonTextWriter(writer);

            _serializer.Serialize(jsonWriter, graph);
            writer.Flush();
        }

        public object Deserialize(TextReader reader)
        {
            if (reader == null) throw new ArgumentNullException(nameof(reader));

            var jsonReader = new JsonTextReader(reader);

            try
            {
                return _serializer.Deserialize(jsonReader);
            }
            catch (JsonSerializationException e)
            {
                throw new SerializationException("Unable to deserialize reader.", e);
            }
        }

        private static JsonSerializer CreateSerializer(JsonSerializerSettings settings)
        {
            settings.EnsureFormatting();

            var serializer = JsonSerializer.Create(settings);

            return serializer;
        }
    }
}