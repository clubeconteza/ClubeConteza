using System;
using System.Windows.Forms;
using System.IO;

namespace ContezaAdmin
{
    public partial class TinyMCE : UserControl
    {
        public TinyMCE()
        {
            InitializeComponent();
        }

        public string HtmlContent
        {
            get
            {
                string content = string.Empty;
                if (webBrowserControl.Document != null)
                {
                    object html = webBrowserControl.Document.InvokeScript("GetContent");
                    content = html as string;
                }
                return content;
            }
            set
            {
                if (webBrowserControl.Document != null)
                {
                    webBrowserControl.Document.InvokeScript("SetContent", new object[] { value });
                }
            }
        }

        public void CreateEditor()
        {

            // Check if the main script file exist being used by the HTML page
            if (File.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"tinymce\jscripts\tiny_mce\tiny_mce.js")))
            {
                webBrowserControl.Url = new Uri(@"file:///" + Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "tinymce.htm").Replace('\\', '/'));
            }
            else
            {
                MessageBox.Show("Não foi possível encontrar o diretório de script tinyMCE. Verifique se o diretório está no mesmo local que tinymce.html", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //string ArquivoJS = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + @"\tinymce\jscripts\tiny_mce\tinymce.js");
            //if (File.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory + @"\tinymce\jscripts\tiny_mce\tinymce.js")))
            //{
            //    webBrowserControl.Url = new Uri(@"file:///" + Path.Combine(AppDomain.CurrentDomain.BaseDirectory + @"test.htm").Replace('\\', '/'));
            //}

            //else
            //{
            //    MessageBox.Show("Não foi possível encontrar o diretório de script tinyMCE. Verifique se o diretório está no mesmo local que tinymce.html", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }
    }
}
