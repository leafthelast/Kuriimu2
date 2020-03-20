﻿using System.Collections.Generic;
using System.IO;
using Kontract.Interfaces.FileSystem;
using Kontract.Interfaces.Plugins.State;
using Kontract.Interfaces.Progress;
using Kontract.Interfaces.Providers;
using Kontract.Models.IO;
using Kontract.Models.Text;

namespace plugin_yuusha_shisu.TALK
{
    class TalkState : ITextState, ILoadFiles, ISaveFiles
    {
        private readonly TALK _talk;

        public IList<TextEntry> Texts { get; private set; }

        public bool ContentChanged { get; }

        public TalkState()
        {
            _talk = new TALK();
        }

        public async void Load(IFileSystem fileSystem, UPath filePath, ITemporaryStreamProvider temporaryStreamProvider,
            IProgressContext progress)
        {
            var fileStream = await fileSystem.OpenFileAsync(filePath);
            Texts = new[] { _talk.Load(fileStream) };
        }

        public void Save(IFileSystem fileSystem, UPath savePath, IProgressContext progress)
        {
            var output = fileSystem.OpenFile(savePath, FileMode.Create);
            _talk.Save(output, Texts[0]);
        }
    }
}