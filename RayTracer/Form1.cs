namespace RayTracer
{
    using System;
    using System.IO;
    using System.Windows.Forms;
    using RayTracer.Service;
    using RayTracer.Strategies;

    public partial class Form1 : Form
    {
        private Parser parser = new Parser();
        public ObjectContext context = new ObjectContext();

        public Form1()
        {
            this.InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void loadBtnClick(object sender, EventArgs e)
        {
            string fileContent;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Read the contents of the file into a stream
                    var fileStream = openFileDialog.OpenFile();

                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        fileContent = reader.ReadToEnd();
                        this.parser.Parse(fileContent, this.context);
                    }
                }
            }
        }
    }
}
