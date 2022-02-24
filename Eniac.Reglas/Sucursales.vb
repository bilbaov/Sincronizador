#Region "Option/Imports"
Option Strict On
Option Explicit On

Imports Eniac.Datos
Imports Eniac.Entidades
#End Region
Public Class Sucursales
   Inherits Reglas.Base

#Region "Constructores"

   Public Sub New()
      Me.New(New Datos.DataAccess())
   End Sub

   Public Sub New(accesoDatos As Datos.DataAccess)
      Me.NombreEntidad = "Sucursales"
      da = accesoDatos
   End Sub

#End Region

#Region "Overrides"

   Public Overrides Sub Insertar(ByVal entidad As Eniac.Entidades.Entidad)
      Me.EjecutaSP(entidad, TipoSP._I)
   End Sub

   Public Overrides Sub Actualizar(ByVal entidad As Eniac.Entidades.Entidad)
      Me.EjecutaSP(entidad, TipoSP._U)
   End Sub

   Public Overrides Sub Borrar(ByVal entidad As Eniac.Entidades.Entidad)
      Me.EjecutaSP(entidad, TipoSP._D)
   End Sub

   Public Overrides Function Buscar(ByVal entidad As Eniac.Entidades.Buscar) As DataTable
      Return New SqlServer.Sucursales(da).Buscar(entidad.Columna, entidad.Valor.ToString())
   End Function

   Public Overrides Function GetAll() As System.Data.DataTable
      Return New SqlServer.Sucursales(Me.da).Sucursales_GA(False)
   End Function

#End Region

#Region "Metodos"

   Public Function EstoyEn(incluirLogo As Boolean) As Eniac.Entidades.Sucursal
      Dim dt As DataTable = New SqlServer.Sucursales(da).Sucursales_GA(estoyAca:=True, soyLaCentral:=Nothing, incluirLogo:=incluirLogo)
      Return CargaUna(dt, AccionesSiNoExisteRegistro.Excepcion, "No existe una Sucursal con Estoy Aca.", incluirLogo)
   End Function

   Public Function GetUna(id As Integer, incluirLogo As Boolean) As Eniac.Entidades.Sucursal
      Dim dt As DataTable = New SqlServer.Sucursales(da).Sucursales_G1(id, incluirLogo)
      Return CargaUna(dt, AccionesSiNoExisteRegistro.Excepcion, String.Format("No existe una Sucursal con Id = {0}.", id), incluirLogo)
   End Function

   Public Overloads Function GetTodas(incluirLogo As Boolean) As List(Of Entidades.Sucursal)
      Return GetTodas(idFuncion:=String.Empty, incluirLogo:=incluirLogo)
   End Function
   Public Overloads Function GetTodas(idFuncion As String, incluirLogo As Boolean) As List(Of Entidades.Sucursal)
      Return GetTodas(idFuncion, Entidades.Publicos.SiNoTodos.TODOS, incluirLogo)
   End Function
   Public Overloads Function GetTodas(publicarEnWeb As Entidades.Publicos.SiNoTodos, incluirLogo As Boolean) As List(Of Sucursal)
      Return GetTodas(idFuncion:=String.Empty, publicarEnWeb:=publicarEnWeb, incluirLogo:=incluirLogo)
   End Function

   Private Overloads Function GetTodas(idFuncion As String, publicarEnWeb As Entidades.Publicos.SiNoTodos, incluirLogo As Boolean) As List(Of Sucursal)
      Dim dt As DataTable = New SqlServer.Sucursales(da).Sucursales_GA(idFuncion, publicarEnWeb, incluirLogo)
      Return CargaLista(dt, Sub(o, dr) CargaUna(o, dr, incluirLogo), Function() New Entidades.Sucursal())
   End Function

   Public Overloads Function GetTodas(sacarLaSucursal As Int32, incluirLogo As Boolean) As List(Of Sucursal)
      Dim dt As DataTable = New SqlServer.Sucursales(da).Sucursales_GA_Excluye(sacarLaSucursal, incluirLogo)
      Return CargaLista(dt, Sub(o, dr) CargaUna(o, dr, incluirLogo), Function() New Entidades.Sucursal())
   End Function

   Public Function GetSoloIdsDeTodas() As List(Of Integer)
      Try
         Me.da.OpenConection()

         Return Me._GetSoloIdsDeTodas()

      Catch ex As Exception
         Throw
      Finally
         Me.da.CloseConection()

      End Try
   End Function

   Friend Function _GetSoloIdsDeTodas() As List(Of Integer)
      Dim sql As SqlServer.Sucursales = New SqlServer.Sucursales(Me.da)
      Return sql.GetSoloIdsDeTodas()
   End Function


   Public Function GetEstoyAca(incluirLogo As Boolean) As Entidades.Sucursal
      Return EstoyEn(incluirLogo)
   End Function

   Public Function GetSoyLaCentral(incluirLogo As Boolean) As Entidades.Sucursal
      Dim dt As DataTable = New SqlServer.Sucursales(da).Sucursales_GA(estoyAca:=Nothing, soyLaCentral:=True, incluirLogo:=incluirLogo)
      Return CargaUna(dt, AccionesSiNoExisteRegistro.Excepcion, "No existe una Sucursal con Soy La Central.", incluirLogo)
   End Function

   Public Function GetCodigoMaximo() As Integer
      Return New SqlServer.Sucursales(da).GetCodigoMaximo()
   End Function

   Public Function GetPorCodigo(id As Integer, idFuncion As String) As DataTable
      Return New SqlServer.Sucursales(da).GetPorCodigoNombre(id, String.Empty, idFuncion, False)
   End Function

   Public Function GetPorNombre(nombre As String, idFuncion As String) As DataTable
      Return New SqlServer.Sucursales(da).GetPorCodigoNombre(0, nombre, idFuncion, False)
   End Function

   Public Sub EjecutaLimpiezaDeSucursales(idSucursal As Integer)
      Dim sql As SqlServer.Sucursales = New SqlServer.Sucursales(da)
      sql.EjecutaLimpiezaDeSucursales(idSucursal)
   End Sub

