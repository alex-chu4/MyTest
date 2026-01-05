using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FSP.Client
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = saveFileDialog1.InitialDirectory = Path.GetPathRoot(Application.ExecutablePath);
        }

        private void buttonOpen_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBoxSource.Text = openFileDialog1.FileName;
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBoxTarget.Text = saveFileDialog1.FileName;
            }
        }

        private void buttonODS_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                if (!string.IsNullOrEmpty(textBoxSource.Text) && !string.IsNullOrEmpty(textBoxTarget.Text))
                {
                    byte[] bytes = File.ReadAllBytes(textBoxSource.Text);
                    if (bytes != null)
                    {
                        byte[] newBytes = Convert(ConfigurationManager.AppSettings["OdsUrl"], bytes);
                        if (newBytes != null)
                        {
                            File.WriteAllBytes(textBoxTarget.Text, newBytes);
                            ShowMessage("作業完成！");
                        }
                        else
                        {
                            ShowMessage("檔案轉換失敗！");
                        }
                    }
                    else
                    {
                        ShowMessage("無法讀取來源檔案！");
                    }
                }
                else
                {
                    ShowMessage("請輸入來源檔案或目的檔案才能轉檔！");
                }
                
            }
            catch (Exception ex)
            {
                ShowMessage(ex.Message);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void buttonODT_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                if (!string.IsNullOrEmpty(textBoxSource.Text) && !string.IsNullOrEmpty(textBoxTarget.Text))
                {
                    byte[] bytes = File.ReadAllBytes(textBoxSource.Text);
                    if (bytes != null)
                    {
                        byte[] newBytes = Convert(ConfigurationManager.AppSettings["OdtUrl"], bytes);
                        if (newBytes != null)
                        {
                            File.WriteAllBytes(textBoxTarget.Text, newBytes);
                            ShowMessage("作業完成！");
                        }
                        else
                        {
                            ShowMessage("檔案轉換失敗！");
                        }
                    }
                    else
                    {
                        ShowMessage("無法讀取來源檔案！");
                    }
                }
                else
                {
                    ShowMessage("請輸入來源檔案或目的檔案才能轉檔！");
                }
                
            }
            catch (Exception ex)
            {
                ShowMessage(ex.Message);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private byte[] Convert(string url, byte[] content)
        {
            byte[] responseBytes = null;

            HttpWebRequest webRequest = WebRequest.Create(url) as HttpWebRequest;
            webRequest.Method = "post";
            webRequest.ContentType = "application/octet-stream";
            webRequest.GetRequestStream().Write(content, 0, content.Length);

            HttpWebResponse webResponse = webRequest.GetResponse() as HttpWebResponse;
            int statusCode = (int)webResponse.StatusCode;
            if ((statusCode >= 200) && (statusCode < 300))
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    webResponse.GetResponseStream().CopyTo(memoryStream);
                    responseBytes = memoryStream.ToArray();
                }
            }

            return responseBytes;
        }

        private void ShowMessage(string message)
        {
            MessageBox.Show(message, Text, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }
    }
}
