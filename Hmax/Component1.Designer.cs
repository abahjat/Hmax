namespace Hmax
{
    partial class Component1
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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Profiles = new System.Windows.Forms.Label();
            // 
            // Profiles
            // 
            this.Profiles.Location = new System.Drawing.Point(0, 0);
            this.Profiles.Name = "Profiles";
            this.Profiles.Size = new System.Drawing.Size(100, 100);
            this.Profiles.TabIndex = 0;
            this.Profiles.Text = "label1";

        }

        #endregion

        private System.Windows.Forms.Label Profiles;

    }
}
