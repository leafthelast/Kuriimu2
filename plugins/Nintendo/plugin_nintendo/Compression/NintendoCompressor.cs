﻿using System;
using System.IO;
using Kontract.Kompression.Configuration;
using plugin_nintendo.Compression;

namespace plugin_level5.Compression
{
    class NintendoCompressor
    {
        public static IKompressionConfiguration GetConfiguration(NintendoCompressionMethod method)
        {
            switch (method)
            {
                case NintendoCompressionMethod.Lz10:
                    return Kompression.Implementations.Compressions.Nintendo.Lz10;

                case NintendoCompressionMethod.Lz11:
                    return Kompression.Implementations.Compressions.Nintendo.Lz11;

                case NintendoCompressionMethod.Lz40:
                    return Kompression.Implementations.Compressions.Nintendo.Lz40;

                case NintendoCompressionMethod.Lz60:
                    return Kompression.Implementations.Compressions.Nintendo.Lz60;

                case NintendoCompressionMethod.Huffman4:
                    return Kompression.Implementations.Compressions.Nintendo.Huffman4Bit;

                case NintendoCompressionMethod.Huffman8:
                    return Kompression.Implementations.Compressions.Nintendo.Huffman8Bit;

                case NintendoCompressionMethod.Rle:
                    return Kompression.Implementations.Compressions.Nintendo.Rle;

                default:
                    throw new NotSupportedException($"Unknown compression method {method}");
            }
        }
    }
}
