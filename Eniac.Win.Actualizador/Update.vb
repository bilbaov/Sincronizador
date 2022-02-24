#Region "Imports"

Imports System
Imports System.Drawing
Imports System.Collections
Imports System.Windows.Forms
Imports System.Runtime.InteropServices
Imports System.IO
Imports System.Net
Imports System.Net.Sockets
Imports System.Threading
Imports System.Reflection
Imports WSUpdater
Imports System.Xml
Imports System.Text
Imports Eniac.Ayudantes.Actualizador.Common

Imports System.Diagnostics
Imports System.ComponentModel

Imports Eniac.Ayudantes.Functions

#End Region

Namespace Eniac.Ayudantes.Actualizador

   Public Class frmUpdate
      Inherits System.Windows.Forms.Form

#Region "Campos"

      Private pbProgress As System.Windows.Forms.ProgressBar
      Private WithEvents btnUpdate As System.Windows.Forms.Button

#End Region

#Region "Private Data"

      ' Form size
      Private realSize As Size
      ' Http request/response
      Private m_req As HttpWebRequest
      Private m_resp As HttpWebResponse
      Private m_fs As FileStream

      ' Data buffer for stream operations
      Private dataBuffer() As Byte
      Private Const DataBlockSize As Integer = 65536

      ' Configuration
      'Private xmlConfig As XmlDocument
      Private Shared _consultaUrl As String
      Private m_Status As String
      Private UpdateUrl As String
      Private pbVal, maxVal As Integer
      Private _URLCorrecta As String

      Private FileName As String
      Private nodeUpdate As XmlElement
      Private info As UpdateInfo

      Public Shared pathSetup As String

      'prms
      Private Shared appToUpdate As String
      Private Shared moduleToUpdate As String
      Private Shared pathModule As String
      Private Shared _downloadMode As String
      Private Shared AppToRun As String

      Private Shared configurationAppSettings As System.Configuration.AppSettingsReader

      Private ModuleName As String
      Private Version As String
      Friend WithEvents DataGridTableStyle1 As System.Windows.Forms.DataGridTableStyle
      Friend WithEvents txtDsc As System.Windows.Forms.DataGridTextBoxColumn
      Friend WithEvents txtAplic As System.Windows.Forms.DataGridTextBoxColumn
      Friend WithEvents txtFecha As System.Windows.Forms.DataGridTextBoxColumn
      Friend WithEvents txtVersion As System.Windows.Forms.DataGridTextBoxColumn
      Friend WithEvents txtReq As System.Windows.Forms.DataGridTextBoxColumn
      Friend WithEvents dgvDatos As System.Windows.Forms.DataGridView
      Friend WithEvents colDate As System.Windows.Forms.DataGridViewTextBoxColumn
      Friend WithEvents colName As System.Windows.Forms.DataGridViewTextBoxColumn
      Friend WithEvents colVersion As System.Windows.Forms.DataGridViewTextBoxColumn
      Friend WithEvents colsubject As System.Windows.Forms.DataGridViewTextBoxColumn
      Friend WithEvents colDSC As System.Windows.Forms.DataGridViewTextBoxColumn
      Private dt As DataTable

#End Region

#Region "Construction/Destruction"

      '  Constructor
      Public Sub New()
         InitializeComponent()
      End Sub 'New

      ' Clean up any resources being used.
      Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
         MyBase.Dispose(disposing)
      End Sub 'Dispose

#End Region

