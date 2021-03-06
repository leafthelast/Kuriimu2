﻿using System;
using System.IO;
using Kompression.Implementations.Encoders.Headerless;
using Kontract.Kompression;
using Kontract.Kompression.Configuration;

namespace Kompression.Implementations.Encoders.Level5
{
    public class Lz10Encoder : IEncoder
    {
        private readonly Lz10HeaderlessEncoder _encoder;

        public Lz10Encoder(IMatchParser matchParser)
        {
            _encoder = new Lz10HeaderlessEncoder(matchParser);
        }

        public void Encode(Stream input, Stream output)
        {
            if (input.Length > 0x1FFFFFFF)
                throw new InvalidOperationException("Data to compress is too long.");

            var compressionHeader = new[] {
                (byte)((byte)(input.Length << 3) | 1),
                (byte)(input.Length >> 5),
                (byte)(input.Length >> 13),
                (byte)(input.Length >> 21) };
            output.Write(compressionHeader, 0, 4);

            _encoder.Encode(input,output);
        }

        public void Dispose()
        {
            // Nothing to dispose
        }
    }
}
