namespace RayTracer
{
    using System;
    using System.Diagnostics;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Threading;
    using System.Windows.Forms;
    using RayTracer.Service;
    using RayTracer.Strategies;

    public partial class Form1 : Form
    {
        private Parser parser = new Parser();
        public ObjectContext context = new ObjectContext();
        private RayTracer rayTracer = new RayTracer();

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
            try
            {
                Thread threadInput = new Thread(this.DisplayDataAndCalculate);
                threadInput.Start();
            }
            catch (Exception ex)
            {
            }
        }

        private void DisplayDataAndCalculate()
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            this.SetLoading(true);
            this.context.IsEnvironmentEnabled = this.environmentReflection.Checked;
            this.context.IsReflectionEnabled = this.specularReflection.Checked;
            this.context.IsDiffuseReflectionEnabled = this.difuseReflection.Checked;
            this.context.IsRefractionEnabled = this.refraction.Checked;

            this.PrimaryCalculations();

            var t = TimeSpan.FromMilliseconds(stopWatch.ElapsedMilliseconds);
            var time = $"Successfully Rendered in \n" + 
                (t.Minutes == 0 ? 
                $"{t.Seconds}.{t.Milliseconds} " + "seconds." : 
                $"{t.Minutes}:{t.Seconds}.{t.Milliseconds} minutes.");
            this.SetLoading(false, time);
        }

        private void SetLoading(bool displayLoader, string time = null)
        {
            if (displayLoader)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    this.pictureBox2.Image = Properties.Resources.Loading_icon;
                    this.pictureBox2.Visible = true;
                    this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
                });
            }
            else
            {
                this.Invoke((MethodInvoker)delegate
                {
                    this.pictureBox2.Image = Properties.Resources.CheckImage;
                    this.Cursor = System.Windows.Forms.Cursors.Default;
                    this.labelTime.Text = time;
                    this.saveBtn.Enabled = true;
                });
            }
        }

        private void PrimaryCalculations()
        {
            var cameraScene = this.context.CameraScene;
            var imageScene = this.context.ImageScene;

            var fieldOfVision = cameraScene.VisionField * Math.PI / 180;
            var projectionHeight = 2.0 * cameraScene.Distance * Math.Tan(fieldOfVision / 2.0);
            var projectionWidth = projectionHeight * imageScene.Horizontal / imageScene.Vertical;
            var pixelDimension = projectionHeight / imageScene.Vertical; // size of the pixel (the sides are equal since they are a square)

            this.rayTracer.CalculatePrimaryRays(cameraScene, imageScene, projectionHeight, projectionWidth, pixelDimension, this.pictureBox1, this.context);
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
