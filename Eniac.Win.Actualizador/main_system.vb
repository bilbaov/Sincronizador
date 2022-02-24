Option Strict On
Option Explicit On
Imports System.Runtime.InteropServices

Public Class main_system

#Region "Campos"
   '<<< Define DLL de moviento de Formulario.- >>>
   <DllImport("user32.DLL", EntryPoint:="ReleaseCapture")>
   Private Shared Sub ReleaseCapture()
   End Sub
   <DllImport("user32.DLL", EntryPoint:="SendMessage")>
   Private Shared Sub SendMessage(ByVal hWnd As System.IntPtr, ByVal wMsg As Integer, ByVal wParam As Integer, ByVal lParam As Integer)
   End Sub
   '-- Color de Click de Botones.- --
   Public oColorClick As Color = Color.FromArgb(12, 61, 92)

#End Region

#Region "Constructores"
   Public Sub New()
      '-- Inicializa los componentes.- --
      InitializeComponent()
   End Sub

#End Region

#Region "Eventos"
   ''' <summary>
   ''' Eventos de Boton Minimizar Aplicacion.-
   ''' </summary>
   ''' <param name="sender"></param>
   ''' <param name="e"></param>
   Private Sub pcbxMinimizar_Click(sender As Object, e As EventArgs) Handles pcbxMinimizar.Click
      Me.WindowState = FormWindowState.Minimized
   End Sub
   Private Sub pcbxMinimizar_MouseHover(sender As Object, e As EventArgs) Handles pcbxMinimizar.MouseHover
      CRG_ToolTips_Bttn("Permite minimizar el Formulario Principal.")
   End Sub
   Private Sub LimpiarToolTips_MouseLeave(sender As Object, e As EventArgs) Handles pcbxMinimizar.MouseLeave,
                                                                                    pcbxCerrar.MouseLeave,
                                                                                    btnSalidaSistema.MouseLeave,
                                                                                    btnBackupDatos.MouseLeave
      CRG_ToolTips_Bttn("")
   End Sub
   ''' <summary>
   ''' Eventos de la Salida del Formulario.-
   ''' </summary>
   ''' <param name="sender"></param>
   ''' <param name="e"></param>
   Private Sub CerrarSistema_Click(sender As Object, e As EventArgs) Handles pcbxCerrar.Click, btnSalidaSistema.Click
      '<<< Procedimeinto de Cierre de formulario.- >>>
      Call Cerrar_Aplicacion_Sist()
   End Sub
   Private Sub Pcbx_Cerrar_MouseHover(sender As Object, e As EventArgs) Handles pcbxCerrar.MouseHover, btnSalidaSistema.MouseHover
      CRG_ToolTips_Bttn("Opción de Salida del Sistema.")
   End Sub
   ''' <summary>
   ''' Procedimiento de Backup de Bases de Datos.- --
   ''' </summary>
   ''' <param name="e"></param>
   Private Sub BackupBaseDatos_Click(sender As Object, e As EventArgs) Handles btnBackupDatos.Click
      '<<< Apertura de Formulario.- >>>
      AbrirControlEnPanel(New ucBackupBaseDatos)
      '-- Cambia el Color del Boton Presionado.---
      btnBackupDatos.BackColor = oColorClick
   End Sub
   Private Sub BackupBaseDatos_MouseHover(sender As Object, e As EventArgs) Handles btnBackupDatos.MouseHover
      CRG_ToolTips_Bttn("Realizar Operaciones de Backup de Bases de Datos.")
   End Sub
   ''' <summary>
   ''' 
   ''' </summary>
   ''' <param name="sender"></param>
   ''' <param name="e"></param>
   Private Sub btnRestauracionDatos_Click(sender As Object, e As EventArgs) Handles btnRestauracionDatos.Click
      '<<< Apertura de Formulario.- >>>
      AbrirControlEnPanel(New ucRestoreBaseDatos)
      '-- Cambia el Color del Boton Presionado.---
      btnBackupDatos.BackColor = oColorClick
   End Sub

   ''' <summary>
   ''' Permite realizar el movimiento del formulario.-
   ''' </summary>
   ''' <param name="sender"></param>
   ''' <param name="e"></param>
   Private Sub pnlBarraTitulo_MouseMove(sender As Object, e As MouseEventArgs) Handles pnlBarraTitulo.MouseMove
      ReleaseCapture()
      SendMessage(Me.Handle, &H112&, &HF012&, 0)
   End Sub

#End Region

#Region "Overrides"
   Protected Overrides Sub OnLoad(ByVal e As System.EventArgs)
      MyBase.OnLoad(e)
      '-- Limpia los comentarios.- --
      tslPrincipal.Text = String.Empty
   End Sub
#End Region

#Region "Metodos"
   ''' <summary>
   ''' Establece el ToolTip del Boton.-
   ''' </summary>
   ''' <param name="gConTxt"></param>
   Sub CRG_ToolTips_Bttn(ByVal gConTxt As String)
      tslPrincipal.Text = gConTxt
   End Sub
   ''' <summary>
   ''' Procedimiento de Cierre de Formulario.- 
   ''' </summary>
   Sub Cerrar_Aplicacion_Sist()
      If MessageBox.Show("¿Desea cerrar la aplicación?", "Mensaje de Salida.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
         Application.Exit()
      End If
   End Sub

   ''' <summary>
   ''' Funcion de Apertura de Formulario.-
   ''' </summary>
   ''' <param name="Miform">Nombre del formulario que se abre.-</param>
   Private Sub AbrirControlEnPanel(oControl As Control)
      flpFormularios.Controls.Clear()
      flpFormularios.Controls.Add(oControl)
   End Sub


#End Region


End Class