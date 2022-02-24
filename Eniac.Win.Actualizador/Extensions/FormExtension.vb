#Region "Option/Imports"
Option Strict On
Option Explicit On

Imports System.Runtime.CompilerServices
#End Region
Namespace Extensiones
    Public Module FormExtensions

        <Extension, System.Diagnostics.DebuggerStepThrough()>
        Public Function ShowError(owner As Form, ex As Exception, recursivo As Boolean) As DialogResult
            Dim st = New System.Text.StringBuilder()
            If recursivo Then
                owner.ArmaErrorRecursivo(ex, st)
            Else
                st.AppendLine(ex.Message)
            End If
            Return owner.ShowMessage(st.ToString())
        End Function
        <Extension, System.Diagnostics.DebuggerStepThrough()>
        Public Function ShowMessage(owner As Form, mensaje As String) As DialogResult

            If owner Is Nothing OrElse owner.IsDisposed Then
                Return MessageBox.Show(mensaje, owner.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                Return MessageBox.Show(owner, mensaje, owner.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End Function
        <Extension, System.Diagnostics.DebuggerStepThrough()>
        Public Function ShowPregunta(owner As Form, mensaje As String, ParamArray args As Object()) As DialogResult
            Return ShowPregunta(owner, String.Format(mensaje, args))
        End Function
        <Extension, System.Diagnostics.DebuggerStepThrough()>
        Public Function ShowPregunta(owner As Form, mensaje As String, defaultButton As MessageBoxDefaultButton, ParamArray args As Object()) As DialogResult
            Return ShowPregunta(owner, String.Format(mensaje, args), defaultButton)
        End Function
        <Extension, System.Diagnostics.DebuggerStepThrough()>
        Public Function ShowPregunta(owner As Form, mensaje As String) As DialogResult
            Return System.Windows.Forms.MessageBox.Show(owner, mensaje, owner.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
        End Function
        <Extension, System.Diagnostics.DebuggerStepThrough()>
        Public Function ShowPregunta(owner As Form, mensaje As String, defaultButton As MessageBoxDefaultButton) As DialogResult
            Return System.Windows.Forms.MessageBox.Show(owner, mensaje.Truncar(1000), owner.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question, defaultButton)
        End Function
        <Extension, System.Diagnostics.DebuggerStepThrough()>
        Public Sub ArmaErrorRecursivo(owner As Form, ex As Exception, stb As System.Text.StringBuilder)
            If ex IsNot Nothing Then
                stb.AppendLine(ex.Message)
                If ex.InnerException IsNot Nothing Then
                    stb.AppendLine()
                    owner.ArmaErrorRecursivo(ex.InnerException, stb)
                End If
            End If
        End Sub

    End Module

End Namespace