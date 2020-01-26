using SpriteHelper.Contract;
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
using System.Xml.Serialization;

namespace SpriteHelper.Dialogs
{
    public partial class startingIdLabel : Form
    {
        public startingIdLabel()
        {
            InitializeComponent();
        }

        private void ProcessButtonClick(object sender, EventArgs e)
        {
            var id = int.Parse(this.staringIdTextBox.Text);
            var strings = new List<StringConfig>();
            foreach (var line in File.ReadAllLines(this.inputTextBox.Text))
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                strings.Add(new StringConfig
                {
                    Id = id++,
                    Value = line,
                });
            }

            var xmlSerializer = new XmlSerializer(typeof(StringConfig[]));
            using (var ms = new MemoryStream())
            {
                xmlSerializer.Serialize(ms, strings.ToArray());
                ms.Flush();
                ms.Position = 0;
                this.outputTextBox.Text = Encoding.UTF8.GetString(ms.ToArray());
            }
        }
    }
}
