namespace Chat_ProyectoIG
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel sidebar;
        private System.Windows.Forms.Panel chatPanel;
        private System.Windows.Forms.ListBox chatBox;
        private System.Windows.Forms.Panel bottomPanel;
        private System.Windows.Forms.TextBox inputBox;
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
            this.sidebar = new System.Windows.Forms.Panel();
            this.chatPanel = new System.Windows.Forms.Panel();
            this.chatBox = new System.Windows.Forms.ListBox();
            this.bottomPanel = new System.Windows.Forms.Panel();
            this.emoji = new System.Windows.Forms.Button();
            this.inputBox = new System.Windows.Forms.TextBox();
            this.sendButton = new System.Windows.Forms.Button();
            this.memberList = new System.Windows.Forms.ListBox();
            this.boton_agregar = new System.Windows.Forms.Button();
            this.Crear_Grupo = new System.Windows.Forms.Button();
            this.chatPanel.SuspendLayout();
            this.bottomPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // sidebar
            // 
            this.sidebar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(34)))), ((int)(((byte)(37)))));
            this.sidebar.Dock = System.Windows.Forms.DockStyle.Left;
            this.sidebar.Location = new System.Drawing.Point(3, 64);
            this.sidebar.Name = "sidebar";
            this.sidebar.Size = new System.Drawing.Size(60, 533);
            this.sidebar.TabIndex = 0;
            // 
            // chatPanel
            // 
            this.chatPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(57)))), ((int)(((byte)(63)))));
            this.chatPanel.Controls.Add(this.chatBox);
            this.chatPanel.Controls.Add(this.bottomPanel);
            this.chatPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chatPanel.Location = new System.Drawing.Point(63, 64);
            this.chatPanel.Name = "chatPanel";
            this.chatPanel.Size = new System.Drawing.Size(634, 533);
            this.chatPanel.TabIndex = 1;
            // 
            // chatBox
            // 
            this.chatBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(57)))), ((int)(((byte)(63)))));
            this.chatBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.chatBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chatBox.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chatBox.ForeColor = System.Drawing.Color.White;
            this.chatBox.FormattingEnabled = true;
            this.chatBox.ItemHeight = 45;
            this.chatBox.Location = new System.Drawing.Point(0, 0);
            this.chatBox.Name = "chatBox";
            this.chatBox.Size = new System.Drawing.Size(634, 483);
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
            this.bottomPanel.Size = new System.Drawing.Size(634, 50);
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
            this.emoji.Location = new System.Drawing.Point(497, 10);
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
            this.inputBox.Size = new System.Drawing.Size(465, 25);
            this.inputBox.TabIndex = 0;
            this.inputBox.TextChanged += new System.EventHandler(this.inputBox_TextChanged);
            // 
            // sendButton
            // 
            this.sendButton.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.sendButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(101)))), ((int)(((byte)(242)))));
            this.sendButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.sendButton.ForeColor = System.Drawing.Color.White;
            this.sendButton.Location = new System.Drawing.Point(554, 10);
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
            this.memberList.Location = new System.Drawing.Point(697, 64);
            this.memberList.Name = "memberList";
            this.memberList.Size = new System.Drawing.Size(200, 533);
            this.memberList.TabIndex = 2;
            // 
            // boton_agregar
            // 
            this.boton_agregar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(36)))), ((int)(((byte)(40)))));
            this.boton_agregar.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.boton_agregar.Location = new System.Drawing.Point(70, 520);
            this.boton_agregar.Name = "boton_agregar";
            this.boton_agregar.Size = new System.Drawing.Size(120, 25);
            this.boton_agregar.TabIndex = 3;
            this.boton_agregar.Text = "Agregar Usuario";
            this.boton_agregar.UseVisualStyleBackColor = false;
            this.boton_agregar.Click += new System.EventHandler(this.boton_agregar_Click);
            // 
            // Crear_Grupo
            // 
            this.Crear_Grupo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(36)))), ((int)(((byte)(40)))));
            this.Crear_Grupo.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.Crear_Grupo.Location = new System.Drawing.Point(200, 520);
            this.Crear_Grupo.Name = "Crear_Grupo";
            this.Crear_Grupo.Size = new System.Drawing.Size(120, 25);
            this.Crear_Grupo.TabIndex = 4;
            this.Crear_Grupo.Text = "Crear Grupo";
            this.Crear_Grupo.UseVisualStyleBackColor = false;
            this.Crear_Grupo.Click += new System.EventHandler(this.Crear_Grupo_Click);
            // 
            // Form1
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(45)))), ((int)(((byte)(49)))));
            this.ClientSize = new System.Drawing.Size(900, 600);
            this.Controls.Add(this.chatPanel);
            this.Controls.Add(this.sidebar);
            this.Controls.Add(this.memberList);
            this.Controls.Add(this.boton_agregar);
            this.Controls.Add(this.Crear_Grupo);
            this.MinimumSize = new System.Drawing.Size(600, 400);
            this.Name = "Form1";
            this.Text = "Discord Style Chat";
            this.chatPanel.ResumeLayout(false);
            this.bottomPanel.ResumeLayout(false);
            this.bottomPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Button emoji;
    }
}