#Region "Windows Form Designer generated code"

      ' <summary>
      ' Required method for Designer support - do not modify
      ' the contents of this method with the code editor.
      ' </summary>
      Friend WithEvents btnSalir As System.Windows.Forms.Button
      Friend WithEvents imgSmallIconos As System.Windows.Forms.ImageList
      Private components As System.ComponentModel.IContainer
      Friend WithEvents lblNuevaVersion As System.Windows.Forms.Label
      Friend WithEvents lblEmpresa As System.Windows.Forms.Label
      Friend WithEvents lblURLServidor As System.Windows.Forms.Label
      Friend WithEvents lblApp As System.Windows.Forms.Label
      Friend WithEvents chkDetalle As System.Windows.Forms.CheckBox
      Friend WithEvents lblProgreso As System.Windows.Forms.Label
      Friend WithEvents dsUpgrades As System.Data.DataSet
      Private Sub InitializeComponent()
         Me.components = New System.ComponentModel.Container
         Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmUpdate))
         Me.pbProgress = New System.Windows.Forms.ProgressBar
         Me.btnUpdate = New System.Windows.Forms.Button
         Me.imgSmallIconos = New System.Windows.Forms.ImageList(Me.components)
         Me.lblNuevaVersion = New System.Windows.Forms.Label
         Me.btnSalir = New System.Windows.Forms.Button
         Me.lblProgreso = New System.Windows.Forms.Label
         Me.lblEmpresa = New System.Windows.Forms.Label
         Me.lblApp = New System.Windows.Forms.Label
         Me.lblURLServidor = New System.Windows.Forms.Label
         Me.chkDetalle = New System.Windows.Forms.CheckBox
         Me.dsUpgrades = New System.Data.DataSet
         Me.DataGridTableStyle1 = New System.Windows.Forms.DataGridTableStyle
         Me.txtFecha = New System.Windows.Forms.DataGridTextBoxColumn
         Me.txtAplic = New System.Windows.Forms.DataGridTextBoxColumn
         Me.txtVersion = New System.Windows.Forms.DataGridTextBoxColumn
         Me.txtReq = New System.Windows.Forms.DataGridTextBoxColumn
         Me.txtDsc = New System.Windows.Forms.DataGridTextBoxColumn
         Me.dgvDatos = New System.Windows.Forms.DataGridView
         Me.colDate = New System.Windows.Forms.DataGridViewTextBoxColumn
         Me.colName = New System.Windows.Forms.DataGridViewTextBoxColumn
         Me.colVersion = New System.Windows.Forms.DataGridViewTextBoxColumn
         Me.colsubject = New System.Windows.Forms.DataGridViewTextBoxColumn
         Me.colDSC = New System.Windows.Forms.DataGridViewTextBoxColumn
         CType(Me.dsUpgrades, System.ComponentModel.ISupportInitialize).BeginInit()
         CType(Me.dgvDatos, System.ComponentModel.ISupportInitialize).BeginInit()
         Me.SuspendLayout()
         '
         'pbProgress
         '
         Me.pbProgress.Location = New System.Drawing.Point(80, 312)
         Me.pbProgress.Name = "pbProgress"
         Me.pbProgress.Size = New System.Drawing.Size(376, 16)
         Me.pbProgress.TabIndex = 12
         '
         'btnUpdate
         '
         Me.btnUpdate.Enabled = False
         Me.btnUpdate.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
         Me.btnUpdate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
         Me.btnUpdate.ImageIndex = 0
         Me.btnUpdate.ImageList = Me.imgSmallIconos
         Me.btnUpdate.Location = New System.Drawing.Point(465, 304)
         Me.btnUpdate.Name = "btnUpdate"
         Me.btnUpdate.Size = New System.Drawing.Size(85, 28)
         Me.btnUpdate.TabIndex = 13
         Me.btnUpdate.Text = "&Actualizar"
         Me.btnUpdate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
         '
         'imgSmallIconos
         '
         Me.imgSmallIconos.ImageStream = CType(resources.GetObject("imgSmallIconos.ImageStream"), System.Windows.Forms.ImageListStreamer)
         Me.imgSmallIconos.TransparentColor = System.Drawing.Color.Transparent
         Me.imgSmallIconos.Images.SetKeyName(0, "")
         Me.imgSmallIconos.Images.SetKeyName(1, "")
         Me.imgSmallIconos.Images.SetKeyName(2, "")
         Me.imgSmallIconos.Images.SetKeyName(3, "")
         Me.imgSmallIconos.Images.SetKeyName(4, "")
         Me.imgSmallIconos.Images.SetKeyName(5, "")
         '
         'lblNuevaVersion
         '
         Me.lblNuevaVersion.AutoSize = True
         Me.lblNuevaVersion.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
         Me.lblNuevaVersion.ForeColor = System.Drawing.Color.RoyalBlue
         Me.lblNuevaVersion.Location = New System.Drawing.Point(14, 33)
         Me.lblNuevaVersion.Name = "lblNuevaVersion"
         Me.lblNuevaVersion.Size = New System.Drawing.Size(261, 15)
         Me.lblNuevaVersion.TabIndex = 8
         Me.lblNuevaVersion.Text = "Existe una nueva versión disponible en "
         Me.lblNuevaVersion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
         '
         'btnSalir
         '
         Me.btnSalir.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
         Me.btnSalir.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
         Me.btnSalir.ImageIndex = 1
         Me.btnSalir.ImageList = Me.imgSmallIconos
         Me.btnSalir.Location = New System.Drawing.Point(553, 304)
         Me.btnSalir.Name = "btnSalir"
         Me.btnSalir.Size = New System.Drawing.Size(85, 28)
         Me.btnSalir.TabIndex = 3
         Me.btnSalir.Text = "&Cancelar"
         Me.btnSalir.TextAlign = System.Drawing.ContentAlignment.MiddleRight
         '
         'lblProgreso
         '
         Me.lblProgreso.Location = New System.Drawing.Point(16, 312)
         Me.lblProgreso.Name = "lblProgreso"
         Me.lblProgreso.Size = New System.Drawing.Size(56, 16)
         Me.lblProgreso.TabIndex = 14
         Me.lblProgreso.Text = "Progreso"
         Me.lblProgreso.TextAlign = System.Drawing.ContentAlignment.MiddleRight
         '
         'lblEmpresa
         '
         Me.lblEmpresa.AutoSize = True
         Me.lblEmpresa.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
         Me.lblEmpresa.Location = New System.Drawing.Point(11, 13)
         Me.lblEmpresa.Name = "lblEmpresa"
         Me.lblEmpresa.Size = New System.Drawing.Size(59, 13)
         Me.lblEmpresa.TabIndex = 16
         Me.lblEmpresa.Text = "Aplicación:"
         Me.lblEmpresa.TextAlign = System.Drawing.ContentAlignment.MiddleRight
         '
         'lblApp
         '
         Me.lblApp.AutoSize = True
         Me.lblApp.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
         Me.lblApp.Location = New System.Drawing.Point(69, 12)
         Me.lblApp.Name = "lblApp"
         Me.lblApp.Size = New System.Drawing.Size(73, 16)
         Me.lblApp.TabIndex = 17
         Me.lblApp.Text = "appname"
         '
         'lblURLServidor
         '
         Me.lblURLServidor.AutoSize = True
         Me.lblURLServidor.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
         Me.lblURLServidor.Location = New System.Drawing.Point(266, 32)
         Me.lblURLServidor.Name = "lblURLServidor"
         Me.lblURLServidor.Size = New System.Drawing.Size(67, 16)
         Me.lblURLServidor.TabIndex = 18
         Me.lblURLServidor.Text = "Servidor"
         '
         'chkDetalle
         '
         Me.chkDetalle.Location = New System.Drawing.Point(560, 36)
         Me.chkDetalle.Name = "chkDetalle"
         Me.chkDetalle.Size = New System.Drawing.Size(81, 24)
         Me.chkDetalle.TabIndex = 19
         Me.chkDetalle.Text = "ver detalle"
         '
         'dsUpgrades
         '
         Me.dsUpgrades.DataSetName = "NewDataSet"
         '
         'DataGridTableStyle1
         '
         Me.DataGridTableStyle1.DataGrid = Nothing
         Me.DataGridTableStyle1.GridColumnStyles.AddRange(New System.Windows.Forms.DataGridColumnStyle() {Me.txtFecha, Me.txtAplic, Me.txtVersion, Me.txtReq, Me.txtDsc})
         Me.DataGridTableStyle1.HeaderForeColor = System.Drawing.SystemColors.ControlText
         '
         'txtFecha
         '
         Me.txtFecha.Format = ""
         Me.txtFecha.FormatInfo = Nothing
         Me.txtFecha.HeaderText = "Fecha"
         Me.txtFecha.MappingName = "date"
         Me.txtFecha.Width = 65
         '
         'txtAplic
         '
         Me.txtAplic.Format = ""
         Me.txtAplic.FormatInfo = Nothing
         Me.txtAplic.HeaderText = "Aplicación"
         Me.txtAplic.MappingName = "name"
         Me.txtAplic.Width = 80
         '
         'txtVersion
         '
         Me.txtVersion.Format = ""
         Me.txtVersion.FormatInfo = Nothing
         Me.txtVersion.HeaderText = "Versión"
         Me.txtVersion.MappingName = "version"
         Me.txtVersion.Width = 75
         '
         'txtReq
         '
         Me.txtReq.Format = ""
         Me.txtReq.FormatInfo = Nothing
         Me.txtReq.HeaderText = "Requerimiento"
         Me.txtReq.MappingName = "subject"
         Me.txtReq.Width = 110
         '
         'txtDsc
         '
         Me.txtDsc.Format = ""
         Me.txtDsc.FormatInfo = Nothing
         Me.txtDsc.HeaderText = "Descripción"
         Me.txtDsc.MappingName = "dsc"
         Me.txtDsc.Width = 240
         '
         'dgvDatos
         '
         Me.dgvDatos.AllowUserToAddRows = False
         Me.dgvDatos.AllowUserToDeleteRows = False
         Me.dgvDatos.AllowUserToOrderColumns = True
         Me.dgvDatos.AllowUserToResizeRows = False
         Me.dgvDatos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
         Me.dgvDatos.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colDate, Me.colName, Me.colVersion, Me.colsubject, Me.colDSC})
         Me.dgvDatos.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
         Me.dgvDatos.Location = New System.Drawing.Point(12, 66)
         Me.dgvDatos.Name = "dgvDatos"
         Me.dgvDatos.RowHeadersVisible = False
         Me.dgvDatos.Size = New System.Drawing.Size(634, 232)
         Me.dgvDatos.TabIndex = 20
         Me.dgvDatos.Visible = False
         '
         'colDate
         '
         Me.colDate.DataPropertyName = "date"
         Me.colDate.HeaderText = "Fecha"
         Me.colDate.Name = "colDate"
         Me.colDate.Width = 80
         '
         'colName
         '
         Me.colName.DataPropertyName = "name"
         Me.colName.HeaderText = "Aplicación"
         Me.colName.Name = "colName"
         Me.colName.Width = 120
         '
         'colVersion
         '
         Me.colVersion.DataPropertyName = "version"
         Me.colVersion.HeaderText = "Versión"
         Me.colVersion.Name = "colVersion"
         Me.colVersion.Width = 80
         '
         'colsubject
         '
         Me.colsubject.DataPropertyName = "subject"
         Me.colsubject.HeaderText = "Motivo"
         Me.colsubject.Name = "colsubject"
         '
         'colDSC
         '
         Me.colDSC.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
         Me.colDSC.DataPropertyName = "dsc"
         Me.colDSC.HeaderText = "Descripción"
         Me.colDSC.Name = "colDSC"
         '
         'frmUpdate
         '
         Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
         Me.BackColor = System.Drawing.SystemColors.Control
         Me.ClientSize = New System.Drawing.Size(658, 344)
         Me.Controls.Add(Me.dgvDatos)
         Me.Controls.Add(Me.chkDetalle)
         Me.Controls.Add(Me.lblURLServidor)
         Me.Controls.Add(Me.lblApp)
         Me.Controls.Add(Me.lblEmpresa)
         Me.Controls.Add(Me.lblProgreso)
         Me.Controls.Add(Me.btnSalir)
         Me.Controls.Add(Me.lblNuevaVersion)
         Me.Controls.Add(Me.pbProgress)
         Me.Controls.Add(Me.btnUpdate)
         Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
         Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
         Me.MaximizeBox = False
         Me.MinimizeBox = False
         Me.Name = "frmUpdate"
         Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
         Me.Text = "Actualizador de versiones"
         CType(Me.dsUpgrades, System.ComponentModel.ISupportInitialize).EndInit()
         CType(Me.dgvDatos, System.ComponentModel.ISupportInitialize).EndInit()
         Me.ResumeLayout(False)
         Me.PerformLayout()

      End Sub 'InitializeComponent

