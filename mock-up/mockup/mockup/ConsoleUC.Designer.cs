namespace mockup
{
    partial class ConsoleUC
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Vendeur");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Manager");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Acteurs", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2});
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Serveur mongodb 1");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Serveurs", new System.Windows.Forms.TreeNode[] {
            treeNode4});
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Flux 1");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("Application 2", new System.Windows.Forms.TreeNode[] {
            treeNode6});
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("Flux", new System.Windows.Forms.TreeNode[] {
            treeNode7});
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("Name");
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("Status");
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("Account", new System.Windows.Forms.TreeNode[] {
            treeNode9,
            treeNode10});
            System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("Models", new System.Windows.Forms.TreeNode[] {
            treeNode11});
            System.Windows.Forms.TreeNode treeNode13 = new System.Windows.Forms.TreeNode("Exposition du model account");
            System.Windows.Forms.TreeNode treeNode14 = new System.Windows.Forms.TreeNode("Services", new System.Windows.Forms.TreeNode[] {
            treeNode13});
            System.Windows.Forms.TreeNode treeNode15 = new System.Windows.Forms.TreeNode("Structures", new System.Windows.Forms.TreeNode[] {
            treeNode8,
            treeNode12,
            treeNode14});
            System.Windows.Forms.TreeNode treeNode16 = new System.Windows.Forms.TreeNode("Actions");
            System.Windows.Forms.TreeNode treeNode17 = new System.Windows.Forms.TreeNode("Processus 1");
            System.Windows.Forms.TreeNode treeNode18 = new System.Windows.Forms.TreeNode("Processus 2");
            System.Windows.Forms.TreeNode treeNode19 = new System.Windows.Forms.TreeNode("Workflows", new System.Windows.Forms.TreeNode[] {
            treeNode17,
            treeNode18});
            System.Windows.Forms.TreeNode treeNode20 = new System.Windows.Forms.TreeNode("Francais");
            System.Windows.Forms.TreeNode treeNode21 = new System.Windows.Forms.TreeNode("Languages", new System.Windows.Forms.TreeNode[] {
            treeNode20});
            System.Windows.Forms.TreeNode treeNode22 = new System.Windows.Forms.TreeNode("Name");
            System.Windows.Forms.TreeNode treeNode23 = new System.Windows.Forms.TreeNode("Bundle civilité", new System.Windows.Forms.TreeNode[] {
            treeNode22});
            System.Windows.Forms.TreeNode treeNode24 = new System.Windows.Forms.TreeNode("Traductions", new System.Windows.Forms.TreeNode[] {
            treeNode21,
            treeNode23});
            System.Windows.Forms.TreeNode treeNode25 = new System.Windows.Forms.TreeNode("Name");
            System.Windows.Forms.TreeNode treeNode26 = new System.Windows.Forms.TreeNode("Component Infos generales", new System.Windows.Forms.TreeNode[] {
            treeNode25});
            System.Windows.Forms.TreeNode treeNode27 = new System.Windows.Forms.TreeNode("Components", new System.Windows.Forms.TreeNode[] {
            treeNode26});
            System.Windows.Forms.TreeNode treeNode28 = new System.Windows.Forms.TreeNode("Screen 1");
            System.Windows.Forms.TreeNode treeNode29 = new System.Windows.Forms.TreeNode("Screens", new System.Windows.Forms.TreeNode[] {
            treeNode28});
            System.Windows.Forms.TreeNode treeNode30 = new System.Windows.Forms.TreeNode("Interfaces utilisateur", new System.Windows.Forms.TreeNode[] {
            treeNode27,
            treeNode29});
            System.Windows.Forms.TreeNode treeNode31 = new System.Windows.Forms.TreeNode("Role Administrateur");
            System.Windows.Forms.TreeNode treeNode32 = new System.Windows.Forms.TreeNode("Roles", new System.Windows.Forms.TreeNode[] {
            treeNode31});
            System.Windows.Forms.TreeNode treeNode33 = new System.Windows.Forms.TreeNode("Permission name");
            System.Windows.Forms.TreeNode treeNode34 = new System.Windows.Forms.TreeNode("Permissions", new System.Windows.Forms.TreeNode[] {
            treeNode33});
            System.Windows.Forms.TreeNode treeNode35 = new System.Windows.Forms.TreeNode("Securité", new System.Windows.Forms.TreeNode[] {
            treeNode32,
            treeNode34});
            System.Windows.Forms.TreeNode treeNode36 = new System.Windows.Forms.TreeNode("Application CRM", new System.Windows.Forms.TreeNode[] {
            treeNode3,
            treeNode5,
            treeNode15,
            treeNode16,
            treeNode19,
            treeNode24,
            treeNode30,
            treeNode35});
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeView1);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            this.splitContainer1.Size = new System.Drawing.Size(862, 631);
            this.splitContainer1.SplitterDistance = 287;
            this.splitContainer1.TabIndex = 0;
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Location = new System.Drawing.Point(0, 54);
            this.treeView1.Name = "treeView1";
            treeNode1.Name = "Node10";
            treeNode1.Text = "Vendeur";
            treeNode2.Name = "Node11";
            treeNode2.Text = "Manager";
            treeNode3.Name = "Node9";
            treeNode3.Text = "Acteurs";
            treeNode4.Name = "Node31";
            treeNode4.Text = "Serveur mongodb 1";
            treeNode5.Name = "Node30";
            treeNode5.Text = "Serveurs";
            treeNode6.Name = "Node33";
            treeNode6.Text = "Flux 1";
            treeNode7.Name = "Node34";
            treeNode7.Text = "Application 2";
            treeNode8.Name = "Node32";
            treeNode8.Text = "Flux";
            treeNode9.Name = "Node3";
            treeNode9.Text = "Name";
            treeNode10.Name = "Node4";
            treeNode10.Text = "Status";
            treeNode11.Name = "Node2";
            treeNode11.Text = "Account";
            treeNode12.Name = "Node0";
            treeNode12.Text = "Models";
            treeNode13.Name = "Node29";
            treeNode13.Text = "Exposition du model account";
            treeNode14.Name = "Node28";
            treeNode14.Text = "Services";
            treeNode15.Name = "Node1";
            treeNode15.Text = "Structures";
            treeNode16.Name = "Node0";
            treeNode16.Text = "Actions";
            treeNode17.Name = "Node7";
            treeNode17.Text = "Processus 1";
            treeNode18.Name = "Node6";
            treeNode18.Text = "Processus 2";
            treeNode19.Name = "Node5";
            treeNode19.Text = "Workflows";
            treeNode20.Name = "Node16";
            treeNode20.Text = "Francais";
            treeNode21.Name = "Node15";
            treeNode21.Text = "Languages";
            treeNode22.Name = "Node14";
            treeNode22.Text = "Name";
            treeNode23.Name = "Node13";
            treeNode23.Text = "Bundle civilité";
            treeNode24.Name = "Node12";
            treeNode24.Text = "Traductions";
            treeNode25.Name = "Node21";
            treeNode25.Text = "Name";
            treeNode26.Name = "Node20";
            treeNode26.Text = "Component Infos generales";
            treeNode27.Name = "Node19";
            treeNode27.Text = "Components";
            treeNode28.Name = "Node18";
            treeNode28.Text = "Screen 1";
            treeNode29.Name = "Node17";
            treeNode29.Text = "Screens";
            treeNode30.Name = "Node23";
            treeNode30.Text = "Interfaces utilisateur";
            treeNode31.Name = "Node26";
            treeNode31.Text = "Role Administrateur";
            treeNode32.Name = "Node25";
            treeNode32.Text = "Roles";
            treeNode33.Name = "Node27";
            treeNode33.Text = "Permission name";
            treeNode34.Name = "Node24";
            treeNode34.Text = "Permissions";
            treeNode35.Name = "Node22";
            treeNode35.Text = "Securité";
            treeNode36.Name = "Node0";
            treeNode36.Text = "Application CRM";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode36});
            this.treeView1.Size = new System.Drawing.Size(287, 577);
            this.treeView1.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.comboBox1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(287, 54);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Application";
            // 
            // comboBox1
            // 
            this.comboBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(6, 19);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(275, 21);
            this.comboBox1.TabIndex = 1;
            // 
            // ConsoleUC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "ConsoleUC";
            this.Size = new System.Drawing.Size(862, 631);
            this.splitContainer1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox comboBox1;
    }
}
