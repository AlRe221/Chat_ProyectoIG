using System.Drawing;

namespace Chat_ProyectoIG
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel chatPanel;
        private System.Windows.Forms.ListBox chatBox;
        private System.Windows.Forms.Panel bottomPanel;
        private System.Windows.Forms.RichTextBox inputBox;
        private MaterialSkin.Controls.MaterialButton sendButton;
        private System.Windows.Forms.ListBox memberList;
        private MaterialSkin.Controls.MaterialButton boton_agregar;
        private MaterialSkin.Controls.MaterialButton Crear_Grupo;
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
            this.sendButton = new MaterialSkin.Controls.MaterialButton();
            this.memberList = new System.Windows.Forms.ListBox();
            this.boton_agregar = new MaterialSkin.Controls.MaterialButton();
            this.Crear_Grupo = new MaterialSkin.Controls.MaterialButton();
            this.groupList = new System.Windows.Forms.ListBox();
            this.miembrosGrupo = new System.Windows.Forms.ListBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.chatPanel.SuspendLayout();
            this.bottomPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // chatPanel
            // 
            this.chatPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chatPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(57)))), ((int)(((byte)(63)))));
            this.chatPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.chatPanel.Controls.Add(this.chatBox);
            this.chatPanel.Controls.Add(this.bottomPanel);
            this.chatPanel.Location = new System.Drawing.Point(174, 64);
            this.chatPanel.Name = "chatPanel";
            this.chatPanel.Size = new System.Drawing.Size(501, 533);
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
            this.chatBox.Size = new System.Drawing.Size(499, 456);
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
            this.bottomPanel.Location = new System.Drawing.Point(0, 456);
            this.bottomPanel.Name = "bottomPanel";
            this.bottomPanel.Size = new System.Drawing.Size(499, 75);
            this.bottomPanel.TabIndex = 1;
            // 
            // emoji
            // 
            this.emoji.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.emoji.Font = new Font("Segoe UI Emoji",400,FontStyle.Bold);
            this.emoji.Text = ":)";
            this.emoji.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.emoji.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.emoji.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.emoji.Location = new System.Drawing.Point(346, 22);
            this.emoji.Name = "emoji";
            this.emoji.Size = new System.Drawing.Size(38, 34);
            this.emoji.TabIndex = 6;
            this.emoji.UseVisualStyleBackColor = true;
            this.emoji.Click += new System.EventHandler(this.emoji_Click);
            // 
            // inputBox
            // 
            this.inputBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.inputBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(68)))), ((int)(((byte)(75)))));
            this.inputBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.inputBox.Font = new System.Drawing.Font("Segoe UI Emoji", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.inputBox.ForeColor = System.Drawing.Color.White;
            this.inputBox.Location = new System.Drawing.Point(14, 25);
            this.inputBox.Name = "inputBox";
            this.inputBox.Size = new System.Drawing.Size(314, 25);
            this.inputBox.TabIndex = 0;
            this.inputBox.Text = "";
            this.inputBox.TextChanged += new System.EventHandler(this.inputBox_TextChanged);
            // 
            // sendButton
            // 
            this.sendButton.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.sendButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.sendButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(101)))), ((int)(((byte)(242)))));
            this.sendButton.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.sendButton.Depth = 0;
            this.sendButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.sendButton.ForeColor = System.Drawing.Color.White;
            this.sendButton.HighEmphasis = true;
            this.sendButton.Icon = null;
            this.sendButton.Location = new System.Drawing.Point(408, 21);
            this.sendButton.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.sendButton.MouseState = MaterialSkin.MouseState.HOVER;
            this.sendButton.Name = "sendButton";
            this.sendButton.NoAccentTextColor = System.Drawing.Color.Empty;
            this.sendButton.Size = new System.Drawing.Size(64, 36);
            this.sendButton.TabIndex = 1;
            this.sendButton.Text = "Send";
            this.sendButton.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.sendButton.UseAccentColor = false;
            this.sendButton.UseVisualStyleBackColor = false;
            this.sendButton.Click += new System.EventHandler(this.SendButton_Click);
            // 
            // memberList
            // 
            this.memberList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(49)))), ((int)(((byte)(54)))));
            this.memberList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.memberList.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.memberList.ForeColor = System.Drawing.Color.White;
            this.memberList.FormattingEnabled = true;
            this.memberList.ItemHeight = 17;
            this.memberList.Location = new System.Drawing.Point(117, 37);
            this.memberList.Name = "memberList";
            this.memberList.Size = new System.Drawing.Size(100, 493);
            this.memberList.TabIndex = 2;
            this.memberList.SelectedIndexChanged += new System.EventHandler(this.memberList_SelectedIndexChanged_1);
            // 
            // boton_agregar
            // 
            this.boton_agregar.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.boton_agregar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(36)))), ((int)(((byte)(40)))));
            this.boton_agregar.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.boton_agregar.Depth = 0;
            this.boton_agregar.Font = new System.Drawing.Font("Modern No. 20", 8.249999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.boton_agregar.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.boton_agregar.HighEmphasis = true;
            this.boton_agregar.Icon = null;
            this.boton_agregar.Location = new System.Drawing.Point(3, 89);
            this.boton_agregar.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.boton_agregar.MouseState = MaterialSkin.MouseState.HOVER;
            this.boton_agregar.Name = "boton_agregar";
            this.boton_agregar.NoAccentTextColor = System.Drawing.Color.Empty;
            this.boton_agregar.Size = new System.Drawing.Size(153, 36);
            this.boton_agregar.TabIndex = 3;
            this.boton_agregar.Text = "Agregar Usuario";
            this.boton_agregar.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.boton_agregar.UseAccentColor = false;
            this.boton_agregar.UseVisualStyleBackColor = false;
            this.boton_agregar.Click += new System.EventHandler(this.boton_agregar_Click);
            // 
            // Crear_Grupo
            // 
            this.Crear_Grupo.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Crear_Grupo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(36)))), ((int)(((byte)(40)))));
            this.Crear_Grupo.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.Crear_Grupo.Depth = 0;
            this.Crear_Grupo.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.Crear_Grupo.HighEmphasis = true;
            this.Crear_Grupo.Icon = null;
            this.Crear_Grupo.Location = new System.Drawing.Point(17, 31);
            this.Crear_Grupo.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.Crear_Grupo.MouseState = MaterialSkin.MouseState.HOVER;
            this.Crear_Grupo.Name = "Crear_Grupo";
            this.Crear_Grupo.NoAccentTextColor = System.Drawing.Color.Empty;
            this.Crear_Grupo.Size = new System.Drawing.Size(119, 36);
            this.Crear_Grupo.TabIndex = 4;
            this.Crear_Grupo.Text = "Crear Grupo";
            this.Crear_Grupo.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.Crear_Grupo.UseAccentColor = false;
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
            this.groupList.Location = new System.Drawing.Point(-3, 36);
            this.groupList.Name = "groupList";
            this.groupList.Size = new System.Drawing.Size(114, 238);
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
            this.miembrosGrupo.Location = new System.Drawing.Point(0, 309);
            this.miembrosGrupo.Name = "miembrosGrupo";
            this.miembrosGrupo.Size = new System.Drawing.Size(114, 221);
            this.miembrosGrupo.TabIndex = 6;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(57)))), ((int)(((byte)(63)))));
            this.panel1.Controls.Add(this.Crear_Grupo);
            this.panel1.Controls.Add(this.boton_agregar);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(3, 64);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(165, 533);
            this.panel1.TabIndex = 7;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.memberList);
            this.panel2.Controls.Add(this.groupList);
            this.panel2.Controls.Add(this.miembrosGrupo);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(681, 64);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(223, 533);
            this.panel2.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Modern No. 20", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label3.Location = new System.Drawing.Point(3, 285);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 18);
            this.label3.TabIndex = 9;
            this.label3.Text = "miembros ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Modern No. 20", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label2.Location = new System.Drawing.Point(122, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 18);
            this.label2.TabIndex = 8;
            this.label2.Text = "Usuarios";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Modern No. 20", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label1.Location = new System.Drawing.Point(3, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 18);
            this.label1.TabIndex = 7;
            this.label1.Text = "Lista grupos";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // Form1
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(101)))), ((int)(((byte)(242)))));
            this.ClientSize = new System.Drawing.Size(907, 600);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.chatPanel);
            this.Font = new System.Drawing.Font("Modern No. 20", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MinimumSize = new System.Drawing.Size(600, 400);
            this.Name = "Form1";
            this.Text = "Discord Style Chat";
            this.chatPanel.ResumeLayout(false);
            this.bottomPanel.ResumeLayout(false);
            this.bottomPanel.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Button emoji;
        private System.Windows.Forms.ListBox groupList;
        private System.Windows.Forms.ListBox miembrosGrupo;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
    }
}