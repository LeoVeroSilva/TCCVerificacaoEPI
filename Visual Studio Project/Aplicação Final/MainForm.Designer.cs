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
            this.gbSourceImage = new System.Windows.Forms.GroupBox();
            this.pbImage = new System.Windows.Forms.PictureBox();
            this.pbSemaphore = new System.Windows.Forms.PictureBox();
            this.tbValidation = new System.Windows.Forms.TextBox();
            this.gbSourceImage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbSemaphore)).BeginInit();
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
            this.bSend.Location = new System.Drawing.Point(107, 739);
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
            // pbSemaphore
            // 
            this.pbSemaphore.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pbSemaphore.InitialImage = null;
            this.pbSemaphore.Location = new System.Drawing.Point(684, 739);
            this.pbSemaphore.Name = "pbSemaphore";
            this.pbSemaphore.Size = new System.Drawing.Size(25, 25);
            this.pbSemaphore.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbSemaphore.TabIndex = 4;
            this.pbSemaphore.TabStop = false;
            // 
            // tbValidation
            // 
            this.tbValidation.HideSelection = false;
            this.tbValidation.Location = new System.Drawing.Point(395, 742);
            this.tbValidation.Name = "tbValidation";
            this.tbValidation.ReadOnly = true;
            this.tbValidation.Size = new System.Drawing.Size(283, 20);
            this.tbValidation.TabIndex = 22;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(722, 771);
            this.Controls.Add(this.tbValidation);
            this.Controls.Add(this.pbSemaphore);
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
            this.gbSourceImage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbSemaphore)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion
        private System.Windows.Forms.ComboBox cbCameraDevices;
        private System.Windows.Forms.Button bImageAction;
        private System.Windows.Forms.Button bSend;
        private System.Windows.Forms.Button bConnectDisconnect;
        private System.Windows.Forms.Label lCameraSource;
        private System.Windows.Forms.GroupBox gbSourceImage;
        private System.Windows.Forms.PictureBox pbImage;
        private System.Windows.Forms.PictureBox pbSemaphore;
        private System.Windows.Forms.TextBox tbValidation;
    }
}

