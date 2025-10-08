namespace Chat_ProyectoIG
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel chatPanel;
        private System.Windows.Forms.ListBox chatBox;
        private System.Windows.Forms.Panel bottomPanel;
        private System.Windows.Forms.RichTextBox inputBox;
        private System.Windows.Forms.Button sendButton;
        private System.Windows.Forms.ListBox memberList;
        private System.Windows.Forms.Button boton_agregar;
        private System.Windows.Forms.Button Crear_Grupo;

        /// <summary>
        /// Limpiar recursos
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        /// <summary>
        /// Método generado por el Diseñador
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.chatPanel = new System.Windows.Forms.Panel();
            this.chatBox = new System.Windows.Forms.ListBox();
            this.bottomPanel = new System.Windows.Forms.Panel();
            this.emoji = new System.Windows.Forms.Button();
            this.inputBox = new System.Windows.Forms.RichTextBox();
            this.sendButton = new System.Windows.Forms.Button();
            this.memberList = new System.Windows.Forms.ListBox();
            this.boton_agregar = new System.Windows.Forms.Button();
            this.Crear_Grupo = new System.Windows.Forms.Button();
            this.groupList = new System.Windows.Forms.ListBox();
            this.miembrosGrupo = new System.Windows.Forms.ListBox();
            this.chatPanel.SuspendLayout();
            this.bottomPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // chatPanel
            // 
            this.chatPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(57)))), ((int)(((byte)(63)))));
            this.chatPanel.Controls.Add(this.chatBox);
            this.chatPanel.Controls.Add(this.bottomPanel);
            this.chatPanel.Location = new System.Drawing.Point(104, 64);
            this.chatPanel.Name = "chatPanel";
            this.chatPanel.Size = new System.Drawing.Size(554, 533);
            this.chatPanel.TabIndex = 1;
            // 
            // chatBox
            // 
            this.chatBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(57)))), ((int)(((byte)(63)))));
            this.chatBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.chatBox.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chatBox.ForeColor = System.Drawing.Color.White;
            this.chatBox.FormattingEnabled = true;
            this.chatBox.ItemHeight = 45;
            this.chatBox.Location = new System.Drawing.Point(0, 0);
            this.chatBox.Name = "chatBox";
            this.chatBox.Size = new System.Drawing.Size(551, 450);
            this.chatBox.TabIndex = 0;
            this.chatBox.SelectedIndexChanged += new System.EventHandler(this.chatBox_SelectedIndexChanged);
            // 
            // bottomPanel
            // 
            this.bottomPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(49)))), ((int)(((byte)(54)))));
            this.bottomPanel.Controls.Add(this.emoji);
            this.bottomPanel.Controls.Add(this.inputBox);
            this.bottomPanel.Controls.Add(this.sendButton);
            this.bottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bottomPanel.Location = new System.Drawing.Point(0, 483);
            this.bottomPanel.Name = "bottomPanel";
            this.bottomPanel.Size = new System.Drawing.Size(554, 50);
            this.bottomPanel.TabIndex = 1;
            // 
            // emoji
            // 
            this.emoji.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.emoji.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("emoji.BackgroundImage")));
            this.emoji.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.emoji.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.emoji.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.emoji.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.emoji.Location = new System.Drawing.Point(401, 10);
            this.emoji.Name = "emoji";
            this.emoji.Size = new System.Drawing.Size(38, 34);
            this.emoji.TabIndex = 6;
            this.emoji.UseVisualStyleBackColor = true;
            this.emoji.Click += new System.EventHandler(this.emoji_Click);
            // 
            // inputBox
            // 
            this.inputBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.inputBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(68)))), ((int)(((byte)(75)))));
            this.inputBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.inputBox.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.inputBox.ForeColor = System.Drawing.Color.White;
            this.inputBox.Location = new System.Drawing.Point(10, 12);
            this.inputBox.Name = "inputBox";
            this.inputBox.Size = new System.Drawing.Size(369, 25);
            this.inputBox.TabIndex = 0;
            this.inputBox.Text = "";
            this.inputBox.TextChanged += new System.EventHandler(this.inputBox_TextChanged);
            // 
            // sendButton
            // 
            this.sendButton.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.sendButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(101)))), ((int)(((byte)(242)))));
            this.sendButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.sendButton.ForeColor = System.Drawing.Color.White;
            this.sendButton.Location = new System.Drawing.Point(458, 10);
            this.sendButton.Name = "sendButton";
            this.sendButton.Size = new System.Drawing.Size(70, 30);
            this.sendButton.TabIndex = 1;
            this.sendButton.Text = "Send";
            this.sendButton.UseVisualStyleBackColor = false;
            this.sendButton.Click += new System.EventHandler(this.SendButton_Click);
            // 
            // memberList
            // 
            this.memberList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(49)))), ((int)(((byte)(54)))));
            this.memberList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.memberList.Dock = System.Windows.Forms.DockStyle.Right;
            this.memberList.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.memberList.ForeColor = System.Drawing.Color.White;
            this.memberList.FormattingEnabled = true;
            this.memberList.ItemHeight = 17;
            this.memberList.Location = new System.Drawing.Point(784, 64);
            this.memberList.Name = "memberList";
            this.memberList.Size = new System.Drawing.Size(120, 533);
            this.memberList.TabIndex = 2;
            this.memberList.SelectedIndexChanged += new System.EventHandler(this.memberList_SelectedIndexChanged_1);
            // 
            // boton_agregar
            // 
            this.boton_agregar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(36)))), ((int)(((byte)(40)))));
            this.boton_agregar.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.boton_agregar.Location = new System.Drawing.Point(6, 107);
            this.boton_agregar.Name = "boton_agregar";
            this.boton_agregar.Size = new System.Drawing.Size(92, 25);
            this.boton_agregar.TabIndex = 3;
            this.boton_agregar.Text = "Agregar Usuario";
            this.boton_agregar.UseVisualStyleBackColor = false;
            this.boton_agregar.Click += new System.EventHandler(this.boton_agregar_Click);
            // 
            // Crear_Grupo
            // 
            this.Crear_Grupo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(36)))), ((int)(((byte)(40)))));
            this.Crear_Grupo.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.Crear_Grupo.Location = new System.Drawing.Point(6, 67);
            this.Crear_Grupo.Name = "Crear_Grupo";
            this.Crear_Grupo.Size = new System.Drawing.Size(92, 25);
            this.Crear_Grupo.TabIndex = 4;
            this.Crear_Grupo.Text = "Crear Grupo";
            this.Crear_Grupo.UseVisualStyleBackColor = false;
            this.Crear_Grupo.Click += new System.EventHandler(this.Crear_Grupo_Click);
            // 
            // groupList
            // 
            this.groupList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(49)))), ((int)(((byte)(54)))));
            this.groupList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.groupList.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.groupList.ForeColor = System.Drawing.Color.White;
            this.groupList.FormattingEnabled = true;
            this.groupList.ItemHeight = 17;
            this.groupList.Location = new System.Drawing.Point(664, 64);
            this.groupList.Name = "groupList";
            this.groupList.Size = new System.Drawing.Size(114, 272);
            this.groupList.TabIndex = 5;
            // 
            // miembrosGrupo
            // 
            this.miembrosGrupo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(49)))), ((int)(((byte)(54)))));
            this.miembrosGrupo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.miembrosGrupo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.miembrosGrupo.ForeColor = System.Drawing.Color.White;
            this.miembrosGrupo.FormattingEnabled = true;
            this.miembrosGrupo.ItemHeight = 17;
            this.miembrosGrupo.Location = new System.Drawing.Point(664, 342);
            this.miembrosGrupo.Name = "miembrosGrupo";
            this.miembrosGrupo.Size = new System.Drawing.Size(114, 255);
            this.miembrosGrupo.TabIndex = 6;
            // 
            // Form1
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(101)))), ((int)(((byte)(242)))));
            this.ClientSize = new System.Drawing.Size(907, 600);
            this.Controls.Add(this.miembrosGrupo);
            this.Controls.Add(this.groupList);
            this.Controls.Add(this.chatPanel);
            this.Controls.Add(this.memberList);
            this.Controls.Add(this.boton_agregar);
            this.Controls.Add(this.Crear_Grupo);
            this.Font = new System.Drawing.Font("Modern No. 20", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MinimumSize = new System.Drawing.Size(600, 400);
            this.Name = "Form1";
            this.Text = "Discord Style Chat";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.chatPanel.ResumeLayout(false);
            this.bottomPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Button emoji;
        private System.Windows.Forms.ListBox groupList;
        private System.Windows.Forms.ListBox miembrosGrupo;
    }
}