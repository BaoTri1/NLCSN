namespace MaDITuan
{
    partial class frm_Menu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_Menu));
            this.lblHeading1 = new System.Windows.Forms.Label();
            this.lblHeading2 = new System.Windows.Forms.Label();
            this.btnMoPhong = new System.Windows.Forms.Button();
            this.btnDoiKhang = new System.Windows.Forms.Button();
            this.btnThoat = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblHeading1
            // 
            this.lblHeading1.AutoSize = true;
            this.lblHeading1.Font = new System.Drawing.Font("Ravie", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeading1.ForeColor = System.Drawing.Color.DeepPink;
            this.lblHeading1.Location = new System.Drawing.Point(212, 24);
            this.lblHeading1.Name = "lblHeading1";
            this.lblHeading1.Size = new System.Drawing.Size(356, 40);
            this.lblHeading1.TabIndex = 1;
            this.lblHeading1.Text = "Ứng Dụng Trò Chơi";
            // 
            // lblHeading2
            // 
            this.lblHeading2.AutoSize = true;
            this.lblHeading2.Font = new System.Drawing.Font("Ravie", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeading2.ForeColor = System.Drawing.Color.DeepPink;
            this.lblHeading2.Location = new System.Drawing.Point(227, 76);
            this.lblHeading2.Name = "lblHeading2";
            this.lblHeading2.Size = new System.Drawing.Size(326, 54);
            this.lblHeading2.TabIndex = 2;
            this.lblHeading2.Text = "Mã Đi Tuần";
            // 
            // btnMoPhong
            // 
            this.btnMoPhong.BackColor = System.Drawing.Color.GhostWhite;
            this.btnMoPhong.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMoPhong.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMoPhong.ImageKey = "(none)";
            this.btnMoPhong.Location = new System.Drawing.Point(179, 248);
            this.btnMoPhong.Name = "btnMoPhong";
            this.btnMoPhong.Size = new System.Drawing.Size(450, 58);
            this.btnMoPhong.TabIndex = 3;
            this.btnMoPhong.Text = "Mô phỏng Mã Đi Tuần";
            this.btnMoPhong.UseVisualStyleBackColor = false;
            this.btnMoPhong.Click += new System.EventHandler(this.btnMoPhong_Click);
            // 
            // btnDoiKhang
            // 
            this.btnDoiKhang.BackColor = System.Drawing.SystemColors.HighlightText;
            this.btnDoiKhang.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDoiKhang.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDoiKhang.ImageKey = "(none)";
            this.btnDoiKhang.Location = new System.Drawing.Point(179, 418);
            this.btnDoiKhang.Name = "btnDoiKhang";
            this.btnDoiKhang.Size = new System.Drawing.Size(450, 58);
            this.btnDoiKhang.TabIndex = 4;
            this.btnDoiKhang.Text = "Trò chơi đối kháng";
            this.btnDoiKhang.UseVisualStyleBackColor = false;
            this.btnDoiKhang.Click += new System.EventHandler(this.btnDoiKhang_Click);
            // 
            // btnThoat
            // 
            this.btnThoat.BackColor = System.Drawing.SystemColors.HighlightText;
            this.btnThoat.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThoat.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnThoat.ImageKey = "(none)";
            this.btnThoat.Location = new System.Drawing.Point(164, 597);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(450, 58);
            this.btnThoat.TabIndex = 5;
            this.btnThoat.Text = "Thoát";
            this.btnThoat.UseVisualStyleBackColor = false;
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // frm_Menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightCyan;
            this.ClientSize = new System.Drawing.Size(802, 795);
            this.Controls.Add(this.btnThoat);
            this.Controls.Add(this.btnDoiKhang);
            this.Controls.Add(this.btnMoPhong);
            this.Controls.Add(this.lblHeading2);
            this.Controls.Add(this.lblHeading1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frm_Menu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ứng dụng trò chơi Mã Đi Tuần";
            this.Load += new System.EventHandler(this.frm_Menu_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblHeading1;
        private System.Windows.Forms.Label lblHeading2;
        private System.Windows.Forms.Button btnMoPhong;
        private System.Windows.Forms.Button btnDoiKhang;
        private System.Windows.Forms.Button btnThoat;
    }
}

