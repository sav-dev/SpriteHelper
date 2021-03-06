﻿using SpriteHelper.Contract;
using SpriteHelper.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpriteHelper.Dialogs
{
    public partial class ChrProcess : Form
    {
        public ChrProcess()
        {
            InitializeComponent();

            var tilesets = Tilesets.Read(FileConstants.Tilesets);
            this.inputTextBox.Lines = (new[] { FileConstants.SprChr, FileConstants.TitleChr }).Union(tilesets.LoadedSets.Select(ts => ts.ChrPath())).ToArray();
        }

        private void ProcessButtonClick(object sender, EventArgs e)
        {
            foreach (var file in inputTextBox.Lines)
            {
                ProcessFile(file);
            }
        }

        private void ProcessFile(string path)
        {
            var bytes = File.ReadAllBytes(path);
            var result = new List<byte>();
            result.AddRange(Enumerable.Repeat<byte>(0, 16).ToList()); // 1st tile is always empty
            for (var i = 16; i < 4096; i += 16)
            {
                var isEmpty = true;
                for (var j = 0; j < 16; j++)
                {
                    if (bytes[i + j] != 0)
                    {
                        isEmpty = false;
                    }
                }

                if (!isEmpty)
                {
                    for (var j = 0; j < 16; j++)
                    {
                        result.Add(bytes[i + j]);
                    }
                }
                else
                {
                    break;
                }
            }

            // Pad to 256
            var targetSize = ((result.Count + 255) / 256) * 256;
            while (result.Count < targetSize)
            {
                result.Add(0);
            }

            var fi = new FileInfo(path);
            var targetFile = path.Substring(0, path.Length - fi.Extension.Length) + "Processed" + fi.Extension;
            File.WriteAllBytes(targetFile, result.ToArray());
        }
    }
}
