using System.IO;
using MsgPack.Light;

namespace SerializationPerformanceTest.Testers
{
    class MsgPackLightStreams<T> : SerializationTester<T>
    {
        private readonly MsgPackContext _rootContext;

        public MsgPackLightStreams(T testObject) : base(testObject)
        {
            _rootContext = new MsgPackContext();
            _rootContext.RegisterConverter(new BeerConverter());
        }

        protected override T Deserialize()
        {
            MemoryStream.Position = 0;
            return MsgPackSerializer.Deserialize<T>(MemoryStream, _rootContext);
        }

        protected override MemoryStream Serialize()
        {
            var stream = new MemoryStream();
            MsgPackSerializer.Serialize(TestObject, stream, _rootContext);
            return stream;
        }
    }
}