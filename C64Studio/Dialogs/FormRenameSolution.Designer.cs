﻿using RetroDevStudio.Controls;



namespace RetroDevStudio.Dialogs
{
  partial class FormRenameSolution
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose( bool disposing )
    {
      if ( disposing && ( components != null ) )
      {
        components.Dispose();
      }
      base.Dispose( disposing );
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.btnCancel = new System.Windows.Forms.Button();
      this.btnOK = new System.Windows.Forms.Button();
      this.label1 = new System.Windows.Forms.Label();
      this.editSolutionName = new System.Windows.Forms.TextBox();
      this.labelRenameInfo = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // btnCancel
      // 
      this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnCancel.Location = new System.Drawing.Point(426, 234);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(75, 23);
      this.btnCancel.TabIndex = 1;
      this.btnCancel.Text = "Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      // 
      // btnOK
      // 
      this.btnOK.Location = new System.Drawing.Point(345, 234);
      this.btnOK.Name = "btnOK";
      this.btnOK.Size = new System.Drawing.Size(75, 23);
      this.btnOK.TabIndex = 1;
      this.btnOK.Text = "OK";
      this.btnOK.UseVisualStyleBackColor = true;
      this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(12, 12);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(103, 13);
      this.label1.TabIndex = 2;
      this.label1.Text = "Rename Solution to:";
      // 
      // editSolutionName
      // 
      this.editSolutionName.Location = new System.Drawing.Point(121, 9);
      this.editSolutionName.Name = "editSolutionName";
      this.editSolutionName.Size = new System.Drawing.Size(272, 20);
      this.editSolutionName.TabIndex = 3;
      this.editSolutionName.TextChanged += new System.EventHandler(this.editSolutionName_TextChanged);
      // 
      // labelRenameInfo
      // 
      this.labelRenameInfo.Location = new System.Drawing.Point(13, 44);
      this.labelRenameInfo.Name = "labelRenameInfo";
      this.labelRenameInfo.Size = new System.Drawing.Size(480, 172);
      this.labelRenameInfo.TabIndex = 4;
      this.labelRenameInfo.Text = "label2";
      // 
      // FormRenameSolution
      // 
      this.AcceptButton = this.btnOK;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.btnCancel;
      this.ClientSize = new System.Drawing.Size(505, 261);
      this.Controls.Add(this.labelRenameInfo);
      this.Controls.Add(this.editSolutionName);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.btnOK);
      this.Controls.Add(this.btnCancel);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "FormRenameSolution";
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Rename Solution";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.Button btnOK;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox editSolutionName;
    private System.Windows.Forms.Label labelRenameInfo;
  }
}