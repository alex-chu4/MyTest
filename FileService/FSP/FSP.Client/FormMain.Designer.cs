namespace FSP.Client
{
    partial class FormMain
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.labelSource = new System.Windows.Forms.Label();
            this.textBoxSource = new System.Windows.Forms.TextBox();
            this.textBoxTarget = new System.Windows.Forms.TextBox();
            this.labelTarget = new System.Windows.Forms.Label();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.buttonOpen = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonODS = new System.Windows.Forms.Button();
            this.buttonODT = new System.Windows.Forms.Button();
            this.buttonExit = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // labelSource
            // 
            this.labelSource.AutoSize = true;
            this.labelSource.Font = new System.Drawing.Font("微軟正黑體", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.labelSource.Location = new System.Drawing.Point(30, 30);
            this.labelSource.Name = "labelSource";
            this.labelSource.Size = new System.Drawing.Size(133, 30);
            this.labelSource.TabIndex = 0;
            this.labelSource.Text = "來源檔案：";
            // 
            // textBoxSource
            // 
            this.textBoxSource.Font = new System.Drawing.Font("微軟正黑體", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textBoxSource.Location = new System.Drawing.Point(169, 27);
            this.textBoxSource.Name = "textBoxSource";
            this.textBoxSource.ReadOnly = true;
            this.textBoxSource.Size = new System.Drawing.Size(569, 38);
            this.textBoxSource.TabIndex = 1;
            // 
            // textBoxTarget
            // 
            this.textBoxTarget.Font = new System.Drawing.Font("微軟正黑體", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textBoxTarget.Location = new System.Drawing.Point(169, 83);
            this.textBoxTarget.Name = "textBoxTarget";
            this.textBoxTarget.ReadOnly = true;
            this.textBoxTarget.Size = new System.Drawing.Size(569, 38);
            this.textBoxTarget.TabIndex = 3;
            // 
            // labelTarget
            // 
            this.labelTarget.AutoSize = true;
            this.labelTarget.Font = new System.Drawing.Font("微軟正黑體", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.labelTarget.Location = new System.Drawing.Point(30, 86);
            this.labelTarget.Name = "labelTarget";
            this.labelTarget.Size = new System.Drawing.Size(133, 30);
            this.labelTarget.TabIndex = 2;
            this.labelTarget.Text = "目的檔案：";
            // 
            // buttonOpen
            // 
            this.buttonOpen.Font = new System.Drawing.Font("微軟正黑體", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.buttonOpen.Location = new System.Drawing.Point(744, 27);
            this.buttonOpen.Name = "buttonOpen";
            this.buttonOpen.Size = new System.Drawing.Size(38, 38);
            this.buttonOpen.TabIndex = 4;
            this.buttonOpen.Text = "…";
            this.buttonOpen.UseVisualStyleBackColor = true;
            this.buttonOpen.Click += new System.EventHandler(this.buttonOpen_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Font = new System.Drawing.Font("微軟正黑體", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.buttonSave.Location = new System.Drawing.Point(744, 81);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(38, 38);
            this.buttonSave.TabIndex = 5;
            this.buttonSave.Text = "…";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonODS
            // 
            this.buttonODS.Font = new System.Drawing.Font("微軟正黑體", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.buttonODS.Location = new System.Drawing.Point(169, 148);
            this.buttonODS.Name = "buttonODS";
            this.buttonODS.Size = new System.Drawing.Size(91, 38);
            this.buttonODS.TabIndex = 6;
            this.buttonODS.Text = "OD&S";
            this.buttonODS.UseVisualStyleBackColor = true;
            this.buttonODS.Click += new System.EventHandler(this.buttonODS_Click);
            // 
            // buttonODT
            // 
            this.buttonODT.Font = new System.Drawing.Font("微軟正黑體", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.buttonODT.Location = new System.Drawing.Point(266, 148);
            this.buttonODT.Name = "buttonODT";
            this.buttonODT.Size = new System.Drawing.Size(91, 38);
            this.buttonODT.TabIndex = 7;
            this.buttonODT.Text = "OD&T";
            this.buttonODT.UseVisualStyleBackColor = true;
            this.buttonODT.Click += new System.EventHandler(this.buttonODT_Click);
            // 
            // buttonExit
            // 
            this.buttonExit.Font = new System.Drawing.Font("微軟正黑體", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.buttonExit.Location = new System.Drawing.Point(625, 148);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(113, 38);
            this.buttonExit.TabIndex = 8;
            this.buttonExit.Text = "離開(&X)";
            this.buttonExit.UseVisualStyleBackColor = true;
            this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 234);
            this.Controls.Add(this.buttonExit);
            this.Controls.Add(this.buttonODT);
            this.Controls.Add(this.buttonODS);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonOpen);
            this.Controls.Add(this.textBoxTarget);
            this.Controls.Add(this.labelTarget);
            this.Controls.Add(this.textBoxSource);
            this.Controls.Add(this.labelSource);
            this.Name = "FormMain";
            this.Text = "轉檔";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelSource;
        private System.Windows.Forms.TextBox textBoxSource;
        private System.Windows.Forms.TextBox textBoxTarget;
        private System.Windows.Forms.Label labelTarget;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Button buttonOpen;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonODS;
        private System.Windows.Forms.Button buttonODT;
        private System.Windows.Forms.Button buttonExit;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}