#End Region

      Public Shared Sub Main(ByVal CmdArgs() As String)

         configurationAppSettings = New System.Configuration.AppSettingsReader()

         Try
            'ejecucion por modulo (el modulo llama a Updater) o Qry de versiones
            appToUpdate = CmdArgs(0)
            moduleToUpdate = CmdArgs(1)

            If CmdArgs(3).Trim() = "QRY" Then
               Application.Run(New frmDetails(appToUpdate, moduleToUpdate, CmdArgs(2)))
               Exit Sub
            End If

            If Not String.IsNullOrEmpty(CmdArgs(4)) Then
               _consultaUrl = CmdArgs(4)
            End If

            _downloadMode = CmdArgs(2)
            pathModule = CmdArgs(3).Replace("***", " ")

         Catch
            Try
               'Nombre aplicacion
               appToUpdate = configurationAppSettings.GetValue("appName", GetType(System.String))
               'ALL-MODULE
               _downloadMode = "ALL"
               'Aplicacion a ejecutar sino encuentra nuevas versiones
               AppToRun = configurationAppSettings.GetValue("AppToRun", GetType(System.String))
            Catch ex As Exception
               MessageBox.Show("Archivo de configuración inválido o no existente.", "Actualizador", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
               Exit Sub
            End Try
         End Try

         Try
            Application.Run(New frmUpdate)
         Catch ex As Exception
            'error del control HTML si no carga ventana
            If Not ex.TargetSite.Name = "MakeVisibleWithShow" Then
               MessageBox.Show(ex.Message.ToString(), "Actualizador", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            End If
         End Try

      End Sub


      ' Form load event handler. Here we read configuration
      Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load

         Try
            If Not System.IO.Directory.Exists(GetCurrentDirectory() & "\downloads\") Then
               Throw New Exception("Falta la carpeta [downloads] en la ejecución del Actualizador. " & Chr(13) & "No existe la carpeta 'downloads'." & Chr(13) & Chr(13) & "Contacte con el administrador del sistema.")
            End If

            Me.lblApp.Text = appToUpdate + " - " + moduleToUpdate
            Me.Height = 160
            Me.btnUpdate.Top = 80
            Me.btnSalir.Top = 80
            Me.lblProgreso.Top = 80
            Me.pbProgress.Top = 82

            Me.dgvDatos.Visible = False

            Me.lblURLServidor.Text = _consultaUrl.Substring(0, _consultaUrl.LastIndexOf("WSActualizador.asmx"))

            If Me.lblURLServidor.Text = "" Then
               Throw New Exception("No se encuentra seteado el servidor de aplicaciones.")
            End If

            Dim frmSlash As Splash = New Splash()
            frmSlash.lblServer.Text = Me.lblURLServidor.Text
            frmSlash.Show()
            frmSlash.Refresh()

            Cursor.Current = Cursors.WaitCursor

            Me._URLCorrecta = Me.lblURLServidor.Text

            If _downloadMode = "MODULE" Then
               Me.CheckModuleVersion()
            ElseIf _downloadMode = "ALL" Then
               Me.CheckAppVersion()
            End If

            If info.IsAvailable Then

               dt = New DataTable()
               'para poder ordenar por fecha y como viene la fecha tipo string se hace el volcado al dt
               dt.Columns.Add("date", Type.GetType("System.DateTime"))
               dt.Columns.Add("name", Type.GetType("System.String"))
               dt.Columns.Add("version", Type.GetType("System.String"))
               dt.Columns.Add("subject", Type.GetType("System.String"))
               dt.Columns.Add("dsc", Type.GetType("System.String"))

               Dim dr As DataRow
               Dim dtFecha As DateTime

               For Each drWS As DataRow In info.dsUpgrades.Tables("upgrade").Rows
                  If drWS("name").ToString() = moduleToUpdate Then
                     dtFecha = New DateTime(Convert.ToInt32(drWS("date").ToString().Split("/")(2)), Convert.ToInt32(drWS("date").ToString().Split("/")(1)), Convert.ToInt32(drWS("date").ToString().Split("/")(0)))
                     dr = dt.NewRow()
                     dr("date") = dtFecha
                     dr("dsc") = drWS("dsc") '"Ampliar"
                     dr("name") = drWS("name")
                     dr("version") = drWS("version")
                     dr("subject") = drWS("subject")
                     dt.Rows.Add(dr)
                  End If
               Next

               dt.DefaultView.Sort = "date DESC"
               Me.dgvDatos.DataSource = dt.DefaultView

            End If

            Cursor.Current = Cursors.Default
            frmSlash.Hide()

         Catch ex As Exception
            MessageBox.Show(ex.Message.ToString(), "Updater", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            AppExit(False)
         End Try

      End Sub 'Form_Load

      ' Get the assembly version for the specified assembly (config file)
      ' Use web service to query update availability
      Private Sub CheckModuleVersion()

         Dim agent As WSUpdater.Actualizador
         Dim name As AssemblyName
         name = Eniac.Ayudantes.Actualizador.Common.GetAssemblyName(pathModule)

         ' Use web service to inquire as of update availability
         agent = New WSUpdater.Actualizador()
         agent.Url = _consultaUrl
         info = agent.GetUpdateInfo(appToUpdate, moduleToUpdate, name.Version.Major, name.Version.Minor, name.Version.Build)

         ' If there is an updated version allow user to proceed with update
         If info.IsAvailable Then
            btnUpdate.Enabled = True
            UpdateUrl = info.Url
         Else
            AppExit(False)
         End If

      End Sub 'btnCheck_Click

      Private Sub CheckAppVersion()

         Dim a As [Assembly] = Nothing
         Dim name As New AssemblyName
         Dim dir As New DirectoryInfo(Application.StartupPath)
         Dim f As FileInfo
         Dim flag As Boolean = False

         ' If assembly does not exists, presume the version to be 0.0.0
         name.Version = New Version("0.0.0")

         For Each f In dir.GetFiles("*.exe")

            Try
               a = [Assembly].LoadFrom(f.FullName)
            Catch
            End Try

            If Not a Is Nothing Then
               name = a.GetName()
               moduleToUpdate = GetAssemblyAttribs(a)("ModuleName")

               If Not moduleToUpdate Is Nothing Then
                  Dim VersionLocal As String
                  VersionLocal = String.Format("{0}.{1}.{2}", name.Version.Major, name.Version.Minor, name.Version.Build)

                  ' Use web service to inquire as of update availability
                  Dim agent As New WSUpdater.Actualizador()
                  agent.Url = _consultaUrl
                  info = agent.GetUpdateInfo(appToUpdate, moduleToUpdate, name.Version.Major, name.Version.Minor, name.Version.Build)
                  ' If there is an updated version allow user to proceed with update
                  If info.IsAvailable Then

                     'Los módulos que se actualizan independientemente NO tienen seteada el setupPath.

                     'La actualización del módulo se hará cuando se quiera acceder al mismo.
                     If info.Url = "" Then
                        UpdateUrl = info.AppUrl
                        btnUpdate.Enabled = True
                        flag = True
                        Exit For
                     End If
                  Else
                     If info.ErrorMessage <> "" Then
                        Throw New Exception("Ha ocurrido un error en el servidor. " & info.ErrorMessage)
                     End If
                  End If
               End If
            End If

         Next f

         If Not flag Then

            Dim AppParam As String
            AppParam = configurationAppSettings.GetValue("AppParam", GetType(System.String))

            Dim myProcess As New Process
            Dim myProcessStartInfo As New ProcessStartInfo(AppToRun, AppParam)
            myProcessStartInfo.WorkingDirectory = Application.StartupPath
            myProcessStartInfo.UseShellExecute = False
            myProcessStartInfo.RedirectStandardOutput = True
            myProcess.StartInfo = myProcessStartInfo

            myProcess.Start()
            AppExit(False)

         End If

      End Sub

      'Funciones del Api
      <DllImport("user32.dll")> _
      Public Shared Function FindWindow(ByVal strclassName As String, ByVal strWindowName As String) As System.IntPtr
      End Function

      Private Sub btnUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdate.Click

         Cursor.Current = Cursors.WaitCursor
         btnUpdate.Enabled = False

         Dim Parte As Integer

         Parte = UpdateUrl.LastIndexOf("/")
         FileName = UpdateUrl.Substring(Parte + 1)

         m_req = CType(HttpWebRequest.Create(UpdateUrl), HttpWebRequest)
         m_req.BeginGetResponse(New AsyncCallback(AddressOf ResponseReceived), Nothing)

      End Sub 'btnUpdate_Click

      ' Asynchronous routine to process the http web request
      Sub ResponseReceived(ByVal res As IAsyncResult)
         ' Try getting the web response. If there was an error (404 or other),
         ' web exception will be thrown hete
         Try
            m_resp = CType(m_req.EndGetResponse(res), HttpWebResponse)
         Catch ThisExcept As WebException
            Return
         End Try
         dataBuffer = New Byte(DataBlockSize) {}
         ' Prepare the propgres bar
         maxVal = Fix(m_resp.ContentLength)
         pbProgress.Invoke(New EventHandler(AddressOf SetProgressMax))

         If File.Exists(GetCurrentDirectory() & "\downloads\" & FileName) Then
            File.Delete(GetCurrentDirectory() & "\downloads\" & FileName)
         End If

         m_fs = New FileStream(GetCurrentDirectory() & "\downloads\" & FileName, FileMode.Create)

         ' Start reading from network stream asynchronously
         m_resp.GetResponseStream().BeginRead(dataBuffer, 0, DataBlockSize, New AsyncCallback(AddressOf OnDataRead), Me)


      End Sub 'ResponseReceived

      ' Asynchronous network stream reading
      Sub OnDataRead(ByVal res As IAsyncResult)
         ' Get bytecount of the received chunk
         Dim nBytes As Integer = m_resp.GetResponseStream().EndRead(res)
         ' Dump it to the output stream
         m_fs.Write(dataBuffer, 0, nBytes)
         ' Update prgress bar
         pbVal += nBytes
         pbProgress.Invoke(New EventHandler(AddressOf UpdateProgressValue))
         If nBytes > 0 Then
            ' If read was successful, continue reading asynchronously as there is more data
            m_resp.GetResponseStream().BeginRead(dataBuffer, 0, DataBlockSize, New AsyncCallback(AddressOf OnDataRead), Me)
         Else
            ' Otherwise close the stream and proceed with installation
            m_fs.Close()
            m_fs = Nothing

            MessageBox.Show("Operación finalizada. Se va a proceder a la instalación de la nueva versión.", "Actualizador", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)
            pathSetup = GetCurrentDirectory() & "\downloads\" & FileName

            Dim pr As Process = New Process
            Dim strtInfo As ProcessStartInfo = New ProcessStartInfo(pathSetup)
            strtInfo.WindowStyle = ProcessWindowStyle.Normal
            pr.StartInfo = strtInfo

            pr.Start()
            pr.Close()
            AppExit(True)

         End If
      End Sub 'OnDataRead

      Private Sub btnSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSalir.Click
         AppExit(True)
      End Sub

      Private Sub AppExit(ByVal output As Boolean)
         Console.WriteLine(output)
         Me.Invoke(New CrossAppDomainDelegate(AddressOf Me.Close))
      End Sub

      ' Delegate for updating the file download progress
      Public Sub UpdateProgressValue(ByVal sender As Object, ByVal e As EventArgs)
         pbProgress.Value = pbVal
         Application.DoEvents()
      End Sub 'UpdateProgressValue

      ' Delegate for setting the progress bar size
      Public Sub SetProgressMax(ByVal sender As Object, ByVal e As EventArgs)
         pbProgress.Maximum = maxVal
         Application.DoEvents()
      End Sub 'SetProgressMax

      Private Sub chkDetalle_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkDetalle.Click
         If chkDetalle.Checked Then
            Me.Height = 484
            Me.dgvDatos.Height = 312
            Me.dgvDatos.Visible = True
            btnUpdate.Top = 405
            btnSalir.Top = 405
            lblProgreso.Top = 410
            pbProgress.Top = 412
         Else
            Me.Height = 160
            Me.dgvDatos.Visible = False
            btnUpdate.Top = 80
            btnSalir.Top = 80
            lblProgreso.Top = 80
            pbProgress.Top = 82
         End If
      End Sub

      Public Declare Function ShellExecute Lib "shell32.dll" Alias "ShellExecuteA" (ByVal hWnd As IntPtr, ByVal lpOperation As String, ByVal lpFile As String, ByVal lpParameters As String, ByVal lpDirectory As String, ByVal nShowCmd As Integer) As Integer

      Public Const SW_SHOWMAXIMIZED As Integer = 3
      Public Const GACCION_EDICION_DRAF As String = "2"

   End Class

End Namespace
