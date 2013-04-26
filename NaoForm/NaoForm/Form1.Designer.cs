using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Aldebaran.Proxies;
using System.Threading;
using Aldebaran.LanguageCompatibility;
using System.Collections;
using System.Drawing.Imaging;

namespace NaoForm
{
    partial class Form1
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.pictureBox1.Location = new System.Drawing.Point(12, 16);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(781, 486);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(804, 15);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(249, 105);
            this.button1.TabIndex = 1;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1070, 514);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;

         public void doRobotStuff() {
        //Jorik's image example
            String ip = "192.168.0.116";

            TextToSpeechProxy tts = new TextToSpeechProxy(ip, 9559);
            tts.say("Fuck You");

            VideoDeviceProxy video = new VideoDeviceProxy(ip, 9559);
            String myName = video.subscribeCamera("MyVideoModule", 0, 1, 11, 5);

            MotionProxy motion = new MotionProxy(ip, 9559);
            motion.moveInit();
            motion.move(0.01F, 0, 0);

            for (int i = 0; i < 10;i++)
            {
                ArrayList imageData = (ArrayList)video.getImageRemote(myName);
                byte[] imageBytes = (byte[])(imageData)[6];
                Bitmap image = new Bitmap(320, 240, PixelFormat.Format32bppArgb);
                for (int x = 0; x < 320; x++)
                {
                    for (int y = 0; y < 240; y++)
                    {
                        image.SetPixel(x, y, Color.FromArgb(imageBytes[320 * 3 * y + x * 3 + 0], imageBytes[320 * 3 * y + x * 3 + 1], imageBytes[320 * 3 * y + x * 3 + 2]));

                    }
                }

                pictureBox1.Image = image;
                pictureBox1.Refresh();
            }
            motion.killWalk();
            video.unsubscribe(myName);
    }

         private Button button1;

         private void button1_Click(object sender, System.EventArgs e)
         {
             doRobotStuff();
         }
    }
}

