namespace CIA
{
    partial class CIAForm
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
            if(disposing && (components != null))
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CIAForm));
            this.scOutputContainer = new System.Windows.Forms.SplitContainer();
            this.tbToken = new System.Windows.Forms.TextBox();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.btnRead = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.readToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.tbEnv = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbEntityType = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.scOutputContainer)).BeginInit();
            this.scOutputContainer.Panel1.SuspendLayout();
            this.scOutputContainer.Panel2.SuspendLayout();
            this.scOutputContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // scOutputContainer
            // 
            this.scOutputContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.scOutputContainer.Location = new System.Drawing.Point(13, 27);
            this.scOutputContainer.Name = "scOutputContainer";
            // 
            // scOutputContainer.Panel1
            // 
            this.scOutputContainer.Panel1.Controls.Add(this.tbToken);
            // 
            // scOutputContainer.Panel2
            // 
            this.scOutputContainer.Panel2.Controls.Add(this.dgvData);
            this.scOutputContainer.Size = new System.Drawing.Size(774, 339);
            this.scOutputContainer.SplitterDistance = 278;
            this.scOutputContainer.TabIndex = 29;
            // 
            // tbToken
            // 
            this.tbToken.AllowDrop = true;
            this.tbToken.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbToken.Location = new System.Drawing.Point(1, 3);
            this.tbToken.Multiline = true;
            this.tbToken.Name = "tbToken";
            this.tbToken.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbToken.Size = new System.Drawing.Size(278, 336);
            this.tbToken.TabIndex = 3;
            this.tbToken.Click += new System.EventHandler(this.OnTokenClick);
            // 
            // dgvData
            // 
            this.dgvData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgvData.Location = new System.Drawing.Point(1, 3);
            this.dgvData.Name = "dgvData";
            this.dgvData.Size = new System.Drawing.Size(491, 336);
            this.dgvData.TabIndex = 4;
            this.dgvData.TabStop = false;
            // 
            // btnRead
            // 
            this.btnRead.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnRead.Location = new System.Drawing.Point(359, 374);
            this.btnRead.Name = "btnRead";
            this.btnRead.Size = new System.Drawing.Size(75, 23);
            this.btnRead.TabIndex = 25;
            this.btnRead.Text = "Read";
            this.btnRead.UseVisualStyleBackColor = true;
            this.btnRead.Click += new System.EventHandler(this.OnRead);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(799, 24);
            this.menuStrip1.TabIndex = 20;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.readToolStripMenuItem,
            this.aboutToolStripMenuItem1});
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(51, 20);
            this.aboutToolStripMenuItem.Text = "Token";
            // 
            // readToolStripMenuItem
            // 
            this.readToolStripMenuItem.Name = "readToolStripMenuItem";
            this.readToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.readToolStripMenuItem.Text = "Read";
            // 
            // aboutToolStripMenuItem1
            // 
            this.aboutToolStripMenuItem1.Name = "aboutToolStripMenuItem1";
            this.aboutToolStripMenuItem1.Size = new System.Drawing.Size(107, 22);
            this.aboutToolStripMenuItem1.Text = "About";
            this.aboutToolStripMenuItem1.Click += new System.EventHandler(this.OnAbout);
            // 
            // tbEnv
            // 
            this.tbEnv.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tbEnv.Location = new System.Drawing.Point(79, 375);
            this.tbEnv.Name = "tbEnv";
            this.tbEnv.ReadOnly = true;
            this.tbEnv.Size = new System.Drawing.Size(79, 20);
            this.tbEnv.TabIndex = 30;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 379);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 31;
            this.label1.Text = "Environment:";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(167, 379);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 33;
            this.label2.Text = "Entity Type:";
            // 
            // tbEntityType
            // 
            this.tbEntityType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tbEntityType.Location = new System.Drawing.Point(230, 375);
            this.tbEntityType.Name = "tbEntityType";
            this.tbEntityType.ReadOnly = true;
            this.tbEntityType.Size = new System.Drawing.Size(79, 20);
            this.tbEntityType.TabIndex = 32;
            // 
            // CIAForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(799, 407);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbEntityType);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbEnv);
            this.Controls.Add(this.scOutputContainer);
            this.Controls.Add(this.btnRead);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CIAForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CIA - Covert Identity Access";
            this.scOutputContainer.Panel1.ResumeLayout(false);
            this.scOutputContainer.Panel1.PerformLayout();
            this.scOutputContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scOutputContainer)).EndInit();
            this.scOutputContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.SplitContainer scOutputContainer;
        private System.Windows.Forms.TextBox tbToken;
        private System.Windows.Forms.DataGridView dgvData;
        private System.Windows.Forms.Button btnRead;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem readToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem1;
        private System.Windows.Forms.TextBox tbEnv;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbEntityType;
    }
}

