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
            this.panel1 = new System.Windows.Forms.Panel();
            this.recursion = new System.Windows.Forms.NumericUpDown();
            this.startBtn = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.environmentReflection = new System.Windows.Forms.CheckBox();
            this.difuseReflection = new System.Windows.Forms.CheckBox();
            this.specularReflection = new System.Windows.Forms.CheckBox();
            this.refraction = new System.Windows.Forms.CheckBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.recursion)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // loadBtn
            // 
            this.loadBtn.Location = new System.Drawing.Point(21, 15);
            this.loadBtn.Name = "loadBtn";
            this.loadBtn.Size = new System.Drawing.Size(127, 55);
            this.loadBtn.TabIndex = 0;
            this.loadBtn.Text = "LOAD";
            this.loadBtn.UseVisualStyleBackColor = true;
            this.loadBtn.Click += new System.EventHandler(this.loadBtnClick);
            // 
            // saveBtn
            // 
            this.saveBtn.Location = new System.Drawing.Point(21, 76);
            this.saveBtn.Name = "saveBtn";
            this.saveBtn.Size = new System.Drawing.Size(127, 53);
            this.saveBtn.TabIndex = 1;
            this.saveBtn.Text = "SAVE";
            this.saveBtn.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.saveBtn);
            this.panel1.Controls.Add(this.loadBtn);
            this.panel1.Location = new System.Drawing.Point(30, 382);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(180, 145);
            this.panel1.TabIndex = 2;
            // 
            // recursion
            // 
            this.recursion.Location = new System.Drawing.Point(145, 39);
            this.recursion.Name = "recursion";
            this.recursion.Size = new System.Drawing.Size(49, 27);
            this.recursion.TabIndex = 3;
            // 
            // startBtn
            // 
            this.startBtn.Location = new System.Drawing.Point(200, 29);
            this.startBtn.Name = "startBtn";
            this.startBtn.Size = new System.Drawing.Size(109, 44);
            this.startBtn.TabIndex = 2;
            this.startBtn.Text = "START";
            this.startBtn.UseVisualStyleBackColor = true;
            this.startBtn.Click += new System.EventHandler(this.startBtn_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.startBtn);
            this.panel2.Controls.Add(this.recursion);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Location = new System.Drawing.Point(248, 449);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(327, 78);
            this.panel2.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 20);
            this.label1.TabIndex = 5;
            this.label1.Text = "Recursion depth";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 20);
            this.label3.TabIndex = 6;
            this.label3.Text = "Renderer";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(33, 374);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 20);
            this.label2.TabIndex = 5;
            this.label2.Text = "Scene";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(248, 539);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(125, 29);
            this.progressBar1.TabIndex = 7;
            // 
            // environmentReflection
            // 
            this.environmentReflection.AutoSize = true;
            this.environmentReflection.Location = new System.Drawing.Point(709, 423);
            this.environmentReflection.Name = "environmentReflection";
            this.environmentReflection.Size = new System.Drawing.Size(185, 24);
            this.environmentReflection.TabIndex = 2;
            this.environmentReflection.Text = "Environment Reflection";
            this.environmentReflection.UseVisualStyleBackColor = true;
            // 
            // difuseReflection
            // 
            this.difuseReflection.AutoSize = true;
            this.difuseReflection.Location = new System.Drawing.Point(709, 462);
            this.difuseReflection.Name = "difuseReflection";
            this.difuseReflection.Size = new System.Drawing.Size(144, 24);
            this.difuseReflection.TabIndex = 8;
            this.difuseReflection.Text = "Difuse Reflection";
            this.difuseReflection.UseVisualStyleBackColor = true;
            // 
            // specularReflection
            // 
            this.specularReflection.AutoSize = true;
            this.specularReflection.Location = new System.Drawing.Point(709, 503);
            this.specularReflection.Name = "specularReflection";
            this.specularReflection.Size = new System.Drawing.Size(159, 24);
            this.specularReflection.TabIndex = 9;
            this.specularReflection.Text = "Specular Reflection";
            this.specularReflection.UseVisualStyleBackColor = true;
            // 
            // refraction
            // 
            this.refraction.AutoSize = true;
            this.refraction.Location = new System.Drawing.Point(709, 539);
            this.refraction.Name = "refraction";
            this.refraction.Size = new System.Drawing.Size(99, 24);
            this.refraction.TabIndex = 10;
            this.refraction.Text = "Refraction";
            this.refraction.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            this.panel3.Location = new System.Drawing.Point(687, 397);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(250, 178);
            this.panel3.TabIndex = 11;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(85, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(852, 364);
            this.pictureBox1.TabIndex = 12;
            this.pictureBox1.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1019, 719);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.refraction);
            this.Controls.Add(this.specularReflection);
            this.Controls.Add(this.difuseReflection);
            this.Controls.Add(this.environmentReflection);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel3);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.recursion)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button loadBtn;
        private System.Windows.Forms.Button saveBtn;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.NumericUpDown recursion;
        private System.Windows.Forms.Button startBtn;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.CheckBox environmentReflection;
        private System.Windows.Forms.CheckBox difuseReflection;
        private System.Windows.Forms.CheckBox specularReflection;
        private System.Windows.Forms.CheckBox refraction;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

