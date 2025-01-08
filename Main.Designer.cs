namespace VRChat_Photo_Directory_Changer
{
    partial class Main
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            previousFolder = new Label();
            newFolder = new Label();
            ChangeButton = new Button();
            newFolderTextbox = new TextBox();
            openNewFolder = new Button();
            previousFolderLabel = new Label();
            openPreviousFolder = new Button();
            SuspendLayout();
            // 
            // previousFolder
            // 
            previousFolder.AutoSize = true;
            previousFolder.Font = new Font("Yu Gothic UI", 13F);
            previousFolder.Location = new Point(12, 9);
            previousFolder.Name = "previousFolder";
            previousFolder.Size = new Size(66, 25);
            previousFolder.TabIndex = 0;
            previousFolder.Text = "変更前";
            // 
            // newFolder
            // 
            newFolder.AutoSize = true;
            newFolder.Font = new Font("Yu Gothic UI", 13F);
            newFolder.Location = new Point(12, 46);
            newFolder.Name = "newFolder";
            newFolder.Size = new Size(66, 25);
            newFolder.TabIndex = 1;
            newFolder.Text = "変更後";
            // 
            // ChangeButton
            // 
            ChangeButton.Font = new Font("Yu Gothic UI", 13F);
            ChangeButton.Location = new Point(253, 93);
            ChangeButton.Name = "ChangeButton";
            ChangeButton.Size = new Size(110, 35);
            ChangeButton.TabIndex = 2;
            ChangeButton.Text = "変更";
            ChangeButton.UseVisualStyleBackColor = true;
            ChangeButton.Click += ChangeButton_Click;
            // 
            // newFolderTextbox
            // 
            newFolderTextbox.Font = new Font("Yu Gothic UI", 11F);
            newFolderTextbox.Location = new Point(84, 45);
            newFolderTextbox.Name = "newFolderTextbox";
            newFolderTextbox.Size = new Size(394, 27);
            newFolderTextbox.TabIndex = 4;
            // 
            // openNewFolder
            // 
            openNewFolder.Location = new Point(484, 44);
            openNewFolder.Name = "openNewFolder";
            openNewFolder.Size = new Size(110, 30);
            openNewFolder.TabIndex = 5;
            openNewFolder.Text = "フォルダを開く";
            openNewFolder.UseVisualStyleBackColor = true;
            openNewFolder.Click += openNewFolder_Click;
            // 
            // previousFolderLabel
            // 
            previousFolderLabel.AutoSize = true;
            previousFolderLabel.Font = new Font("Yu Gothic UI", 11F);
            previousFolderLabel.Location = new Point(84, 11);
            previousFolderLabel.Name = "previousFolderLabel";
            previousFolderLabel.Size = new Size(131, 20);
            previousFolderLabel.TabIndex = 6;
            previousFolderLabel.Text = "取得できませんでした";
            // 
            // openPreviousFolder
            // 
            openPreviousFolder.Location = new Point(484, 7);
            openPreviousFolder.Name = "openPreviousFolder";
            openPreviousFolder.Size = new Size(110, 30);
            openPreviousFolder.TabIndex = 7;
            openPreviousFolder.Text = "フォルダを開く";
            openPreviousFolder.UseVisualStyleBackColor = true;
            openPreviousFolder.Click += openPreviousFolder_Click;
            // 
            // Main
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(606, 140);
            Controls.Add(openPreviousFolder);
            Controls.Add(previousFolderLabel);
            Controls.Add(openNewFolder);
            Controls.Add(newFolderTextbox);
            Controls.Add(ChangeButton);
            Controls.Add(newFolder);
            Controls.Add(previousFolder);
            MaximizeBox = false;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "Main";
            Text = "VRChat Photo Directory Changer";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label previousFolder;
        private Label newFolder;
        private Button ChangeButton;
        private TextBox newFolderTextbox;
        private Button openNewFolder;
        private Label previousFolderLabel;
        private Button openPreviousFolder;
    }
}
