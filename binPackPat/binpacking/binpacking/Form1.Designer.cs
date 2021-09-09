namespace binpacking
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnOpenFile = new System.Windows.Forms.Button();
            this.pictureBoxDraw = new System.Windows.Forms.PictureBox();
            this.btnRun = new System.Windows.Forms.Button();
            this.NumberGenerationLabel = new System.Windows.Forms.Label();
            this.XMLopenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.panelPaint = new System.Windows.Forms.Panel();
            this.BestFitnesslabel = new System.Windows.Forms.Label();
            this.TimeElapsedlabel = new System.Windows.Forms.Label();
            this.lblBin = new System.Windows.Forms.Label();
            this.MainDataGridView = new System.Windows.Forms.DataGridView();
            this.btnBestSolution = new System.Windows.Forms.Button();
            this.btnDataSet = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.GenerationNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ChromosomeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FitnessColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDraw)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MainDataGridView)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOpenFile
            // 
            this.btnOpenFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOpenFile.Location = new System.Drawing.Point(12, 12);
            this.btnOpenFile.Name = "btnOpenFile";
            this.btnOpenFile.Size = new System.Drawing.Size(141, 67);
            this.btnOpenFile.TabIndex = 0;
            this.btnOpenFile.Text = "Select Data Set";
            this.btnOpenFile.UseVisualStyleBackColor = true;
            this.btnOpenFile.Click += new System.EventHandler(this.btnOpenFile_Click);
            // 
            // pictureBoxDraw
            // 
            this.pictureBoxDraw.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxDraw.Location = new System.Drawing.Point(13, 97);
            this.pictureBoxDraw.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBoxDraw.Name = "pictureBoxDraw";
            this.pictureBoxDraw.Size = new System.Drawing.Size(968, 695);
            this.pictureBoxDraw.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxDraw.TabIndex = 2;
            this.pictureBoxDraw.TabStop = false;
            // 
            // btnRun
            // 
            this.btnRun.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRun.Location = new System.Drawing.Point(159, 12);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(131, 67);
            this.btnRun.TabIndex = 2;
            this.btnRun.Text = "Run";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // NumberGenerationLabel
            // 
            this.NumberGenerationLabel.AutoSize = true;
            this.NumberGenerationLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NumberGenerationLabel.Location = new System.Drawing.Point(6, 66);
            this.NumberGenerationLabel.Name = "NumberGenerationLabel";
            this.NumberGenerationLabel.Size = new System.Drawing.Size(138, 24);
            this.NumberGenerationLabel.TabIndex = 3;
            this.NumberGenerationLabel.Text = "Generation No.";
            // 
            // XMLopenFileDialog
            // 
            this.XMLopenFileDialog.FileName = "XMLopenFileDialog";
            // 
            // panelPaint
            // 
            this.panelPaint.Location = new System.Drawing.Point(619, 114);
            this.panelPaint.Name = "panelPaint";
            this.panelPaint.Size = new System.Drawing.Size(318, 678);
            this.panelPaint.TabIndex = 5;
            // 
            // BestFitnesslabel
            // 
            this.BestFitnesslabel.AutoSize = true;
            this.BestFitnesslabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BestFitnesslabel.Location = new System.Drawing.Point(6, 90);
            this.BestFitnesslabel.Name = "BestFitnesslabel";
            this.BestFitnesslabel.Size = new System.Drawing.Size(0, 24);
            this.BestFitnesslabel.TabIndex = 6;
            // 
            // TimeElapsedlabel
            // 
            this.TimeElapsedlabel.AutoSize = true;
            this.TimeElapsedlabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TimeElapsedlabel.Location = new System.Drawing.Point(6, 18);
            this.TimeElapsedlabel.Name = "TimeElapsedlabel";
            this.TimeElapsedlabel.Size = new System.Drawing.Size(132, 24);
            this.TimeElapsedlabel.TabIndex = 7;
            this.TimeElapsedlabel.Text = "Time Elapsed:";
            // 
            // lblBin
            // 
            this.lblBin.AutoSize = true;
            this.lblBin.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBin.Location = new System.Drawing.Point(6, 42);
            this.lblBin.Name = "lblBin";
            this.lblBin.Size = new System.Drawing.Size(137, 24);
            this.lblBin.TabIndex = 9;
            this.lblBin.Text = "Container Size:";
            // 
            // MainDataGridView
            // 
            this.MainDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.MainDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.GenerationNumber,
            this.ChromosomeColumn,
            this.FitnessColumn});
            this.MainDataGridView.Location = new System.Drawing.Point(978, 378);
            this.MainDataGridView.Name = "MainDataGridView";
            this.MainDataGridView.RowTemplate.Height = 24;
            this.MainDataGridView.Size = new System.Drawing.Size(900, 423);
            this.MainDataGridView.TabIndex = 10;
            this.MainDataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.MainDataGridView_CellContentClick);
            // 
            // btnBestSolution
            // 
            this.btnBestSolution.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBestSolution.Location = new System.Drawing.Point(1489, 302);
            this.btnBestSolution.Name = "btnBestSolution";
            this.btnBestSolution.Size = new System.Drawing.Size(225, 70);
            this.btnBestSolution.TabIndex = 11;
            this.btnBestSolution.Text = "View All Solutions";
            this.btnBestSolution.UseVisualStyleBackColor = true;
            this.btnBestSolution.Click += new System.EventHandler(this.btnBestSolution_Click);
            // 
            // btnDataSet
            // 
            this.btnDataSet.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDataSet.Location = new System.Drawing.Point(296, 12);
            this.btnDataSet.Name = "btnDataSet";
            this.btnDataSet.Size = new System.Drawing.Size(131, 67);
            this.btnDataSet.TabIndex = 12;
            this.btnDataSet.Text = "Create Data Set";
            this.btnDataSet.UseVisualStyleBackColor = true;
            this.btnDataSet.Click += new System.EventHandler(this.btnDataSet_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Controls.Add(this.TimeElapsedlabel);
            this.groupBox1.Controls.Add(this.lblBin);
            this.groupBox1.Controls.Add(this.BestFitnesslabel);
            this.groupBox1.Controls.Add(this.NumberGenerationLabel);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(581, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(445, 169);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Statistics";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(433, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(131, 67);
            this.button1.TabIndex = 14;
            this.button1.Text = "Settings";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // GenerationNumber
            // 
            this.GenerationNumber.HeaderText = "Generation Number";
            this.GenerationNumber.Name = "GenerationNumber";
            this.GenerationNumber.ReadOnly = true;
            this.GenerationNumber.Width = 148;
            // 
            // ChromosomeColumn
            // 
            this.ChromosomeColumn.FillWeight = 217F;
            this.ChromosomeColumn.HeaderText = "Best Chromosome from the generation";
            this.ChromosomeColumn.MinimumWidth = 60;
            this.ChromosomeColumn.Name = "ChromosomeColumn";
            this.ChromosomeColumn.ReadOnly = true;
            this.ChromosomeColumn.Width = 372;
            // 
            // FitnessColumn
            // 
            this.FitnessColumn.HeaderText = "Out of container area";
            this.FitnessColumn.Name = "FitnessColumn";
            this.FitnessColumn.ReadOnly = true;
            this.FitnessColumn.Width = 110;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DimGray;
            this.ClientSize = new System.Drawing.Size(1905, 874);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnDataSet);
            this.Controls.Add(this.btnBestSolution);
            this.Controls.Add(this.MainDataGridView);
            this.Controls.Add(this.panelPaint);
            this.Controls.Add(this.btnRun);
            this.Controls.Add(this.pictureBoxDraw);
            this.Controls.Add(this.btnOpenFile);
            this.Name = "MainForm";
            this.Text = "MainForm";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDraw)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MainDataGridView)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOpenFile;
        private System.Windows.Forms.PictureBox pictureBoxDraw;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.Label NumberGenerationLabel;
        private System.Windows.Forms.OpenFileDialog XMLopenFileDialog;
        private System.Windows.Forms.Panel panelPaint;
        private System.Windows.Forms.Label BestFitnesslabel;
        private System.Windows.Forms.Label TimeElapsedlabel;
        private System.Windows.Forms.Label lblBin;
        private System.Windows.Forms.DataGridView MainDataGridView;
        private System.Windows.Forms.Button btnBestSolution;
        private System.Windows.Forms.Button btnDataSet;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridViewTextBoxColumn GenerationNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn ChromosomeColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn FitnessColumn;
    }
}

