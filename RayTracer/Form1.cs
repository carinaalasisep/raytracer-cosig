namespace RayTracer
{
    using System;
    using System.Diagnostics;
    using System.Drawing.Imaging;
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
                openFileDialog.Filter = "Text Files|*.txt";
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

            this.loadedLabel.Visible = true;
            this.startBtn.Enabled = true;
            this.height.Value = this.context.ImageScene.Vertical;
            this.width.Value = this.context.ImageScene.Horizontal;
        }

        private void startBtn_Click(object sender, EventArgs e)
        {
            this.renderingLabel.Text = "Rendering...";

            this.context.IsEnvironmentEnabled = this.environmentReflection.Checked;
            this.context.IsReflectionEnabled = this.specularReflection.Checked;
            this.context.IsDiffuseReflectionEnabled = this.difuseReflection.Checked;
            this.context.IsRefractionEnabled = this.refraction.Checked;

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            this.progressBar1.Maximum = this.context.ImageScene.Horizontal* this.context.ImageScene.Vertical;
            this.progressBar1.Step = 1;

            this.PrimaryCalculations();

            var t = TimeSpan.FromMilliseconds(stopwatch.ElapsedMilliseconds);

            this.labelTime.Text = "Successfully Rendered in " + string.Format("{0:D2}:{1:D2}.{2:D3}",
            t.Minutes,
            t.Seconds,
            t.Milliseconds);

            this.renderingLabel.Text = "Scene Rendered!";
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

            if(this.context.RecursiveLevel > 0)
            {
                this.refraction.Enabled = true;
                this.specularReflection.Enabled = true;
            }
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            this.pictureBox1.Image.Save(AppDomain.CurrentDomain.BaseDirectory + "image" + DateTime.Now.ToFileTime() + ".jpeg", ImageFormat.Jpeg);

            MessageBox.Show("Image saved with success");
        }

        private void refraction_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void specularReflection_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void difuseReflection_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void environmentReflection_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void height_ValueChanged(object sender, EventArgs e)
        {
            var num = sender as NumericUpDown;

            if(this.context.ImageScene != null)
            {
                this.context.ImageScene.Vertical = (int)num.Value;
            }
        }

        private void width_ValueChanged(object sender, EventArgs e)
        {
            var num = sender as NumericUpDown;

            if (this.context.ImageScene != null)
            {
                this.context.ImageScene.Horizontal = (int)num.Value;
            }
        }
    }
}
