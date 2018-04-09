namespace ServiceManager
{
    partial class ServiceSelectionForm
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
            this.serviceSelectList = new MaterialSkin.Controls.MaterialListView();
            this.ServiceName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.MachineName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Description = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cancelBtn = new MaterialSkin.Controls.MaterialFlatButton();
            this.saveBtn = new MaterialSkin.Controls.MaterialRaisedButton();
            this.SuspendLayout();
            // 
            // serviceSelectList
            // 
            this.serviceSelectList.AllowColumnReorder = true;
            this.serviceSelectList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.serviceSelectList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ServiceName,
            this.MachineName,
            this.Description});
            this.serviceSelectList.Depth = 0;
            this.serviceSelectList.Font = new System.Drawing.Font("Roboto", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
            this.serviceSelectList.FullRowSelect = true;
            this.serviceSelectList.Location = new System.Drawing.Point(12, 75);
            this.serviceSelectList.MouseLocation = new System.Drawing.Point(-1, -1);
            this.serviceSelectList.MouseState = MaterialSkin.MouseState.OUT;
            this.serviceSelectList.Name = "serviceSelectList";
            this.serviceSelectList.OwnerDraw = true;
            this.serviceSelectList.Size = new System.Drawing.Size(878, 375);
            this.serviceSelectList.TabIndex = 0;
            this.serviceSelectList.UseCompatibleStateImageBehavior = false;
            this.serviceSelectList.View = System.Windows.Forms.View.Details;
            this.serviceSelectList.SelectedIndexChanged += new System.EventHandler(this.serviceSelectList_SelectedIndexChanged);
            // 
            // ServiceName
            // 
            this.ServiceName.Text = "Service Name";
            this.ServiceName.Width = 100;
            // 
            // MachineName
            // 
            this.MachineName.Text = "Machine Name";
            this.MachineName.Width = 100;
            // 
            // Description
            // 
            this.Description.Text = "Description";
            this.Description.Width = 185;
            // 
            // cancelBtn
            // 
            this.cancelBtn.AutoSize = true;
            this.cancelBtn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.cancelBtn.Depth = 0;
            this.cancelBtn.Icon = null;
            this.cancelBtn.Location = new System.Drawing.Point(817, 455);
            this.cancelBtn.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.cancelBtn.MouseState = MaterialSkin.MouseState.HOVER;
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Primary = false;
            this.cancelBtn.Size = new System.Drawing.Size(73, 36);
            this.cancelBtn.TabIndex = 1;
            this.cancelBtn.Text = "Cancel";
            this.cancelBtn.UseVisualStyleBackColor = true;
            // 
            // saveBtn
            // 
            this.saveBtn.AutoSize = true;
            this.saveBtn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.saveBtn.Depth = 0;
            this.saveBtn.Icon = null;
            this.saveBtn.Location = new System.Drawing.Point(755, 455);
            this.saveBtn.MouseState = MaterialSkin.MouseState.HOVER;
            this.saveBtn.Name = "saveBtn";
            this.saveBtn.Primary = true;
            this.saveBtn.Size = new System.Drawing.Size(55, 36);
            this.saveBtn.TabIndex = 2;
            this.saveBtn.Text = "Save";
            this.saveBtn.UseVisualStyleBackColor = true;
            // 
            // ServiceSelectionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 500);
            this.Controls.Add(this.saveBtn);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.serviceSelectList);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ServiceSelectionForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Sizable = false;
            this.Text = "Service Selection";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MaterialSkin.Controls.MaterialListView serviceSelectList;
        private MaterialSkin.Controls.MaterialFlatButton cancelBtn;
        private MaterialSkin.Controls.MaterialRaisedButton saveBtn;
        private System.Windows.Forms.ColumnHeader ServiceName;
        private System.Windows.Forms.ColumnHeader MachineName;
        private System.Windows.Forms.ColumnHeader Description;
    }
}