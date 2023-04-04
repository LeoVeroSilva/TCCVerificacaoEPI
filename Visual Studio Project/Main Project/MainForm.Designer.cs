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
            this.rbFile = new System.Windows.Forms.RadioButton();
            this.rbCamera = new System.Windows.Forms.RadioButton();
            this.gbImageSource = new System.Windows.Forms.GroupBox();
            this.pbImage = new System.Windows.Forms.PictureBox();
            this.tbFilePath = new System.Windows.Forms.TextBox();
            this.bBrowse = new System.Windows.Forms.Button();
            this.rtResponse = new System.Windows.Forms.RichTextBox();
            this.rtConsole = new System.Windows.Forms.RichTextBox();
            this.gbResponseType = new System.Windows.Forms.GroupBox();
            this.rbValidation = new System.Windows.Forms.RadioButton();
            this.rbServiceResponse = new System.Windows.Forms.RadioButton();
            this.cbCOMPort = new System.Windows.Forms.ComboBox();
            this.bImageAction = new System.Windows.Forms.Button();
            this.bSend = new System.Windows.Forms.Button();
            this.gbImage = new System.Windows.Forms.GroupBox();
            this.bConnectDisconnect = new System.Windows.Forms.Button();
            this.gbImageSource.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).BeginInit();
            this.gbResponseType.SuspendLayout();
            this.gbImage.SuspendLayout();
            this.SuspendLayout();
            // 
            // rbFile
            // 
            this.rbFile.AutoSize = true;
            this.rbFile.Checked = true;
            this.rbFile.Location = new System.Drawing.Point(6, 19);
            this.rbFile.Name = "rbFile";
            this.rbFile.Size = new System.Drawing.Size(41, 17);
            this.rbFile.TabIndex = 0;
            this.rbFile.TabStop = true;
            this.rbFile.Text = "File";
            this.rbFile.UseVisualStyleBackColor = true;
            this.rbFile.CheckedChanged += new System.EventHandler(this.rbFile_CheckedChanged);
            this.rbFile.Click += new System.EventHandler(this.rbFile_Click);
            // 
            // rbCamera
            // 
            this.rbCamera.AutoSize = true;
            this.rbCamera.Location = new System.Drawing.Point(53, 19);
            this.rbCamera.Name = "rbCamera";
            this.rbCamera.Size = new System.Drawing.Size(61, 17);
            this.rbCamera.TabIndex = 1;
            this.rbCamera.Text = "Camera";
            this.rbCamera.UseVisualStyleBackColor = true;
            this.rbCamera.CheckedChanged += new System.EventHandler(this.rb_camera_CheckedChanged);
            this.rbCamera.Click += new System.EventHandler(this.rbCamera_Click);
            // 
            // gbImageSource
            // 
            this.gbImageSource.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.gbImageSource.Controls.Add(this.rbCamera);
            this.gbImageSource.Controls.Add(this.rbFile);
            this.gbImageSource.Location = new System.Drawing.Point(12, 12);
            this.gbImageSource.Name = "gbImageSource";
            this.gbImageSource.Size = new System.Drawing.Size(121, 50);
            this.gbImageSource.TabIndex = 2;
            this.gbImageSource.TabStop = false;
            // 
            // pbImage
            // 
            this.pbImage.Location = new System.Drawing.Point(6, 12);
            this.pbImage.Name = "pbImage";
            this.pbImage.Size = new System.Drawing.Size(371, 433);
            this.pbImage.TabIndex = 3;
            this.pbImage.TabStop = false;
            // 
            // tbFilePath
            // 
            this.tbFilePath.Location = new System.Drawing.Point(12, 68);
            this.tbFilePath.Name = "tbFilePath";
            this.tbFilePath.Size = new System.Drawing.Size(286, 20);
            this.tbFilePath.TabIndex = 4;
            // 
            // bBrowse
            // 
            this.bBrowse.Location = new System.Drawing.Point(304, 68);
            this.bBrowse.Name = "bBrowse";
            this.bBrowse.Size = new System.Drawing.Size(75, 23);
            this.bBrowse.TabIndex = 5;
            this.bBrowse.Text = "Browse";
            this.bBrowse.UseVisualStyleBackColor = true;
            this.bBrowse.Click += new System.EventHandler(this.bBrowse_Click);
            // 
            // rtResponse
            // 
            this.rtResponse.Location = new System.Drawing.Point(425, 121);
            this.rtResponse.Name = "rtResponse";
            this.rtResponse.ReadOnly = true;
            this.rtResponse.Size = new System.Drawing.Size(363, 451);
            this.rtResponse.TabIndex = 6;
            this.rtResponse.Text = "";
            // 
            // rtConsole
            // 
            this.rtConsole.Location = new System.Drawing.Point(12, 578);
            this.rtConsole.Name = "rtConsole";
            this.rtConsole.ReadOnly = true;
            this.rtConsole.Size = new System.Drawing.Size(776, 21);
            this.rtConsole.TabIndex = 7;
            this.rtConsole.Text = "";
            // 
            // gbResponseType
            // 
            this.gbResponseType.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.gbResponseType.Controls.Add(this.rbValidation);
            this.gbResponseType.Controls.Add(this.rbServiceResponse);
            this.gbResponseType.Location = new System.Drawing.Point(425, 12);
            this.gbResponseType.Name = "gbResponseType";
            this.gbResponseType.Size = new System.Drawing.Size(200, 50);
            this.gbResponseType.TabIndex = 3;
            this.gbResponseType.TabStop = false;
            // 
            // rbValidation
            // 
            this.rbValidation.AutoSize = true;
            this.rbValidation.Location = new System.Drawing.Point(124, 19);
            this.rbValidation.Name = "rbValidation";
            this.rbValidation.Size = new System.Drawing.Size(71, 17);
            this.rbValidation.TabIndex = 1;
            this.rbValidation.Text = "Validation";
            this.rbValidation.UseVisualStyleBackColor = true;
            // 
            // rbServiceResponse
            // 
            this.rbServiceResponse.AutoSize = true;
            this.rbServiceResponse.Checked = true;
            this.rbServiceResponse.Location = new System.Drawing.Point(6, 19);
            this.rbServiceResponse.Name = "rbServiceResponse";
            this.rbServiceResponse.Size = new System.Drawing.Size(112, 17);
            this.rbServiceResponse.TabIndex = 0;
            this.rbServiceResponse.TabStop = true;
            this.rbServiceResponse.Text = "Service Response";
            this.rbServiceResponse.UseVisualStyleBackColor = true;
            this.rbServiceResponse.CheckedChanged += new System.EventHandler(this.rbServiceResponse_CheckedChanged);
            // 
            // cbCOMPort
            // 
            this.cbCOMPort.Enabled = false;
            this.cbCOMPort.FormattingEnabled = true;
            this.cbCOMPort.Location = new System.Drawing.Point(12, 94);
            this.cbCOMPort.Name = "cbCOMPort";
            this.cbCOMPort.Size = new System.Drawing.Size(121, 21);
            this.cbCOMPort.TabIndex = 8;
            this.cbCOMPort.Visible = false;
            // 
            // bImageAction
            // 
            this.bImageAction.Location = new System.Drawing.Point(12, 605);
            this.bImageAction.Name = "bImageAction";
            this.bImageAction.Size = new System.Drawing.Size(75, 23);
            this.bImageAction.TabIndex = 9;
            this.bImageAction.Text = "Load";
            this.bImageAction.UseVisualStyleBackColor = true;
            this.bImageAction.Click += new System.EventHandler(this.bImageAction_Click);
            // 
            // bSend
            // 
            this.bSend.Location = new System.Drawing.Point(425, 605);
            this.bSend.Name = "bSend";
            this.bSend.Size = new System.Drawing.Size(75, 23);
            this.bSend.TabIndex = 10;
            this.bSend.Text = "Send";
            this.bSend.UseVisualStyleBackColor = true;
            // 
            // gbImage
            // 
            this.gbImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.gbImage.Controls.Add(this.pbImage);
            this.gbImage.Location = new System.Drawing.Point(12, 115);
            this.gbImage.Name = "gbImage";
            this.gbImage.Size = new System.Drawing.Size(383, 457);
            this.gbImage.TabIndex = 4;
            this.gbImage.TabStop = false;
            // 
            // bConnectDisconnect
            // 
            this.bConnectDisconnect.Enabled = false;
            this.bConnectDisconnect.Location = new System.Drawing.Point(139, 94);
            this.bConnectDisconnect.Name = "bConnectDisconnect";
            this.bConnectDisconnect.Size = new System.Drawing.Size(75, 23);
            this.bConnectDisconnect.TabIndex = 11;
            this.bConnectDisconnect.Text = "Connect";
            this.bConnectDisconnect.UseVisualStyleBackColor = true;
            this.bConnectDisconnect.Visible = false;
            this.bConnectDisconnect.Click += new System.EventHandler(this.bConnectDisconnect_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(798, 634);
            this.Controls.Add(this.bConnectDisconnect);
            this.Controls.Add(this.gbImage);
            this.Controls.Add(this.bSend);
            this.Controls.Add(this.bImageAction);
            this.Controls.Add(this.cbCOMPort);
            this.Controls.Add(this.gbResponseType);
            this.Controls.Add(this.rtConsole);
            this.Controls.Add(this.rtResponse);
            this.Controls.Add(this.bBrowse);
            this.Controls.Add(this.tbFilePath);
            this.Controls.Add(this.gbImageSource);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.gbImageSource.ResumeLayout(false);
            this.gbImageSource.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).EndInit();
            this.gbResponseType.ResumeLayout(false);
            this.gbResponseType.PerformLayout();
            this.gbImage.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private System.Windows.Forms.RadioButton rbFile;
        private System.Windows.Forms.RadioButton rbCamera;
        private System.Windows.Forms.GroupBox gbImageSource;
        private System.Windows.Forms.PictureBox pbImage;
        private System.Windows.Forms.TextBox tbFilePath;
        private System.Windows.Forms.Button bBrowse;
        private System.Windows.Forms.RichTextBox rtResponse;
        private System.Windows.Forms.RichTextBox rtConsole;
        private System.Windows.Forms.GroupBox gbResponseType;
        private System.Windows.Forms.RadioButton rbValidation;
        private System.Windows.Forms.RadioButton rbServiceResponse;
        private System.Windows.Forms.ComboBox cbCOMPort;
        private System.Windows.Forms.Button bImageAction;
        private System.Windows.Forms.Button bSend;
        private System.Windows.Forms.GroupBox gbImage;
        private System.Windows.Forms.Button bConnectDisconnect;
    }
}

