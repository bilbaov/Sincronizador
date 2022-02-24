<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class main_system
   Inherits System.Windows.Forms.Form

   'Form reemplaza a Dispose para limpiar la lista de componentes.
   <System.Diagnostics.DebuggerNonUserCode()>
   Protected Overrides Sub Dispose(ByVal disposing As Boolean)
      Try
         If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
         End If
      Finally
         MyBase.Dispose(disposing)
      End Try
   End Sub

   'Requerido por el Diseñador de Windows Forms
   Private components As System.ComponentModel.IContainer

   'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
   'Se puede modificar usando el Diseñador de Windows Forms.  
   'No lo modifique con el editor de código.
   <System.Diagnostics.DebuggerStepThrough()>
   Private Sub InitializeComponent()
      Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(main_system))
      Me.tlspBarraBottom = New System.Windows.Forms.ToolStrip()
      Me.tslPrincipal = New System.Windows.Forms.ToolStripLabel()
      Me.pnlContenedor = New System.Windows.Forms.Panel()
      Me.flpFormularios = New System.Windows.Forms.FlowLayoutPanel()
      Me.pnlMenuPrincipal = New System.Windows.Forms.Panel()
      Me.pnlAnalizadorQuerys = New System.Windows.Forms.Panel()
      Me.btnAnalizadorQuerys = New System.Windows.Forms.Button()
      Me.pnlSalidaSistema = New System.Windows.Forms.Panel()
      Me.btnSalidaSistema = New System.Windows.Forms.Button()
      Me.lbHerramientas = New System.Windows.Forms.Label()
      Me.pnlRestauracionDatos = New System.Windows.Forms.Panel()
      Me.pnlBackupDatos = New System.Windows.Forms.Panel()
      Me.btnRestauracionDatos = New System.Windows.Forms.Button()
      Me.btnBackupDatos = New System.Windows.Forms.Button()
      Me.pnlBarraTitulo = New System.Windows.Forms.Panel()
      Me.pcbxMinimizar = New System.Windows.Forms.PictureBox()
      Me.pcbxCerrar = New System.Windows.Forms.PictureBox()
      Me.lblTituloFormulario = New System.Windows.Forms.Label()
        Me.tlspBarraBottom.SuspendLayout()
        Me.pnlContenedor.SuspendLayout()
        Me.pnlMenuPrincipal.SuspendLayout()
        Me.pnlBarraTitulo.SuspendLayout()
        CType(Me.pcbxMinimizar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pcbxCerrar, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'tlspBarraBottom
        '
        Me.tlspBarraBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.tlspBarraBottom.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tslPrincipal})
        Me.tlspBarraBottom.Location = New System.Drawing.Point(0, 675)
        Me.tlspBarraBottom.Name = "tlspBarraBottom"
        Me.tlspBarraBottom.Size = New System.Drawing.Size(1152, 25)
        Me.tlspBarraBottom.TabIndex = 0
        '
        'tslPrincipal
        '
        Me.tslPrincipal.Name = "tslPrincipal"
        Me.tslPrincipal.Size = New System.Drawing.Size(87, 22)
        Me.tslPrincipal.Text = "ToolStripLabel1"
        '
        'pnlContenedor
        '
        Me.pnlContenedor.BackColor = System.Drawing.Color.LightSkyBlue
        Me.pnlContenedor.Controls.Add(Me.flpFormularios)
        Me.pnlContenedor.Controls.Add(Me.pnlMenuPrincipal)
        Me.pnlContenedor.Controls.Add(Me.pnlBarraTitulo)
        Me.pnlContenedor.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlContenedor.Location = New System.Drawing.Point(0, 0)
        Me.pnlContenedor.Name = "pnlContenedor"
        Me.pnlContenedor.Size = New System.Drawing.Size(1152, 675)
        Me.pnlContenedor.TabIndex = 1
        '
        'flpFormularios
        '
        Me.flpFormularios.BackColor = System.Drawing.Color.Silver
        Me.flpFormularios.Dock = System.Windows.Forms.DockStyle.Fill
        Me.flpFormularios.Location = New System.Drawing.Point(200, 32)
        Me.flpFormularios.Name = "flpFormularios"
        Me.flpFormularios.Size = New System.Drawing.Size(952, 643)
        Me.flpFormularios.TabIndex = 2
        '
        'pnlMenuPrincipal
        '
        Me.pnlMenuPrincipal.BackColor = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(54, Byte), Integer), CType(CType(75, Byte), Integer))
        Me.pnlMenuPrincipal.Controls.Add(Me.pnlAnalizadorQuerys)
        Me.pnlMenuPrincipal.Controls.Add(Me.btnAnalizadorQuerys)
        Me.pnlMenuPrincipal.Controls.Add(Me.pnlSalidaSistema)
        Me.pnlMenuPrincipal.Controls.Add(Me.btnSalidaSistema)
        Me.pnlMenuPrincipal.Controls.Add(Me.lbHerramientas)
        Me.pnlMenuPrincipal.Controls.Add(Me.pnlRestauracionDatos)
        Me.pnlMenuPrincipal.Controls.Add(Me.pnlBackupDatos)
        Me.pnlMenuPrincipal.Controls.Add(Me.btnRestauracionDatos)
        Me.pnlMenuPrincipal.Controls.Add(Me.btnBackupDatos)
        Me.pnlMenuPrincipal.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlMenuPrincipal.Location = New System.Drawing.Point(0, 32)
        Me.pnlMenuPrincipal.Name = "pnlMenuPrincipal"
        Me.pnlMenuPrincipal.Size = New System.Drawing.Size(200, 643)
        Me.pnlMenuPrincipal.TabIndex = 1
        '
        'pnlAnalizadorQuerys
        '
        Me.pnlAnalizadorQuerys.BackColor = System.Drawing.Color.FromArgb(CType(CType(13, Byte), Integer), CType(CType(93, Byte), Integer), CType(CType(142, Byte), Integer))
        Me.pnlAnalizadorQuerys.Location = New System.Drawing.Point(0, 135)
        Me.pnlAnalizadorQuerys.Name = "pnlAnalizadorQuerys"
        Me.pnlAnalizadorQuerys.Size = New System.Drawing.Size(5, 40)
        Me.pnlAnalizadorQuerys.TabIndex = 18
        '
        'btnAnalizadorQuerys
        '
        Me.btnAnalizadorQuerys.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnAnalizadorQuerys.Enabled = False
        Me.btnAnalizadorQuerys.FlatAppearance.BorderSize = 0
        Me.btnAnalizadorQuerys.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(50, Byte), Integer), CType(CType(70, Byte), Integer))
        Me.btnAnalizadorQuerys.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(61, Byte), Integer), CType(CType(92, Byte), Integer))
        Me.btnAnalizadorQuerys.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAnalizadorQuerys.ForeColor = System.Drawing.Color.Gainsboro
        Me.btnAnalizadorQuerys.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnAnalizadorQuerys.Location = New System.Drawing.Point(5, 135)
        Me.btnAnalizadorQuerys.Name = "btnAnalizadorQuerys"
        Me.btnAnalizadorQuerys.Size = New System.Drawing.Size(195, 40)
        Me.btnAnalizadorQuerys.TabIndex = 17
        Me.btnAnalizadorQuerys.Text = "Analizador"
        Me.btnAnalizadorQuerys.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnAnalizadorQuerys.UseVisualStyleBackColor = True
        '
        'pnlSalidaSistema
        '
        Me.pnlSalidaSistema.BackColor = System.Drawing.Color.FromArgb(CType(CType(13, Byte), Integer), CType(CType(93, Byte), Integer), CType(CType(142, Byte), Integer))
        Me.pnlSalidaSistema.Location = New System.Drawing.Point(0, 600)
        Me.pnlSalidaSistema.Name = "pnlSalidaSistema"
        Me.pnlSalidaSistema.Size = New System.Drawing.Size(5, 40)
        Me.pnlSalidaSistema.TabIndex = 15
        '
        'btnSalidaSistema
        '
        Me.btnSalidaSistema.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnSalidaSistema.FlatAppearance.BorderSize = 0
        Me.btnSalidaSistema.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(50, Byte), Integer), CType(CType(70, Byte), Integer))
        Me.btnSalidaSistema.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(61, Byte), Integer), CType(CType(92, Byte), Integer))
        Me.btnSalidaSistema.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSalidaSistema.ForeColor = System.Drawing.Color.Gainsboro
        Me.btnSalidaSistema.Location = New System.Drawing.Point(5, 600)
        Me.btnSalidaSistema.Name = "btnSalidaSistema"
        Me.btnSalidaSistema.Size = New System.Drawing.Size(195, 40)
        Me.btnSalidaSistema.TabIndex = 11
        Me.btnSalidaSistema.Text = "Salida del Sistema.-"
        Me.btnSalidaSistema.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSalidaSistema.UseVisualStyleBackColor = True
        '
        'lbHerramientas
        '
        Me.lbHerramientas.AutoSize = True
        Me.lbHerramientas.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbHerramientas.ForeColor = System.Drawing.SystemColors.ButtonFace
        Me.lbHerramientas.Location = New System.Drawing.Point(12, 12)
        Me.lbHerramientas.Name = "lbHerramientas"
        Me.lbHerramientas.Size = New System.Drawing.Size(169, 14)
        Me.lbHerramientas.TabIndex = 8
        Me.lbHerramientas.Text = "Herramientas de Datos.-"
        '
        'pnlRestauracionDatos
        '
        Me.pnlRestauracionDatos.BackColor = System.Drawing.Color.FromArgb(CType(CType(13, Byte), Integer), CType(CType(93, Byte), Integer), CType(CType(142, Byte), Integer))
        Me.pnlRestauracionDatos.Location = New System.Drawing.Point(0, 89)
        Me.pnlRestauracionDatos.Name = "pnlRestauracionDatos"
        Me.pnlRestauracionDatos.Size = New System.Drawing.Size(5, 40)
        Me.pnlRestauracionDatos.TabIndex = 7
        '
        'pnlBackupDatos
        '
        Me.pnlBackupDatos.BackColor = System.Drawing.Color.FromArgb(CType(CType(13, Byte), Integer), CType(CType(93, Byte), Integer), CType(CType(142, Byte), Integer))
        Me.pnlBackupDatos.Location = New System.Drawing.Point(0, 43)
        Me.pnlBackupDatos.Name = "pnlBackupDatos"
        Me.pnlBackupDatos.Size = New System.Drawing.Size(5, 40)
        Me.pnlBackupDatos.TabIndex = 6
        '
        'btnRestauracionDatos
        '
        Me.btnRestauracionDatos.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnRestauracionDatos.FlatAppearance.BorderSize = 0
        Me.btnRestauracionDatos.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(50, Byte), Integer), CType(CType(70, Byte), Integer))
        Me.btnRestauracionDatos.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(61, Byte), Integer), CType(CType(92, Byte), Integer))
        Me.btnRestauracionDatos.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRestauracionDatos.ForeColor = System.Drawing.Color.Gainsboro
        Me.btnRestauracionDatos.Location = New System.Drawing.Point(5, 89)
        Me.btnRestauracionDatos.Name = "btnRestauracionDatos"
        Me.btnRestauracionDatos.Size = New System.Drawing.Size(195, 40)
        Me.btnRestauracionDatos.TabIndex = 3
        Me.btnRestauracionDatos.Text = "Restauración de Datos.-"
        Me.btnRestauracionDatos.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnRestauracionDatos.UseVisualStyleBackColor = True
        '
        'btnBackupDatos
        '
        Me.btnBackupDatos.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnBackupDatos.FlatAppearance.BorderSize = 0
        Me.btnBackupDatos.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(50, Byte), Integer), CType(CType(70, Byte), Integer))
        Me.btnBackupDatos.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(61, Byte), Integer), CType(CType(92, Byte), Integer))
        Me.btnBackupDatos.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBackupDatos.ForeColor = System.Drawing.Color.Gainsboro
        Me.btnBackupDatos.Location = New System.Drawing.Point(5, 43)
        Me.btnBackupDatos.Name = "btnBackupDatos"
        Me.btnBackupDatos.Size = New System.Drawing.Size(195, 40)
        Me.btnBackupDatos.TabIndex = 2
        Me.btnBackupDatos.Text = "Backup de Datos.-"
        Me.btnBackupDatos.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnBackupDatos.UseVisualStyleBackColor = True
        '
        'pnlBarraTitulo
        '
        Me.pnlBarraTitulo.BackColor = System.Drawing.Color.FromArgb(CType(CType(13, Byte), Integer), CType(CType(93, Byte), Integer), CType(CType(142, Byte), Integer))
        Me.pnlBarraTitulo.Controls.Add(Me.pcbxMinimizar)
        Me.pnlBarraTitulo.Controls.Add(Me.pcbxCerrar)
        Me.pnlBarraTitulo.Controls.Add(Me.lblTituloFormulario)
        Me.pnlBarraTitulo.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlBarraTitulo.Location = New System.Drawing.Point(0, 0)
        Me.pnlBarraTitulo.Name = "pnlBarraTitulo"
        Me.pnlBarraTitulo.Size = New System.Drawing.Size(1152, 32)
        Me.pnlBarraTitulo.TabIndex = 0
        '
        'pcbxMinimizar
        '
        Me.pcbxMinimizar.Cursor = System.Windows.Forms.Cursors.Hand
        Me.pcbxMinimizar.ErrorImage = Nothing
        Me.pcbxMinimizar.Image = Global.My.Resources.Resources._001_01
        Me.pcbxMinimizar.InitialImage = Nothing
        Me.pcbxMinimizar.Location = New System.Drawing.Point(1091, 4)
        Me.pcbxMinimizar.Name = "pcbxMinimizar"
        Me.pcbxMinimizar.Size = New System.Drawing.Size(24, 24)
        Me.pcbxMinimizar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.pcbxMinimizar.TabIndex = 3
        Me.pcbxMinimizar.TabStop = False
        '
        'pcbxCerrar
        '
        Me.pcbxCerrar.Cursor = System.Windows.Forms.Cursors.Hand
        Me.pcbxCerrar.ErrorImage = Nothing
        Me.pcbxCerrar.Image = CType(resources.GetObject("pcbxCerrar.Image"), System.Drawing.Image)
        Me.pcbxCerrar.InitialImage = Nothing
        Me.pcbxCerrar.Location = New System.Drawing.Point(1121, 4)
        Me.pcbxCerrar.Name = "pcbxCerrar"
        Me.pcbxCerrar.Size = New System.Drawing.Size(24, 24)
        Me.pcbxCerrar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.pcbxCerrar.TabIndex = 1
        Me.pcbxCerrar.TabStop = False
        '
        'lblTituloFormulario
        '
        Me.lblTituloFormulario.AutoSize = True
        Me.lblTituloFormulario.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTituloFormulario.ForeColor = System.Drawing.SystemColors.Control
        Me.lblTituloFormulario.Location = New System.Drawing.Point(7, 8)
        Me.lblTituloFormulario.Name = "lblTituloFormulario"
        Me.lblTituloFormulario.Size = New System.Drawing.Size(228, 14)
        Me.lblTituloFormulario.TabIndex = 0
        Me.lblTituloFormulario.Text = "Formulario Principal Syncro Pro.-"
        '
        'main_system
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.ClientSize = New System.Drawing.Size(1152, 700)
        Me.Controls.Add(Me.pnlContenedor)
        Me.Controls.Add(Me.tlspBarraBottom)
        Me.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "main_system"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "menu_principal"
        Me.tlspBarraBottom.ResumeLayout(False)
        Me.tlspBarraBottom.PerformLayout()
        Me.pnlContenedor.ResumeLayout(False)
        Me.pnlMenuPrincipal.ResumeLayout(False)
        Me.pnlMenuPrincipal.PerformLayout()
        Me.pnlBarraTitulo.ResumeLayout(False)
        Me.pnlBarraTitulo.PerformLayout()
        CType(Me.pcbxMinimizar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pcbxCerrar, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents tlspBarraBottom As ToolStrip
   Friend WithEvents pnlContenedor As Panel
   Friend WithEvents pnlBarraTitulo As Panel
   Friend WithEvents lblTituloFormulario As Label
   Friend WithEvents pnlMenuPrincipal As Panel
   Friend WithEvents pcbxMinimizar As PictureBox
   Friend WithEvents pcbxCerrar As PictureBox
   Friend WithEvents btnRestauracionDatos As Button
   Friend WithEvents btnBackupDatos As Button
   Friend WithEvents pnlRestauracionDatos As Panel
   Friend WithEvents pnlBackupDatos As Panel
   Friend WithEvents pnlSalidaSistema As Panel
   Friend WithEvents btnSalidaSistema As Button
   Friend WithEvents lbHerramientas As Label
   Friend WithEvents tslPrincipal As ToolStripLabel
   Friend WithEvents pnlAnalizadorQuerys As Panel
   Friend WithEvents btnAnalizadorQuerys As Button
   Friend WithEvents flpFormularios As FlowLayoutPanel
End Class
