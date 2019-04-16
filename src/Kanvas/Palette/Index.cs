﻿using Kanvas.Interface;
using Kanvas.Models;
using Komponent.IO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace Kanvas.Palette
{
    public class Index : IPaletteImageFormat
    {
        public int IndexDepth { get; }
        public string FormatName { get; }

        public ByteOrder ByteOrder { get; set; } = ByteOrder.LittleEndian;

        public Index(int indexDepth)
        {
            if (indexDepth % 4 != 0) throw new InvalidOperationException("IndexDepth has to be dividable by 4.");

            IndexDepth = indexDepth;
            FormatName = $"Palette {indexDepth}Bit";
        }

        public IEnumerable<IndexData> LoadIndeces(byte[] data)
        {
            using (var br = new BinaryReaderX(new MemoryStream(data), false, ByteOrder))
                while (br.BaseStream.Position < br.BaseStream.Length || !br.IsFirstNibble)
                    switch (IndexDepth)
                    {
                        case 4:
                            yield return new IndexData(br.ReadNibble());
                            break;
                        case 8:
                            yield return new IndexData(br.ReadByte());
                            break;
                        default:
                            throw new InvalidOperationException($"IndexDepth {IndexDepth} not supported.");
                    }
        }

        public Color RetrieveColor(IndexData indexData, IList<Color> palette)
        {
            return palette[indexData.Index];
        }

        // TODO: Rethink this method, as it wouldn't work for AI properly
        public IndexData RetrieveIndexData(int index, Color color)
        {
            return new IndexData(index);
        }

        public byte[] SaveIndices(IEnumerable<IndexData> indeces)
        {
            var ms = new MemoryStream();
            using (var bw = new BinaryWriterX(ms, true, ByteOrder))
            {
                foreach (var indexData in indeces)
                    switch (IndexDepth)
                    {
                        case 4:
                            bw.WriteNibble(indexData.Index);
                            break;
                        case 8:
                            bw.Write((byte)indexData.Index);
                            break;
                        default:
                            throw new Exception($"IndexDepth {IndexDepth} not supported!");
                    }
            }

            return ms.ToArray();
        }
    }
}