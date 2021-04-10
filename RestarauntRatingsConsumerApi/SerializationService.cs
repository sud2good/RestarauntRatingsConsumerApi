
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace RestarauntRatingsConsumerApi
{
    public class SerializationService
    {
        public List<RatingsDto> Deserialize(string filePath)
        {
            string inputData = string.Empty;
            inputData = System.IO.File.ReadAllText(filePath);
            var model = JsonSerializer.Deserialize<List<RatingsDto>>(inputData);

            return model;
        }
    }
}