#End Region

#Region "Metodos Privados"

   Private Sub EjecutaSP(ByVal entidad As Eniac.Entidades.Entidad, ByVal tipo As TipoSP)

      Dim sucu As Entidades.Sucursal = DirectCast(entidad, Entidades.Sucursal)

      Try
         da.OpenConection()
         da.BeginTransaction()

         Dim sql As SqlServer.Sucursales = New SqlServer.Sucursales(Me.da)
         Select Case tipo

            Case TipoSP._I

               sql.Sucursales_I(sucu.IdSucursal, sucu.Nombre, sucu.Direccion, sucu.Localidad.IdLocalidad, sucu.Telefono,
                                sucu.Correo, sucu.FechaInicioActiv, sucu.EstoyAca, sucu.SoyLaCentral, sucu.IdSucursalAsociada,
                                sucu.ColorSucursal, sucu.LogoSucursal, sucu.DireccionComercial, sucu.LocalidadComercial.IdLocalidad,
                                sucu.RedesSociales, sucu.IdSucursalAsociadaPrecios, sucu.PublicarEnWeb, sucu.IdEmpresa)

               Dim sqlPS As SqlServer.ProductosSucursales = New SqlServer.ProductosSucursales(Me.da)
               sqlPS.ProductosSucursales_IPorSucursal(sucu.IdSucursal, sucu.Usuario)

               Dim sqlPSP As SqlServer.ProductosSucursalesPrecios = New SqlServer.ProductosSucursalesPrecios(Me.da)
               sqlPSP.ProductosSucursalesPrecios_IPorSucursal(sucu.IdSucursal, sucu.Usuario)

               '# Llamo a la regla para que agregue las Formas de Pago a la nueva sucursal
               Dim rVentasFormasPagoSucursales As Reglas.VentasFormasPagoSucursales = New Reglas.VentasFormasPagoSucursales(Me.da)
               For Each formaPago As Entidades.VentasFormasPagoSucursales In sucu.FormasDePago
                  rVentasFormasPagoSucursales._Inserta(formaPago)
               Next

               Dim sImpresoras As SqlServer.Impresoras = New SqlServer.Impresoras(da)
               sImpresoras.Impresoras_IPorSucursal(actual.Sucursal.Id, sucu.IdSucursal, "NORMAL")

               'Genero la Caja Maestra 
               Dim sqlCN As SqlServer.CajasNombres = New SqlServer.CajasNombres(Me.da)
               'vml por defecto pongo el plan de cuentas principal.
               sqlCN.CajasNombres_I(sucu.IdSucursal, 1, "Principal", String.Empty, String.Empty, 1, 0, 0, 0, Nothing, String.Empty, String.Empty, String.Empty, String.Empty)

               'Genero la cabecara de la primer planilla de caja.
               Dim sqlCaja As SqlServer.Cajas = New SqlServer.Cajas(Me.da)

               sqlCaja.Cajas_I(sucu.IdSucursal, 1, 1, Date.Now, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)

               ''Genero la numeracion en cero, es muy importante mantener los comprobantes relacionados (FACT, NCRED, NDEB)
               'Dim sqlVentasNumero As SqlServer.VentasNumeros = New SqlServer.VentasNumeros(Me.da)
               'sqlVentasNumero.VentasNumeros_GenerarNumeracion(sucu.IdSucursal)

            Case TipoSP._U
               sql.Sucursales_U(sucu.IdSucursal, sucu.Nombre, sucu.Direccion, sucu.Localidad.IdLocalidad, sucu.Telefono, _
                                sucu.Correo, sucu.FechaInicioActiv, sucu.EstoyAca, sucu.SoyLaCentral, sucu.IdSucursalAsociada, _
                                sucu.ColorSucursal, sucu.LogoSucursal, sucu.DireccionComercial, sucu.LocalidadComercial.IdLocalidad, _
                                sucu.RedesSociales, sucu.IdSucursalAsociadaPrecios, sucu.PublicarEnWeb, sucu.IdEmpresa)

               '# Llamo a la regla para que borre todas las FP asociadas a ésta sucursal
               Dim rVentasFormasPagoSucursales As Reglas.VentasFormasPagoSucursales = New Reglas.VentasFormasPagoSucursales(Me.da)
               rVentasFormasPagoSucursales.BorrarTodas(sucu.IdSucursal, False)

               '# Y agrego nuevamente las FP a la sucursal
               For Each formaPago As Entidades.VentasFormasPagoSucursales In sucu.FormasDePago
                  rVentasFormasPagoSucursales._Inserta(formaPago)
               Next

            Case TipoSP._D

               'Pero solo los datos maestros, nada de movimientos, si tuvo... ya no puede borrarla.

               'Dim sqlVentasNumero As SqlServer.VentasNumeros = New SqlServer.VentasNumeros(Me.da)
               'sqlVentasNumero.VentasNumeros_D(sucu.IdSucursal)

               Dim sqlCaja As SqlServer.Cajas = New SqlServer.Cajas(Me.da)
               sqlCaja.Cajas_D(sucu.IdSucursal, 0)

               Dim sqlCajasNombres As SqlServer.CajasNombres = New SqlServer.CajasNombres(Me.da)
               sqlCajasNombres.CajasNombres_D(sucu.IdSucursal, 0)

               Dim sqlHP As SqlServer.HistorialPrecios = New SqlServer.HistorialPrecios(Me.da)
               sqlHP.HistorialPrecios_D(sucu.IdSucursal)

               Dim sqlPSP As SqlServer.ProductosSucursalesPrecios = New SqlServer.ProductosSucursalesPrecios(Me.da)
               sqlPSP.ProductosSucursalesPrecios_DPorSucursal(sucu.IdSucursal)

               Dim sqlPS As SqlServer.ProductosSucursales = New SqlServer.ProductosSucursales(Me.da)
               sqlPS.ProductosSucursales_DPorSucursal(sucu.IdSucursal)

               Dim sImpresoras As SqlServer.Impresoras = New SqlServer.Impresoras(da)
               sImpresoras.Impresoras_D(sucu.IdSucursal, String.Empty)

               '# Llamo a la regla para que borre todas las FP asociadas a ésta sucursal
               Dim rVentasFormasPagoSucursales As Reglas.VentasFormasPagoSucursales = New Reglas.VentasFormasPagoSucursales(Me.da)
               rVentasFormasPagoSucursales.BorrarTodas(sucu.IdSucursal, False)

               sql.Sucursales_D(sucu.IdSucursal)

         End Select

         da.CommitTransaction()

      Catch
         da.RollbakTransaction()
         Throw
      Finally
         da.CloseConection()
      End Try

   End Sub

   Private Sub CargaUna(o As Entidades.Sucursal, dr As DataRow, incluirLogo As Boolean)
      o.Id = Integer.Parse(dr("Id").ToString())
      o.IdSucursal = Integer.Parse(dr("IdSucursal").ToString())

      o.Empresa = New Reglas.Empresas(da).GetUno(Integer.Parse(dr(Entidades.Sucursal.Columnas.IdEmpresa.ToString()).ToString()))

      o.Nombre = dr("Nombre").ToString()

      o.Direccion = dr("Direccion").ToString()
      o.Localidad = New Localidades(Me.da).GetUna(Int32.Parse(dr("IdLocalidad").ToString()))
      o.Telefono = dr("Telefono").ToString()
      o.Correo = dr("Correo").ToString()

      If Not String.IsNullOrEmpty(dr("IdSucursalAsociada").ToString()) Then
         o.IdSucursalAsociada = Integer.Parse(dr("IdSucursalAsociada").ToString())
      End If

      If Not String.IsNullOrEmpty(dr("IdSucursalAsociadaPrecios").ToString()) Then
         o.IdSucursalAsociadaPrecios = Integer.Parse(dr("IdSucursalAsociadaPrecios").ToString())
      End If

      If Not String.IsNullOrEmpty(dr("FechaInicioActiv").ToString()) Then
         o.FechaInicioActiv = Date.Parse(dr("FechaInicioActiv").ToString())
      End If
      o.EstoyAca = Boolean.Parse(dr("EstoyAca").ToString())
      o.SoyLaCentral = Boolean.Parse(dr("SoyLaCentral").ToString())
      o.PublicarEnWeb = Boolean.Parse(dr("PublicarEnWeb").ToString())

      If Not String.IsNullOrEmpty(dr("ColorSucursal").ToString()) Then
         o.ColorSucursal = Int32.Parse(dr("ColorSucursal").ToString())
      End If
      o.DireccionComercial = dr("DireccionComercial").ToString()
      o.LocalidadComercial = New Localidades(Me.da).GetUna(Integer.Parse(dr("IdLocalidadComercial").ToString()))
      o.RedesSociales = dr("RedesSociales").ToString()

      If incluirLogo And Not String.IsNullOrEmpty(dr("LogoSucursal").ToString()) Then
         Dim content() As Byte = CType(dr("LogoSucursal"), Byte())
         Dim stream As System.IO.MemoryStream = New System.IO.MemoryStream(content)
         o.LogoSucursal = System.Drawing.Image.FromStream(stream) ' New System.Drawing.Bitmap(stream)
      End If

      ' # Cargo las Formas de Pago asociadas a la sucursal
      o.FormasDePago = CargarFormasDePagoSucursal(o)
      
   End Sub

   Private Function CargaUna(dt As DataTable, accion As AccionesSiNoExisteRegistro, mensajeExcepcion As String, incluirLogo As Boolean) As Entidades.Sucursal
      Dim o As Entidades.Sucursal = New Entidades.Sucursal()
      If dt.Rows.Count > 0 Then
         Me.CargaUna(o, dt.Rows(0), incluirLogo)
      Else
         If accion = AccionesSiNoExisteRegistro.Excepcion Then
            Throw New Exception(mensajeExcepcion)
         ElseIf accion = AccionesSiNoExisteRegistro.Nulo Then
            Return Nothing
         End If
      End If
      Return o
   End Function

   Private Function CargarFormasDePagoSucursal(o As Entidades.Sucursal) As List(Of Entidades.VentasFormasPagoSucursales)
      Dim rVentasFormasPagoSucursales As Reglas.VentasFormasPagoSucursales = New Reglas.VentasFormasPagoSucursales
      Dim lista As List(Of Entidades.VentasFormasPagoSucursales) = New List(Of Entidades.VentasFormasPagoSucursales)
      Dim eVFPS As Entidades.VentasFormasPagoSucursales
      Dim dt As DataTable = rVentasFormasPagoSucursales.GetAll(o.Id)
      If dt.Rows.Count > 0 Then
         For Each dr As DataRow In dt.Rows
            eVFPS = New Entidades.VentasFormasPagoSucursales

            eVFPS.IdSucursal = dr.Field(Of Integer)(Entidades.VentasFormasPagoSucursales.Columnas.IdSucursal.ToString())
            eVFPS.IdFormasPago = dr.Field(Of Integer)(Entidades.VentasFormasPagoSucursales.Columnas.IdFormasPago.ToString())
            eVFPS.DescripcionFormasPago = dr.Field(Of String)(Entidades.VentaFormaPago.Columnas.DescripcionFormasPago.ToString())
            eVFPS.OrdenVentas = dr.Field(Of Integer)(Entidades.VentaFormaPago.Columnas.OrdenVentas.ToString())
            eVFPS.OrdenCompras = dr.Field(Of Integer)(Entidades.VentaFormaPago.Columnas.OrdenCompras.ToString())
            eVFPS.OrdenFichas = dr.Field(Of Integer)(Entidades.VentaFormaPago.Columnas.OrdenFichas.ToString())

            lista.Add(eVFPS)
         Next
      End If
      Return lista
   End Function

#End Region

End Class