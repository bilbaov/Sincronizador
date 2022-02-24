<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ucBackupBaseDatos
    Inherits ucBaseSistema

    'UserControl reemplaza a Dispose para limpiar la lista de componentes.
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ucBackupBaseDatos))
        Me.Gpbx_DatosConexion = New System.Windows.Forms.GroupBox()
        Me.Bttn_BuscarServidores_SQL = New System.Windows.Forms.Button()
        Me.Cmbx_Servidor = New System.Windows.Forms.ComboBox()
        Me.Bttn_Conectar = New System.Windows.Forms.Button()
        Me.Gpbx_Autentica = New System.Windows.Forms.GroupBox()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Txtb_PassWord = New System.Windows.Forms.TextBox()
        Me.Lbl_InfoAuten_Password = New System.Windows.Forms.Label()
        Me.Txtb_Usuario = New System.Windows.Forms.TextBox()
        Me.Lbl_InfoAuten_Usuario = New System.Windows.Forms.Label()
        Me.Cmbx_Autentica = New System.Windows.Forms.ComboBox()
        Me.Lbl_InfoConex_Autentica = New System.Windows.Forms.Label()
        Me.Lbl_InfoConex_Servidor = New System.Windows.Forms.Label()
        Me.Gpbx_OrigenBakup = New System.Windows.Forms.GroupBox()
        Me.Chkb_BorraBaseDatos = New System.Windows.Forms.CheckBox()
        Me.Bttn_BackupDB = New System.Windows.Forms.Button()
        Me.Bttn_PathFile = New System.Windows.Forms.Button()
        Me.Txtb_PathFile = New System.Windows.Forms.TextBox()
        Me.Lbl_InfoOrigen_PathFile = New System.Windows.Forms.Label()
        Me.Gpbx_DatosConexion.SuspendLayout()
        Me.Gpbx_Autentica.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Gpbx_OrigenBakup.SuspendLayout()
        Me.SuspendLayout()
        '
        'Gpbx_DatosConexion
        '
        Me.Gpbx_DatosConexion.Controls.Add(Me.Bttn_BuscarServidores_SQL)
        Me.Gpbx_DatosConexion.Controls.Add(Me.Cmbx_Servidor)
        Me.Gpbx_DatosConexion.Controls.Add(Me.Bttn_Conectar)
        Me.Gpbx_DatosConexion.Controls.Add(Me.Gpbx_Autentica)
        Me.Gpbx_DatosConexion.Controls.Add(Me.Cmbx_Autentica)
        Me.Gpbx_DatosConexion.Controls.Add(Me.Lbl_InfoConex_Autentica)
        Me.Gpbx_DatosConexion.Controls.Add(Me.Lbl_InfoConex_Servidor)
        Me.Gpbx_DatosConexion.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Gpbx_DatosConexion.Location = New System.Drawing.Point(11, 4)
        Me.Gpbx_DatosConexion.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Gpbx_DatosConexion.Name = "Gpbx_DatosConexion"
        Me.Gpbx_DatosConexion.Padding = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Gpbx_DatosConexion.Size = New System.Drawing.Size(364, 252)
        Me.Gpbx_DatosConexion.TabIndex = 16
        Me.Gpbx_DatosConexion.TabStop = False
        Me.Gpbx_DatosConexion.Text = "Datos de Conexión: "
        '
        'Bttn_BuscarServidores_SQL
        '
        Me.Bttn_BuscarServidores_SQL.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Bttn_BuscarServidores_SQL.Location = New System.Drawing.Point(319, 22)
        Me.Bttn_BuscarServidores_SQL.Name = "Bttn_BuscarServidores_SQL"
        Me.Bttn_BuscarServidores_SQL.Size = New System.Drawing.Size(29, 26)
        Me.Bttn_BuscarServidores_SQL.TabIndex = 1
        Me.Bttn_BuscarServidores_SQL.Text = "..."
        Me.Bttn_BuscarServidores_SQL.UseVisualStyleBackColor = True
        '
        'Cmbx_Servidor
        '
        Me.Cmbx_Servidor.FormattingEnabled = True
        Me.Cmbx_Servidor.Location = New System.Drawing.Point(76, 23)
        Me.Cmbx_Servidor.Name = "Cmbx_Servidor"
        Me.Cmbx_Servidor.Size = New System.Drawing.Size(242, 24)
        Me.Cmbx_Servidor.TabIndex = 0
        '
        'Bttn_Conectar
        '
        Me.Bttn_Conectar.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Bttn_Conectar.Location = New System.Drawing.Point(16, 195)
        Me.Bttn_Conectar.Name = "Bttn_Conectar"
        Me.Bttn_Conectar.Size = New System.Drawing.Size(332, 36)
        Me.Bttn_Conectar.TabIndex = 5
        Me.Bttn_Conectar.Text = "---"
        Me.Bttn_Conectar.UseVisualStyleBackColor = True
        '
        'Gpbx_Autentica
        '
        Me.Gpbx_Autentica.Controls.Add(Me.PictureBox1)
        Me.Gpbx_Autentica.Controls.Add(Me.Txtb_PassWord)
        Me.Gpbx_Autentica.Controls.Add(Me.Lbl_InfoAuten_Password)
        Me.Gpbx_Autentica.Controls.Add(Me.Txtb_Usuario)
        Me.Gpbx_Autentica.Controls.Add(Me.Lbl_InfoAuten_Usuario)
        Me.Gpbx_Autentica.Enabled = False
        Me.Gpbx_Autentica.Location = New System.Drawing.Point(16, 83)
        Me.Gpbx_Autentica.Name = "Gpbx_Autentica"
        Me.Gpbx_Autentica.Size = New System.Drawing.Size(332, 95)
        Me.Gpbx_Autentica.TabIndex = 4
        Me.Gpbx_Autentica.TabStop = False
        Me.Gpbx_Autentica.Text = "Datos Autenticación: "
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(11, 18)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(32, 32)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.PictureBox1.TabIndex = 8
        Me.PictureBox1.TabStop = False
        '
        'Txtb_PassWord
        '
        Me.Txtb_PassWord.Location = New System.Drawing.Point(139, 54)
        Me.Txtb_PassWord.Name = "Txtb_PassWord"
        Me.Txtb_PassWord.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.Txtb_PassWord.Size = New System.Drawing.Size(178, 23)
        Me.Txtb_PassWord.TabIndex = 4
        Me.Txtb_PassWord.UseSystemPasswordChar = True
        '
        'Lbl_InfoAuten_Password
        '
        Me.Lbl_InfoAuten_Password.AutoSize = True
        Me.Lbl_InfoAuten_Password.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lbl_InfoAuten_Password.Location = New System.Drawing.Point(63, 57)
        Me.Lbl_InfoAuten_Password.Name = "Lbl_InfoAuten_Password"
        Me.Lbl_InfoAuten_Password.Size = New System.Drawing.Size(70, 16)
        Me.Lbl_InfoAuten_Password.TabIndex = 4
        Me.Lbl_InfoAuten_Password.Text = "PassWord:"
        '
        'Txtb_Usuario
        '
        Me.Txtb_Usuario.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.Txtb_Usuario.Location = New System.Drawing.Point(139, 22)
        Me.Txtb_Usuario.Name = "Txtb_Usuario"
        Me.Txtb_Usuario.Size = New System.Drawing.Size(178, 23)
        Me.Txtb_Usuario.TabIndex = 3
        '
        'Lbl_InfoAuten_Usuario
        '
        Me.Lbl_InfoAuten_Usuario.AutoSize = True
        Me.Lbl_InfoAuten_Usuario.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lbl_InfoAuten_Usuario.Location = New System.Drawing.Point(77, 25)
        Me.Lbl_InfoAuten_Usuario.Name = "Lbl_InfoAuten_Usuario"
        Me.Lbl_InfoAuten_Usuario.Size = New System.Drawing.Size(56, 16)
        Me.Lbl_InfoAuten_Usuario.TabIndex = 2
        Me.Lbl_InfoAuten_Usuario.Text = "Usuario:"
        '
        'Cmbx_Autentica
        '
        Me.Cmbx_Autentica.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmbx_Autentica.FormattingEnabled = True
        Me.Cmbx_Autentica.Items.AddRange(New Object() {"Windows.-", "SQL Server.-"})
        Me.Cmbx_Autentica.Location = New System.Drawing.Point(108, 53)
        Me.Cmbx_Autentica.Name = "Cmbx_Autentica"
        Me.Cmbx_Autentica.Size = New System.Drawing.Size(240, 24)
        Me.Cmbx_Autentica.TabIndex = 2
        '
        'Lbl_InfoConex_Autentica
        '
        Me.Lbl_InfoConex_Autentica.AutoSize = True
        Me.Lbl_InfoConex_Autentica.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lbl_InfoConex_Autentica.Location = New System.Drawing.Point(13, 56)
        Me.Lbl_InfoConex_Autentica.Name = "Lbl_InfoConex_Autentica"
        Me.Lbl_InfoConex_Autentica.Size = New System.Drawing.Size(89, 16)
        Me.Lbl_InfoConex_Autentica.TabIndex = 2
        Me.Lbl_InfoConex_Autentica.Text = "Autenticación:"
        '
        'Lbl_InfoConex_Servidor
        '
        Me.Lbl_InfoConex_Servidor.AutoSize = True
        Me.Lbl_InfoConex_Servidor.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lbl_InfoConex_Servidor.Location = New System.Drawing.Point(13, 26)
        Me.Lbl_InfoConex_Servidor.Name = "Lbl_InfoConex_Servidor"
        Me.Lbl_InfoConex_Servidor.Size = New System.Drawing.Size(61, 16)
        Me.Lbl_InfoConex_Servidor.TabIndex = 0
        Me.Lbl_InfoConex_Servidor.Text = "Servidor:"
        '
        'Gpbx_OrigenBakup
        '
        Me.Gpbx_OrigenBakup.Controls.Add(Me.Chkb_BorraBaseDatos)
        Me.Gpbx_OrigenBakup.Controls.Add(Me.Bttn_BackupDB)
        Me.Gpbx_OrigenBakup.Controls.Add(Me.Bttn_PathFile)
        Me.Gpbx_OrigenBakup.Controls.Add(Me.Txtb_PathFile)
        Me.Gpbx_OrigenBakup.Controls.Add(Me.Lbl_InfoOrigen_PathFile)
        Me.Gpbx_OrigenBakup.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Gpbx_OrigenBakup.Location = New System.Drawing.Point(11, 263)
        Me.Gpbx_OrigenBakup.Name = "Gpbx_OrigenBakup"
        Me.Gpbx_OrigenBakup.Size = New System.Drawing.Size(364, 213)
        Me.Gpbx_OrigenBakup.TabIndex = 15
        Me.Gpbx_OrigenBakup.TabStop = False
        Me.Gpbx_OrigenBakup.Text = "Destino del Backup:"
        '
        'Chkb_BorraBaseDatos
        '
        Me.Chkb_BorraBaseDatos.AutoSize = True
        Me.Chkb_BorraBaseDatos.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Chkb_BorraBaseDatos.Location = New System.Drawing.Point(16, 74)
        Me.Chkb_BorraBaseDatos.Name = "Chkb_BorraBaseDatos"
        Me.Chkb_BorraBaseDatos.Size = New System.Drawing.Size(310, 20)
        Me.Chkb_BorraBaseDatos.TabIndex = 61
        Me.Chkb_BorraBaseDatos.Text = "Eliminar Base en el proceso de Backup de Datos.-"
        Me.Chkb_BorraBaseDatos.UseVisualStyleBackColor = True
        '
        'Bttn_BackupDB
        '
        Me.Bttn_BackupDB.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Bttn_BackupDB.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Bttn_BackupDB.Location = New System.Drawing.Point(16, 100)
        Me.Bttn_BackupDB.Name = "Bttn_BackupDB"
        Me.Bttn_BackupDB.Size = New System.Drawing.Size(332, 36)
        Me.Bttn_BackupDB.TabIndex = 13
        Me.Bttn_BackupDB.Text = "Backup DB"
        Me.Bttn_BackupDB.UseVisualStyleBackColor = True
        '
        'Bttn_PathFile
        '
        Me.Bttn_PathFile.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Bttn_PathFile.Location = New System.Drawing.Point(318, 41)
        Me.Bttn_PathFile.Name = "Bttn_PathFile"
        Me.Bttn_PathFile.Size = New System.Drawing.Size(30, 25)
        Me.Bttn_PathFile.TabIndex = 8
        Me.Bttn_PathFile.Text = "..."
        Me.Bttn_PathFile.UseVisualStyleBackColor = True
        '
        'Txtb_PathFile
        '
        Me.Txtb_PathFile.Location = New System.Drawing.Point(16, 42)
        Me.Txtb_PathFile.Name = "Txtb_PathFile"
        Me.Txtb_PathFile.Size = New System.Drawing.Size(296, 23)
        Me.Txtb_PathFile.TabIndex = 7
        '
        'Lbl_InfoOrigen_PathFile
        '
        Me.Lbl_InfoOrigen_PathFile.AutoSize = True
        Me.Lbl_InfoOrigen_PathFile.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lbl_InfoOrigen_PathFile.Location = New System.Drawing.Point(6, 23)
        Me.Lbl_InfoOrigen_PathFile.Name = "Lbl_InfoOrigen_PathFile"
        Me.Lbl_InfoOrigen_PathFile.Size = New System.Drawing.Size(90, 16)
        Me.Lbl_InfoOrigen_PathFile.TabIndex = 4
        Me.Lbl_InfoOrigen_PathFile.Text = "Path Archivos:"
        '
        'ucBackupBaseDatos
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.Gpbx_DatosConexion)
        Me.Controls.Add(Me.Gpbx_OrigenBakup)
        Me.Name = "ucBackupBaseDatos"
        Me.Size = New System.Drawing.Size(946, 639)
        Me.Gpbx_DatosConexion.ResumeLayout(False)
        Me.Gpbx_DatosConexion.PerformLayout()
        Me.Gpbx_Autentica.ResumeLayout(False)
        Me.Gpbx_Autentica.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Gpbx_OrigenBakup.ResumeLayout(False)
        Me.Gpbx_OrigenBakup.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Gpbx_DatosConexion As GroupBox
    Friend WithEvents Bttn_BuscarServidores_SQL As Button
    Friend WithEvents Cmbx_Servidor As ComboBox
    Friend WithEvents Bttn_Conectar As Button
    Friend WithEvents Gpbx_Autentica As GroupBox
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents Txtb_PassWord As TextBox
    Friend WithEvents Lbl_InfoAuten_Password As Label
    Friend WithEvents Txtb_Usuario As TextBox
    Friend WithEvents Lbl_InfoAuten_Usuario As Label
    Friend WithEvents Cmbx_Autentica As ComboBox
    Friend WithEvents Lbl_InfoConex_Autentica As Label
    Friend WithEvents Lbl_InfoConex_Servidor As Label
    Friend WithEvents Gpbx_OrigenBakup As GroupBox
    Friend WithEvents Chkb_BorraBaseDatos As CheckBox
    Friend WithEvents Bttn_BackupDB As Button
    Friend WithEvents Bttn_PathFile As Button
    Friend WithEvents Txtb_PathFile As TextBox
    Friend WithEvents Lbl_InfoOrigen_PathFile As Label
End Class
