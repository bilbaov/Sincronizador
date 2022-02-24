#Region "Option"
Option Explicit On
Option Strict On
#End Region
Public Class Parametros
   Inherits Eniac.Reglas.Base

#Region "Constructores"

   Public Sub New()
      Me.New(Nothing)
   End Sub

   Public Sub New(ByVal accesoDatos As Datos.DataAccess)
      Me.NombreEntidad = Entidades.Parametro.NombreTabla
      If accesoDatos Is Nothing Then
         da = New Datos.DataAccess()
      Else
         da = accesoDatos
      End If
   End Sub

#End Region

#Region "Overrides/Overloads"

   Public Overrides Sub Insertar(entidad As Eniac.Entidades.Entidad)
      EjecutaConTransaccion(Sub() _Insertar(DirectCast(entidad, Entidades.Parametro)))
   End Sub
   Public Overrides Sub Actualizar(entidad As Eniac.Entidades.Entidad)
      EjecutaConTransaccion(Sub() _Actualizar(DirectCast(entidad, Entidades.Parametro)))
   End Sub
   Public Overloads Sub Actualizar(entidades As List(Of Entidades.Parametro))
      EjecutaConTransaccion(Sub() _Actualizar(entidades))
   End Sub
   Public Overrides Sub Borrar(entidad As Eniac.Entidades.Entidad)
      EjecutaConTransaccion(Sub() _Borrar(DirectCast(entidad, Entidades.Parametro)))
   End Sub

   Public Overrides Function Buscar(ByVal entidad As Eniac.Entidades.Buscar) As DataTable

      Dim stbQuery As StringBuilder = New StringBuilder()

      With stbQuery
         .Append("SELECT  ")
         .Append("idParametro, ")
         .Append("NombreParametro, ")
         .Append("TipoParametro, ")
         .Append("Categoria, ")
         .Append("ValorParametro ")
         .Append("FROM Parametros ")
         .Append("  WHERE ")
         .Append(entidad.Columna)
         .Append(" LIKE '%")
         .Append(entidad.Valor.ToString())
         .Append("%'")
      End With

      Return Me.da.GetDataTable(stbQuery.ToString())

   End Function

   Public Overrides Function GetAll() As System.Data.DataTable
      Return New SqlServer.Parametros(Me.da).Parametros_GA(actual.Sucursal.IdEmpresa)
   End Function

#End Region

#Region "Metodos Privados"

   Private Sub EjecutaSP(en As Entidades.Parametro, tipo As TipoSP)
      Dim sql As SqlServer.Parametros = New SqlServer.Parametros(Me.da)

      Dim rAudit As Reglas.Auditorias = New Reglas.Auditorias(da)
      Dim OperacAudit As Entidades.OperacionesAuditorias
      Dim clavePrimariaAuditoria As String = String.Format("{0} = {1} AND {2} = '{3}'",
                                                           Entidades.Parametro.Columnas.IdEmpresa.ToString(), en.IdEmpresa,
                                                           Entidades.Parametro.Columnas.IdParametro.ToString(), en.IdParametro)

      If tipo = TipoSP._I Or tipo = TipoSP._U Then
         If sql.Existe(en.IdEmpresa, en.IdParametro) Then
            sql.Parametros_U(en.IdEmpresa, en.IdParametro, en.ValorParametro, en.CategoriaParametro, en.DescripcionParametro)
            OperacAudit = rAudit.OperacionSegunAuditoria(Entidades.Parametro.NombreTabla, clavePrimariaAuditoria)

         Else
            sql.Parametros_I(en.IdEmpresa, en.IdParametro, en.ValorParametro, en.CategoriaParametro, en.DescripcionParametro)
            OperacAudit = Entidades.OperacionesAuditorias.Alta

         End If
      ElseIf tipo = TipoSP._D Then
         sql.Parametros_D(en.IdEmpresa, en.IdParametro)
         OperacAudit = Entidades.OperacionesAuditorias.Baja

      End If

      rAudit.Insertar(Entidades.Parametro.NombreTabla, OperacAudit, actual.Nombre, clavePrimariaAuditoria, conMilisegundos:=False)

   End Sub

#End Region

