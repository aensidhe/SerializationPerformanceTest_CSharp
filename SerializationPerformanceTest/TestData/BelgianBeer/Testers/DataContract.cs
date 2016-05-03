using System.IO;
using System.Runtime.Serialization;

namespace SerializationPerformanceTest.Testers
{
    class DataContract<TTestObject> : SerializationTester<TTestObject>
    {
        private readonly DataContractSerializer serializer;

        public DataContract(TTestObject testObject)
            : base(testObject)
        {
            serializer = new DataContractSerializer(typeof(TTestObject));
        }

        protected override TTestObject Deserialize()
        {
            base.MemoryStream.Seek(0, 0);
            return (TTestObject)serializer.ReadObject(base.MemoryStream);
        }
        
        protected override MemoryStream Serialize()
        {
            var stream = new MemoryStream();

            serializer.WriteObject(stream, base.TestObject);

            return stream;
        }
    }
}