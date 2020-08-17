namespace TestWinForm
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.tb_data = new System.Windows.Forms.TextBox();
            this.btn_addqueue = new System.Windows.Forms.Button();
            this.btn_quitqueue = new System.Windows.Forms.Button();
            this.lab_quitData = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // tb_data
            // 
            this.tb_data.Location = new System.Drawing.Point(133, 46);
            this.tb_data.Multiline = true;
            this.tb_data.Name = "tb_data";
            this.tb_data.Size = new System.Drawing.Size(387, 41);
            this.tb_data.TabIndex = 0;
            // 
            // btn_addqueue
            // 
            this.btn_addqueue.Location = new System.Drawing.Point(150, 184);
            this.btn_addqueue.Name = "btn_addqueue";
            this.btn_addqueue.Size = new System.Drawing.Size(75, 23);
            this.btn_addqueue.TabIndex = 1;
            this.btn_addqueue.Text = "入队";
            this.btn_addqueue.UseVisualStyleBackColor = true;
            this.btn_addqueue.Click += new System.EventHandler(this.btn_addqueue_Click);
            // 
            // btn_quitqueue
            // 
            this.btn_quitqueue.Location = new System.Drawing.Point(431, 184);
            this.btn_quitqueue.Name = "btn_quitqueue";
            this.btn_quitqueue.Size = new System.Drawing.Size(75, 23);
            this.btn_quitqueue.TabIndex = 1;
            this.btn_quitqueue.Text = "出队";
            this.btn_quitqueue.UseVisualStyleBackColor = true;
            this.btn_quitqueue.Click += new System.EventHandler(this.btn_quitqueue_Click);
            // 
            // lab_quitData
            // 
            this.lab_quitData.AutoSize = true;
            this.lab_quitData.Location = new System.Drawing.Point(133, 113);
            this.lab_quitData.Name = "lab_quitData";
            this.lab_quitData.Size = new System.Drawing.Size(0, 15);
            this.lab_quitData.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(622, 252);
            this.Controls.Add(this.lab_quitData);
            this.Controls.Add(this.btn_quitqueue);
            this.Controls.Add(this.btn_addqueue);
            this.Controls.Add(this.tb_data);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tb_data;
        private System.Windows.Forms.Button btn_addqueue;
        private System.Windows.Forms.Button btn_quitqueue;
        private System.Windows.Forms.Label lab_quitData;
    }
}

