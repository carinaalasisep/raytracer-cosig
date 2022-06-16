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
        private RaysService raysService = new RaysService();

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

            this.labelSceneLoad.Visible = true;
            this.startBtn.Enabled = true;
        }

        private void startBtn_Click(object sender, EventArgs e)
        {
            this.PrimaryCalculations();

            this.saveBtn.Enabled = true;
        }

        private void PrimaryCalculations()
        {
            var cameraScene = this.context.CameraScene;
            var imageScene = this.context.ImageScene;

            var fieldOfVision = cameraScene.VisionField * Math.PI / 180;
            var projectionHeight = 2.0 * cameraScene.Distance * Math.Tan(fieldOfVision / 2.0);
            var projectionWidth = projectionHeight * imageScene.Horizontal / imageScene.Vertical;
            var pixelDimension = projectionHeight / imageScene.Vertical; // size of the pixel (the sides are equal since they are a square)

            this.raysService.CalculatePrimaryRays(cameraScene, imageScene, projectionHeight, projectionWidth, pixelDimension, this.pictureBox1, this.context);
        }

        private void recursion_ValueChanged(object sender, EventArgs e)
        {
            var num = sender as NumericUpDown;
            
            this.context.RecursiveLevel = (int)num.Value;
        }
    }
}
