using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using MsgPack.Light;
using SerializationPerformanceTest.TestData.BelgianBeer;

namespace SerializationPerformanceTest.Testers
{
    class MsgPackLightTester<T> : SerializationTester<T>
    {
        private readonly MsgPackContext context;

        public MsgPackLightTester(T testObject) : base(testObject)
        {
            this.context = new MsgPackContext();
            this.context.RegisterConverter(new Converter());
        }

        protected override T Deserialize()
        {
            base.MemoryStream.Position = 0;
            return MsgPackSerializer.Deserialize<T>(base.MemoryStream.ToArray(), this.context);
        }

        protected override MemoryStream Serialize()
        {
            return new MemoryStream(MsgPackSerializer.Serialize(base.TestObject, this.context));
        }

        private class Converter : IMsgPackConverter<Beer>
        {
            public void Write(Beer value, IMsgPackWriter writer, MsgPackContext context)
            {
                if (value == null)
                {
                    context.NullConverter.Write(null, writer, context);
                    return;
                }

                writer.WriteArrayHeader(4);
                context.GetConverter<string>().Write(value.Brand, writer, context);
                context.GetConverter<List<string>>().Write(value.Sort, writer, context);
                context.GetConverter<float>().Write(value.Alcohol, writer, context);
                context.GetConverter<string>().Write(value.Brewery, writer, context);
            }

            public Beer Read(IMsgPackReader reader, MsgPackContext context, Func<Beer> creator)
            {
                var length = reader.ReadArrayLength();
                if (length == null)
                {
                    return null;
                }

                if (length != 4)
                {
                    throw new SerializationException("Bad format");
                }

                return new Beer
                {
                    Brand = context.GetConverter<string>().Read(reader, context, null),
                    Sort = context.GetConverter<List<string>>().Read(reader, context, null),
                    Alcohol = context.GetConverter<float>().Read(reader, context, null),
                    Brewery = context.GetConverter<string>().Read(reader, context, null)
                };
            }
        }
    }
}