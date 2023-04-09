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
            this.tbFilePath = new System.Windows.Forms.TextBox();
            this.bBrowse = new System.Windows.Forms.Button();
            this.rtbRawReturn = new System.Windows.Forms.RichTextBox();
            this.rtbConsole = new System.Windows.Forms.RichTextBox();
            this.gbResponseType = new System.Windows.Forms.GroupBox();
            this.rbResponseBoundingBox = new System.Windows.Forms.RadioButton();
            this.rbResponseValidation = new System.Windows.Forms.RadioButton();
            this.rbRawResponse = new System.Windows.Forms.RadioButton();
            this.cbCOMPort = new System.Windows.Forms.ComboBox();
            this.bImageAction = new System.Windows.Forms.Button();
            this.bSend = new System.Windows.Forms.Button();
            this.bConnectDisconnect = new System.Windows.Forms.Button();
            this.cbHelmet = new System.Windows.Forms.CheckBox();
            this.cbMask = new System.Windows.Forms.CheckBox();
            this.cbGloves = new System.Windows.Forms.CheckBox();
            this.lFilePath = new System.Windows.Forms.Label();
            this.lCameraSource = new System.Windows.Forms.Label();
            this.lminConfiLvl = new System.Windows.Forms.Label();
            this.tbMinConfLvl = new System.Windows.Forms.TextBox();
            this.rtbResponseValidation = new System.Windows.Forms.RichTextBox();
            this.pbBoudingBox = new System.Windows.Forms.PictureBox();
            this.gbResponse = new System.Windows.Forms.GroupBox();
            this.gbSourceImage = new System.Windows.Forms.GroupBox();
            this.pbImage = new System.Windows.Forms.PictureBox();
            this.gbImageSource.SuspendLayout();
            this.gbResponseType.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbBoudingBox)).BeginInit();
            this.gbResponse.SuspendLayout();
            this.gbSourceImage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).BeginInit();
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
            // tbFilePath
            // 
            this.tbFilePath.Location = new System.Drawing.Point(65, 69);
            this.tbFilePath.Name = "tbFilePath";
            this.tbFilePath.Size = new System.Drawing.Size(249, 20);
            this.tbFilePath.TabIndex = 4;
            // 
            // bBrowse
            // 
            this.bBrowse.Location = new System.Drawing.Point(320, 67);
            this.bBrowse.Name = "bBrowse";
            this.bBrowse.Size = new System.Drawing.Size(75, 23);
            this.bBrowse.TabIndex = 5;
            this.bBrowse.Text = "Browse";
            this.bBrowse.UseVisualStyleBackColor = true;
            this.bBrowse.Click += new System.EventHandler(this.bBrowse_Click);
            // 
            // rtbRawReturn
            // 
            this.rtbRawReturn.Location = new System.Drawing.Point(6, 12);
            this.rtbRawReturn.Name = "rtbRawReturn";
            this.rtbRawReturn.ReadOnly = true;
            this.rtbRawReturn.Size = new System.Drawing.Size(330, 433);
            this.rtbRawReturn.TabIndex = 6;
            this.rtbRawReturn.Text = "";
            // 
            // rtbConsole
            // 
            this.rtbConsole.Location = new System.Drawing.Point(12, 578);
            this.rtbConsole.Name = "rtbConsole";
            this.rtbConsole.ReadOnly = true;
            this.rtbConsole.Size = new System.Drawing.Size(755, 21);
            this.rtbConsole.TabIndex = 7;
            this.rtbConsole.Text = "";
            // 
            // gbResponseType
            // 
            this.gbResponseType.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.gbResponseType.Controls.Add(this.rbResponseBoundingBox);
            this.gbResponseType.Controls.Add(this.rbResponseValidation);
            this.gbResponseType.Controls.Add(this.rbRawResponse);
            this.gbResponseType.Location = new System.Drawing.Point(425, 12);
            this.gbResponseType.Name = "gbResponseType";
            this.gbResponseType.Size = new System.Drawing.Size(232, 50);
            this.gbResponseType.TabIndex = 3;
            this.gbResponseType.TabStop = false;
            // 
            // rbResponseBoundingBox
            // 
            this.rbResponseBoundingBox.AutoSize = true;
            this.rbResponseBoundingBox.Location = new System.Drawing.Point(83, 19);
            this.rbResponseBoundingBox.Name = "rbResponseBoundingBox";
            this.rbResponseBoundingBox.Size = new System.Drawing.Size(91, 17);
            this.rbResponseBoundingBox.TabIndex = 2;
            this.rbResponseBoundingBox.Text = "Bounding Box";
            this.rbResponseBoundingBox.UseVisualStyleBackColor = true;
            this.rbResponseBoundingBox.CheckedChanged += new System.EventHandler(this.rbResponseBoundingBox_CheckedChanged);
            // 
            // rbResponseValidation
            // 
            this.rbResponseValidation.AutoSize = true;
            this.rbResponseValidation.Location = new System.Drawing.Point(6, 19);
            this.rbResponseValidation.Name = "rbResponseValidation";
            this.rbResponseValidation.Size = new System.Drawing.Size(71, 17);
            this.rbResponseValidation.TabIndex = 1;
            this.rbResponseValidation.Text = "Validation";
            this.rbResponseValidation.UseVisualStyleBackColor = true;
            this.rbResponseValidation.CheckedChanged += new System.EventHandler(this.rbResponseValidation_CheckedChanged);
            // 
            // rbRawResponse
            // 
            this.rbRawResponse.AutoSize = true;
            this.rbRawResponse.Checked = true;
            this.rbRawResponse.Location = new System.Drawing.Point(180, 19);
            this.rbRawResponse.Name = "rbRawResponse";
            this.rbRawResponse.Size = new System.Drawing.Size(47, 17);
            this.rbRawResponse.TabIndex = 0;
            this.rbRawResponse.TabStop = true;
            this.rbRawResponse.Text = "Raw";
            this.rbRawResponse.UseVisualStyleBackColor = true;
            this.rbRawResponse.CheckedChanged += new System.EventHandler(this.rbRawResponse_CheckedChanged);
            // 
            // cbCOMPort
            // 
            this.cbCOMPort.Enabled = false;
            this.cbCOMPort.FormattingEnabled = true;
            this.cbCOMPort.Location = new System.Drawing.Point(98, 92);
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
            this.bSend.Click += new System.EventHandler(this.bSend_Click);
            // 
            // bConnectDisconnect
            // 
            this.bConnectDisconnect.Enabled = false;
            this.bConnectDisconnect.Location = new System.Drawing.Point(225, 92);
            this.bConnectDisconnect.Name = "bConnectDisconnect";
            this.bConnectDisconnect.Size = new System.Drawing.Size(75, 23);
            this.bConnectDisconnect.TabIndex = 11;
            this.bConnectDisconnect.Text = "Connect";
            this.bConnectDisconnect.UseVisualStyleBackColor = true;
            this.bConnectDisconnect.Visible = false;
            this.bConnectDisconnect.Click += new System.EventHandler(this.bConnectDisconnect_Click);
            // 
            // cbHelmet
            // 
            this.cbHelmet.AutoSize = true;
            this.cbHelmet.Checked = true;
            this.cbHelmet.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbHelmet.Location = new System.Drawing.Point(425, 69);
            this.cbHelmet.Name = "cbHelmet";
            this.cbHelmet.Size = new System.Drawing.Size(59, 17);
            this.cbHelmet.TabIndex = 12;
            this.cbHelmet.Text = "Helmet";
            this.cbHelmet.UseVisualStyleBackColor = true;
            // 
            // cbMask
            // 
            this.cbMask.AutoSize = true;
            this.cbMask.Checked = true;
            this.cbMask.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbMask.Location = new System.Drawing.Point(490, 69);
            this.cbMask.Name = "cbMask";
            this.cbMask.Size = new System.Drawing.Size(52, 17);
            this.cbMask.TabIndex = 13;
            this.cbMask.Text = "Mask";
            this.cbMask.UseVisualStyleBackColor = true;
            // 
            // cbGloves
            // 
            this.cbGloves.AutoSize = true;
            this.cbGloves.Checked = true;
            this.cbGloves.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbGloves.Location = new System.Drawing.Point(549, 68);
            this.cbGloves.Name = "cbGloves";
            this.cbGloves.Size = new System.Drawing.Size(59, 17);
            this.cbGloves.TabIndex = 14;
            this.cbGloves.Text = "Gloves";
            this.cbGloves.UseVisualStyleBackColor = true;
            // 
            // lFilePath
            // 
            this.lFilePath.AutoSize = true;
            this.lFilePath.Location = new System.Drawing.Point(12, 72);
            this.lFilePath.Name = "lFilePath";
            this.lFilePath.Size = new System.Drawing.Size(48, 13);
            this.lFilePath.TabIndex = 15;
            this.lFilePath.Text = "File Path";
            // 
            // lCameraSource
            // 
            this.lCameraSource.AutoSize = true;
            this.lCameraSource.Location = new System.Drawing.Point(12, 95);
            this.lCameraSource.Name = "lCameraSource";
            this.lCameraSource.Size = new System.Drawing.Size(80, 13);
            this.lCameraSource.TabIndex = 16;
            this.lCameraSource.Text = "Camera Source";
            this.lCameraSource.Visible = false;
            // 
            // lminConfiLvl
            // 
            this.lminConfiLvl.AutoSize = true;
            this.lminConfiLvl.Location = new System.Drawing.Point(422, 97);
            this.lminConfiLvl.Name = "lminConfiLvl";
            this.lminConfiLvl.Size = new System.Drawing.Size(113, 13);
            this.lminConfiLvl.TabIndex = 17;
            this.lminConfiLvl.Text = "Min. Confidence Level";
            // 
            // tbMinConfLvl
            // 
            this.tbMinConfLvl.Location = new System.Drawing.Point(541, 93);
            this.tbMinConfLvl.Name = "tbMinConfLvl";
            this.tbMinConfLvl.Size = new System.Drawing.Size(132, 20);
            this.tbMinConfLvl.TabIndex = 18;
            this.tbMinConfLvl.Text = "80";
            this.tbMinConfLvl.Leave += new System.EventHandler(this.tbMinConfLvl_Leave);
            // 
            // rtbResponseValidation
            // 
            this.rtbResponseValidation.Enabled = false;
            this.rtbResponseValidation.Location = new System.Drawing.Point(6, 12);
            this.rtbResponseValidation.Name = "rtbResponseValidation";
            this.rtbResponseValidation.ReadOnly = true;
            this.rtbResponseValidation.Size = new System.Drawing.Size(330, 433);
            this.rtbResponseValidation.TabIndex = 19;
            this.rtbResponseValidation.Text = "";
            this.rtbResponseValidation.Visible = false;
            // 
            // pbBoudingBox
            // 
            this.pbBoudingBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pbBoudingBox.Enabled = false;
            this.pbBoudingBox.Location = new System.Drawing.Point(6, 12);
            this.pbBoudingBox.Name = "pbBoudingBox";
            this.pbBoudingBox.Size = new System.Drawing.Size(330, 433);
            this.pbBoudingBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbBoudingBox.TabIndex = 3;
            this.pbBoudingBox.TabStop = false;
            this.pbBoudingBox.Visible = false;
            // 
            // gbResponse
            // 
            this.gbResponse.Controls.Add(this.rtbRawReturn);
            this.gbResponse.Controls.Add(this.rtbResponseValidation);
            this.gbResponse.Controls.Add(this.pbBoudingBox);
            this.gbResponse.Location = new System.Drawing.Point(425, 121);
            this.gbResponse.Name = "gbResponse";
            this.gbResponse.Size = new System.Drawing.Size(342, 451);
            this.gbResponse.TabIndex = 20;
            this.gbResponse.TabStop = false;
            // 
            // gbSourceImage
            // 
            this.gbSourceImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.gbSourceImage.Controls.Add(this.pbImage);
            this.gbSourceImage.Location = new System.Drawing.Point(18, 121);
            this.gbSourceImage.Name = "gbSourceImage";
            this.gbSourceImage.Size = new System.Drawing.Size(383, 451);
            this.gbSourceImage.TabIndex = 5;
            this.gbSourceImage.TabStop = false;
            // 
            // pbImage
            // 
            this.pbImage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pbImage.Location = new System.Drawing.Point(6, 12);
            this.pbImage.Name = "pbImage";
            this.pbImage.Size = new System.Drawing.Size(371, 433);
            this.pbImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbImage.TabIndex = 3;
            this.pbImage.TabStop = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 634);
            this.Controls.Add(this.gbSourceImage);
            this.Controls.Add(this.gbResponse);
            this.Controls.Add(this.tbMinConfLvl);
            this.Controls.Add(this.lminConfiLvl);
            this.Controls.Add(this.lCameraSource);
            this.Controls.Add(this.bConnectDisconnect);
            this.Controls.Add(this.lFilePath);
            this.Controls.Add(this.cbGloves);
            this.Controls.Add(this.cbMask);
            this.Controls.Add(this.cbHelmet);
            this.Controls.Add(this.bSend);
            this.Controls.Add(this.bImageAction);
            this.Controls.Add(this.cbCOMPort);
            this.Controls.Add(this.gbResponseType);
            this.Controls.Add(this.rtbConsole);
            this.Controls.Add(this.bBrowse);
            this.Controls.Add(this.tbFilePath);
            this.Controls.Add(this.gbImageSource);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.gbImageSource.ResumeLayout(false);
            this.gbImageSource.PerformLayout();
            this.gbResponseType.ResumeLayout(false);
            this.gbResponseType.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbBoudingBox)).EndInit();
            this.gbResponse.ResumeLayout(false);
            this.gbSourceImage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private System.Windows.Forms.RadioButton rbFile;
        private System.Windows.Forms.RadioButton rbCamera;
        private System.Windows.Forms.GroupBox gbImageSource;
        private System.Windows.Forms.TextBox tbFilePath;
        private System.Windows.Forms.Button bBrowse;
        private System.Windows.Forms.RichTextBox rtbRawReturn;
        private System.Windows.Forms.RichTextBox rtbConsole;
        private System.Windows.Forms.GroupBox gbResponseType;
        private System.Windows.Forms.RadioButton rbResponseValidation;
        private System.Windows.Forms.RadioButton rbRawResponse;
        private System.Windows.Forms.ComboBox cbCOMPort;
        private System.Windows.Forms.Button bImageAction;
        private System.Windows.Forms.Button bSend;
        private System.Windows.Forms.Button bConnectDisconnect;
        private System.Windows.Forms.CheckBox cbHelmet;
        private System.Windows.Forms.CheckBox cbMask;
        private System.Windows.Forms.CheckBox cbGloves;
        private System.Windows.Forms.Label lFilePath;
        private System.Windows.Forms.Label lCameraSource;
        private System.Windows.Forms.Label lminConfiLvl;
        private System.Windows.Forms.TextBox tbMinConfLvl;
        private System.Windows.Forms.RadioButton rbResponseBoundingBox;
        private System.Windows.Forms.RichTextBox rtbResponseValidation;
        private System.Windows.Forms.PictureBox pbBoudingBox;
        private System.Windows.Forms.GroupBox gbResponse;
        private System.Windows.Forms.GroupBox gbSourceImage;
        private System.Windows.Forms.PictureBox pbImage;
    }
}