#Region "Metodos Publicos"
   Public Sub _Insertar(entidad As Entidades.Parametro)
      Me.EjecutaSP(entidad, TipoSP._I)
   End Sub
   Public Sub _Actualizar(entidad As Entidades.Parametro)
      Me.EjecutaSP(entidad, TipoSP._U)
   End Sub
   Public Sub _Actualizar(entidades As List(Of Entidades.Parametro))
      For Each ent As Entidades.Parametro In entidades
         _Actualizar(ent)
      Next
   End Sub
   Public Sub _Borrar(entidad As Entidades.Parametro)
      Me.EjecutaSP(entidad, TipoSP._D)
   End Sub

   Public Function GetPorCodigo(idEmpresa As Integer, idParametro As String) As DataTable
      Dim dtResult As DataTable = New SqlServer.Parametros(Me.da).Parametros_GA(idEmpresa, idParametro, True)
      If dtResult.Rows.Count = 0 Then
         dtResult = New SqlServer.Parametros(Me.da).Parametros_GA(idEmpresa, idParametro, False)
      End If
      Return dtResult
   End Function

   'GET POR NOMBRE
   Public Function GetPorNombre(idEmpresa As Integer, idParametro As String) As DataTable
      Return New SqlServer.Parametros(da).Parametros_GA(idEmpresa, idParametro)
   End Function

   Public Function GetUno(idEmpresa As Integer, idParametro As String, acciones As AccionesSiNoExisteRegistro) As Entidades.Parametro
      Dim dt As DataTable = New SqlServer.Parametros(da).Parametros_G1(idEmpresa, idParametro)
      Dim o As Entidades.Parametro = New Entidades.Parametro()
      If dt.Rows.Count > 0 Then
         o.IdEmpresa = Integer.Parse(dt.Rows(0)("IdEmpresa").ToString())
         o.IdParametro = dt.Rows(0)("IdParametro").ToString()
         o.ValorParametro = dt.Rows(0)("ValorParametro").ToString()
         o.DescripcionParametro = dt.Rows(0)("DescripcionParametro").ToString()
         o.CategoriaParametro = dt.Rows(0)("CategoriaParametro").ToString()
      Else
         If acciones = AccionesSiNoExisteRegistro.Excepcion Then
            Throw New Exception(String.Format("No se encontró el parámetro {0}", idParametro))
         ElseIf acciones = AccionesSiNoExisteRegistro.Nulo Then
            Return Nothing
         End If
      End If
      Return o
   End Function

   <Obsolete("Por favor no utilizar. Se eliminará el método. Usar GetValorPD.", False)>
   Public Overloads Function _GetValor(idParametro As Entidades.Parametro.Parametros) As String
      Return Me._GetValor(idParametro.ToString())
   End Function

   <Obsolete("Por favor no utilizar. Se eliminará el método. Usar GetValorPD.", False)>
   Public Overloads Function GetValor(idParametro As Entidades.Parametro.Parametros) As String
      Try
         Me.da.OpenConection()
         Return Me._GetValor(idParametro)
      Catch ex As Exception
         Throw
      Finally
         Me.da.CloseConection()
      End Try
   End Function

   <Obsolete("Por favor no utilizar. Se eliminará el método. Usar GetValorPD.", False)>
   Public Overloads Function _GetValor(idParametro As String) As String
      Dim sql As SqlServer.Parametros = New SqlServer.Parametros(Me.da)
      Return sql.GetValor(actual.Sucursal.IdEmpresa, idParametro)
   End Function

   <Obsolete("Por favor no utilizar. Se eliminará el método. Usar GetValorPD.", False)>
   Public Overloads Function GetValor(ByVal idParametro As String) As String
      Try
         Me.da.OpenConection()
         Dim sql As SqlServer.Parametros = New SqlServer.Parametros(Me.da)
         Return sql.GetValor(actual.Sucursal.IdEmpresa, idParametro)
      Catch ex As Exception
         Throw
      Finally
         Me.da.CloseConection()
      End Try
   End Function

   <Obsolete("Por favor no utilizar. Se eliminó el método. Usar GetValorPD pasando String.Empty como valor por defecto.", True)>
   Public Function GetValor2(idParametro As String) As String
      Throw New NotImplementedException("Function GetValor2(String) As String - Se eliminó el método. Usar GetValorPD pasando String.Empty como valor por defecto.")
   End Function

   Public Overloads Function GetValorPD(idParametro As String, ByVal porDefecto As String) As String
      Return ParametrosCache.Instancia.GetValorPD(idParametro, porDefecto)
   End Function

   Public Overloads Function GetValorPD(ByVal idParametro As Entidades.Parametro.Parametros, ByVal porDefecto As String) As String
      Return ParametrosCache.Instancia.GetValorPD(idParametro, porDefecto)
   End Function

   Public Sub SetValor(idEmpresa As Integer, idParametro As String, valorParametro As String)
      Try
         da.OpenConection()
         da.BeginTransaction()

         _SetValor(idEmpresa, idParametro, valorParametro)

         da.CommitTransaction()
      Catch
         da.RollbakTransaction()
         Throw
      Finally
         da.CloseConection()
      End Try
   End Sub

   Public Sub _SetValor(idEmpresa As Integer, idParametro As String, valorParametro As String)
      Dim sql As SqlServer.Parametros = New SqlServer.Parametros(Me.da)
      sql.Parametros_M1(idEmpresa, idParametro, valorParametro)
   End Sub

   Public Function GetSistema(idEmpresa As Integer) As Entidades.Sistema
      Try
         Me.da.OpenConection()

         Dim sistema As Entidades.Sistema = New Entidades.Sistema()
         Dim parametro As String = ParametrosCache.Instancia.GetValorPD(idEmpresa, Entidades.Parametro.Parametros.VENCIMIENTOSISTEMA.ToString(), String.Empty)

         sistema.ClaveActual = parametro

         My.Application.Log.WriteEntry("Parametros - Obtengo parametro de fecha de vencimiento del sistema : " + parametro, TraceEventType.Verbose)
         parametro = New Ayudantes.Criptografia().DecryptString128Bit(parametro, "clave")
         Dim valores() As String = parametro.Split(";"c)
         My.Application.Log.WriteEntry("Parametros - Obtengo fecha de la base.", TraceEventType.Verbose)
         Dim fechaServidor As DateTime = New Reglas.Generales(Me.da)._GetServerDBFechaHora()
         My.Application.Log.WriteEntry("Parametros - Fecha de la base = " + fechaServidor.ToString(), TraceEventType.Verbose)
         My.Application.Log.WriteEntry("Parametros - Fecha de vencimiento = " + valores(0), TraceEventType.Verbose)

         Dim Fecha() As String = valores(0).Split("/"c)     'Por ahora !!, porque se pierde la configuracion regional y da error en la conversion de fechas
         sistema.FechaVencimiento = New DateTime(Integer.Parse(Fecha(2)), Integer.Parse(Fecha(1)), Integer.Parse(Fecha(0))) ''   DateTime.Parse(Fecha(2) & "-" & Fecha(1) & "-" & Fecha(0))
         sistema.NombreEmpresa = valores(1)
         sistema.Habilitado = True

         sistema.CantidadEmpresasContratadas = If(valores.Count > 2 AndAlso IsNumeric(valores(2)), Integer.Parse(valores(2)), 1)

         If sistema.NombreEmpresa <> ParametrosCache.Instancia.GetValorPD(idEmpresa, Entidades.Parametro.Parametros.NOMBREEMPRESA.ToString(), String.Empty) Then
            sistema.Habilitado = False
            _SetValor(idEmpresa, Entidades.Parametro.Parametros.FACTURACIONUTILIZAMONEDADOLAR.ToString(), Boolean.TrueString)
            Return sistema
         End If
         My.Application.Log.WriteEntry("Parametros - Controlo fecha contra vencimiento del sistema.", TraceEventType.Verbose)
         If sistema.FechaVencimiento = New DateTime(9999, 12, 31) Then
            sistema.Habilitado = True
         Else
            If sistema.FechaVencimiento.Subtract(fechaServidor).Days <= 0 Then
               sistema.Habilitado = False
               _SetValor(idEmpresa, Entidades.Parametro.Parametros.FACTURACIONUTILIZAMONEDADOLAR.ToString(), Boolean.TrueString)
            End If
            If sistema.FechaVencimiento.Subtract(fechaServidor).Days < Integer.Parse(ParametrosCache.Instancia.GetValorPD(idEmpresa, Entidades.Parametro.Parametros.DIASAVISOVENCIMIENTOSISTEMA.ToString(), "0")) Then
               sistema.AvisarAlCliente = True
            End If
         End If

         If Not Publicos.UtilizaVencimientoSistema.Equals(Publicos.ValorDefaultParaUtilizaVencimientoSistema) Then
            sistema.Habilitado = False
         End If

         Return sistema

      Finally
         Me.da.CloseConection()
      End Try
   End Function

   Public Function GetParametrosDeOrganizarEntregasv2() As DataTable

      Dim sql As SqlServer.Parametros
      Dim dt As DataTable

      Try
         Me.da.OpenConection()
         Me.da.BeginTransaction()

         sql = New SqlServer.Parametros(Me.da)

         dt = sql.GetParametrosDeOrganizarEntregasv2(actual.Sucursal.IdEmpresa)

         Me.da.CommitTransaction()

         Return dt

      Catch ex As Exception
         Me.da.RollbakTransaction()
         Throw
      Finally
         Me.da.CloseConection()
      End Try
   End Function

   ''' <summary>
   ''' Este metodo obtiene los datos del mail generico que utiliza el sistema seteado en los Parametros
   ''' </summary>
   ''' <returns></returns>
   ''' <remarks></remarks>
   Public Function GetMailGenerico() As Entidades.MailConfig

      Dim sql As SqlServer.Parametros
      Dim dt As DataTable
      Dim mcon As Entidades.MailConfig

      Try
         Me.da.OpenConection()
         mcon = New Entidades.MailConfig()

         sql = New SqlServer.Parametros(Me.da)


         dt = sql.GetMailGenerico(actual.Sucursal.IdEmpresa, "'MAILUSUARIO','MAILSERVIDOR','MAILREQUIERESSL','MAILREQUIEREAUTENTICACION','MAILPUERTOSALIDA','MAILPASSWORD','MAILDIRECCION','CANTEMAILSPORMINUTO','CANTEMAILSPORHORA'")

         mcon = New Entidades.MailConfig()
         For Each dr As DataRow In dt.Rows
            Select Case dr("IdParametro").ToString()
               Case "MAILUSUARIO"
                  mcon.UsuarioMail = dr("ValorParametro").ToString()
               Case "MAILSERVIDOR"
                  mcon.ServidorSMTP = dr("ValorParametro").ToString()
               Case "MAILREQUIERESSL"
                  mcon.RequiereSSL = Boolean.Parse(dr("ValorParametro").ToString())
               Case "MAILREQUIEREAUTENTICACION"
                  mcon.RequiereAutenticacion = Boolean.Parse(dr("ValorParametro").ToString())
               Case "MAILPUERTOSALIDA"
                  mcon.PuertoSalida = Int32.Parse(dr("ValorParametro").ToString())
               Case "MAILPASSWORD"
                  mcon.Clave = dr("ValorParametro").ToString()
               Case "MAILDIRECCION"
                  mcon.Direccion = dr("ValorParametro").ToString()
               Case "CANTEMAILSPORMINUTO"
                  mcon.CantidadXMinuto = Int32.Parse(dr("ValorParametro").ToString())
               Case "CANTEMAILSPORHORA"
                  mcon.CantidadXHora = Int32.Parse(dr("ValorParametro").ToString())
               Case Else
            End Select
         Next

         Return mcon

      Catch
         Throw
      Finally
         Me.da.CloseConection()
      End Try
   End Function

   Public Function CopiarEntreEmpresas(idEmpresaOrigen As Integer, idEmpresaDestino As Integer) As Integer
      Return New SqlServer.Parametros(da).Parametros_M(idEmpresaOrigen, idEmpresaDestino)
   End Function

   Public Overloads Sub Borrar(idEmpresa As Integer)
      Dim sql As SqlServer.Parametros = New SqlServer.Parametros(da)
      sql.Parametros_D(idEmpresa, idParametro:=String.Empty)
   End Sub

   Public Function GetAuditoriasParametros(fechaDesde As Date,
                                           fechaHasta As Date,
                                           idParametro As String,
                                           tipoOperacion As String) As DataTable

      Return New SqlServer.Parametros(da).GetAuditoriasParametros(fechaDesde, fechaHasta, idParametro, tipoOperacion)

   End Function
#End Region

End Class