namespace Chat_ProyectoIG
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.emoji = new System.Windows.Forms.Button();
            this.enviar = new System.Windows.Forms.Button();
            this.cajadetexto = new System.Windows.Forms.RichTextBox();
            this.textoMensaje = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // emoji
            // 
            this.emoji.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("emoji.BackgroundImage")));
            this.emoji.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.emoji.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.emoji.Location = new System.Drawing.Point(697, 341);
            this.emoji.Name = "emoji";
            this.emoji.Size = new System.Drawing.Size(38, 34);
            this.emoji.TabIndex = 6;
            this.emoji.UseVisualStyleBackColor = true;
            this.emoji.Click += new System.EventHandler(this.emoji_Click);
            // 
            // enviar
            // 
            this.enviar.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("enviar.BackgroundImage")));
            this.enviar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.enviar.Location = new System.Drawing.Point(741, 344);
            this.enviar.Name = "enviar";
            this.enviar.Size = new System.Drawing.Size(34, 31);
            this.enviar.TabIndex = 7;
            this.enviar.UseVisualStyleBackColor = true;
            this.enviar.Click += new System.EventHandler(this.enviar_Click);
            // 
            // cajadetexto
            // 
            this.cajadetexto.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.cajadetexto.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.cajadetexto.ForeColor = System.Drawing.SystemColors.WindowText;
            this.cajadetexto.Location = new System.Drawing.Point(132, 30);
            this.cajadetexto.Name = "cajadetexto";
            this.cajadetexto.ReadOnly = true;
            this.cajadetexto.Size = new System.Drawing.Size(603, 294);
            this.cajadetexto.TabIndex = 8;
            this.cajadetexto.Text = "";
            // 
            // textoMensaje
            // 
            this.textoMensaje.Location = new System.Drawing.Point(132, 341);
            this.textoMensaje.Name = "textoMensaje";
            this.textoMensaje.Size = new System.Drawing.Size(559, 34);
            this.textoMensaje.TabIndex = 9;
            this.textoMensaje.Text = "";
            this.textoMensaje.TextChanged += new System.EventHandler(this.textoMensaje_TextChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.textoMensaje);
            this.Controls.Add(this.cajadetexto);
            this.Controls.Add(this.enviar);
            this.Controls.Add(this.emoji);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button emoji;
        private System.Windows.Forms.Button enviar;
        private System.Windows.Forms.RichTextBox cajadetexto;
        private System.Windows.Forms.RichTextBox textoMensaje;
    }
}

