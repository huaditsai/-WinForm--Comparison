namespace Comparison
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
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
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器
        /// 修改這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnCompare = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtInput1 = new System.Windows.Forms.TextBox();
            this.txtResult = new System.Windows.Forms.TextBox();
            this.txtInput0 = new System.Windows.Forms.TextBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.label1 = new System.Windows.Forms.Label();
            this.txtInput2 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnCompare
            // 
            this.btnCompare.Location = new System.Drawing.Point(479, 22);
            this.btnCompare.Name = "btnCompare";
            this.btnCompare.Size = new System.Drawing.Size(75, 23);
            this.btnCompare.TabIndex = 0;
            this.btnCompare.Text = "Compare";
            this.btnCompare.UseVisualStyleBackColor = true;
            this.btnCompare.Click += new System.EventHandler(this.btnCompare_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(398, 22);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 1;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtInput1
            // 
            this.txtInput1.Font = new System.Drawing.Font("新細明體", 12F);
            this.txtInput1.ImeMode = System.Windows.Forms.ImeMode.Close;
            this.txtInput1.Location = new System.Drawing.Point(95, 22);
            this.txtInput1.Name = "txtInput1";
            this.txtInput1.Size = new System.Drawing.Size(191, 27);
            this.txtInput1.TabIndex = 2;
            // 
            // txtResult
            // 
            this.txtResult.Font = new System.Drawing.Font("新細明體", 12F);
            this.txtResult.Location = new System.Drawing.Point(13, 51);
            this.txtResult.Multiline = true;
            this.txtResult.Name = "txtResult";
            this.txtResult.ReadOnly = true;
            this.txtResult.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtResult.Size = new System.Drawing.Size(541, 370);
            this.txtResult.TabIndex = 3;
            // 
            // txtInput0
            // 
            this.txtInput0.Font = new System.Drawing.Font("新細明體", 12F);
            this.txtInput0.ImeMode = System.Windows.Forms.ImeMode.Close;
            this.txtInput0.Location = new System.Drawing.Point(13, 22);
            this.txtInput0.Name = "txtInput0";
            this.txtInput0.Size = new System.Drawing.Size(76, 27);
            this.txtInput0.TabIndex = 5;
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 408);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "label1";
            // 
            // txtInput2
            // 
            this.txtInput2.Font = new System.Drawing.Font("新細明體", 12F);
            this.txtInput2.ImeMode = System.Windows.Forms.ImeMode.Close;
            this.txtInput2.Location = new System.Drawing.Point(292, 21);
            this.txtInput2.Name = "txtInput2";
            this.txtInput2.Size = new System.Drawing.Size(100, 27);
            this.txtInput2.TabIndex = 4;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(563, 433);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtInput0);
            this.Controls.Add(this.txtInput2);
            this.Controls.Add(this.txtResult);
            this.Controls.Add(this.txtInput1);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.btnCompare);
            this.Name = "Form1";
            this.Text = "TWM POC";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCompare;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtInput1;
        private System.Windows.Forms.TextBox txtResult;
        private System.Windows.Forms.TextBox txtInput0;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtInput2;
    }
}

