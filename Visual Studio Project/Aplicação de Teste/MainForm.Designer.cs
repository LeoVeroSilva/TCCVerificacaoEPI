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
            this.cbCameraDevices = new System.Windows.Forms.ComboBox();
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
            this.gbSourceImage = new System.Windows.Forms.GroupBox();
            this.pbImage = new System.Windows.Forms.PictureBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.gbImageSource.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbBoudingBox)).BeginInit();
            this.gbSourceImage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // rbFile
            // 
            this.rbFile.AutoSize = true;
            this.rbFile.Checked = true;
            this.rbFile.Location = new System.Drawing.Point(6, 19);
            this.rbFile.Name = "rbFile";
            this.rbFile.Size = new System.Drawing.Size(61, 17);
            this.rbFile.TabIndex = 0;
            this.rbFile.TabStop = true;
            this.rbFile.Text = "Arquivo";
            this.rbFile.UseVisualStyleBackColor = true;
            this.rbFile.CheckedChanged += new System.EventHandler(this.rbFile_CheckedChanged);
            // 
            // rbCamera
            // 
            this.rbCamera.AutoSize = true;
            this.rbCamera.Location = new System.Drawing.Point(73, 19);
            this.rbCamera.Name = "rbCamera";
            this.rbCamera.Size = new System.Drawing.Size(61, 17);
            this.rbCamera.TabIndex = 1;
            this.rbCamera.Text = "Câmera";
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
            this.gbImageSource.Size = new System.Drawing.Size(135, 50);
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
            this.bBrowse.Text = "Pesquisar";
            this.bBrowse.UseVisualStyleBackColor = true;
            this.bBrowse.Click += new System.EventHandler(this.bBrowse_Click);
            // 
            // rtbRawReturn
            // 
            this.rtbRawReturn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbRawReturn.Location = new System.Drawing.Point(6, 12);
            this.rtbRawReturn.Name = "rtbRawReturn";
            this.rtbRawReturn.ReadOnly = true;
            this.rtbRawReturn.Size = new System.Drawing.Size(371, 400);
            this.rtbRawReturn.TabIndex = 6;
            this.rtbRawReturn.Text = "";
            // 
            // rtbConsole
            // 
            this.rtbConsole.Location = new System.Drawing.Point(1443, 12);
            this.rtbConsole.Name = "rtbConsole";
            this.rtbConsole.ReadOnly = true;
            this.rtbConsole.Size = new System.Drawing.Size(371, 21);
            this.rtbConsole.TabIndex = 7;
            this.rtbConsole.Text = "";
            // 
            // cbCameraDevices
            // 
            this.cbCameraDevices.FormattingEnabled = true;
            this.cbCameraDevices.Location = new System.Drawing.Point(65, 92);
            this.cbCameraDevices.Name = "cbCameraDevices";
            this.cbCameraDevices.Size = new System.Drawing.Size(121, 21);
            this.cbCameraDevices.TabIndex = 8;
            // 
            // bImageAction
            // 
            this.bImageAction.Location = new System.Drawing.Point(15, 827);
            this.bImageAction.Name = "bImageAction";
            this.bImageAction.Size = new System.Drawing.Size(75, 23);
            this.bImageAction.TabIndex = 9;
            this.bImageAction.Text = "Carregar";
            this.bImageAction.UseVisualStyleBackColor = true;
            this.bImageAction.Click += new System.EventHandler(this.bImageAction_Click);
            // 
            // bSend
            // 
            this.bSend.Location = new System.Drawing.Point(724, 827);
            this.bSend.Name = "bSend";
            this.bSend.Size = new System.Drawing.Size(75, 23);
            this.bSend.TabIndex = 10;
            this.bSend.Text = "Enviar";
            this.bSend.UseVisualStyleBackColor = true;
            this.bSend.Click += new System.EventHandler(this.bSend_Click);
            // 
            // bConnectDisconnect
            // 
            this.bConnectDisconnect.Location = new System.Drawing.Point(192, 92);
            this.bConnectDisconnect.Name = "bConnectDisconnect";
            this.bConnectDisconnect.Size = new System.Drawing.Size(75, 23);
            this.bConnectDisconnect.TabIndex = 11;
            this.bConnectDisconnect.Text = "Conectar";
            this.bConnectDisconnect.UseVisualStyleBackColor = true;
            this.bConnectDisconnect.Click += new System.EventHandler(this.bConnectDisconnect_Click);
            // 
            // cbHelmet
            // 
            this.cbHelmet.AutoSize = true;
            this.cbHelmet.Checked = true;
            this.cbHelmet.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbHelmet.Location = new System.Drawing.Point(6, 19);
            this.cbHelmet.Name = "cbHelmet";
            this.cbHelmet.Size = new System.Drawing.Size(72, 17);
            this.cbHelmet.TabIndex = 12;
            this.cbHelmet.Text = "Capacete";
            this.cbHelmet.UseVisualStyleBackColor = true;
            // 
            // cbMask
            // 
            this.cbMask.AutoSize = true;
            this.cbMask.Checked = true;
            this.cbMask.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbMask.Location = new System.Drawing.Point(84, 19);
            this.cbMask.Name = "cbMask";
            this.cbMask.Size = new System.Drawing.Size(67, 17);
            this.cbMask.TabIndex = 13;
            this.cbMask.Text = "Máscara";
            this.cbMask.UseVisualStyleBackColor = true;
            // 
            // cbGloves
            // 
            this.cbGloves.AutoSize = true;
            this.cbGloves.Checked = true;
            this.cbGloves.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbGloves.Location = new System.Drawing.Point(157, 19);
            this.cbGloves.Name = "cbGloves";
            this.cbGloves.Size = new System.Drawing.Size(55, 17);
            this.cbGloves.TabIndex = 14;
            this.cbGloves.Text = "Luvas";
            this.cbGloves.UseVisualStyleBackColor = true;
            // 
            // lFilePath
            // 
            this.lFilePath.AutoSize = true;
            this.lFilePath.Location = new System.Drawing.Point(12, 72);
            this.lFilePath.Name = "lFilePath";
            this.lFilePath.Size = new System.Drawing.Size(43, 13);
            this.lFilePath.TabIndex = 15;
            this.lFilePath.Text = "Arquivo";
            // 
            // lCameraSource
            // 
            this.lCameraSource.AutoSize = true;
            this.lCameraSource.Location = new System.Drawing.Point(12, 95);
            this.lCameraSource.Name = "lCameraSource";
            this.lCameraSource.Size = new System.Drawing.Size(43, 13);
            this.lCameraSource.TabIndex = 16;
            this.lCameraSource.Text = "Câmera";
            // 
            // lminConfiLvl
            // 
            this.lminConfiLvl.AutoSize = true;
            this.lminConfiLvl.Location = new System.Drawing.Point(3, 46);
            this.lminConfiLvl.Name = "lminConfiLvl";
            this.lminConfiLvl.Size = new System.Drawing.Size(92, 13);
            this.lminConfiLvl.TabIndex = 17;
            this.lminConfiLvl.Text = "Confiânça mínima";
            // 
            // tbMinConfLvl
            // 
            this.tbMinConfLvl.Location = new System.Drawing.Point(101, 42);
            this.tbMinConfLvl.Name = "tbMinConfLvl";
            this.tbMinConfLvl.Size = new System.Drawing.Size(132, 20);
            this.tbMinConfLvl.TabIndex = 18;
            this.tbMinConfLvl.Text = "80";
            this.tbMinConfLvl.Leave += new System.EventHandler(this.tbMinConfLvl_Leave);
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
            this.gbSourceImage.Location = new System.Drawing.Point(18, 121);
            this.gbSourceImage.Name = "gbSourceImage";
            this.gbSourceImage.Size = new System.Drawing.Size(700, 700);
            this.gbSourceImage.TabIndex = 5;
            this.gbSourceImage.TabStop = false;
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
            this.groupBox3.Location = new System.Drawing.Point(724, 121);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(700, 700);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Delimitadores";
            // 
            // groupBox1
            // 
            this.groupBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.groupBox1.Controls.Add(this.rtbRawReturn);
            this.groupBox1.Location = new System.Drawing.Point(1430, 403);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(383, 418);
            this.groupBox1.TabIndex = 20;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Bruto";
            // 
            // groupBox2
            // 
            this.groupBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.groupBox2.Controls.Add(this.rtbResponseValidation);
            this.groupBox2.Location = new System.Drawing.Point(1430, 121);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(383, 282);
            this.groupBox2.TabIndex = 21;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Validação";
            // 
            // groupBox4
            // 
            this.groupBox4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.groupBox4.Controls.Add(this.cbHelmet);
            this.groupBox4.Controls.Add(this.cbMask);
            this.groupBox4.Controls.Add(this.cbGloves);
            this.groupBox4.Controls.Add(this.lminConfiLvl);
            this.groupBox4.Controls.Add(this.tbMinConfLvl);
            this.groupBox4.Location = new System.Drawing.Point(730, 12);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(359, 78);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 117);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 22;
            this.label1.Text = "Imagem";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1826, 858);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.gbSourceImage);
            this.Controls.Add(this.lCameraSource);
            this.Controls.Add(this.bConnectDisconnect);
            this.Controls.Add(this.lFilePath);
            this.Controls.Add(this.bSend);
            this.Controls.Add(this.bImageAction);
            this.Controls.Add(this.cbCameraDevices);
            this.Controls.Add(this.rtbConsole);
            this.Controls.Add(this.bBrowse);
            this.Controls.Add(this.tbFilePath);
            this.Controls.Add(this.gbImageSource);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.gbImageSource.ResumeLayout(false);
            this.gbImageSource.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbBoudingBox)).EndInit();
            this.gbSourceImage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
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
        private System.Windows.Forms.ComboBox cbCameraDevices;
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
        private System.Windows.Forms.RichTextBox rtbResponseValidation;
        private System.Windows.Forms.PictureBox pbBoudingBox;
        private System.Windows.Forms.GroupBox gbSourceImage;
        private System.Windows.Forms.PictureBox pbImage;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label1;
    }
}

