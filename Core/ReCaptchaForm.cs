// Decompiled with JetBrains decompiler
// Type: TwitchIRC.Core.ReCaptchaForm
// Assembly: TwitchIRC, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BAB293C8-7ABE-468B-BAB2-AF7B9681198C
// Assembly location: C:\Users\Alex\Desktop\TwitchIRC.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace TwitchIRC.Core
{
  public class ReCaptchaForm : Form
  {
    private IContainer components;
    private PictureBox pictureBox1;
    private Button button1;
    private TextBox textBox1;

    public Image CapchImg { get; set; }

    public string CapchaText
    {
      get
      {
        return this.textBox1.Text;
      }
      set
      {
        this.textBox1.Text = value;
      }
    }

    public ReCaptchaForm()
    {
      this.InitializeComponent();
    }

    private void ReCaptchaForm_Load(object sender, EventArgs e)
    {
      this.pictureBox1.Image = this.CapchImg;
    }

    private void button1_Click(object sender, EventArgs e)
    {
      this.DialogResult = DialogResult.OK;
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.pictureBox1 = new PictureBox();
      this.button1 = new Button();
      this.textBox1 = new TextBox();
      ((ISupportInitialize) this.pictureBox1).BeginInit();
      this.SuspendLayout();
      this.pictureBox1.Location = new Point(12, 12);
      this.pictureBox1.Name = "pictureBox1";
      this.pictureBox1.Size = new Size(300, 57);
      this.pictureBox1.TabIndex = 0;
      this.pictureBox1.TabStop = false;
      this.button1.Location = new Point(124, 101);
      this.button1.Name = "button1";
      this.button1.Size = new Size(75, 23);
      this.button1.TabIndex = 1;
      this.button1.Text = "Go";
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += new EventHandler(this.button1_Click);
      this.textBox1.Location = new Point(12, 75);
      this.textBox1.Name = "textBox1";
      this.textBox1.Size = new Size(300, 20);
      this.textBox1.TabIndex = 2;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(329, 138);
      this.Controls.Add((Control) this.textBox1);
      this.Controls.Add((Control) this.button1);
      this.Controls.Add((Control) this.pictureBox1);
      this.Name = "ReCaptchaForm";
      this.Text = "ReCaptchaForm";
      this.Load += new EventHandler(this.ReCaptchaForm_Load);
      ((ISupportInitialize) this.pictureBox1).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
