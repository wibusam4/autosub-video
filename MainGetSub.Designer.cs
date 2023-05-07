
namespace AutoSub
{
    partial class MainGetSub
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
            this.btnDemoSub = new ns1.SiticoneGradientButton();
            this.btnGetRectangle = new ns1.SiticoneGradientButton();
            this.btnGetSub = new ns1.SiticoneGradientButton();
            this.ProgessGetSub = new ns1.SiticoneProgressBar();
            this.SuspendLayout();
            // 
            // btnDemoSub
            // 
            this.btnDemoSub.BorderRadius = 5;
            this.btnDemoSub.CheckedState.Parent = this.btnDemoSub;
            this.btnDemoSub.CustomImages.Parent = this.btnDemoSub;
            this.btnDemoSub.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.btnDemoSub.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDemoSub.ForeColor = System.Drawing.Color.White;
            this.btnDemoSub.HoveredState.Parent = this.btnDemoSub;
            this.btnDemoSub.Location = new System.Drawing.Point(126, 40);
            this.btnDemoSub.Name = "btnDemoSub";
            this.btnDemoSub.ShadowDecoration.Parent = this.btnDemoSub;
            this.btnDemoSub.Size = new System.Drawing.Size(100, 25);
            this.btnDemoSub.TabIndex = 20;
            this.btnDemoSub.Text = "Test sub";
            // 
            // btnGetRectangle
            // 
            this.btnGetRectangle.BorderRadius = 5;
            this.btnGetRectangle.CheckedState.Parent = this.btnGetRectangle;
            this.btnGetRectangle.CustomImages.Parent = this.btnGetRectangle;
            this.btnGetRectangle.FillColor = System.Drawing.Color.Red;
            this.btnGetRectangle.FillColor2 = System.Drawing.Color.Red;
            this.btnGetRectangle.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGetRectangle.ForeColor = System.Drawing.Color.White;
            this.btnGetRectangle.HoveredState.Parent = this.btnGetRectangle;
            this.btnGetRectangle.Location = new System.Drawing.Point(20, 40);
            this.btnGetRectangle.Name = "btnGetRectangle";
            this.btnGetRectangle.ShadowDecoration.Parent = this.btnGetRectangle;
            this.btnGetRectangle.Size = new System.Drawing.Size(100, 25);
            this.btnGetRectangle.TabIndex = 19;
            this.btnGetRectangle.Text = "Lấy tọa độ";
            // 
            // btnGetSub
            // 
            this.btnGetSub.BorderRadius = 5;
            this.btnGetSub.CheckedState.Parent = this.btnGetSub;
            this.btnGetSub.CustomImages.Parent = this.btnGetSub;
            this.btnGetSub.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnGetSub.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnGetSub.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGetSub.ForeColor = System.Drawing.Color.Black;
            this.btnGetSub.HoveredState.Parent = this.btnGetSub;
            this.btnGetSub.Location = new System.Drawing.Point(232, 40);
            this.btnGetSub.Name = "btnGetSub";
            this.btnGetSub.ShadowDecoration.Parent = this.btnGetSub;
            this.btnGetSub.Size = new System.Drawing.Size(100, 25);
            this.btnGetSub.TabIndex = 18;
            this.btnGetSub.Text = "Bắt đầu";
            // 
            // ProgessGetSub
            // 
            this.ProgessGetSub.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.ProgessGetSub.Location = new System.Drawing.Point(20, 99);
            this.ProgessGetSub.Name = "ProgessGetSub";
            this.ProgessGetSub.ShadowDecoration.Parent = this.ProgessGetSub;
            this.ProgessGetSub.Size = new System.Drawing.Size(807, 25);
            this.ProgessGetSub.TabIndex = 17;
            this.ProgessGetSub.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            // 
            // MainGetSub
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnDemoSub);
            this.Controls.Add(this.btnGetRectangle);
            this.Controls.Add(this.btnGetSub);
            this.Controls.Add(this.ProgessGetSub);
            this.Name = "MainGetSub";
            this.Text = "MainGetSub";
            this.ResumeLayout(false);

        }

        #endregion

        private ns1.SiticoneGradientButton btnDemoSub;
        private ns1.SiticoneGradientButton btnGetRectangle;
        private ns1.SiticoneGradientButton btnGetSub;
        private ns1.SiticoneProgressBar ProgessGetSub;
    }
}