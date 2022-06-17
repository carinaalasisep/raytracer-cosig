namespace RayTracer
{

    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.loadBtn = new System.Windows.Forms.Button();
            this.saveBtn = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.environmentReflection = new System.Windows.Forms.CheckBox();
            this.difuseReflection = new System.Windows.Forms.CheckBox();
            this.specularReflection = new System.Windows.Forms.CheckBox();
            this.refraction = new System.Windows.Forms.CheckBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.renderingLabel = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.startBtn = new System.Windows.Forms.Button();
            this.recursion = new System.Windows.Forms.NumericUpDown();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.width = new System.Windows.Forms.NumericUpDown();
            this.height = new System.Windows.Forms.NumericUpDown();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.loadedLabel = new System.Windows.Forms.Label();
            this.labelTime = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.recursion)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.width)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.height)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // loadBtn
            // 
            this.loadBtn.Location = new System.Drawing.Point(45, 52);
            this.loadBtn.Name = "loadBtn";
            this.loadBtn.Size = new System.Drawing.Size(127, 55);
            this.loadBtn.TabIndex = 0;
            this.loadBtn.Text = "LOAD";
            this.loadBtn.UseVisualStyleBackColor = true;
            this.loadBtn.Click += new System.EventHandler(this.loadBtnClick);
            // 
            // saveBtn
            // 
            this.saveBtn.Enabled = false;
            this.saveBtn.Location = new System.Drawing.Point(45, 170);
            this.saveBtn.Name = "saveBtn";
            this.saveBtn.Size = new System.Drawing.Size(127, 53);
            this.saveBtn.TabIndex = 1;
            this.saveBtn.Text = "SAVE SCENE";
            this.saveBtn.UseVisualStyleBackColor = true;
            this.saveBtn.Click += new System.EventHandler(this.saveBtn_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(439, 605);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(200, 41);
            this.progressBar1.TabIndex = 7;
            // 
            // environmentReflection
            // 
            this.environmentReflection.AutoSize = true;
            this.environmentReflection.Location = new System.Drawing.Point(32, 38);
            this.environmentReflection.Name = "environmentReflection";
            this.environmentReflection.Size = new System.Drawing.Size(185, 24);
            this.environmentReflection.TabIndex = 2;
            this.environmentReflection.Text = "Environment Reflection";
            this.environmentReflection.UseVisualStyleBackColor = true;
            this.environmentReflection.CheckedChanged += new System.EventHandler(this.environmentReflection_CheckedChanged);
            // 
            // difuseReflection
            // 
            this.difuseReflection.AutoSize = true;
            this.difuseReflection.Location = new System.Drawing.Point(32, 68);
            this.difuseReflection.Name = "difuseReflection";
            this.difuseReflection.Size = new System.Drawing.Size(144, 24);
            this.difuseReflection.TabIndex = 8;
            this.difuseReflection.Text = "Difuse Reflection";
            this.difuseReflection.UseVisualStyleBackColor = true;
            this.difuseReflection.CheckedChanged += new System.EventHandler(this.difuseReflection_CheckedChanged);
            // 
            // specularReflection
            // 
            this.specularReflection.AutoSize = true;
            this.specularReflection.Enabled = false;
            this.specularReflection.Location = new System.Drawing.Point(32, 98);
            this.specularReflection.Name = "specularReflection";
            this.specularReflection.Size = new System.Drawing.Size(159, 24);
            this.specularReflection.TabIndex = 9;
            this.specularReflection.Text = "Specular Reflection";
            this.specularReflection.UseVisualStyleBackColor = true;
            this.specularReflection.CheckedChanged += new System.EventHandler(this.specularReflection_CheckedChanged);
            // 
            // refraction
            // 
            this.refraction.AutoSize = true;
            this.refraction.Enabled = false;
            this.refraction.Location = new System.Drawing.Point(32, 128);
            this.refraction.Name = "refraction";
            this.refraction.Size = new System.Drawing.Size(99, 24);
            this.refraction.TabIndex = 10;
            this.refraction.Text = "Refraction";
            this.refraction.UseVisualStyleBackColor = true;
            this.refraction.CheckedChanged += new System.EventHandler(this.refraction_CheckedChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(205, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(573, 387);
            this.pictureBox1.TabIndex = 12;
            this.pictureBox1.TabStop = false;
            // 
            // renderingLabel
            // 
            this.renderingLabel.AutoSize = true;
            this.renderingLabel.Location = new System.Drawing.Point(307, 615);
            this.renderingLabel.Name = "renderingLabel";
            this.renderingLabel.Size = new System.Drawing.Size(0, 20);
            this.renderingLabel.TabIndex = 3;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.startBtn);
            this.groupBox1.Controls.Add(this.recursion);
            this.groupBox1.Location = new System.Drawing.Point(280, 423);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(373, 141);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Renderer";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 67);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 20);
            this.label1.TabIndex = 5;
            this.label1.Text = "Recursion depth";
            // 
            // startBtn
            // 
            this.startBtn.Enabled = false;
            this.startBtn.Location = new System.Drawing.Point(201, 38);
            this.startBtn.Name = "startBtn";
            this.startBtn.Size = new System.Drawing.Size(155, 81);
            this.startBtn.TabIndex = 2;
            this.startBtn.Text = "START";
            this.startBtn.UseVisualStyleBackColor = true;
            this.startBtn.Click += new System.EventHandler(this.startBtn_Click);
            // 
            // recursion
            // 
            this.recursion.Location = new System.Drawing.Point(137, 65);
            this.recursion.Name = "recursion";
            this.recursion.Size = new System.Drawing.Size(49, 27);
            this.recursion.TabIndex = 3;
            this.recursion.ValueChanged += new System.EventHandler(this.recursion_ValueChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.width);
            this.groupBox2.Controls.Add(this.height);
            this.groupBox2.Controls.Add(this.specularReflection);
            this.groupBox2.Controls.Add(this.environmentReflection);
            this.groupBox2.Controls.Add(this.difuseReflection);
            this.groupBox2.Controls.Add(this.refraction);
            this.groupBox2.Location = new System.Drawing.Point(687, 423);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(250, 250);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Options";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(42, 170);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 20);
            this.label2.TabIndex = 16;
            this.label2.Text = "Height";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(42, 205);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 20);
            this.label3.TabIndex = 18;
            this.label3.Text = "Width";
            // 
            // width
            // 
            this.width.Location = new System.Drawing.Point(111, 203);
            this.width.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.width.Name = "width";
            this.width.Size = new System.Drawing.Size(53, 27);
            this.width.TabIndex = 17;
            this.width.ValueChanged += new System.EventHandler(this.width_ValueChanged);
            // 
            // height
            // 
            this.height.Location = new System.Drawing.Point(111, 170);
            this.height.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.height.Name = "height";
            this.height.Size = new System.Drawing.Size(53, 27);
            this.height.TabIndex = 16;
            this.height.ValueChanged += new System.EventHandler(this.height_ValueChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.loadedLabel);
            this.groupBox3.Controls.Add(this.loadBtn);
            this.groupBox3.Controls.Add(this.saveBtn);
            this.groupBox3.Location = new System.Drawing.Point(22, 423);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(208, 250);
            this.groupBox3.TabIndex = 14;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Scene";
            // 
            // loadedLabel
            // 
            this.loadedLabel.AutoSize = true;
            this.loadedLabel.Location = new System.Drawing.Point(60, 110);
            this.loadedLabel.Name = "loadedLabel";
            this.loadedLabel.Size = new System.Drawing.Size(103, 20);
            this.loadedLabel.TabIndex = 2;
            this.loadedLabel.Text = "Scene loaded!";
            this.loadedLabel.Visible = false;
            // 
            // labelTime
            // 
            this.labelTime.AutoSize = true;
            this.labelTime.Location = new System.Drawing.Point(416, 666);
            this.labelTime.Name = "labelTime";
            this.labelTime.Size = new System.Drawing.Size(0, 20);
            this.labelTime.TabIndex = 15;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1019, 719);
            this.Controls.Add(this.labelTime);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.renderingLabel);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.progressBar1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.recursion)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.width)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.height)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button loadBtn;
        private System.Windows.Forms.Button saveBtn;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.CheckBox environmentReflection;
        private System.Windows.Forms.CheckBox difuseReflection;
        private System.Windows.Forms.CheckBox specularReflection;
        private System.Windows.Forms.CheckBox refraction;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label renderingLabel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button startBtn;
        private System.Windows.Forms.NumericUpDown recursion;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label loadedLabel;
        private System.Windows.Forms.Label labelTime;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown width;
        private System.Windows.Forms.NumericUpDown height;
        private System.Windows.Forms.Label label2;
    }
}

