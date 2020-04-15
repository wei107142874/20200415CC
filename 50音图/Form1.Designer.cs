namespace _50音图
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
            this.btn_kz = new System.Windows.Forms.Button();
            this.lab_show = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tb_sort = new System.Windows.Forms.TextBox();
            this.lab_sort = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tb_yin = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.tb_random = new System.Windows.Forms.TextBox();
            this.lab_ci = new System.Windows.Forms.Label();
            this.lab_cut = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.tb_zd = new System.Windows.Forms.TextBox();
            this.tb_lbjg = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.button6 = new System.Windows.Forms.Button();
            this.tb_check = new System.Windows.Forms.TextBox();
            this.button7 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_kz
            // 
            this.btn_kz.Location = new System.Drawing.Point(72, 686);
            this.btn_kz.Name = "btn_kz";
            this.btn_kz.Size = new System.Drawing.Size(82, 55);
            this.btn_kz.TabIndex = 0;
            this.btn_kz.Text = "启动";
            this.btn_kz.UseVisualStyleBackColor = true;
            this.btn_kz.Click += new System.EventHandler(this.btn_kz_Click);
            // 
            // lab_show
            // 
            this.lab_show.AutoSize = true;
            this.lab_show.Font = new System.Drawing.Font("黑体", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_show.Location = new System.Drawing.Point(67, 23);
            this.lab_show.Name = "lab_show";
            this.lab_show.Size = new System.Drawing.Size(133, 29);
            this.lab_show.TabIndex = 1;
            this.lab_show.Text = "等待显示";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(844, 686);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(113, 54);
            this.button1.TabIndex = 2;
            this.button1.Text = "图文释义";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(70, 64);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(600, 600);
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // tb_sort
            // 
            this.tb_sort.Location = new System.Drawing.Point(719, 153);
            this.tb_sort.Name = "tb_sort";
            this.tb_sort.Size = new System.Drawing.Size(100, 21);
            this.tb_sort.TabIndex = 4;
            this.tb_sort.TextChanged += new System.EventHandler(this.tb_sort_TextChanged);
            // 
            // lab_sort
            // 
            this.lab_sort.AutoSize = true;
            this.lab_sort.Location = new System.Drawing.Point(676, 119);
            this.lab_sort.Name = "lab_sort";
            this.lab_sort.Size = new System.Drawing.Size(11, 12);
            this.lab_sort.TabIndex = 5;
            this.lab_sort.Text = "0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(678, 156);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "下标:";
            // 
            // tb_yin
            // 
            this.tb_yin.Location = new System.Drawing.Point(719, 182);
            this.tb_yin.Name = "tb_yin";
            this.tb_yin.Size = new System.Drawing.Size(100, 21);
            this.tb_yin.TabIndex = 4;
            this.tb_yin.TextChanged += new System.EventHandler(this.tb_yin_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(678, 185);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(23, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "音:";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(678, 229);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 7;
            this.button2.Text = "上一条";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(783, 229);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 7;
            this.button3.Text = "下一条";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // tb_random
            // 
            this.tb_random.Location = new System.Drawing.Point(781, 719);
            this.tb_random.Name = "tb_random";
            this.tb_random.Size = new System.Drawing.Size(57, 21);
            this.tb_random.TabIndex = 8;
            // 
            // lab_ci
            // 
            this.lab_ci.AutoSize = true;
            this.lab_ci.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_ci.Location = new System.Drawing.Point(678, 74);
            this.lab_ci.Name = "lab_ci";
            this.lab_ci.Size = new System.Drawing.Size(25, 16);
            this.lab_ci.TabIndex = 9;
            this.lab_ci.Text = "词";
            // 
            // lab_cut
            // 
            this.lab_cut.AutoSize = true;
            this.lab_cut.Location = new System.Drawing.Point(719, 119);
            this.lab_cut.Name = "lab_cut";
            this.lab_cut.Size = new System.Drawing.Size(41, 12);
            this.lab_cut.TabIndex = 10;
            this.lab_cut.Text = "label3";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(160, 686);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 54);
            this.button4.TabIndex = 0;
            this.button4.Text = "轮播5s";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(340, 687);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 54);
            this.button5.TabIndex = 11;
            this.button5.Text = "不认识";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click_1);
            // 
            // tb_zd
            // 
            this.tb_zd.Font = new System.Drawing.Font("黑体", 13F);
            this.tb_zd.Location = new System.Drawing.Point(678, 278);
            this.tb_zd.Multiline = true;
            this.tb_zd.Name = "tb_zd";
            this.tb_zd.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tb_zd.Size = new System.Drawing.Size(279, 386);
            this.tb_zd.TabIndex = 12;
            // 
            // tb_lbjg
            // 
            this.tb_lbjg.Location = new System.Drawing.Point(707, 719);
            this.tb_lbjg.Name = "tb_lbjg";
            this.tb_lbjg.Size = new System.Drawing.Size(53, 21);
            this.tb_lbjg.TabIndex = 8;
            this.tb_lbjg.Text = "20000";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(796, 686);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 13;
            this.label3.Text = "范围";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(705, 686);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 12);
            this.label4.TabIndex = 13;
            this.label4.Text = "轮播间隔(毫秒)";
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(250, 686);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(75, 54);
            this.button6.TabIndex = 11;
            this.button6.Text = "认识";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // tb_check
            // 
            this.tb_check.Location = new System.Drawing.Point(434, 687);
            this.tb_check.Name = "tb_check";
            this.tb_check.Size = new System.Drawing.Size(100, 21);
            this.tb_check.TabIndex = 14;
            this.tb_check.TextChanged += new System.EventHandler(this.tb_check_TextChanged);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(551, 685);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(100, 55);
            this.button7.TabIndex = 15;
            this.button7.Text = "随机生成";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(969, 762);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.tb_check);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tb_zd);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.lab_cut);
            this.Controls.Add(this.lab_ci);
            this.Controls.Add(this.tb_lbjg);
            this.Controls.Add(this.tb_random);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tb_yin);
            this.Controls.Add(this.lab_sort);
            this.Controls.Add(this.tb_sort);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lab_show);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.btn_kz);
            this.Location = new System.Drawing.Point(300, 111);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "50音图记忆工具";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_kz;
        private System.Windows.Forms.Label lab_show;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox tb_sort;
        private System.Windows.Forms.Label lab_sort;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tb_yin;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox tb_random;
        private System.Windows.Forms.Label lab_ci;
        private System.Windows.Forms.Label lab_cut;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.TextBox tb_zd;
        private System.Windows.Forms.TextBox tb_lbjg;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.TextBox tb_check;
        private System.Windows.Forms.Button button7;
    }
}

