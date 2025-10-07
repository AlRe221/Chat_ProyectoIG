namespace Chat_ProyectoIG
{
    partial class Form2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
            this.registrar = new System.Windows.Forms.Button();
            this.Nombre = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.PasswordConfir = new System.Windows.Forms.TextBox();
            this.Pasword = new System.Windows.Forms.TextBox();
            this.Usuario = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // registrar
            // 
            this.registrar.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.registrar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(101)))), ((int)(((byte)(242)))));
            this.registrar.Font = new System.Drawing.Font("Modern No. 20", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.registrar.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.registrar.Location = new System.Drawing.Point(181, 287);
            this.registrar.Name = "registrar";
            this.registrar.Size = new System.Drawing.Size(106, 33);
            this.registrar.TabIndex = 17;
            this.registrar.Text = "Registrar";
            this.registrar.UseVisualStyleBackColor = false;
            this.registrar.Click += new System.EventHandler(this.registrar_Click_1);
            // 
            // Nombre
            // 
            this.Nombre.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Nombre.BackColor = System.Drawing.Color.White;
            this.Nombre.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Nombre.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Nombre.ForeColor = System.Drawing.SystemColors.MenuText;
            this.Nombre.Location = new System.Drawing.Point(252, 234);
            this.Nombre.Name = "Nombre";
            this.Nombre.Size = new System.Drawing.Size(163, 26);
            this.Nombre.TabIndex = 16;
            this.Nombre.TextChanged += new System.EventHandler(this.Nombre_TextChanged);
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Modern No. 20", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label4.Location = new System.Drawing.Point(27, 224);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 24);
            this.label4.TabIndex = 15;
            this.label4.Text = "Nombre:";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // PasswordConfir
            // 
            this.PasswordConfir.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.PasswordConfir.BackColor = System.Drawing.Color.White;
            this.PasswordConfir.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PasswordConfir.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PasswordConfir.ForeColor = System.Drawing.SystemColors.MenuText;
            this.PasswordConfir.Location = new System.Drawing.Point(252, 188);
            this.PasswordConfir.Name = "PasswordConfir";
            this.PasswordConfir.PasswordChar = '*';
            this.PasswordConfir.Size = new System.Drawing.Size(163, 26);
            this.PasswordConfir.TabIndex = 14;
            this.PasswordConfir.TextChanged += new System.EventHandler(this.PasswordConfir_TextChanged);
            // 
            // Pasword
            // 
            this.Pasword.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Pasword.BackColor = System.Drawing.Color.White;
            this.Pasword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Pasword.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Pasword.ForeColor = System.Drawing.SystemColors.MenuText;
            this.Pasword.Location = new System.Drawing.Point(252, 150);
            this.Pasword.Name = "Pasword";
            this.Pasword.PasswordChar = '*';
            this.Pasword.Size = new System.Drawing.Size(163, 26);
            this.Pasword.TabIndex = 13;
            this.Pasword.TextChanged += new System.EventHandler(this.Pasword_TextChanged);
            // 
            // Usuario
            // 
            this.Usuario.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Usuario.BackColor = System.Drawing.Color.White;
            this.Usuario.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Usuario.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Usuario.ForeColor = System.Drawing.SystemColors.MenuText;
            this.Usuario.Location = new System.Drawing.Point(252, 115);
            this.Usuario.Name = "Usuario";
            this.Usuario.Size = new System.Drawing.Size(163, 26);
            this.Usuario.TabIndex = 12;
            this.Usuario.TextChanged += new System.EventHandler(this.Usuario_TextChanged_1);
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Modern No. 20", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label3.Location = new System.Drawing.Point(27, 188);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(208, 24);
            this.label3.TabIndex = 11;
            this.label3.Text = "Confirma contraseña:";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Modern No. 20", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label2.Location = new System.Drawing.Point(27, 152);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(117, 24);
            this.label2.TabIndex = 10;
            this.label2.Text = "Contraseña:";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Modern No. 20", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label1.Location = new System.Drawing.Point(27, 117);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(134, 24);
            this.label1.TabIndex = 9;
            this.label1.Text = "Crea usuario:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 450);
            this.panel1.TabIndex = 18;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(98)))), ((int)(((byte)(108)))), ((int)(((byte)(200)))));
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.PasswordConfir);
            this.panel2.Controls.Add(this.Pasword);
            this.panel2.Controls.Add(this.registrar);
            this.panel2.Controls.Add(this.Usuario);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.Nombre);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Location = new System.Drawing.Point(33, 12);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(731, 418);
            this.panel2.TabIndex = 18;
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel3.BackgroundImage")));
            this.panel3.Location = new System.Drawing.Point(439, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(320, 417);
            this.panel3.TabIndex = 21;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(45)))), ((int)(((byte)(49)))));
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel1);
            this.Name = "Form2";
            this.Text = "Login";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button registrar;
        private System.Windows.Forms.TextBox Nombre;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox PasswordConfir;
        private System.Windows.Forms.TextBox Pasword;
        private System.Windows.Forms.TextBox Usuario;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
    }
}