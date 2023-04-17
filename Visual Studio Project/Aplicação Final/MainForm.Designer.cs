using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon;
using Amazon.Rekognition;
using Amazon.Rekognition.Model;

namespace TCCVerificacaoEPI
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
            this.cbCameraDevices = new System.Windows.Forms.ComboBox();
            this.bImageAction = new System.Windows.Forms.Button();
            this.bSend = new System.Windows.Forms.Button();
            this.bConnectDisconnect = new System.Windows.Forms.Button();
            this.lCameraSource = new System.Windows.Forms.Label();
            this.rtbResponseValidation = new System.Windows.Forms.RichTextBox();
            this.pbBoudingBox = new System.Windows.Forms.PictureBox();
            this.gbSourceImage = new System.Windows.Forms.GroupBox();
            this.pbImage = new System.Windows.Forms.PictureBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbBoudingBox)).BeginInit();
            this.gbSourceImage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbCameraDevices
            // 
            this.cbCameraDevices.FormattingEnabled = true;
            this.cbCameraDevices.Location = new System.Drawing.Point(61, 6);
            this.cbCameraDevices.Name = "cbCameraDevices";
            this.cbCameraDevices.Size = new System.Drawing.Size(121, 21);
            this.cbCameraDevices.TabIndex = 8;
            // 
            // bImageAction
            // 
            this.bImageAction.Location = new System.Drawing.Point(21, 739);
            this.bImageAction.Name = "bImageAction";
            this.bImageAction.Size = new System.Drawing.Size(75, 23);
            this.bImageAction.TabIndex = 9;
            this.bImageAction.Text = "Capturar";
            this.bImageAction.UseVisualStyleBackColor = true;
            this.bImageAction.Click += new System.EventHandler(this.bImageAction_Click);
            // 
            // bSend
            // 
            this.bSend.Location = new System.Drawing.Point(634, 739);
            this.bSend.Name = "bSend";
            this.bSend.Size = new System.Drawing.Size(75, 23);
            this.bSend.TabIndex = 10;
            this.bSend.Text = "Enviar";
            this.bSend.UseVisualStyleBackColor = true;
            this.bSend.Click += new System.EventHandler(this.bSend_Click);
            // 
            // bConnectDisconnect
            // 
            this.bConnectDisconnect.Location = new System.Drawing.Point(188, 4);
            this.bConnectDisconnect.Name = "bConnectDisconnect";
            this.bConnectDisconnect.Size = new System.Drawing.Size(75, 23);
            this.bConnectDisconnect.TabIndex = 11;
            this.bConnectDisconnect.Text = "Conectar";
            this.bConnectDisconnect.UseVisualStyleBackColor = true;
            this.bConnectDisconnect.Click += new System.EventHandler(this.bConnectDisconnect_Click);
            // 
            // lCameraSource
            // 
            this.lCameraSource.AutoSize = true;
            this.lCameraSource.Location = new System.Drawing.Point(12, 9);
            this.lCameraSource.Name = "lCameraSource";
            this.lCameraSource.Size = new System.Drawing.Size(43, 13);
            this.lCameraSource.TabIndex = 16;
            this.lCameraSource.Text = "Câmera";
            // 
            // rtbResponseValidation
            // 
            this.rtbResponseValidation.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbResponseValidation.Location = new System.Drawing.Point(6, 12);
            this.rtbResponseValidation.Name = "rtbResponseValidation";
            this.rtbResponseValidation.ReadOnly = true;
            this.rtbResponseValidation.Size = new System.Drawing.Size(371, 263);
            this.rtbResponseValidation.TabIndex = 19;
            this.rtbResponseValidation.Text = "";
            // 
            // pbBoudingBox
            // 
            this.pbBoudingBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pbBoudingBox.Location = new System.Drawing.Point(6, 12);
            this.pbBoudingBox.Name = "pbBoudingBox";
            this.pbBoudingBox.Size = new System.Drawing.Size(688, 682);
            this.pbBoudingBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbBoudingBox.TabIndex = 3;
            this.pbBoudingBox.TabStop = false;
            // 
            // gbSourceImage
            // 
            this.gbSourceImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.gbSourceImage.Controls.Add(this.pbImage);
            this.gbSourceImage.Location = new System.Drawing.Point(15, 33);
            this.gbSourceImage.Name = "gbSourceImage";
            this.gbSourceImage.Size = new System.Drawing.Size(700, 700);
            this.gbSourceImage.TabIndex = 5;
            this.gbSourceImage.TabStop = false;
            this.gbSourceImage.Text = "Imagem";
            // 
            // pbImage
            // 
            this.pbImage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pbImage.Location = new System.Drawing.Point(6, 12);
            this.pbImage.Name = "pbImage";
            this.pbImage.Size = new System.Drawing.Size(688, 682);
            this.pbImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbImage.TabIndex = 3;
            this.pbImage.TabStop = false;
            // 
            // groupBox3
            // 
            this.groupBox3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.groupBox3.Controls.Add(this.pbBoudingBox);
            this.groupBox3.Location = new System.Drawing.Point(724, 33);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(700, 700);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Delimitadores";
            // 
            // groupBox2
            // 
            this.groupBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.groupBox2.Controls.Add(this.rtbResponseValidation);
            this.groupBox2.Location = new System.Drawing.Point(1431, 33);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(383, 282);
            this.groupBox2.TabIndex = 21;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Validação";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1826, 771);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.gbSourceImage);
            this.Controls.Add(this.lCameraSource);
            this.Controls.Add(this.bConnectDisconnect);
            this.Controls.Add(this.bSend);
            this.Controls.Add(this.bImageAction);
            this.Controls.Add(this.cbCameraDevices);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbBoudingBox)).EndInit();
            this.gbSourceImage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion
        private System.Windows.Forms.ComboBox cbCameraDevices;
        private System.Windows.Forms.Button bImageAction;
        private System.Windows.Forms.Button bSend;
        private System.Windows.Forms.Button bConnectDisconnect;
        private System.Windows.Forms.Label lCameraSource;
        private System.Windows.Forms.RichTextBox rtbResponseValidation;
        private System.Windows.Forms.PictureBox pbBoudingBox;
        private System.Windows.Forms.GroupBox gbSourceImage;
        private System.Windows.Forms.PictureBox pbImage;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}

