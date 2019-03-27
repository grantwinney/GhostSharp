using Newtonsoft.Json;
using RestSharp.Deserializers;
using RestSharp.Serializers;
using System.IO;

namespace GcmSharp.Serialization
{
    // Copyright (c) Philipp Wagner. All rights reserved.
    // Licensed under the MIT license. See LICENSE file in the project root for full license information.
    // https://www.bytefish.de/blog/restsharp_custom_json_serializer/

    public class NewtonsoftJsonSerializer : ISerializer, IDeserializer
    {
        private JsonSerializer serializer;

        public NewtonsoftJsonSerializer(JsonSerializer serializer)
        {
            this.serializer = serializer;
        }

        public string ContentType
        {
            get { return "application/json"; } // Probably used for Serialization?
            set { }
        }

        public string DateFormat { get; set; }

        public string Namespace { get; set; }

        public string RootElement { get; set; }

        public string Serialize(object obj)
        {
            using (var stringWriter = new StringWriter())
            {
                using (var jsonTextWriter = new JsonTextWriter(stringWriter))
                {
                    serializer.Serialize(jsonTextWriter, obj);

                    return stringWriter.ToString();
                }
            }
        }

        public T Deserialize<T>(RestSharp.IRestResponse response)
        {
            var content = response.Content;

            using (var stringReader = new StringReader(content))
            {
                using (var jsonTextReader = new JsonTextReader(stringReader))
                {
                    return serializer.Deserialize<T>(jsonTextReader);
                }
            }
        }

        public static NewtonsoftJsonSerializer Default
        {
            get
            {
                return new NewtonsoftJsonSerializer(new JsonSerializer()
                {
                    NullValueHandling = NullValueHandling.Ignore,
                });
            }
        }
    }
}
